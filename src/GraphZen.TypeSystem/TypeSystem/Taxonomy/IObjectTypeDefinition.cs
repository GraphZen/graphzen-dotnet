// Copyright (c) GraphZen LLC. All rights reserved.
// Licensed under the GraphZen Community License. See the LICENSE file in the project root for license information.
using JetBrains.Annotations;



using GraphZen.Infrastructure;

namespace GraphZen.TypeSystem.Taxonomy
{
    [GraphQLIgnore]
    public interface IObjectTypeDefinition :
        IFieldsContainerDefinition,
        IInterfacesContainerDefinition,
        ICompositeTypeDefinition, IOutputDefinition
    {
        
        IsTypeOf<object, GraphQLContext>? IsTypeOf { get; }
    }
}