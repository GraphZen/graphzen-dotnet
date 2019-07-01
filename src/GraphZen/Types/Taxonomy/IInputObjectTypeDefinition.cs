// Copyright (c) GraphZen LLC. All rights reserved.
// Licensed under the GraphZen Community License. See the LICENSE file in the project root for license information.

using System.Collections.Generic;
using GraphZen.Infrastructure;
using JetBrains.Annotations;

namespace GraphZen.Types
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