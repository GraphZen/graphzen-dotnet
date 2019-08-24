// Copyright (c) GraphZen LLC. All rights reserved.
// Licensed under the GraphZen Community License. See the LICENSE file in the project root for license information.
using JetBrains.Annotations;



using GraphZen.Infrastructure;

namespace GraphZen.TypeSystem.Taxonomy
{
    [GraphQLIgnore]
    public interface IMutableFieldDefinition : IFieldDefinition, IMutableAnnotatableDefinition,
        IMutableNamed,
        IMutableDescription,
        IMutableArgumentsContainerDefinition,
        IMutableDeprecation
    {
        
        new IGraphQLTypeReference FieldType { get; set; }

        
        new Resolver<object, object> Resolver { get; set; }

        
        new IMutableFieldsContainerDefinition DeclaringType { get; }
    }
}