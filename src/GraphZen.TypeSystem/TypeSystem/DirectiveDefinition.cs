// Copyright (c) GraphZen LLC. All rights reserved.
// Licensed under the GraphZen Community License. See the LICENSE file in the project root for license information.
using JetBrains.Annotations;
#nullable disable


using System;
using System.Collections.Generic;
using System.Diagnostics;
using GraphZen.Infrastructure;
using GraphZen.LanguageModel;
using GraphZen.TypeSystem.Internal;
using GraphZen.TypeSystem.Taxonomy;

namespace GraphZen.TypeSystem
{
    [DebuggerDisplay("{DebuggerDisplay,nq}")]
    public class DirectiveDefinition : MemberDefinition, IMutableDirectiveDefinition,
        IInfrastructure<InternalDirectiveBuilder>
    {
        
        private readonly Dictionary<string, ArgumentDefinition> _arguments =
            new Dictionary<string, ArgumentDefinition>();

         private DirectiveLocation[] _locations = { };

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
        public IReadOnlyList<DirectiveLocation> Locations => _locations;

        public bool RenameArgument(ArgumentDefinition argument, string name, ConfigurationSource configurationSource)
        {
            if (!configurationSource.Overrides(argument.GetNameConfigurationSource()))
            {
                return false;
            }

            if (this.TryGetArgument(name, out var existing) && existing != argument)
            {
                throw new InvalidOperationException(
                    $"Cannot rename {argument} to '{name}'. {this} already contains a field named '{name}'.");
            }

            _arguments.Remove(argument.Name);
            _arguments[name] = argument;
            return true;
        }

        public IEnumerable<IArgumentDefinition> GetArguments() => _arguments.Values;
        public IReadOnlyDictionary<string, ArgumentDefinition> Arguments => _arguments;

        IEnumerable<ArgumentDefinition> IMutableArgumentsContainerDefinition.GetArguments() => Arguments.Values;

        public void SetLocations(DirectiveLocation[] locations)
        {
            Check.NotNull(locations, nameof(locations));
            _locations = locations;
        }

        
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
    }
}