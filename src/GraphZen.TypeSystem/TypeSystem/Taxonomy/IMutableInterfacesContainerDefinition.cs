#nullable disable
using System.Collections.Generic;
using GraphZen.Infrastructure;
using GraphZen.TypeSystem.Internal;

namespace GraphZen.TypeSystem.Taxonomy
{
    public interface IMutableInterfacesContainerDefinition : IInterfacesContainerDefinition
    {

        [NotNull]
        [ItemNotNull]
        new IEnumerable<InterfaceTypeDefinition> GetInterfaces();

        ConfigurationSource? FindIgnoredInterfaceConfigurationSource(string name);
    }
}