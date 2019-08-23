using System.Collections.Generic;
using GraphZen.Infrastructure;

namespace GraphZen.TypeSystem.Taxonomy
{
    [GraphQLIgnore]
    public interface IInputObjectTypesContainer : IInputObjectTypesContainerDefinition
    {
        [GraphQLIgnore]
        [NotNull]
        [ItemNotNull]
        new IEnumerable<InputObjectType> GetInputObjects();

        [GraphQLIgnore]
        [NotNull]
        [ItemNotNull]
        IReadOnlyList<InputObjectType> InputObjects { get; }
    }
}