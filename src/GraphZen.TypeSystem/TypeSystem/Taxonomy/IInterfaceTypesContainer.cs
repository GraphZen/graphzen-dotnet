using System.Collections.Generic;
using GraphZen.Infrastructure;

namespace GraphZen.TypeSystem.Taxonomy
{
    [GraphQLIgnore]
    public interface IInterfaceTypesContainer : IInterfaceTypesContainerDefinition
    {
        [GraphQLIgnore]
        [NotNull]
        [ItemNotNull]
        new IEnumerable<InterfaceType> GetInterfaces();

        [GraphQLIgnore]
        [NotNull]
        [ItemNotNull]
        IReadOnlyList<InterfaceType> Interfaces { get; }
    }
}