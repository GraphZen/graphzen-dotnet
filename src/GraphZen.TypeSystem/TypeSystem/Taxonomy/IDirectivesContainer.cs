using System.Collections.Generic;
using GraphZen.Infrastructure;

namespace GraphZen.TypeSystem.Taxonomy
{
    [GraphQLIgnore]
    public interface IDirectivesContainer : IDirectivesContainerDefinition
    {
        [GraphQLIgnore]
        [NotNull]
        [ItemNotNull]
        new IEnumerable<Directive> GetDirectives();

        [GraphQLIgnore]
        [NotNull]
        [ItemNotNull]
        IReadOnlyList<Directive> Directives { get; }

    }
}