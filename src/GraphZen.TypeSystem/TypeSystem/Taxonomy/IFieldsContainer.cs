#nullable disable
// Copyright (c) GraphZen LLC. All rights reserved.
// Licensed under the GraphZen Community License. See the LICENSE file in the project root for license information.

using System.Collections.Generic;
using GraphZen.Infrastructure;

namespace GraphZen.TypeSystem.Taxonomy
{
    [GraphQLIgnore]
    public interface IFieldsContainer : IFieldsContainerDefinition, INamedType
    {
        [NotNull]
        IReadOnlyDictionary<string, Field> Fields { get; }

        [NotNull]
        [ItemNotNull]
        [GraphQLCanBeNull]
        new IEnumerable<Field> GetFields();
    }
}