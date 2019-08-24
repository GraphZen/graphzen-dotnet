using System.Collections.Generic;
using GraphZen.Infrastructure;

namespace GraphZen.TypeSystem.Taxonomy
{
    [GraphQLIgnore]
    public interface IEnumValuesContainer : IEnumValuesContainerDefinition
    {
        [NotNull]
        IReadOnlyDictionary<string, EnumValue> Values { get; }

        [NotNull]
        IReadOnlyDictionary<object, EnumValue> ValuesByValue { get; }

        [NotNull]
        [ItemNotNull]
        new IEnumerable<EnumValue> GetValues();
    }
}