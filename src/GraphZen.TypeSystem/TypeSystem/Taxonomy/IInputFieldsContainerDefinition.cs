#nullable disable
using System.Collections.Generic;
using GraphZen.Infrastructure;

namespace GraphZen.TypeSystem.Taxonomy
{
    public interface IInputFieldsContainerDefinition
    {
        [NotNull]
        [ItemNotNull]
        IEnumerable<IInputFieldDefinition> GetFields();

    }
}