// Copyright (c) GraphZen LLC. All rights reserved.
// Licensed under the GraphZen Community License. See the LICENSE file in the project root for license information.

using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using GraphZen.Infrastructure;
using GraphZen.LanguageModel;
using GraphZen.TypeSystem.Internal;
using JetBrains.Annotations;

namespace GraphZen.TypeSystem
{
    [DebuggerDisplay("{" + nameof(DebuggerDisplay) + ",nq}")]
    [DisplayName("enum")]
    public partial class MutableEnumType : MutableNamedTypeDefinition, IMutableEnumType
    {
        private readonly Dictionary<string, ConfigurationSource> _ignoredValues =
            new Dictionary<string, ConfigurationSource>();

        internal readonly Dictionary<string, MutableEnumValue> InternalValues =
            new Dictionary<string, MutableEnumValue>();


        public MutableEnumType(TypeIdentity identity, MutableSchema schema,
            ConfigurationSource configurationSource)
            : base(identity, schema, configurationSource)
        {
            InternalBuilder = new InternalEnumTypeBuilder(this);
            Builder = new EnumTypeBuilder(InternalBuilder);
        }

        public override IEnumerable<IMember> Children() => GetValues();


        private string DebuggerDisplay => $"enum {Name}";

        internal new InternalEnumTypeBuilder InternalBuilder { get; }
        public new EnumTypeBuilder Builder { get; }
        protected override INamedTypeDefinitionBuilder GetBuilder() => Builder;

        protected override MemberDefinitionBuilder GetInternalBuilder() => InternalBuilder;

        public override DirectiveLocation DirectiveLocation { get; } = DirectiveLocation.Enum;

        public override TypeKind Kind { get; } = TypeKind.Enum;

        [GenDictionaryAccessors(nameof(MutableEnumValue.Name), nameof(MutableEnumValue.Value))]
        public IReadOnlyDictionary<string, MutableEnumValue> Values => InternalValues;

        public ConfigurationSource? FindIgnoredValueConfigurationSource(string name) =>
            _ignoredValues.TryGetValue(name, out var cs) ? cs : (ConfigurationSource?)null;

        public bool RemoveValue(MutableEnumValue value)
        {
            InternalValues.Remove(value.Name);
            return true;
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
                    InternalValues.Remove(name);
                    return true;
                }
            }

            return false;
        }

        public bool UnignoreValue(string name, ConfigurationSource configurationSource)
        {
            var ignoredConfigurationSource = FindIgnoredValueConfigurationSource(name);
            if (!configurationSource.Overrides(ignoredConfigurationSource))
            {
                return false;
            }

            _ignoredValues.Remove(name);
            return true;
        }

        public MutableEnumValue AddValue(string name, ConfigurationSource configurationSource,
            ConfigurationSource nameConfigurationSource)
        {
            var definition =
                new MutableEnumValue(name, nameConfigurationSource, this, Schema, configurationSource);
            InternalValues[name] = definition;
            return definition;
        }

        public IEnumerable<MutableEnumValue> GetValues() => Values.Values;

        IEnumerable<IEnumValue> IEnumValuesDefinition.GetValues() => GetValues();
    }
}