#nullable disable
using System.Collections.Generic;
using GraphZen.Infrastructure;

namespace GraphZen.TypeSystem.Taxonomy
{
    [GraphQLIgnore]
    public interface IMutableObjectTypesContainerDefinition : IObjectTypesContainerDefinition
    {
        [GraphQLIgnore]
        [NotNull]
        [ItemNotNull]
        new IEnumerable<ObjectTypeDefinition> GetObjects();
    }
}