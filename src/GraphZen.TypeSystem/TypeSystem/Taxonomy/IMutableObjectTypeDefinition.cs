// Copyright (c) GraphZen LLC. All rights reserved.
// Licensed under the GraphZen Community License. See the LICENSE file in the project root for license information.
using JetBrains.Annotations;



using GraphZen.Infrastructure;

namespace GraphZen.TypeSystem.Taxonomy
{
    [GraphQLIgnore]
    public interface IMutableObjectTypeDefinition : IObjectTypeDefinition, IMutableGraphQLTypeDefinition,
        IMutableFieldsContainerDefinition, IMutableInterfacesContainerDefinition
    {
        new IsTypeOf<object, GraphQLContext> IsTypeOf { get; set; }
    }
}