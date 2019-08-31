// Copyright (c) GraphZen LLC. All rights reserved.
// Licensed under the GraphZen Community License. See the LICENSE file in the project root for license information.

using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using GraphZen.Infrastructure;
using GraphZen.LanguageModel;
using GraphZen.TypeSystem.Internal;
using GraphZen.TypeSystem.Taxonomy;
using JetBrains.Annotations;

namespace GraphZen.TypeSystem
{
    [DebuggerDisplay("{" + nameof(DebuggerDisplay) + ",nq}")]
    public class DirectiveDefinition : MemberDefinition, IMutableDirectiveDefinition,
        IInfrastructure<InternalDirectiveBuilder>
    {
        private readonly Dictionary<string, ArgumentDefinition> _arguments =
            new Dictionary<string, ArgumentDefinition>();

        private readonly ConcurrentDictionary<DirectiveLocation, ConfigurationSource> _locations =
            new ConcurrentDictionary<DirectiveLocation, ConfigurationSource>();

        private readonly ConcurrentDictionary<DirectiveLocation, ConfigurationSource> _ignoredLocations =
            new ConcurrentDictionary<DirectiveLocation, ConfigurationSource>();

        public DirectiveDefinition(string name, SchemaDefinition schema, ConfigurationSource configurationSource) :
            base(configurationSource)
        {
            Name = Check.NotNull(name, nameof(name));
            Builder = new InternalDirectiveBuilder(this, Check.NotNull(schema, nameof(schema)).Builder);
        }


        private InternalDirectiveBuilder Builder { get; }

        private string DebuggerDisplay => $"directive {Name}";

        InternalDirectiveBuilder IInfrastructure<InternalDirectiveBuilder>.Instance => Builder;

        public string Name { get; }

        public bool SetName(string name, ConfigurationSource configurationSource) =>
            throw new NotImplementedException();

        public ConfigurationSource GetNameConfigurationSource() => throw new NotImplementedException();

        public bool RenameArgument(ArgumentDefinition argument, string name, ConfigurationSource configurationSource)
        {
            if (!configurationSource.Overrides(argument.GetNameConfigurationSource())) return false;

            if (this.TryGetArgument(name, out var existing) && existing != argument)
                throw new InvalidOperationException(
                    $"Cannot rename {argument} to '{name}'. {this} already contains a field named '{name}'.");

            _arguments.Remove(argument.Name);
            _arguments[name] = argument;
            return true;
        }

        public IEnumerable<IArgumentDefinition> GetArguments() => _arguments.Values;

        public IReadOnlyDictionary<string, ArgumentDefinition> Arguments => _arguments;

        IEnumerable<ArgumentDefinition> IMutableArgumentsDefinition.GetArguments() => Arguments.Values;

        public ArgumentDefinition GetOrAddArgument(string name, ConfigurationSource configurationSource)
        {
            if (!_arguments.TryGetValue(Check.NotNull(name, nameof(name)), out var argument))
            {
                argument = new ArgumentDefinition(name, ConfigurationSource.Convention, Builder.Schema,
                    configurationSource, this, null);
                _arguments[name] = argument;
            }

            return argument;
        }

        public bool AddLocation(DirectiveLocation location, ConfigurationSource configurationSource)
        {
            var locationConfigurationSource = FindDirectiveLocationConfigurationSource(location);
            if (configurationSource.Overrides(locationConfigurationSource))
            {
                _locations.AddOrUpdate(location, configurationSource, (dl, cs) => configurationSource);
                return true;
            }

            return false;
        }

        public bool IgnoreLocation(DirectiveLocation location, ConfigurationSource configurationSource)
        {
            var existingConfigurationSource = FindDirectiveLocationConfigurationSource(location);
            if (configurationSource.Overrides(existingConfigurationSource))
            {
                var ignoredConfigurationSource = FindIgnoredDirectiveLocationConfigurationSource(location);
                var newIgnoredConfigurationSource = configurationSource.Max(ignoredConfigurationSource);
                _ignoredLocations.AddOrUpdate(location, newIgnoredConfigurationSource,
                    (dl, cs) => newIgnoredConfigurationSource);
                return true;
            }

            return false;
        }

        public bool UnignoreLocation(DirectiveLocation location, ConfigurationSource configurationSource)
        {
            var ignoredConfigurationSource = FindIgnoredDirectiveLocationConfigurationSource(location);
            if (ignoredConfigurationSource.HasValue && configurationSource.Overrides(ignoredConfigurationSource))
            {
                _ignoredLocations.Remove(location, out _);
                return true;
            }

            return false;
        }


        public ConfigurationSource? FindDirectiveLocationConfigurationSource(DirectiveLocation directiveLocation) =>
            _locations.TryGetValue(directiveLocation, out var cs) ? cs : (ConfigurationSource?)null;

        public ConfigurationSource?
            FindIgnoredDirectiveLocationConfigurationSource(DirectiveLocation directiveLocation) =>
            _ignoredLocations.TryGetValue(directiveLocation, out var cs) ? cs : (ConfigurationSource?)null;

        public IReadOnlyCollection<DirectiveLocation> Locations =>
            (IReadOnlyCollection<DirectiveLocation>)_locations.Keys;
    }
}