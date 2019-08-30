using System.Diagnostics.CodeAnalysis;
using GraphZen.Infrastructure;
using GraphZen.TypeSystem.Internal;
using JetBrains.Annotations;

namespace GraphZen.TypeSystem.Taxonomy
{
    public interface IMutableValueParserDefinition : IValueParserDefinition
    {
        ConfigurationSource? GetValueParserConfigurationSource();
        bool SetValueParser(LeafValueParser<object>? valueParser, ConfigurationSource configurationSource);
    }
}