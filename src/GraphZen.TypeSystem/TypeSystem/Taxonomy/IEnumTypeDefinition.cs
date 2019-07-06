// Copyright (c) GraphZen LLC. All rights reserved.
// Licensed under the GraphZen Community License. See the LICENSE file in the project root for license information.

using System.Collections.Generic;
using GraphZen.Infrastructure;

namespace GraphZen.TypeSystem.Taxonomy
{
    [GraphQLIgnore]
    public interface IEnumTypeDefinition : ILeafTypeDefinition, IInputDefinition, IOutputDefinition
    {
        [NotNull]
        [ItemNotNull]
        IEnumerable<IEnumValueDefinition> GetValues();
    }
}