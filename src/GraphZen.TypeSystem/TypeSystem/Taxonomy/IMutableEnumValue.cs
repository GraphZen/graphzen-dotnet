using System.Diagnostics.CodeAnalysis;
using GraphZen.Infrastructure;
using JetBrains.Annotations;

namespace GraphZen.TypeSystem
{
    [GraphQLIgnore]
    // ReSharper disable once PossibleInterfaceMemberAmbiguity
    public interface IMutableEnumValue : IBuildableEnumValue, IMutableDirectives, IMutableName, IMutableDescription, IMutableMaybeDeprecated
    {
         new IEnumValueBuilder Builder { get; }
        // Set custom value?
    }
}