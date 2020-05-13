// Copyright (c) GraphZen LLC. All rights reserved.
// Licensed under the GraphZen Community License. See the LICENSE file in the project root for license information.

using System;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Reflection;
using GraphZen.Infrastructure;
using JetBrains.Annotations;

namespace GraphZen.TypeSystem.Internal
{
    public class InternalInputObjectTypeBuilder : AnnotatableMemberDefinitionBuilder<InputObjectTypeDefinition>
    {
        public InternalInputObjectTypeBuilder(InputObjectTypeDefinition definition,
            InternalSchemaBuilder schemaBuilder) :
            base(definition,
                schemaBuilder)
        {
        }

        public InternalInputValueBuilder? Field(string name, ConfigurationSource configurationSource) =>
            Definition.GetOrAddField(name, configurationSource)?.Builder;

        public bool IgnoreField(string fieldName, ConfigurationSource configurationSource)
        {
            var ignoredConfigurationSource = Definition.FindIgnoredFieldConfigurationSource(fieldName);
            if (ignoredConfigurationSource.HasValue)
            {
                if (configurationSource.Overrides(ignoredConfigurationSource) &&
                    configurationSource != ignoredConfigurationSource)
                {
                    Definition.IgnoreField(fieldName, configurationSource);
                }

                return true;
            }

            var field = Definition.FindField(fieldName);
            if (field != null)
            {
                return Ignore(field, configurationSource);
            }


            Definition.IgnoreField(fieldName, configurationSource);
            return true;
        }

        public bool UnignoreField(string name, ConfigurationSource configurationSource)
        {
            var ignoredConfigurationSource = Definition.FindIgnoredFieldConfigurationSource(name);
            if (!configurationSource.Overrides(ignoredConfigurationSource))
            {
                return false;
            }

            Definition.UnignoreField(name);
            return true;
        }


        public InternalInputObjectTypeBuilder ClrType(Type clrType, ConfigurationSource configurationSource)
        {
            if (Definition.SetClrType(clrType, configurationSource))
            {
                ConfigureFromClrType();
            }

            return this;
        }

        public bool ConfigureFromClrType()
        {
            var clrType = Definition.ClrType;
            if (clrType == null)
            {
                return false;
            }

            if (clrType.TryGetDescriptionFromDataAnnotation(out var description))
            {
                Definition.SetDescription(description, ConfigurationSource.DataAnnotation);
            }

            var flags = BindingFlags.Public | BindingFlags.Instance | BindingFlags.Static;
            // ReSharper disable once PossibleNullReferenceException

            var fieldMembers = clrType.GetMembers(flags)
                .OfType<PropertyInfo>()
                .OrderBy(_ => _.MetadataToken);
            foreach (var property in fieldMembers)
            {
                Field(property, ConfigurationSource.Convention);
            }

            return true;
        }

        public bool IsFieldIgnored(string member, ConfigurationSource configurationSource)
        {
            if (configurationSource == ConfigurationSource.Explicit)
            {
                return false;
            }

            var ignoredMemberConfigurationSource = Definition.FindIgnoredFieldConfigurationSource(member);
            return ignoredMemberConfigurationSource.HasValue &&
                   ignoredMemberConfigurationSource.Overrides(configurationSource);
        }

        public bool IgnoreField(InputFieldDefinition field, ConfigurationSource configurationSource)
        {
            if (!configurationSource.Overrides(field.GetConfigurationSource()))
            {
                return false;
            }

            Definition.IgnoreField(field.Name, configurationSource);

            return RemoveField(field, configurationSource);
        }

        private bool Ignore(InputFieldDefinition field, ConfigurationSource configurationSource)
        {
            if (!configurationSource.Overrides(field.GetConfigurationSource()))
            {
                return false;
            }

            return RemoveField(field, configurationSource);
        }


        public bool RemoveField(string name, ConfigurationSource configurationSource) =>
            Definition.TryGetField(name, out var f) && RemoveField(f, configurationSource);

        public bool RemoveField(InputFieldDefinition field, ConfigurationSource configurationSource)
        {
            if (!configurationSource.Overrides(field.GetConfigurationSource()))
            {
                return false;
            }

            Definition.IgnoreField(field.Name, configurationSource);

            Definition.RemoveField(field);

            return true;
        }


        public InternalInputValueBuilder? Field(PropertyInfo property, ConfigurationSource configurationSource)
        {
            var (fieldName, _) = property.GetGraphQLFieldName();
            if (property.IsIgnoredByDataAnnotation())
            {
                IgnoreField(property, ConfigurationSource.DataAnnotation);
            }

            if (IsFieldIgnored(fieldName, configurationSource))
            {
                return null;
            }


            if (property.TryGetGraphQLTypeInfo(out _, out var innerClrType))
            {
                var fieldInnerType = Schema.Builder.InputType(innerClrType, configurationSource);
                if (fieldInnerType == null)
                {
                    IgnoreField(property, ConfigurationSource.Convention);
                }
            }
            else
            {
                IgnoreField(property, ConfigurationSource.Convention);
            }


            if (IsFieldIgnored(fieldName, configurationSource))
            {
                return null;
            }

            var field = Definition.FindField(property);
            if (field == null)
            {
                Definition.UnignoreField(fieldName);
                field = Definition.AddField(property, configurationSource);
            }
            else
            {
                field.UpdateConfigurationSource(configurationSource);
            }

            if (property.TryGetDescriptionFromDataAnnotation(out var desc))
            {
                field.Builder.Description(desc, ConfigurationSource.DataAnnotation);
            }

            return field.Builder;
        }

        public bool IgnoreField(MemberInfo member, ConfigurationSource configurationSource)
        {
            var (fieldName, _) = member.GetGraphQLFieldName();
            var ignoredConfigurationSource = Definition.FindIgnoredFieldConfigurationSource(fieldName);
            if (ignoredConfigurationSource.HasValue)
            {
                if (configurationSource.Overrides(ignoredConfigurationSource) &&
                    configurationSource != ignoredConfigurationSource)
                {
                    Definition.IgnoreField(fieldName, configurationSource);
                    return true;
                }
            }

            var field = Definition.FindField(member);
            if (field != null)
            {
                return IgnoreField(field, configurationSource);
            }

            Definition.IgnoreField(fieldName, configurationSource);
            return true;
        }

        public void RemoveClrType(ConfigurationSource configurationSource)
        {
            Definition.RemoveClrType(configurationSource);
        }
    }
}