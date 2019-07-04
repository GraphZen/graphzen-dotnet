// Copyright (c) GraphZen LLC. All rights reserved.
// Licensed under the GraphZen Community License. See the LICENSE file in the project root for license information.

using System.Collections.Generic;
using GraphZen.Infrastructure;

namespace GraphZen.TypeSystem.Taxonomy
{
    [GraphQLIgnore]
    public interface IObjectTypeDefinition :
        IFieldsContainerDefinition,
        ICompositeTypeDefinition, IOutputDefinition
    {
        [CanBeNull]
        IsTypeOf<object, GraphQLContext> IsTypeOf { get; }

        [NotNull]
        [ItemNotNull]
        IEnumerable<INamedTypeReference> Interfaces { get; }
    }
}