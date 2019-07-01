// Copyright (c) GraphZen LLC. All rights reserved.
// Licensed under the GraphZen Community License. See the LICENSE file in the project root for license information.

using System.Collections.Generic;
using GraphZen.Infrastructure;
using JetBrains.Annotations;

namespace GraphZen.Types
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