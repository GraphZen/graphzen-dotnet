using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using GraphZen.Infrastructure;
using JetBrains.Annotations;

namespace GraphZen.TypeSystem.Taxonomy
{
    [GraphQLIgnore]
    public interface IMemberParentDefinition : IMemberDefinition
    {
        IEnumerable<IMemberDefinition> Children();
    }
}