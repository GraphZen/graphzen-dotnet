using System.Collections.Generic;
using GraphZen.Infrastructure;

namespace GraphZen.TypeSystem.Taxonomy
{
    public interface IInterfacesContainerDefinition
    {
        [NotNull]
        [ItemNotNull]
        IEnumerable<IInterfaceTypeDefinition> GetInterfaces();
    }
}