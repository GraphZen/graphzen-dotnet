// Copyright (c) GraphZen LLC. All rights reserved.
// Licensed under the GraphZen Community License. See the LICENSE file in the project root for license information.

using JetBrains.Annotations;
#nullable disable
using System.Collections.Generic;
using GraphZen.Infrastructure;

namespace GraphZen.TypeSystem.Taxonomy
{
    public interface IInterfacesContainer : IInterfacesContainerDefinition

    {
        
        
        IReadOnlyList<InterfaceType> Interfaces { get; }

        
        IReadOnlyDictionary<string, InterfaceType> InterfacesMap { get; }

        
        
        new IEnumerable<InterfaceType> GetInterfaces();
    }
}