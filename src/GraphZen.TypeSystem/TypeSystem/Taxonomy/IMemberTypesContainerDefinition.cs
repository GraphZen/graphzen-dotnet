#nullable disable
using System.Collections.Generic;
using GraphZen.Infrastructure;

namespace GraphZen.TypeSystem.Taxonomy
{
    [GraphQLIgnore]
    public interface IMemberTypesContainerDefinition
    {
        [NotNull]
        [ItemNotNull]
        IEnumerable<IObjectTypeDefinition> GetMemberTypes();
    }
}