// Copyright (c) GraphZen LLC. All rights reserved.
// Licensed under the GraphZen Community License. See the LICENSE file in the project root for license information.

using JetBrains.Annotations;

using System.Collections.Generic;
using GraphZen.Infrastructure;

namespace GraphZen.TypeSystem.Taxonomy
{
    [GraphQLIgnore]
    public interface IObjectTypesContainer : IObjectTypesContainerDefinition
    {
        [GraphQLIgnore]
        
        
        new IEnumerable<ObjectType> GetObjects();

        
        
        [GraphQLIgnore]
        IReadOnlyList<ObjectType> Objects { get; }
    }
}