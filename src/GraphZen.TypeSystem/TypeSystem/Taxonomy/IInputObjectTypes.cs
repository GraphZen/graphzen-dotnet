// Copyright (c) GraphZen LLC. All rights reserved.
// Licensed under the GraphZen Community License. See the LICENSE file in the project root for license information.

namespace GraphZen.TypeSystem.Taxonomy;

[GraphQLIgnore]
public interface IInputObjectTypes : IInputObjectTypesDefinition
{
    [GraphQLIgnore] IReadOnlyList<InputObjectType> InputObjects { get; }

    [GraphQLIgnore]
    new IEnumerable<InputObjectType> GetInputObjects();
}
