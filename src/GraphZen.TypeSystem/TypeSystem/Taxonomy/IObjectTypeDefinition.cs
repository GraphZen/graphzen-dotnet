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
        IInterfacesContainerDefinition,
        ICompositeTypeDefinition, IOutputDefinition
    {
        [CanBeNull]
        IsTypeOf<object, GraphQLContext> IsTypeOf { get; }
    }


    public interface IInterfacesContainer : IInterfacesContainerDefinition

    {
        [NotNull]
        [ItemNotNull]
        IReadOnlyList<InterfaceType> Interfaces { get; }

        [NotNull]
        IReadOnlyDictionary<string, InterfaceType> InterfacesMap { get; }

        [NotNull]
        [ItemNotNull]
        new IEnumerable<InterfaceType> GetInterfaces();
    }

    public interface IInterfacesContainerDefinition
    {
        [NotNull]
        [ItemNotNull]
        IEnumerable<IInterfaceTypeDefinition> GetInterfaces();
    }

    public interface IMutableInterfacesContainerDefinition : IInterfacesContainerDefinition
    {

        [NotNull]
        [ItemNotNull]
        new IEnumerable<InterfaceTypeDefinition> GetInterfaces();

        ConfigurationSource? FindIgnoredInterfaceConfigurationSource(string name);
    }
}