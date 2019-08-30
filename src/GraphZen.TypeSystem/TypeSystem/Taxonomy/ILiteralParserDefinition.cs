using System.Diagnostics.CodeAnalysis;
using GraphZen.Infrastructure;
using GraphZen.LanguageModel;
using JetBrains.Annotations;

namespace GraphZen.TypeSystem.Taxonomy
{
    public interface ILiteralParserDefinition
    {
        LeafLiteralParser<object, ValueSyntax>? LiteralParser { get; }
    }
}