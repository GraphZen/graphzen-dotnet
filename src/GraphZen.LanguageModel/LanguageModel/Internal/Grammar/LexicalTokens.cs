// Copyright (c) GraphZen LLC. All rights reserved.
// Licensed under the GraphZen Community License. See the LICENSE file in the project root for license information.

using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using GraphZen.Infrastructure;
using JetBrains.Annotations;
using Superpower;
using Superpower.Model;
using Superpower.Parsers;

#nullable disable


namespace GraphZen.LanguageModel.Internal.Grammar
{
    internal static partial class Grammar
    {
        internal static TokenListParser<TokenKind, NameSyntax> Name { get; } =
            Token.EqualTo(TokenKind.Name).Select(t => new NameSyntax(t.ToStringValue(), t.Span.ToLocation()))
                .Named("name");

        private static TokenListParser<TokenKind, Token<TokenKind>> Comment { get; } =
            Token.EqualTo(TokenKind.Comment).Named("comment");


        private static TokenListParser<TokenKind, PunctuatorSyntax> Bang { get; } =
            Punctuator(TokenKind.Bang);

        private static TokenListParser<TokenKind, PunctuatorSyntax> DollarSign { get; } =
            Punctuator(TokenKind.Dollar);

        private static TokenListParser<TokenKind, PunctuatorSyntax> LeftParen { get; } =
            Punctuator(TokenKind.LeftParen);

        private static TokenListParser<TokenKind, PunctuatorSyntax> RightParen { get; } =
            Punctuator(TokenKind.RightParen);

        private static TokenListParser<TokenKind, PunctuatorSyntax> Spread { get; } =
            Punctuator(TokenKind.Spread);

        private static TokenListParser<TokenKind, PunctuatorSyntax> Colon { get; } =
            Punctuator(TokenKind.Colon);

        private static TokenListParser<TokenKind, PunctuatorSyntax> Assignment { get; } =
            Punctuator(TokenKind.Assignment);

        private static TokenListParser<TokenKind, PunctuatorSyntax> AtSymbol { get; } =
            Punctuator(TokenKind.At);

        private static TokenListParser<TokenKind, PunctuatorSyntax> LeftBracket { get; } =
            Punctuator(TokenKind.LeftBracket);

        private static TokenListParser<TokenKind, PunctuatorSyntax> RightBracket { get; } =
            Punctuator(TokenKind.RightBracket);

        private static TokenListParser<TokenKind, PunctuatorSyntax> LeftBrace { get; } =
            Punctuator(TokenKind.LeftBrace);

        private static TokenListParser<TokenKind, PunctuatorSyntax> RightBrace { get; } =
            Punctuator(TokenKind.RightBrace);

        private static TokenListParser<TokenKind, PunctuatorSyntax> Pipe { get; } =
            Punctuator(TokenKind.Pipe);

        private static TokenListParser<TokenKind, Unit> Ampersand { get; } =
            Token.EqualTo(TokenKind.Ampersand).Value(Unit.Value);

        private static TokenListParser<TokenKind, NameSyntax> Keyword(string name)
        {
            return Token.EqualToValue(TokenKind.Name, name)
                .Select(t => new NameSyntax(t.ToStringValue(), t.Span.ToLocation()))
                .Named($"keyword `{name}`");
        }


        private static TokenListParser<TokenKind, NameSyntax> KeywordIn(IEnumerable<string> names)
        {
            return input =>
            {
                foreach (var name in names)
                {
                    // ReSharper disable once PossibleNullReferenceException
                    var result = Keyword(name).Try()(input);
                    if (result.HasValue) return result;
                }

                return TokenListParserResult.Empty<TokenKind, NameSyntax>(input);
            };
        }

        private static TokenListParser<TokenKind, PunctuatorSyntax> Punctuator(TokenKind tokenKind)
        {
            return Token
                .EqualTo(tokenKind)
                .Select(t => new PunctuatorSyntax(t.Span.ToLocation()));
        }
    }
}