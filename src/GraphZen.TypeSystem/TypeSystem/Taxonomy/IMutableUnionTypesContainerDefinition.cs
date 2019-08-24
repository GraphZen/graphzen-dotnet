#nullable disable
using System.Collections.Generic;
using GraphZen.Infrastructure;

namespace GraphZen.TypeSystem.Taxonomy
{
    [GraphQLIgnore]
    public interface IMutableUnionTypesContainerDefinition : IUnionTypesContainerDefinition
    {
        [GraphQLIgnore]
        [NotNull]
        [ItemNotNull]
        new IEnumerable<UnionTypeDefinition> GetUnions();
    }
}