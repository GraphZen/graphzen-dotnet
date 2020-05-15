// Copyright (c) GraphZen LLC. All rights reserved.
// Licensed under the GraphZen Community License. See the LICENSE file in the project root for license information.

using System.Collections.Generic;
using System.Collections.Immutable;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Reflection;
using GraphZen.Infrastructure;
using GraphZen.Internal;
using JetBrains.Annotations;

namespace GraphZen.TypeSystem.Internal
{
    public abstract class
        InternalFieldsBuilder<TDefinition, TBuilder> : AnnotatableMemberDefinitionBuilder<TDefinition>
        where TDefinition : FieldsDefinition
        where TBuilder : InternalFieldsBuilder<TDefinition, TBuilder>
    {
        protected InternalFieldsBuilder(TDefinition definition,
            InternalSchemaBuilder schemaBuilder)
            : base(definition, schemaBuilder)
        {
        }


        private IReadOnlyList<string> IgnoredMethodNames { get; } =

            typeof(object).GetMethods().Select(_ => _.Name).ToImmutableList();


        public InternalFieldBuilder? Field(string name,
            ConfigurationSource nameConfigurationSource,
            ConfigurationSource configurationSource)
        {
            if (!name.IsValidGraphQLName())
            {
                throw new InvalidNameException(TypeSystemExceptionMessages.InvalidNameException
                    .CannotGetOrCreateFieldBuilderWithInvalidName(name, Definition));
            }

            return Definition.GetOrAddField(name, nameConfigurationSource, configurationSource)?.Builder;
        }

        protected void ConfigureOutputFields()
        {
            if (Definition.ClrType == null)
            {
                return;
            }

            var flags = BindingFlags.Public | BindingFlags.Instance | BindingFlags.Static;

            var fieldMembers = Definition.ClrType.GetMembers(flags)
                .Where(_ => !(_ is MethodInfo method) || method.DeclaringType != typeof(object) &&
                    method.ReturnType != typeof(void) &&
                    !IgnoredMethodNames.Contains(method.Name) && !method.IsSpecialName)
                .OrderBy(_ => _.MetadataToken);
            foreach (var fieldMember in fieldMembers)
            {
                switch (fieldMember)
                {
                    case MethodInfo method:
                        {
                            Field(method, ConfigurationSource.Convention);
                        }
                        break;
                    case PropertyInfo property:
                        {
                            Field(property, ConfigurationSource.Convention);
                        }
                        break;
                }
            }
        }

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

        public bool IgnoreField(FieldDefinition field, ConfigurationSource configurationSource)
        {
            if (!configurationSource.Overrides(field.GetConfigurationSource()))
            {
                return false;
            }

            Definition.IgnoreField(field.Name, configurationSource);

            return RemoveField(field, configurationSource);
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


        public InternalFieldBuilder? Field(PropertyInfo property, ConfigurationSource configurationSource)
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

            if (property.TryGetDescriptionFromDataAnnotation(out var desc))
            {
                field.Builder.Description(desc, ConfigurationSource.DataAnnotation);
            }

            return field.Builder;
        }


        public InternalFieldBuilder? Field(MethodInfo method, ConfigurationSource configurationSource)
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


            if (method.TryGetDescriptionFromDataAnnotation(out var desc))
            {
                field.Builder.Description(desc, ConfigurationSource.DataAnnotation);
            }

            foreach (var parameter in method.GetParameters())
            {
                field.Builder.Argument(parameter, ConfigurationSource.Convention);
            }

            return field.Builder;
        }

        private bool Ignore(FieldDefinition field, ConfigurationSource configurationSource)
        {
            if (!configurationSource.Overrides(field.GetConfigurationSource()))
            {
                return false;
            }

            return RemoveField(field, configurationSource);
        }

        public bool RemoveField(string name, ConfigurationSource configurationSource) =>
            Definition.TryGetField(name, out var field) && RemoveField(field, configurationSource);

        public bool RemoveField(FieldDefinition field, ConfigurationSource configurationSource)
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