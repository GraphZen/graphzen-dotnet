// Copyright (c) GraphZen LLC. All rights reserved.
// Licensed under the GraphZen Community License. See the LICENSE file in the project root for license information.

using JetBrains.Annotations;

using System.Collections.Generic;
using GraphZen.Infrastructure;
using GraphZen.TypeSystem.Internal;

namespace GraphZen.TypeSystem.Taxonomy
{
    [GraphQLIgnore]
    public interface IMutableEnumValuesContainerDefinition : IEnumValuesContainerDefinition
    {
        
        IReadOnlyDictionary<string, EnumValueDefinition> Values { get; }
        ConfigurationSource? FindIgnoredValueConfigurationSource(string name);
        
        
        new IEnumerable<EnumValueDefinition> GetValues();

    }
}