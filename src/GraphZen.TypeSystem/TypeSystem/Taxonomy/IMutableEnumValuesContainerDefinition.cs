#nullable disable
using System.Collections.Generic;
using GraphZen.Infrastructure;
using GraphZen.TypeSystem.Internal;

namespace GraphZen.TypeSystem.Taxonomy
{
    [GraphQLIgnore]
    public interface IMutableEnumValuesContainerDefinition : IEnumValuesContainerDefinition
    {
        [NotNull]
        IReadOnlyDictionary<string, EnumValueDefinition> Values { get; }
        ConfigurationSource? FindIgnoredValueConfigurationSource(string name);
        [NotNull]
        [ItemNotNull]
        new IEnumerable<EnumValueDefinition> GetValues();

    }
}