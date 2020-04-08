// Copyright (c) GraphZen LLC. All rights reserved.
// Licensed under the GraphZen Community License. See the LICENSE file in the project root for license information.

using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using GraphZen.Infrastructure;
using GraphZen.TypeSystem.Internal;
using JetBrains.Annotations;

namespace GraphZen.TypeSystem.Taxonomy
{
    [GraphQLIgnore]
    public interface IMutableEnumValuesDefinition : IEnumValuesDefinition
    {
        [GenAccessorExtensions("Value")]
        IReadOnlyDictionary<string, EnumValueDefinition> Values { get; }
        ConfigurationSource? FindIgnoredValueConfigurationSource(string name);
        EnumValueDefinition? FindValue(string name);
        bool IgnoreValue(string name, ConfigurationSource configurationSource);
        bool UnignoreValue(string name, ConfigurationSource configurationSource);

        EnumValueDefinition AddValue(string name, ConfigurationSource configurationSource,
            ConfigurationSource nameConfigurationSource);

        new IEnumerable<EnumValueDefinition> GetValues();
    }
}