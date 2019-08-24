using System.Collections.Generic;
using GraphZen.Infrastructure;

namespace GraphZen.TypeSystem.Taxonomy
{
    [GraphQLIgnore]
    public interface IEnumTypesContainer : IEnumTypesContainerDefinition
    {
        [GraphQLIgnore]
        [NotNull]
        [ItemNotNull]
        new IEnumerable<EnumType> GetEnums();

        [GraphQLIgnore]
        [NotNull]
        [ItemNotNull]
        IReadOnlyList<EnumType> Enums { get; }
    }
}