using System.Diagnostics.CodeAnalysis;
using GraphZen.Infrastructure;
using GraphZen.TypeSystem.Internal;
using JetBrains.Annotations;

namespace GraphZen.TypeSystem.Taxonomy
{
    public interface IMutableTypeReferenceDefinition : ITypeReferenceDefinition, IMutableDefinition
    {
        new TypeReference TypeReference { get; }
        ConfigurationSource GetTypeReferenceConfigurationSource();
    }
}