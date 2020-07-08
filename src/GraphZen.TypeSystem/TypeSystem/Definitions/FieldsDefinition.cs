// Copyright (c) GraphZen LLC. All rights reserved.
// Licensed under the GraphZen Community License. See the LICENSE file in the project root for license information.

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using GraphZen.Infrastructure;
using GraphZen.Internal;
using GraphZen.LanguageModel;
using GraphZen.TypeSystem.Internal;
using JetBrains.Annotations;

namespace GraphZen.TypeSystem
{
    public abstract partial class FieldsDefinition : NamedTypeDefinition, IMutableFieldsDefinition
    {
        private readonly Dictionary<string, FieldDefinition> _fields =
            new Dictionary<string, FieldDefinition>();


        private readonly Dictionary<string, ConfigurationSource> _ignoredFields =
            new Dictionary<string, ConfigurationSource>();


        protected FieldsDefinition(TypeIdentity identity, SchemaDefinition schema,
            ConfigurationSource configurationSource) : base(identity, schema, configurationSource)
        {
        }

        [GenDictionaryAccessors(nameof(FieldDefinition.Name), "Field")]
        public IReadOnlyDictionary<string, FieldDefinition> Fields => _fields;


        IEnumerable<IFieldDefinition> IFieldsDefinition.GetFields() => GetFields();

        public bool AddField(FieldDefinition field)
        {
            if (_fields.ContainsKey(field.Name))
            {
                throw new InvalidOperationException(
                    $"Duplicate field names: Cannot add field '{field.Name}' to {Kind.ToString().ToLower()} '{Name}', a field with that name already exists.");
            }

            _fields.Add(field.Name, field);
            return true;
        }

        public bool RemoveField(FieldDefinition field)
        {
            _fields.Remove(field.Name);
            return true;
        }

        public ConfigurationSource? FindIgnoredFieldConfigurationSource(string fieldName)
        {
            if (_ignoredFields.TryGetValue(fieldName, out var cs))
            {
                return cs;
            }

            return null;
        }


        public IEnumerable<FieldDefinition> GetFields() => _fields.Values;

        public void UnignoreField(string fieldName)
        {
            _ignoredFields.Remove(fieldName);
        }

        public bool IgnoreField(string fieldName, ConfigurationSource configurationSource)
        {
            var ignoredConfigurationSource = FindIgnoredFieldConfigurationSource(fieldName);
            if (ignoredConfigurationSource.HasValue &&
                ignoredConfigurationSource.Overrides(configurationSource))
            {
                return true;
            }

            if (ignoredConfigurationSource != null)
            {
                configurationSource = configurationSource.Max(ignoredConfigurationSource);
            }

            _ignoredFields[fieldName] = configurationSource;
            var existing = FindField(fieldName);

            if (existing != null)
            {
                return IgnoreField(existing, configurationSource);
            }

            return true;
        }

        public FieldDefinition? FindField(MemberInfo member)
        {
            // ReSharper disable once PossibleNullReferenceException
            var memberMatch = _fields.Values.SingleOrDefault(_ => _.ClrInfo == member);
            if (memberMatch != null)
            {
                return memberMatch;
            }

            var (fieldName, _) = member.GetGraphQLFieldName();
            return FindField(fieldName);
        }

        internal bool RenameField(FieldDefinition field, string name,
            ConfigurationSource configurationSource)
        {
            if (TryGetField(name, out var existing) && existing != field)
            {
                throw TypeSystemExceptions.DuplicateItemException.ForRename(field, name);
            }

            _fields.Remove(field.Name);
            _fields[name] = field;
            return true;
        }


        private bool IgnoreField(FieldDefinition field, ConfigurationSource configurationSource)
        {
            if (configurationSource.Overrides(field.GetConfigurationSource()))
            {
                _fields.Remove(field.Name);
                return true;
            }

            return false;
        }


