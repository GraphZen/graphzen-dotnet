#nullable disable
using System.Collections.Generic;
using GraphZen.Infrastructure;

namespace GraphZen.TypeSystem.Taxonomy
{
    [GraphQLIgnore]
    public interface IScalarTypesContainer : IScalarTypesContainerDefinition
    {
        [GraphQLIgnore]
        [NotNull]
        [ItemNotNull]
        new IEnumerable<ScalarType> GetScalars();

        [GraphQLIgnore]
        [NotNull]
        [ItemNotNull]
        IReadOnlyList<ScalarType> Scalars { get; }
    }
}