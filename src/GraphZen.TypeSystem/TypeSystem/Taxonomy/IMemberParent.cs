using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using GraphZen.Infrastructure;
using JetBrains.Annotations;

namespace GraphZen.TypeSystem.Taxonomy
{
    [GraphQLIgnore]
    public interface IMemberParent : IMemberParentDefinition, IMember
    {
        new IEnumerable<IMember> Children();
    }
}