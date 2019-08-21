// Copyright (c) GraphZen LLC. All rights reserved.
// Licensed under the GraphZen Community License. See the LICENSE file in the project root for license information.

using System.Linq;
using System.Reflection;
using GraphZen.Infrastructure;

namespace GraphZen.TypeSystem.Internal
{
    public class InternalInputObjectTypeBuilder : AnnotatableMemberDefinitionBuilder<InputObjectTypeDefinition>
    {
        public InternalInputObjectTypeBuilder([NotNull] InputObjectTypeDefinition definition,
            [NotNull] InternalSchemaBuilder schemaBuilder) :
            base(definition,
                schemaBuilder)
        {
        }

        [NotNull]
        public InternalInputValueBuilder Field([NotNull] string name, ConfigurationSource configurationSource) =>
            Definition.GetOrAddField(name, configurationSource).Builder;

        public bool IgnoreField([NotNull] string fieldName, ConfigurationSource configurationSource)
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
            // ReSharper disable once AssignNullToNotNullAttribute
            var fieldMembers = clrType.GetMembers(flags)
                .OfType<PropertyInfo>()
                .OrderBy(_ => _.MetadataToken);
            foreach (var property in fieldMembers)
            {
                Field(property, ConfigurationSource.Convention);
            }
            return true;
        }

        public bool IsFieldIgnored([NotNull] string member, ConfigurationSource configurationSource)
        {
            if (configurationSource == ConfigurationSource.Explicit)
            {
                return false;
            }

            var ignoredMemberConfigurationSource = Definition.FindIgnoredFieldConfigurationSource(member);
            return ignoredMemberConfigurationSource.HasValue &&
                   ignoredMemberConfigurationSource.Overrides(configurationSource);
        }

        public bool IgnoreField([NotNull] InputFieldDefinition field, ConfigurationSource configurationSource)
        {
            if (!configurationSource.Overrides(field.GetConfigurationSource()))
            {
                return false;
            }

            Definition.IgnoreField(field.Name, configurationSource);

            return RemoveField(field, configurationSource);
        }

        private bool Ignore([NotNull] InputFieldDefinition field, ConfigurationSource configurationSource)
        {
            if (!configurationSource.Overrides(field.GetConfigurationSource()))
            {
                return false;
            }

            return RemoveField(field, configurationSource);
        }

        public bool RemoveField([NotNull] InputFieldDefinition field, ConfigurationSource configurationSource)
        {
            if (!configurationSource.Overrides(field.GetConfigurationSource()))
            {
                return false;
            }

            Definition.IgnoreField(field.Name, configurationSource);

            Definition.RemoveField(field);

            return true;
        }

        [CanBeNull]
        public InternalInputValueBuilder Field([NotNull] PropertyInfo property, ConfigurationSource configurationSource)
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


            return field?.Builder;
        }

        public bool IgnoreField([NotNull] MemberInfo member, ConfigurationSource configurationSource)
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


        [NotNull]
        public InternalInputObjectTypeBuilder Name([CanBeNull] string name, ConfigurationSource configurationSource)
        {
            Definition.SetName(name, configurationSource);
            return this;
        }
    }
}