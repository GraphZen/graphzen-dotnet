// Copyright (c) GraphZen LLC. All rights reserved.
// Licensed under the GraphZen Community License. See the LICENSE file in the project root for license information.

using System.Collections.Generic;
using GraphZen.Infrastructure;
using GraphZen.TypeSystem.Internal;

namespace GraphZen.TypeSystem.Taxonomy
{
    [GraphQLIgnore]
    public interface IObjectTypeDefinition :
        IFieldsContainerDefinition,
        IImplementedInterfacesContainerDefinition,
        ICompositeTypeDefinition, IOutputDefinition
    {
        [CanBeNull]
        IsTypeOf<object, GraphQLContext> IsTypeOf { get; }

    }


    public interface IImplementedInterfacesContainer : IImplementedInterfacesContainerDefinition

    {
        [NotNull]
        [ItemNotNull]
        new IEnumerable<InterfaceType> GetImplementedInterfaces();

        [NotNull]
        [ItemNotNull]
        IReadOnlyList<InterfaceType> ImplementedInterfaces { get; }

        [NotNull]
        IReadOnlyDictionary<string, InterfaceType> ImplementedInterfacesMap { get; }
    }

    public interface IImplementedInterfacesContainerDefinition
    {
        [NotNull]
        [ItemNotNull]
        IEnumerable<INamedTypeReference> GetImplementedInterfaces();

    }

    public interface IMutableImplementedInterfacesContainerDefinition : IImplementedInterfacesContainerDefinition
    {

        ConfigurationSource? FindIgnoredImplementedInterfaceConfigurationSource(string name);
    }
}