using System.Diagnostics.CodeAnalysis;
using GraphZen.Infrastructure;
using JetBrains.Annotations;
using Superpower;

namespace GraphZen.LanguageModel.Internal
{
    public static class TokenListParserExtensions
    {
        public static TokenListParser<TokenKind, T?> OptionalOrNull<T>(this TokenListParser<TokenKind, T> parser) where T : class =>
            parser.Select(_ => (T?)_).OptionalOrDefault();
    }
}
