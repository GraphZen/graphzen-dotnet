// Copyright (c) GraphZen LLC. All rights reserved.
// Licensed under the GraphZen Community License. See the LICENSE file in the project root for license information.

using GraphZen.Infrastructure;
using JetBrains.Annotations;

namespace GraphZen.Types
{
    [GraphQLIgnore]
    public interface IMutableObjectTypeDefinition : IObjectTypeDefinition, IMutableGraphQLTypeDefinition,
        IMutableFieldsContainerDefinition
    {
        new IsTypeOf<object, GraphQLContext> IsTypeOf { get; set; }
    }
}