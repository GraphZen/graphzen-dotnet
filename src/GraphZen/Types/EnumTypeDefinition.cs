// Copyright (c) GraphZen LLC. All rights reserved.
// Licensed under the GraphZen Community License. See the LICENSE file in the project root for license information.

using System.Collections.Generic;
using System.Diagnostics;
using GraphZen.Infrastructure;
using GraphZen.Language;
using GraphZen.Types.Internal;
using JetBrains.Annotations;

namespace GraphZen.Types
{
    [DebuggerDisplay("{DebuggerDisplay,nq}")]
    public class EnumTypeDefinition : NamedTypeDefinition, IMutableEnumTypeDefinition
    {
        [NotNull] private readonly Dictionary<string, EnumValueDefinition>
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

        [NotNull]
        public InternalEnumTypeBuilder Builder { get; }

        public override DirectiveLocation DirectiveLocation { get; } = DirectiveLocation.Enum;

        public override TypeKind Kind { get; } = TypeKind.Enum;
        IEnumerable<IEnumValueDefinition> IEnumTypeDefinition.GetValues() => GetValues();


        public IReadOnlyDictionary<string, EnumValueDefinition> ValuesByName => _values;
        public IEnumerable<EnumValueDefinition> GetValues() => ValuesByName.Values;

        public override string ToString() => $"enum {Name}";

        [NotNull]
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