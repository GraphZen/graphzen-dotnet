// Copyright (c) GraphZen LLC. All rights reserved.
// Licensed under the GraphZen Community License. See the LICENSE file in the project root for license information.

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using GraphZen.Infrastructure;
using GraphZen.Internal;
using GraphZen.Language;
using GraphZen.TypeSystem.Internal;
using JetBrains.Annotations;

namespace GraphZen.TypeSystem
{
    [DebuggerDisplay("{DebuggerDisplay,nq}")]
    public class InputObjectTypeDefinition : NamedTypeDefinition, IMutableInputObjectTypeDefinition
    {
        [NotNull] private readonly Dictionary<string, InputFieldDefinition> _fields =
            new Dictionary<string, InputFieldDefinition>();

        [NotNull] private readonly Dictionary<string, ConfigurationSource> _ignoredFields =
            new Dictionary<string, ConfigurationSource>();

        public InputObjectTypeDefinition(TypeIdentity identity, SchemaDefinition schema,
            ConfigurationSource configurationSource) : base(
            Check.NotNull(identity, nameof(identity)),
            Check.NotNull(schema, nameof(schema)), configurationSource)
        {
            Builder = new InternalInputObjectTypeBuilder(this, schema.Builder);
            identity.Definition = this;
        }

        private string DebuggerDisplay => $"input {Name}";

        [NotNull]
        public InternalInputObjectTypeBuilder Builder { get; }

        public override DirectiveLocation DirectiveLocation { get; } = DirectiveLocation.InputObject;


        public override TypeKind Kind { get; } = TypeKind.InputObject;


        public IReadOnlyDictionary<string, InputFieldDefinition> Fields => _fields;

        public IEnumerable<InputFieldDefinition> GetFields() => _fields.Values;

        IEnumerable<IInputFieldDefinition> IInputObjectTypeDefinition.GetFields() => GetFields();

        public bool RenameField([NotNull] InputFieldDefinition field, [NotNull] string name,
            ConfigurationSource configurationSource)
        {
            if (!configurationSource.Overrides(field.GetNameConfigurationSource()))
            {
                return false;
            }

            if (this.TryGetField(name, out var existing) && existing != field)
            {
                throw new InvalidOperationException(
                    $"Cannot rename {field} to '{name}'. {this} already contains a field named '{name}'.");
            }

            _fields.Remove(field.Name);
            _fields[name] = field;
            return true;
        }

        public InputFieldDefinition FindField([NotNull] MemberInfo member)
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

        public void UnignoreField([NotNull] string fieldName)
        {
            _ignoredFields.Remove(fieldName);
        }

        private bool IgnoreField([NotNull] InputFieldDefinition field, ConfigurationSource configurationSource)
        {
            if (configurationSource.Overrides(field.GetConfigurationSource()))
            {
                _fields.Remove(field.Name);
                return true;
            }

            return false;
        }

        private InputFieldDefinition AddField([NotNull] InputFieldDefinition field)
        {
            if (_fields.ContainsKey(field.Name))
            {
                throw new InvalidOperationException(
                    $"Duplicate field names: Cannot add field '{field.Name}' to {Kind.ToString().ToLower()} '{Name}', a field with that name already exists.");
            }

            _fields.Add(field.Name, field);
            return field;
        }

        public InputFieldDefinition AddField([NotNull] PropertyInfo property,
            ConfigurationSource configurationSource)
        {
            if (ClrType == null)
            {
                throw new InvalidOperationException(
                    "Cannot add field from property on a type that does not have a CLR type mapped.");
            }

            // ReSharper disable once AssignNullToNotNullAttribute
            if (!ClrType.IsSameOrSubclass(property.DeclaringType))
            {
                throw new InvalidOperationException(
                    $"Cannot add field from property with a declaring type ({property.DeclaringType}) that does not exist on the parent's {Kind.ToString().ToLower()} type's mapped CLR type ({ClrType}).");
            }

            var (fieldName, nameConfigurationSource) = property.GetGraphQLFieldName();
            var field = new InputFieldDefinition(fieldName, nameConfigurationSource, Schema, configurationSource,
                property, this);


            // Configure method w/conventions
            var fb = field.Builder;
            fb.FieldType(property);
            fb.DefaultValue(property, configurationSource);
            if (property.TryGetDescriptionFromDataAnnotation(out var description))
            {
                fb.Description(description, ConfigurationSource.DataAnnotation);
            }

            return AddField(field);
        }

        public void RemoveField([NotNull] InputFieldDefinition field)
        {
            _fields.Remove(field.Name);
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

        public ConfigurationSource? FindIgnoredFieldConfigurationSource([NotNull] string fieldName)
        {
            if (_ignoredFields.TryGetValue(fieldName, out var cs))
            {
                return cs;
            }

            return null;
        }

        [NotNull]
        public InputValueDefinition GetOrAddField(string name, ConfigurationSource configurationSource)
        {
            Check.NotNull(name, nameof(name));
            if (this.TryGetField(name, out var existing))
            {
                return existing;
            }

            var field = new InputFieldDefinition(name, ConfigurationSource.Convention, Schema, configurationSource,
                null, this);
            _fields[name] = field;
            return field;
        }
    }
}