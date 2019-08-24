#nullable disable
using System.Collections.Generic;
using GraphZen.Infrastructure;

namespace GraphZen.TypeSystem.Taxonomy
{
    [GraphQLIgnore]
    public interface IObjectTypesContainer : IObjectTypesContainerDefinition
    {
        [GraphQLIgnore]
        [NotNull]
        [ItemNotNull]
        new IEnumerable<ObjectType> GetObjects();

        [NotNull]
        [ItemNotNull]
        [GraphQLIgnore]
        IReadOnlyList<ObjectType> Objects { get; }
    }
}