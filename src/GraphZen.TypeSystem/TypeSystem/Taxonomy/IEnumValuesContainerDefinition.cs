#nullable disable
using System.Collections.Generic;
using GraphZen.Infrastructure;

namespace GraphZen.TypeSystem.Taxonomy
{
    [GraphQLIgnore]
    public interface IEnumValuesContainerDefinition
    {
        [NotNull]
        [ItemNotNull]
        IEnumerable<IEnumValueDefinition> GetValues();
    }
}