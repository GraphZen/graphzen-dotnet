// Copyright (c) GraphZen LLC. All rights reserved.
// Licensed under the GraphZen Community License. See the LICENSE file in the project root for license information.

using System;
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

        public ConfigurationSource? FindIgnoredValueConfigurationSource(string name)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<EnumValueDefinition> GetValues()
        {
            return Values.Values;
        }

        public override string ToString()
        {
            return $"enum {Name}";
        }

        IEnumerable<IEnumValueDefinition> IEnumValuesContainerDefinition.GetValues()
        {
            return GetValues();
        }


        public EnumValueDefinition GetOrAddValue(string name,
            ConfigurationSource nameConfigurationSource,
            ConfigurationSource configurationSource)
        {
            return _values.TryGetValue(Check.NotNull(name, nameof(name)), out var value)
                ? value
                : _values[name] = new EnumValueDefinition(name,
                    nameConfigurationSource,
                    this, Schema, configurationSource);
        }
    }
}