// Copyright (c) GraphZen LLC. All rights reserved.
// Licensed under the GraphZen Community License. See the LICENSE file in the project root for license information.

using System.Linq;
using System.Reflection;
using GraphZen.Infrastructure;
using GraphZen.Internal;
using JetBrains.Annotations;

namespace GraphZen.TypeSystem.Internal
{
    public abstract class
        InternalFieldsContainerBuilder<TDefinition, TBuilder> : AnnotatableMemberDefinitionBuilder<TDefinition>
        where TDefinition : FieldsContainerDefinition
        where TBuilder : InternalFieldsContainerBuilder<TDefinition, TBuilder>
    {
        protected InternalFieldsContainerBuilder([NotNull] TDefinition definition,
            [NotNull] InternalSchemaBuilder schemaBuilder)
            : base(definition, schemaBuilder)
        {
        }

        public InternalFieldBuilder Field([NotNull] string name,
            ConfigurationSource nameConfigurationSource,
            ConfigurationSource configurationSource) =>
            Definition.GetOrAddField(name, nameConfigurationSource, configurationSource)?.Builder;


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

        public bool IgnoreField([NotNull] FieldDefinition field, ConfigurationSource configurationSource)
        {
            if (!configurationSource.Overrides(field.GetConfigurationSource()))
            {
                return false;
            }

            Definition.IgnoreField(field.Name, configurationSource);

            return RemoveField(field, configurationSource);
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


        public InternalFieldBuilder Field([NotNull] PropertyInfo property, ConfigurationSource configurationSource)
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
                var fieldInnerType = Schema.Builder.OutputType(innerClrType, configurationSource);
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


        public InternalFieldBuilder Field([NotNull] MethodInfo method, ConfigurationSource configurationSource)
        {
            var (fieldName, _) = method.GetGraphQLFieldName();
            if (method.IsIgnoredByDataAnnotation())
            {
                IgnoreField(method, ConfigurationSource.DataAnnotation);
            }


            var parameters = method.GetParameters();

            var hasOutParam = parameters.Any(_ => _.IsOut);
            if (hasOutParam)
            {
                IgnoreField(method, ConfigurationSource.Convention);
            }

            if (method.GetGenericArguments().Any())
            {
                IgnoreField(method, ConfigurationSource.Convention);
            }


            if (IsFieldIgnored(fieldName, configurationSource))
            {
                return null;
            }

            if (method.TryGetGraphQLTypeInfo(out _, out var innerClrType))
            {
                var fieldInnerType = Schema.Builder.OutputType(innerClrType, configurationSource);
                if (fieldInnerType == null)
                {
                    IgnoreField(method, ConfigurationSource.Convention);
                }
            }
            else
            {
                IgnoreField(method, ConfigurationSource.Convention);
            }

            if (IsFieldIgnored(fieldName, configurationSource))
            {
                return null;
            }

            var field = Definition.FindField(method);
            if (field == null)
            {
                Definition.UnignoreField(fieldName);
                field = Definition.AddField(method, configurationSource);
            }
            else
            {
                field.UpdateConfigurationSource(configurationSource);
            }

            return field?.Builder;
        }

        private bool Ignore([NotNull] FieldDefinition field, ConfigurationSource configurationSource)
        {
            if (!configurationSource.Overrides(field.GetConfigurationSource()))
            {
                return false;
            }

            return RemoveField(field, configurationSource);
        }

        public bool RemoveField([NotNull] FieldDefinition field, ConfigurationSource configurationSource)
        {
            if (!configurationSource.Overrides(field.GetConfigurationSource()))
            {
                return false;
            }

            Definition.IgnoreField(field.Name, configurationSource);

            Definition.RemoveField(field);

            return true;
        }
    }
}