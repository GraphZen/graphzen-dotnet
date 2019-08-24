#nullable disable
using System.Collections.Generic;
using GraphZen.Infrastructure;

namespace GraphZen.TypeSystem.Taxonomy
{
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
}