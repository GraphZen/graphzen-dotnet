// Copyright (c) GraphZen LLC. All rights reserved.
// Licensed under the GraphZen Community License. See the LICENSE file in the project root for license information.

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Reflection;
using GraphZen.Infrastructure;
using GraphZen.Internal;
using GraphZen.LanguageModel;
using GraphZen.TypeSystem.Internal;
using GraphZen.TypeSystem.Taxonomy;
using JetBrains.Annotations;

namespace GraphZen.TypeSystem
{
    [DebuggerDisplay("{" + nameof(DebuggerDisplay) + ",nq}")]
    [DisplayName("input object")]
    public partial class InputObjectTypeDefinition : NamedTypeDefinition, IMutableInputObjectTypeDefinition
    {
        private readonly Dictionary<string, InputFieldDefinition> _fields =
            new Dictionary<string, InputFieldDefinition>();

        private readonly Dictionary<string, ConfigurationSource> _ignoredFields =
            new Dictionary<string, ConfigurationSource>();

        public InputObjectTypeDefinition(TypeIdentity identity, SchemaDefinition schema,
            ConfigurationSource configurationSource)
            : base(identity, schema, configurationSource)
        {
            Builder = new InternalInputObjectTypeBuilder(this);
        }

        private string DebuggerDisplay => $"input {Name}";


        public InternalInputObjectTypeBuilder Builder { get; }

        public override DirectiveLocation DirectiveLocation { get; } = DirectiveLocation.InputObject;


        public override TypeKind Kind { get; } = TypeKind.InputObject;


        [GenDictionaryAccessors(nameof(InputFieldDefinition.Name), "Field")]
        public IReadOnlyDictionary<string, InputFieldDefinition> Fields => _fields;

        public IEnumerable<InputFieldDefinition> GetFields() => _fields.Values;

        public ConfigurationSource? FindIgnoredFieldConfigurationSource(string fieldName)
        {
            if (_ignoredFields.TryGetValue(fieldName, out var cs))
            {
                return cs;
            }

            return null;
        }

        IEnumerable<IInputFieldDefinition> IInputFieldsDefinition.GetFields() => GetFields();


        public bool RenameField(InputFieldDefinition field, string name,
            ConfigurationSource configurationSource)
        {
            if (!configurationSource.Overrides(field.GetNameConfigurationSource()))
            {
                return false;
            }

            if (TryGetField(name, out var existing) && !existing.Equals(field))
            {
                throw new DuplicateItemException(
                    TypeSystemExceptionMessages.DuplicateItemException.CannotRenameInputField(field, name));
            }

            _fields.Remove(field.Name);
            _fields[name] = field;
            return true;
        }

        public InputFieldDefinition? FindField(MemberInfo member)
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

        public void UnignoreField(string fieldName)
        {
            _ignoredFields.Remove(fieldName);
        }

        private bool IgnoreField(InputFieldDefinition field, ConfigurationSource configurationSource)
        {
            if (configurationSource.Overrides(field.GetConfigurationSource()))
            {
                _fields.Remove(field.Name);
                return true;
            }

            return false;
        }

        public bool AddField(InputFieldDefinition field)
        {
            if (_fields.ContainsKey(field.Name))
            {
                throw new InvalidOperationException(
                    $"Duplicate field names: Cannot add field '{field.Name}' to {Kind.ToString().ToLower()} '{Name}', a field with that name already exists.");
            }

            _fields.Add(field.Name, field);
            return true;
        }

        public InputFieldDefinition AddField(PropertyInfo property,
            ConfigurationSource configurationSource)
        {
            if (ClrType == null)
            {
                throw new InvalidOperationException(
                    "Cannot add field from property on a type that does not have a CLR type mapped.");
            }

            if (!ClrType.IsSameOrSubclass(property.DeclaringType!))
            {
                throw new InvalidOperationException(
                    $"Cannot add field from property with a declaring type ({property.DeclaringType}) that does not exist on the parent's {Kind.ToString().ToLower()} type's mapped CLR type ({ClrType}).");
            }

            var (fieldName, nameConfigurationSource) = property.GetGraphQLFieldName();


            if (!property.TryGetGraphQLTypeInfo(out var typeNode, out var innerClrType))
            {
                throw new Exception($"Cannot get type info for property {property}");
            }

            var fieldTypeId = Schema.GetOrAddInputTypeIdentity(innerClrType);

            var field = new InputFieldDefinition(fieldName, fieldTypeId, typeNode, nameConfigurationSource, Schema,
                configurationSource,
                property, this);


            // Configure method w/conventions
            var fb = field.Builder;
            fb.DefaultValue(property, configurationSource);
            if (property.TryGetDescriptionFromDataAnnotation(out var description))
            {
                fb.Description(description, ConfigurationSource.DataAnnotation);
            }

            AddField(field);
            return field;
        }

        public bool RemoveField(InputFieldDefinition field)
        {
            _fields.Remove(field.Name);
            return true;
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

        public InputFieldDefinition? GetOrAddField(string name, Type clrType, ConfigurationSource configurationSource)
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
                return field;
            }

            if (!clrType.TryGetGraphQLTypeInfo(out var typeSyntax, out var innerClrType))
            {
                return null;
            }

            var fieldTypeIdentity = Schema.GetOrAddTypeIdentity(innerClrType);
            field = new InputFieldDefinition(name, fieldTypeIdentity, typeSyntax, configurationSource, Schema,
                configurationSource, null, this);
            AddField(field);
            return field;
        }

        public InputFieldDefinition? GetOrAddField(string name, string type, ConfigurationSource configurationSource)
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
                return field;
            }

            TypeSyntax typeNode;
            try
            {
                typeNode = Schema.Builder.Parser.ParseType(type);
            }
            catch (Exception e)
            {
                throw new InvalidTypeReferenceException(
                    "Unable to parse type reference. See inner exception for details.", e);
            }

            var fieldTypeName = typeNode.GetNamedType().Name.Value;
            var fieldTypeIdentity = Schema.GetOrAddTypeIdentity(fieldTypeName);


            field = new InputFieldDefinition(name, fieldTypeIdentity,
                typeNode, configurationSource,
                Schema, configurationSource,
                null, this);
            _fields[name] = field;
            return field;
        }
    }
}