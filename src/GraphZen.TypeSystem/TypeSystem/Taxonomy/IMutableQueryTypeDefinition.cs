using System.Diagnostics.CodeAnalysis;
using GraphZen.Infrastructure;
using GraphZen.TypeSystem.Internal;
using JetBrains.Annotations;

namespace GraphZen.TypeSystem.Taxonomy
{
    public interface IMutableQueryTypeDefinition : IQueryTypeDefinition
    {
        new ObjectTypeDefinition? QueryType { get; }
        bool SetQueryType(ObjectTypeDefinition? type, ConfigurationSource configurationSource);
        ConfigurationSource? GetQueryTypeConfigurationSource();
    }
}