// Copyright (c) GraphZen LLC. All rights reserved.
// Licensed under the GraphZen Community License. See the LICENSE file in the project root for license information.

using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using GraphZen.Infrastructure;
using GraphZen.LanguageModel;
using JetBrains.Annotations;

namespace GraphZen.TypeSystem.Internal
{
    public class InternalDirectiveDefinitionBuilder : MemberDefinitionBuilder<MutableDirectiveDefinition>
    {
        public InternalDirectiveDefinitionBuilder(MutableDirectiveDefinition directive
        ) : base(directive)
        {
        }


        public InternalArgumentBuilder Argument(string name) =>
            Definition.FindArgument(name)?.InternalBuilder ?? throw new NotImplementedException();

        public InternalArgumentBuilder? Argument(string name, string type, ConfigurationSource configurationSource) =>
            Definition.GetOrAddArgument(name, type, configurationSource)?.InternalBuilder;

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
            Definition.GetOrAddArgument(name, clrType, configurationSource)?.InternalBuilder;

        public InternalDirectiveDefinitionBuilder Name(string name, ConfigurationSource configurationSource)
        {
            Definition.SetName(name, configurationSource);
            return this;
        }

        public InternalDirectiveDefinitionBuilder ClrType(Type clrType, bool inferName, ConfigurationSource configurationSource)
        {
            Definition.SetClrType(clrType, inferName, configurationSource);
            return this;
        }

        public InternalDirectiveDefinitionBuilder ClrType(Type clrType, string name, ConfigurationSource configurationSource)
        {
            Definition.SetClrType(clrType, name, configurationSource);
            return this;
        }

        public bool RemoveArgument(string name, ConfigurationSource configurationSource) =>
            throw new NotImplementedException();

        public InternalDirectiveDefinitionBuilder SetName(string name, ConfigurationSource configurationSource)
        {
            Definition.SetName(name, configurationSource);
            return this;
        }

        public InternalDirectiveDefinitionBuilder Description(string description, ConfigurationSource configurationSource)
        {
            Definition.SetDescription(description, configurationSource);
            return this;
        }

        public InternalDirectiveDefinitionBuilder RemoveClrType(ConfigurationSource configurationSource)
        {
            Definition.RemoveClrType(configurationSource);
            return this;
        }

        public InternalDirectiveDefinitionBuilder Repeatable(in bool repeatable, ConfigurationSource configurationSource)
        {
            Definition.SetRepeatable(repeatable, configurationSource);
            return this;
        }

        public InternalDirectiveDefinitionBuilder AddLocation(DirectiveLocation location, ConfigurationSource configurationSource)
        {
            Definition.AddLocation(location, configurationSource);
            return this;
        }

        public InternalDirectiveDefinitionBuilder Locations(IEnumerable<DirectiveLocation> locations,
            ConfigurationSource configurationSource)
        {
            var distinct = locations.ToHashSet();

            foreach (var existing in Definition.Locations)
            {
                if (!distinct.Contains(existing))
                {
                    RemoveLocation(existing, configurationSource);
                }
            }

            foreach (var location in distinct)
            {
                AddLocation(location, configurationSource);
            }

            return this;
        }

        public InternalDirectiveDefinitionBuilder RemoveLocation(DirectiveLocation location,
            ConfigurationSource configurationSource)
        {
            Definition.RemoveLocation(location, configurationSource);
            return this;
        }

        public InternalDirectiveDefinitionBuilder RemoveLocations(ConfigurationSource configurationSource)
        {
            foreach (var location in Definition.Locations)
            {
                Definition.RemoveLocation(location, configurationSource);
            }

            return this;
        }
    }
}