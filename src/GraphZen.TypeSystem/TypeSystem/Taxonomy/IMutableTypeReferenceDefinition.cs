using System.Diagnostics.CodeAnalysis;
using GraphZen.Infrastructure;
using GraphZen.LanguageModel;
using GraphZen.TypeSystem.Internal;
using JetBrains.Annotations;

namespace GraphZen.TypeSystem.Taxonomy
{
    public interface IMutableTypeReferenceDefinition : ITypeReferenceDefinition, IMutableDefinition
    {
        ConfigurationSource GetTypeReferenceConfigurationSource();
        new TypeReference TypeReference { get; }
        // bool SetTypeReference(TypeReference type, ConfigurationSource configurationSource);
        bool SetTypeReference(TypeIdentity identity, TypeSyntax syntax, ConfigurationSource configurationSource);
        bool SetTypeReference(string type, ConfigurationSource configurationSource);
    }
}