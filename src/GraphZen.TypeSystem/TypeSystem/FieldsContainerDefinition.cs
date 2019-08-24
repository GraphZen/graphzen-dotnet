// Copyright (c) GraphZen LLC. All rights reserved.
// Licensed under the GraphZen Community License. See the LICENSE file in the project root for license information.

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using GraphZen.Infrastructure;
using GraphZen.TypeSystem.Internal;
using GraphZen.TypeSystem.Taxonomy;

namespace GraphZen.TypeSystem
{
    public abstract class FieldsContainerDefinition : NamedTypeDefinition, IMutableFieldsContainerDefinition
    {
        [NotNull]
        private readonly Dictionary<string, FieldDefinition> _fields =
            new Dictionary<string, FieldDefinition>();

        [NotNull]
        private readonly Dictionary<string, ConfigurationSource> _ignoredFields =
            new Dictionary<string, ConfigurationSource>();


        public FieldsContainerDefinition([NotNull] TypeIdentity identity, [NotNull] SchemaDefinition schema,
            ConfigurationSource configurationSource) : base(identity, schema, configurationSource)
        {
        }



        [NotNull]
        [ItemNotNull]
        public IEnumerable<FieldDefinition> GetFields() => _fields.Values;

        public IReadOnlyDictionary<string, FieldDefinition> Fields => _fields;

        IEnumerable<IFieldDefinition> IFieldsContainerDefinition.GetFields() => GetFields();


        public void UnignoreField([NotNull] string fieldName)
        {
            _ignoredFields.Remove(fieldName);
        }

        public bool IgnoreField([NotNull] string fieldName, ConfigurationSource configurationSource)
        {
            var ignoredConfigurationSource = FindIgnoredFieldConfigurationSource(fieldName);
            if (ignoredConfigurationSource.HasValue && ignoredConfigurationSource.Overrides(configurationSource))
            {
                return true;
            }

            if (ignoredConfigurationSource != null)
            {
                configurationSource = configurationSource.Max(ignoredConfigurationSource);
            }

            _ignoredFields[fieldName] = configurationSource;
            var existing = this.FindField(fieldName);

            if (existing != null)
            {
                return IgnoreField(existing, configurationSource);
            }

            return true;
        }

        public FieldDefinition FindField([NotNull] MemberInfo member)
        {
            // ReSharper disable once PossibleNullReferenceException
            var memberMatch = _fields.Values.SingleOrDefault(_ => _.ClrInfo == member);
            if (memberMatch != null)
            {
                return memberMatch;
            }

            var (fieldName, _) = member.GetGraphQLFieldName();
            return this.FindField(fieldName);
        }

        public bool RenameField([NotNull] FieldDefinition field, [NotNull] string name,
            ConfigurationSource configurationSource)
        {
            if (!configurationSource.Overrides(field.GetNameConfigurationSource()))
            {
                return false;
            }

            // field.UpdateConfigurationSource(configurationSource);

            if (this.TryGetField(name, out var existing) && existing != field)
            {
                throw new InvalidOperationException(
                    $"Cannot rename {field} to '{name}'. {this} already contains a field named '{name}'.");
            }

            _fields.Remove(field.Name);
            _fields[name] = field;
            return true;
        }


        private bool IgnoreField([NotNull] FieldDefinition field, ConfigurationSource configurationSource)
        {
            if (configurationSource.Overrides(field.GetConfigurationSource()))
            {
                _fields.Remove(field.Name);
                return true;
            }

            return false;
        }


        public FieldDefinition AddField([NotNull] PropertyInfo propertyInfo, ConfigurationSource configurationSource)
        {
            if (ClrType == null)
            {
                throw new InvalidOperationException(
                    "Cannot add field from property on a type that does not have a CLR type mapped.");
            }

            // ReSharper disable once AssignNullToNotNullAttribute
            if (!ClrType.IsSameOrSubclass(propertyInfo.DeclaringType))
            {
                throw new InvalidOperationException(
                    $"Cannot add field from property with a declaring type ({propertyInfo.DeclaringType}) that does not exist on the parent's {Kind.ToString().ToLower()} type's mapped CLR type ({ClrType}).");
            }

            var (fieldName, nameConfigurationSource) = propertyInfo.GetGraphQLFieldName();
            var field = new FieldDefinition(fieldName, nameConfigurationSource,
                Schema, this, configurationSource, propertyInfo);
            var fb = field.Builder;

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
                fb.FieldType(propertyInfo);
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

            return AddField(field);
        }

        public FieldDefinition AddField([NotNull] MethodInfo method, ConfigurationSource configurationSource)
        {
            var (fieldName, nameConfigurationSource) = method.GetGraphQLFieldName();

            var field = new FieldDefinition(fieldName, nameConfigurationSource, Schema, this, configurationSource,
                method);

            var fb = field.Builder;
            fb.FieldType(method);


            

            return AddField(field);
        }

        private FieldDefinition AddField([NotNull] FieldDefinition field)
        {
            if (_fields.ContainsKey(field.Name))
            {
                throw new InvalidOperationException(
                    $"Duplicate field names: Cannot add field '{field.Name}' to {Kind.ToString().ToLower()} '{Name}', a field with that name already exists.");
            }

            _fields.Add(field.Name, field);
            return field;
        }

        public void RemoveField([NotNull] FieldDefinition field)
        {
            _fields.Remove(field.Name);
        }

        public ConfigurationSource? FindIgnoredFieldConfigurationSource(string fieldName)
        {
            if (_ignoredFields.TryGetValue(fieldName, out var cs))
            {
                return cs;
            }

            return null;
        }

        public FieldDefinition GetOrAddField([NotNull] string name, ConfigurationSource nameConfigurationSource,
            ConfigurationSource configurationSource)
        {
            var ignoredConfigurationSource = FindIgnoredFieldConfigurationSource(name);
            if (ignoredConfigurationSource.HasValue)
            {
                if (!nameConfigurationSource.Overrides(ignoredConfigurationSource))
                {
                    return null;
                }

                _ignoredFields.Remove(name);
            }


            var field = this.FindField(name);
            if (field != null)
            {
                field.UpdateConfigurationSource(configurationSource);
                return field;
            }

            field = new FieldDefinition(name, nameConfigurationSource, Schema, this, configurationSource, null);
            _fields.Add(field.Name, field);
            return field;
        }
    }
}