        public FieldDefinition AddField(PropertyInfo propertyInfo, ConfigurationSource configurationSource)
        {
            if (ClrType == null)
            {
                throw new InvalidOperationException(
                    "Cannot add field from property on a type that does not have a CLR type mapped.");
            }


            if (!ClrType.IsSameOrSubclass(propertyInfo.DeclaringType!))
            {
                throw new InvalidOperationException(
                    $"Cannot add field from property with a declaring type ({propertyInfo.DeclaringType}) that does not exist on the parent's {Kind.ToString().ToLower()} type's mapped CLR type ({ClrType}).");
            }

            var (fieldName, nameConfigurationSource) = propertyInfo.GetGraphQLFieldName();
            if (!propertyInfo.TryGetGraphQLTypeInfo(out var typeNode, out var innerClrType))
            {
                throw new InvalidOperationException($"Unable to infer type from property {propertyInfo}");
            }

            var fieldTypeIdentity = Schema.GetTypeIdentities(true)
                                        .SingleOrDefault(_ => _.IsOutputType() == true && _.ClrType == innerClrType) ??
                                    Schema.AddTypeIdentity(new TypeIdentity(innerClrType, Schema));

            var field = new FieldDefinition(fieldName, nameConfigurationSource,
                fieldTypeIdentity, typeNode,
                Schema, this, configurationSource, propertyInfo);

            var fb = field.InternalBuilder;
            try
            {
                var getter = propertyInfo.GetGetMethod();
                var entity = Expression.Parameter(ClrType);
                Debug.Assert(getter != null, nameof(getter) + " != null");
                var getterCall = Expression.Call(entity, getter);
                var castToObject = Expression.Convert(getterCall, typeof(object));
                var lambda = Expression.Lambda(castToObject, entity);
                var propertyFunc = lambda.Compile();

                // Configure method w/conventions
                fb.Resolve((source, args, context, info) => propertyFunc.DynamicInvoke(source));
            }
            catch (Exception e)
            {
                throw new Exception(
                    $"Error creating resolver from property {propertyInfo.Name} on CLR type {propertyInfo.DeclaringType?.Name} for field '{fieldName}' on type '{field.DeclaringType}'. See inner exception for details.",
                    e);
            }


            if (propertyInfo.TryGetDescriptionFromDataAnnotation(out var description))
            {
                fb.Description(description, ConfigurationSource.DataAnnotation);
            }

            AddField(field);
            return field;
        }

        public FieldDefinition AddField(MethodInfo method, ConfigurationSource configurationSource)
        {
            var (fieldName, nameConfigurationSource) = method.GetGraphQLFieldName();

            if (!method.TryGetGraphQLTypeInfo(out var typeNode, out var innerClrType))
            {
                throw new Exception($"Unable to infer type from method {method}");
            }

            var fieldTypeId = Schema.GetOrAddOutputTypeIdentity(innerClrType);
            var field = new FieldDefinition(fieldName, nameConfigurationSource,
                fieldTypeId, typeNode,
                Schema, this, configurationSource,
                method);
            AddField(field);
            return field;
        }


        public FieldDefinition? GetOrAddField(string name, Type clrType, ConfigurationSource configurationSource)
        {
            var ignoredConfigurationSource = FindIgnoredFieldConfigurationSource(name);
            if (ignoredConfigurationSource.HasValue)
            {
                if (!configurationSource.Overrides(ignoredConfigurationSource))
                {
                    return null;
                }

                _ignoredFields.Remove(name);
            }

            var field = FindField(name);
            if (field != null)
            {
                field.UpdateConfigurationSource(configurationSource);
                field?.InternalBuilder.FieldType(clrType, configurationSource);
                return field;
            }

            if (!clrType.TryGetGraphQLTypeInfo(out var typeNode, out var innerClrType))
            {
                throw new InvalidOperationException($"Unable to get field type info from {clrType}");
            }

            var typeIdentity = Schema.GetOrAddTypeIdentity(innerClrType);
            field = new FieldDefinition(name, configurationSource, typeIdentity, typeNode, Schema, this,
                configurationSource, null);
            AddField(field);
            return field;
        }


        public FieldDefinition? GetOrAddField(string name, string type, ConfigurationSource configurationSource)
        {
            var ignoredConfigurationSource = FindIgnoredFieldConfigurationSource(name);
            if (ignoredConfigurationSource.HasValue)
            {
                if (!configurationSource.Overrides(ignoredConfigurationSource))
                {
                    return null;
                }

                _ignoredFields.Remove(name);
            }


            var field = FindField(name);
            if (field != null)
            {
                field.UpdateConfigurationSource(configurationSource);
                field?.InternalBuilder.FieldType(type, configurationSource);
                return field;
            }

            TypeSyntax typeNode;
            try
            {
                typeNode = Schema.InternalBuilder.Parser.ParseType(type);
            }
            catch (Exception e)
            {
                throw new InvalidTypeReferenceException(
                    "Unable to parse type reference. See inner exception for details.", e);
            }


            var fieldTypeName = typeNode.GetNamedType().Name.Value;
            var typeIdentity = Schema.GetOrAddTypeIdentity(fieldTypeName);
            field = new FieldDefinition(name, configurationSource, typeIdentity, typeNode, Schema, this,
                configurationSource, null);
            AddField(field);
            return field;
        }
    }
}