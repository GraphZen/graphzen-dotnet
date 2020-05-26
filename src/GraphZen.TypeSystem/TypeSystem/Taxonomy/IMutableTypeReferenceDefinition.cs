using System.Diagnostics.CodeAnalysis;
using GraphZen.Infrastructure;
using GraphZen.TypeSystem.Internal;
using JetBrains.Annotations;

namespace GraphZen.TypeSystem.Taxonomy
{
    public interface IMutableTypeReferenceDefinition : ITypeReferenceDefinition
    {
        ConfigurationSource GetTypeReferenceConfigurationSource();
        new TypeReference TypeReference { get; }
        bool SetTypeReference(TypeReference reference, ConfigurationSource configurationSource);
    }
}