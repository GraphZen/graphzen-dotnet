// Copyright (c) GraphZen LLC. All rights reserved.
// Licensed under the GraphZen Community License. See the LICENSE file in the project root for license information.

using System;
using System.Diagnostics.CodeAnalysis;
using System.Reflection;
using GraphZen.Infrastructure;
using JetBrains.Annotations;

namespace GraphZen.TypeSystem.Internal
{
    public class InternalFieldBuilder : AnnotatableMemberDefinitionBuilder<MutableField>
    {
        public InternalFieldBuilder(MutableField field) : base(field)
        {
        }

        public InternalFieldBuilder FieldType(Type clrType, ConfigurationSource configurationSource)
        {
            if (clrType.TryGetGraphQLTypeInfo(out var typeNode, out var innerClrType))
            {
                var identity = Schema.GetOrAddTypeIdentity(innerClrType);
                Definition.SetTypeReference(identity, typeNode, configurationSource);
            }

            return this;
        }


        public InternalFieldBuilder FieldType(string type, ConfigurationSource configurationSource)
        {
            Definition.SetTypeReference(type, configurationSource);
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


        public InternalArgumentBuilder? Argument(string name, string type, ConfigurationSource configurationSource) =>
            Definition.GetOrAddArgument(name, type, configurationSource)?.InternalBuilder;

        public InternalArgumentBuilder? Argument(string name, Type clrType, ConfigurationSource configurationSource) =>
            Definition.GetOrAddArgument(name, clrType, configurationSource)?.InternalBuilder;

        public InternalArgumentBuilder Argument(string name) =>
            Definition.FindArgument(name)?.InternalBuilder ?? throw new ItemNotFoundException("TODO");

        public InternalArgumentBuilder? Argument(ParameterInfo parameter,
            ConfigurationSource configurationSource)
        {
            var (argName, _) = parameter.GetGraphQLArgumentName();

            if (parameter.IsIgnoredByDataAnnotation())
            {
                IgnoreArgument(parameter, ConfigurationSource.DataAnnotation);
            }

            if (IsArgumentIgnored(argName, configurationSource))
            {
                return null;
            }

            if (parameter.TryGetGraphQLTypeInfo(out _, out var innerClrType))
            {
                var argumentInnerType = Schema.InternalBuilder.InputType(innerClrType, configurationSource);
                if (argumentInnerType == null)
                {
                    IgnoreArgument(parameter, ConfigurationSource.Convention);
                }
            }
            else
            {
                IgnoreArgument(parameter, ConfigurationSource.Convention);
            }

            if (IsArgumentIgnored(argName, configurationSource))
            {
                return null;
            }

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
            {
                argument.InternalBuilder.Description(desc, ConfigurationSource.DataAnnotation);
            }

            return argument.InternalBuilder;
        }

        public bool IsArgumentIgnored(string name, ConfigurationSource configurationSource)
        {
            if (configurationSource == ConfigurationSource.Explicit)
            {
                return false;
            }

            var ignoredMemberConfigurationSource = Definition.FindIgnoredArgumentConfigurationSource(name);
            return ignoredMemberConfigurationSource.HasValue &&
                   ignoredMemberConfigurationSource.Overrides(configurationSource);
        }

        public bool UnignoreArgument(string name, ConfigurationSource configurationSource)
        {
            var ignoredConfigurationSource = Definition.FindIgnoredArgumentConfigurationSource(name);
            if (!configurationSource.Overrides(ignoredConfigurationSource))
            {
                return false;
            }

            Definition.UnignoreArgument(name);
            return true;
        }


        public bool IgnoreArgument(string name, ConfigurationSource configurationSource)
        {
            var ignoredConfigurationSource = Definition.FindIgnoredArgumentConfigurationSource(name);
            if (ignoredConfigurationSource.HasValue)
            {
                if (configurationSource.Overrides(ignoredConfigurationSource) &&
                    configurationSource != ignoredConfigurationSource)
                {
                    Definition.IgnoreArgument(name, configurationSource);
                    return true;
                }
            }

            var argument = Definition.FindArgument(name);
            if (argument != null)
            {
                return IgnoreArgument(argument, configurationSource);
            }

            Definition.IgnoreArgument(name, configurationSource);
            return true;
        }


        public bool IgnoreArgument(ParameterInfo parameter, ConfigurationSource configurationSource)
        {
            var (argName, _) = parameter.GetGraphQLArgumentName();
            var ignoredConfigurationSource = Definition.FindIgnoredArgumentConfigurationSource(argName);
            if (ignoredConfigurationSource.HasValue)
            {
                if (configurationSource.Overrides(ignoredConfigurationSource) &&
                    configurationSource != ignoredConfigurationSource)
                {
                    Definition.IgnoreArgument(argName, configurationSource);
                    return true;
                }
            }

            var argument = Definition.FindArgument(parameter);
            if (argument != null)
            {
                return IgnoreArgument(argument, configurationSource);
            }

            Definition.IgnoreArgument(argName, configurationSource);
            return true;
        }

        public bool IgnoreArgument(MutableArgument argument, ConfigurationSource configurationSource)
        {
            if (!configurationSource.Overrides(argument.GetConfigurationSource()))
            {
                return false;
            }

            Definition.IgnoreArgument(argument.Name, configurationSource);

            return RemoveArgument(argument, configurationSource);
        }


        public bool RemoveArgument(string name, ConfigurationSource configurationSource) =>
            Definition.TryGetArgument(name, out var arg) && RemoveArgument(arg, configurationSource);

        public bool RemoveArgument(MutableArgument argument, ConfigurationSource configurationSource)
        {
            if (!configurationSource.Overrides(argument.GetConfigurationSource()))
            {
                return false;
            }

            Definition.IgnoreArgument(argument.Name, configurationSource);

            Definition.RemoveArgument(argument);

            return true;
        }

        public InternalFieldBuilder Description(string description, ConfigurationSource configurationSource)
        {
            Definition.SetDescription(description, configurationSource);
            return this;
        }

        public InternalFieldBuilder SetName(string name, ConfigurationSource configurationSource)
        {
            Definition.SetName(name, configurationSource);
            return this;
        }
    }
}