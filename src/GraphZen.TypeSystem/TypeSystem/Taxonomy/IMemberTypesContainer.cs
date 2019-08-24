#nullable disable
using System.Collections.Generic;
using GraphZen.Infrastructure;

namespace GraphZen.TypeSystem.Taxonomy
{
    [GraphQLIgnore]
    public interface IMemberTypesContainer : IMemberTypesContainerDefinition
    {
        [NotNull]
        [ItemNotNull]
        new IEnumerable<ObjectType> GetMemberTypes();

        [NotNull]
        [ItemNotNull]
        IReadOnlyList<ObjectType> MemberTypes { get; }

        [NotNull]
        IReadOnlyDictionary<string, ObjectType> MemberTypesMap { get; }
    }
}