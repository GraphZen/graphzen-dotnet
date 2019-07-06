// Copyright (c) GraphZen LLC. All rights reserved.
// Licensed under the GraphZen Community License. See the LICENSE file in the project root for license information.

using GraphZen.Infrastructure;

namespace GraphZen.TypeSystem.Taxonomy
{
    [GraphQLIgnore]
    // ReSharper disable once PossibleInterfaceMemberAmbiguity
    public interface INamedType : IGraphQLTypeDefinition, INullableType
    {
        [NotNull]
        new string Name { get; }
    }
}