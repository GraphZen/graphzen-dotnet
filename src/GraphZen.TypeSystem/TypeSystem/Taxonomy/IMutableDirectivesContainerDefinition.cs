#nullable disable
using System.Collections.Generic;
using GraphZen.Infrastructure;

namespace GraphZen.TypeSystem.Taxonomy
{
    [GraphQLIgnore]
    public interface IMutableDirectivesContainerDefinition : IDirectivesContainerDefinition
    {
        [GraphQLIgnore]
        [NotNull]
        [ItemNotNull]
        new IEnumerable<DirectiveDefinition> GetDirectives();
    }
}