using System.Collections.Generic;
using GraphZen.Infrastructure;
using GraphZen.TypeSystem.Internal;

namespace GraphZen.TypeSystem.Taxonomy
{
    [GraphQLIgnore]
    public interface IMutableMemberTypesContainerDefinition : IMemberTypesContainerDefinition
    {
        [NotNull]
        [ItemNotNull]
        new IEnumerable<ObjectTypeDefinition> GetMemberTypes();
        ConfigurationSource? FindIgnoredMemberTypeConfigurationSource([NotNull] string name);
    }
}