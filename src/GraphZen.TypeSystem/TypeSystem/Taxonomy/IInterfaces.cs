// Copyright (c) GraphZen LLC. All rights reserved.
// Licensed under the GraphZen Community License. See the LICENSE file in the project root for license information.

namespace GraphZen.TypeSystem.Taxonomy;

public interface IInterfaces : IInterfacesDefinition

{
    IReadOnlyList<InterfaceType> Interfaces { get; }


    IReadOnlyDictionary<string, InterfaceType> InterfacesMap { get; }


    new IEnumerable<InterfaceType> GetInterfaces();
}