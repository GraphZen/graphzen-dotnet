// Copyright (c) GraphZen LLC. All rights reserved.
// Licensed under the GraphZen Community License. See the LICENSE file in the project root for license information.

using System.Collections.Generic;
using GraphZen.Infrastructure;


namespace GraphZen.TypeSystem
{
    [GraphQLIgnore]
    public interface IInputObjectTypeDefinition :
        IGraphQLTypeDefinition, IInputDefinition
    {
        [NotNull]
        [ItemNotNull]
        IEnumerable<IInputFieldDefinition> GetFields();
    }
}