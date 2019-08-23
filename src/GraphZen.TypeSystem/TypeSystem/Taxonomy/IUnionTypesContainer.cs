using System.Collections.Generic;
using GraphZen.Infrastructure;

namespace GraphZen.TypeSystem.Taxonomy
{
    [GraphQLIgnore]
    public interface IUnionTypesContainer : IUnionTypesContainerDefinition
    {
        [GraphQLIgnore]
        [NotNull]
        [ItemNotNull]
        new IEnumerable<UnionType> GetUnions();
        [GraphQLIgnore]
        [NotNull]
        [ItemNotNull]
        IReadOnlyList<UnionType> Unions { get; }

    }
}