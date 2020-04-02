// Copyright (c) GraphZen LLC. All rights reserved.
// Licensed under the GraphZen Community License. See the LICENSE file in the project root for license information.

using System;
using System.Diagnostics.CodeAnalysis;
using System.Reflection;
using GraphZen.Infrastructure;
using GraphZen.Internal;
using JetBrains.Annotations;

namespace GraphZen.TypeSystem.Internal
{
    public class InternalFieldBuilder : AnnotatableMemberDefinitionBuilder<FieldDefinition>
    {
        public InternalFieldBuilder(FieldDefinition definition, InternalSchemaBuilder schemaBuilder)
            : base(definition, schemaBuilder)
        {
        }


        public InternalFieldBuilder FieldType(Type clrType)
        {
            Definition.FieldType = Schema.GetOrAddTypeReference(clrType, false, false, Definition);
            return this;
        }


        public InternalFieldBuilder FieldType(PropertyInfo property
        )
        {
            Definition.FieldType = Schema.GetOrAddTypeReference(property, Definition);
            return this;
        }


        public InternalFieldBuilder FieldType(MethodInfo method)
        {
            Definition.FieldType = Schema.GetOrAddTypeReference(method, Definition);
            return this;
        }


        public InternalFieldBuilder FieldType(string type)
        {
            try
            {
                Definition.FieldType = Schema.GetOrAddTypeReference(type, Definition);
            }
            catch (InvalidTypeReferenceException e)
            {
                throw new InvalidOperationException(
                    $"Invalid type reference: '{type}' is not a valid type reference for {Definition.DeclaringType.Kind.ToDisplayString()} field '{Definition.DeclaringType.Name}.{Definition.Name}'.",
                    e);
            }

            return this;
        }


        public InternalFieldBuilder Resolve(Resolver<object, object?> resolver)
        {
            Definition.Resolver = resolver;
            return this;
        }

        public InternalFieldBuilder Deprecated(bool deprecated)
        {
            Definition.IsDeprecated = deprecated;
            return this;
        }

        public InternalFieldBuilder Deprecated(string reason)
        {
            Definition.DeprecationReason = reason;
            return this;
        }


        public InternalInputValueBuilder Argument(string name, ConfigurationSource configurationSource) =>
            Definition.GetOrAddArgument(name, configurationSource).Builder;

        public InternalInputValueBuilder? Argument(ParameterInfo parameter,
            ConfigurationSource configurationSource)
        {
            var (argName, _) = parameter.GetGraphQLArgumentName();

            if (parameter.IsIgnoredByDataAnnotation()) IgnoreArgument(parameter, ConfigurationSource.DataAnnotation);

            if (IsArgumentIgnored(argName, configurationSource)) return null;

            if (parameter.TryGetGraphQLTypeInfo(out _, out var innerClrType))
            {
                var argumentInnerType = Schema.Builder.InputType(innerClrType, configurationSource);
                if (argumentInnerType == null) IgnoreArgument(parameter, ConfigurationSource.Convention);
            }
            else
            {
                IgnoreArgument(parameter, ConfigurationSource.Convention);
            }

            if (IsArgumentIgnored(argName, configurationSource)) return null;

            var argument = Definition.FindArgument(parameter);
            if (argument == null)
            {
                Definition.UnignoreArgument(argName);
                argument = Definition.AddArgument(parameter, configurationSource);
            }
            else
            {
                argument.UpdateConfigurationSource(configurationSource);
            }

            if (parameter.TryGetDescriptionFromDataAnnotation(out var desc))
                argument.Builder.Description(desc, ConfigurationSource.DataAnnotation);

            return argument.Builder;
        }

        public bool IsArgumentIgnored(string name, ConfigurationSource configurationSource)
        {
            if (configurationSource == ConfigurationSource.Explicit) return false;

            var ignoredMemberConfigurationSource = Definition.FindIgnoredArgumentConfigurationSource(name);
            return ignoredMemberConfigurationSource.HasValue &&
                   ignoredMemberConfigurationSource.Overrides(configurationSource);
        }

        public bool UnignoreArgument(string name, ConfigurationSource configurationSource)
        {
            var ignoredConfigurationSource = Definition.FindIgnoredArgumentConfigurationSource(name);
            if (!configurationSource.Overrides(ignoredConfigurationSource)) return false;

            Definition.UnignoreArgument(name);
            return true;
        }


        public bool IgnoreArgument(string name, ConfigurationSource configurationSource)
        {
            var ignoredConfigurationSource = Definition.FindIgnoredArgumentConfigurationSource(name);
            if (ignoredConfigurationSource.HasValue)
                if (configurationSource.Overrides(ignoredConfigurationSource) &&
                    configurationSource != ignoredConfigurationSource)
                {
                    Definition.IgnoreArgument(name, configurationSource);
                    return true;
                }

            var argument = Definition.FindArgument(name);
            if (argument != null) return IgnoreArgument(argument, configurationSource);

            Definition.IgnoreArgument(name, configurationSource);
            return true;
        }


        public bool IgnoreArgument(ParameterInfo parameter, ConfigurationSource configurationSource)
        {
            var (argName, _) = parameter.GetGraphQLArgumentName();
            var ignoredConfigurationSource = Definition.FindIgnoredArgumentConfigurationSource(argName);
            if (ignoredConfigurationSource.HasValue)
                if (configurationSource.Overrides(ignoredConfigurationSource) &&
                    configurationSource != ignoredConfigurationSource)
                {
                    Definition.IgnoreArgument(argName, configurationSource);
                    return true;
                }

            var argument = Definition.FindArgument(parameter);
            if (argument != null) return IgnoreArgument(argument, configurationSource);

            Definition.IgnoreArgument(argName, configurationSource);
            return true;
        }

        public bool IgnoreArgument(ArgumentDefinition argument, ConfigurationSource configurationSource)
        {
            if (!configurationSource.Overrides(argument.GetConfigurationSource())) return false;

            Definition.IgnoreArgument(argument.Name, configurationSource);

            return RemoveArgument(argument, configurationSource);
        }

        public bool RemoveArgument(ArgumentDefinition argument, ConfigurationSource configurationSource)
        {
            if (!configurationSource.Overrides(argument.GetConfigurationSource())) return false;

            Definition.IgnoreArgument(argument.Name, configurationSource);

            Definition.RemoveArgument(argument);

            return true;
        }
    }
}