using System;
using System.Collections.Generic;
using System.Text;
using Superpower;

namespace GraphZen.LanguageModel.Internal.Extensions.Superpower
{
    public static class TokenListParserExtensions
    {
        public static TokenListParser<TokenKind, T?> OptionalOrNull<T>(this TokenListParser<TokenKind, T> parser) where T : class =>
            parser.Select(_ => (T?) _).OptionalOrDefault();
    }
}
