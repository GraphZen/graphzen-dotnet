// Copyright (c) GraphZen LLC. All rights reserved.
// Licensed under the GraphZen Community License. See the LICENSE file in the project root for license information.

using System.Collections.Generic;
using GraphZen.Infrastructure;
using JetBrains.Annotations;

namespace GraphZen.TypeSystem
{
    [GraphQLIgnore]
    public interface IEnumType : IEnumTypeDefinition,
        ILeafType
    {
        [NotNull]
        [ItemNotNull]
        IReadOnlyList<EnumValue> Values { get; }


        [NotNull]
        IReadOnlyDictionary<string, EnumValue> ValuesByName { get; }

        [NotNull]
        IReadOnlyDictionary<object, EnumValue> ValuesByValue { get; }


        [NotNull]
        [ItemNotNull]
        new IEnumerable<EnumValue> GetValues();
    }
}