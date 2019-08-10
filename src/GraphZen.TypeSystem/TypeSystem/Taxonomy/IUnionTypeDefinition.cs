// Copyright (c) GraphZen LLC. All rights reserved.
// Licensed under the GraphZen Community License. See the LICENSE file in the project root for license information.

using System.Collections.Generic;
using GraphZen.Infrastructure;
using GraphZen.TypeSystem.Internal;

namespace GraphZen.TypeSystem.Taxonomy
{
    [GraphQLIgnore]
    public interface IUnionTypeDefinition : ICompositeTypeDefinition,
        IAbstractTypeDefinition, IOutputDefinition, IMemberTypesContainerDefinition
    {
    }

    [GraphQLIgnore]
    public interface IMemberTypesContainerDefinition
    {
        [NotNull]
        [ItemNotNull]
        IEnumerable<IObjectTypeDefinition> GetMemberTypes();
    }

    [GraphQLIgnore]
    public interface IMemberTypesContainer : IMemberTypesContainerDefinition
    {
        [NotNull]
        IReadOnlyDictionary<string, ObjectType> MemberTypes { get; }
        [NotNull]
        [ItemNotNull]
        new IEnumerable<ObjectType> GetMemberTypes();
    }

    [GraphQLIgnore]
    public interface IMutableMemberTypesContainerDefinition : IMemberTypesContainerDefinition
    {
        [NotNull]
        IReadOnlyDictionary<string, ObjectTypeDefinition> MemberTypes { get; }

        [NotNull]
        [ItemNotNull]
        new IEnumerable<ObjectTypeDefinition> GetMemberTypes();
        ConfigurationSource? FindIgnoredMemberTypeConfigurationSource([NotNull] string name);
    }
}