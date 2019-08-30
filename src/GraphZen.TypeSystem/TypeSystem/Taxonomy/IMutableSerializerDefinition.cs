using System.Diagnostics.CodeAnalysis;
using GraphZen.Infrastructure;
using GraphZen.TypeSystem.Internal;
using JetBrains.Annotations;

namespace GraphZen.TypeSystem.Taxonomy
{
    public interface IMutableSerializerDefinition : ISerializerDefinition
    {
        ConfigurationSource? GetSerializerConfigurationSource();
        bool SetSerializer(LeafSerializer<object>? serializer, ConfigurationSource configurationSource);
    }
}