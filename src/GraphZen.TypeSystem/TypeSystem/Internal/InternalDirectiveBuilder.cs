// Copyright (c) GraphZen LLC. All rights reserved.
// Licensed under the GraphZen Community License. See the LICENSE file in the project root for license information.

using System;
using System.Diagnostics.CodeAnalysis;
using GraphZen.Infrastructure;
using GraphZen.LanguageModel;
using JetBrains.Annotations;

namespace GraphZen.TypeSystem.Internal
{
    public class InternalDirectiveBuilder : MemberDefinitionBuilder<DirectiveDefinition>
    {
        public InternalDirectiveBuilder(DirectiveDefinition directive
        ) : base(directive)
        {
        }

        public InternalDirectiveBuilder Locations(DirectiveLocation[] locations,
            ConfigurationSource configurationSource)
        {
            foreach (var directiveLocation in locations)
            {
                Definition.AddLocation(directiveLocation, configurationSource);
            }

            return this;
        }


        public InternalArgumentBuilder Argument(string name) =>
            Definition.FindArgument(name)?.Builder ?? throw new NotImplementedException();

        public InternalArgumentBuilder? Argument(string name, string type, ConfigurationSource configurationSource) =>
            Definition.GetOrAddArgument(name, type, configurationSource)?.Builder;

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

        public InternalArgumentBuilder? Argument(string name, Type clrType, ConfigurationSource configurationSource) =>
            Definition.GetOrAddArgument(name, clrType, configurationSource)?.Builder;

        public InternalDirectiveBuilder Name(string name, ConfigurationSource configurationSource)
        {
            Definition.SetName(name, configurationSource);
            return this;
        }

        public InternalDirectiveBuilder ClrType(Type clrType, bool inferName, ConfigurationSource configurationSource)
        {
            Definition.SetClrType(clrType, inferName, configurationSource);
            return this;
        }

        public InternalDirectiveBuilder ClrType(Type clrType, string name, ConfigurationSource configurationSource)
        {
            Definition.SetClrType(clrType, name, configurationSource);
            return this;
        }

        public bool RemoveArgument(string name, ConfigurationSource configurationSource) =>
            throw new NotImplementedException();

        public InternalDirectiveBuilder SetName(string name, ConfigurationSource configurationSource)
        {
            Definition.SetName(name, configurationSource);
            return this;
        }

        public InternalDirectiveBuilder Description(string description, ConfigurationSource configurationSource)
        {
            Definition.SetDescription(description, configurationSource);
            return this;
        }

        public InternalDirectiveBuilder RemoveClrType(ConfigurationSource configurationSource)
        {
            Definition.RemoveClrType(configurationSource);
            return this;
        }

        public InternalDirectiveBuilder Repeatable(in bool repeatable, ConfigurationSource configurationSource)
        {
            Definition.SetRepeatable(repeatable, configurationSource);
            return this;
        }
    }
}