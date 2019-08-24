// Copyright (c) GraphZen LLC. All rights reserved.
// Licensed under the GraphZen Community License. See the LICENSE file in the project root for license information.

using JetBrains.Annotations;

using System.Collections.Generic;
using GraphZen.Infrastructure;

namespace GraphZen.TypeSystem.Taxonomy
{
    [GraphQLIgnore]
    public interface IEnumValuesContainer : IEnumValuesContainerDefinition
    {
        
        IReadOnlyDictionary<string, EnumValue> Values { get; }

        
        IReadOnlyDictionary<object, EnumValue> ValuesByValue { get; }

        
        
        new IEnumerable<EnumValue> GetValues();
    }
}