using System.Diagnostics.CodeAnalysis;
using GraphZen.Infrastructure;
using GraphZen.LanguageModel;
using GraphZen.TypeSystem.Internal;
using JetBrains.Annotations;

namespace GraphZen.TypeSystem.Taxonomy
{
    public interface IMutableLiteralParserDefinition : ILiteralParserDefinition
    {
        ConfigurationSource? GetLiteralParserConfigurationSource();

        bool SetLiteralParser(LeafLiteralParser<object, ValueSyntax>? literalParser,
            ConfigurationSource configurationSource);
    }
}