using System.Diagnostics.CodeAnalysis;
using GraphZen.Infrastructure;
using GraphZen.TypeSystem.Internal;
using JetBrains.Annotations;

namespace GraphZen.TypeSystem.Taxonomy
{
    public interface IMutableRemovableNamed : IMutableNamed
    {
        bool RemoveName(ConfigurationSource configurationSource);
    }
}