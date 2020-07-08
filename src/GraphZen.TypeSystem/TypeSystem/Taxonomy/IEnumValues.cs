// Copyright (c) GraphZen LLC. All rights reserved.
// Licensed under the GraphZen Community License. See the LICENSE file in the project root for license information.

using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using GraphZen.Infrastructure;
using JetBrains.Annotations;

namespace GraphZen.TypeSystem
{
    [GraphQLIgnore]
    public partial interface IEnumValues : IEnumValuesDefinition
    {
        [GenDictionaryAccessors(nameof(EnumValue.Name), nameof(EnumValue.Value))]
        IReadOnlyDictionary<string, EnumValue> Values { get; }

        [GenDictionaryAccessors(nameof(EnumValue.Value), nameof(EnumValue.Value))]
        IReadOnlyDictionary<object, EnumValue> ValuesByValue { get; }


        new IEnumerable<EnumValue> GetValues();
    }
}