// Copyright (c) GraphZen LLC. All rights reserved.
// Licensed under the GraphZen Community License. See the LICENSE file in the project root for license information.

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
        internal readonly Dictionary<string, EnumValueDefinition> _values =
            new Dictionary<string, EnumValueDefinition>();

        private readonly Dictionary<string, ConfigurationSource> _ignoredValues =
            new Dictionary<string, ConfigurationSource>();

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
            return _ignoredValues.TryGetValue(name, out var cs) ? cs : (ConfigurationSource?)null;
        }

        public EnumValueDefinition? FindValue(string name)
        {
            return _values.TryGetValue(name, out var value) ? value : null;
        }

        public bool IgnoreValue(string name, ConfigurationSource configurationSource)
        {
            var itemConfigurationSource = FindValue(name)?.GetConfigurationSource();
            if (configurationSource.Overrides(itemConfigurationSource))
            {
                var ignoredConfigurationSource = FindIgnoredValueConfigurationSource(name);
                if (configurationSource.Overrides(ignoredConfigurationSource))
                {
                    _ignoredValues[name] = configurationSource;
                    _values.Remove(name);
                    return true;
                }
            }

            return false;
        }

        public bool UnignoreValue(string name, ConfigurationSource configurationSource)
        {
            var ignoredConfigurationSource = FindIgnoredValueConfigurationSource(name);
            if (!configurationSource.Overrides(ignoredConfigurationSource)) return false;
            _ignoredValues.Remove(name);
            return true;
        }

        public EnumValueDefinition AddValue(string name, ConfigurationSource configurationSource,
            ConfigurationSource nameConfigurationSource)
        {
            var definition =
                new EnumValueDefinition(name, nameConfigurationSource, this, Schema, configurationSource);
            _values[name] = definition;
            return definition;
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