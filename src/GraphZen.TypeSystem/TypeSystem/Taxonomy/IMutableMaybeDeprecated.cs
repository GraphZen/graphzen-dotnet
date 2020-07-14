using System.Diagnostics.CodeAnalysis;
using GraphZen.Infrastructure;
using GraphZen.TypeSystem.Internal;
using JetBrains.Annotations;

namespace GraphZen.TypeSystem
{
    [GraphQLIgnore]
    public interface IMutableMaybeDeprecated : IBuildableMaybeDeprecated
    {
        bool MarkAsDeprecated(ConfigurationSource configurationSource);
        bool MarkAsDeprecated(string reason, ConfigurationSource configurationSource);
        bool RemoveDeprecation(ConfigurationSource configurationSource);
    }
}