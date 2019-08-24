// Copyright (c) GraphZen LLC. All rights reserved.
// Licensed under the GraphZen Community License. See the LICENSE file in the project root for license information.
using JetBrains.Annotations;



using System.Collections.Generic;
using System.Diagnostics;
using GraphZen.Infrastructure;
using GraphZen.LanguageModel;
using GraphZen.TypeSystem.Internal;
using GraphZen.TypeSystem.Taxonomy;

namespace GraphZen.TypeSystem
{
    [DebuggerDisplay("{DebuggerDisplay,nq}")]
    public class EnumTypeDefinition : NamedTypeDefinition, IMutableEnumTypeDefinition
    {
        
        private readonly Dictionary<string, EnumValueDefinition>
            _values = new Dictionary<string, EnumValueDefinition>();

        public EnumTypeDefinition(TypeIdentity identity,
            SchemaDefinition schema,
            ConfigurationSource configurationSource)
            : base(Check.NotNull(identity, nameof(identity)), Check.NotNull(schema, nameof(schema)),
                configurationSource)
        {
            Builder = new InternalEnumTypeBuilder(this, Schema.Builder);
            identity.Definition = this;
        }

        private string DebuggerDisplay => $"enum {Name}";

        
        public InternalEnumTypeBuilder Builder { get; }

        public override DirectiveLocation DirectiveLocation { get; } = DirectiveLocation.Enum;

        public override TypeKind Kind { get; } = TypeKind.Enum;


        public IReadOnlyDictionary<string, EnumValueDefinition> Values => _values;
        public ConfigurationSource? FindIgnoredValueConfigurationSource(string name) => throw new System.NotImplementedException();
        public IEnumerable<EnumValueDefinition> GetValues() => Values.Values;

        public override string ToString() => $"enum {Name}";
        IEnumerable<IEnumValueDefinition> IEnumValuesContainerDefinition.GetValues() => GetValues();

        
        public EnumValueDefinition GetOrAddValue(string name,
            ConfigurationSource nameConfigurationSource,
            ConfigurationSource configurationSource) =>
            _values.TryGetValue(Check.NotNull(name, nameof(name)), out var value)
                ? value
                : _values[name] = new EnumValueDefinition(name,
                    nameConfigurationSource,
                    this, Schema, configurationSource);
    }
}