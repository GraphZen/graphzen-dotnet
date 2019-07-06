// Copyright (c) GraphZen LLC. All rights reserved.
// Licensed under the GraphZen Community License. See the LICENSE file in the project root for license information.

using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using GraphZen.Infrastructure;
using Superpower;
using Superpower.Model;
using Superpower.Parsers;
using Superpower.Tokenizers;

namespace GraphZen.LanguageModel.Internal
{
    internal static class SuperPowerTokenizer
    {
        public static TextParser<Unit> FloatValueToken
        {
            get
            {
                var exp = Character.EqualToIgnoreCase('e').IgnoreThen(Numerics.Integer);
                var withoutDecimal = Numerics.Integer.IgnoreThen(exp).Value(Unit.Value);

                var withDecimal = Numerics.Integer.IgnoreThen(Character.EqualTo('.')).IgnoreThen(Numerics.Natural)
                    .IgnoreThen(exp.OptionalOrDefault()).Value(Unit.Value);

                return withDecimal.Try().Or(withoutDecimal);
            }
        }

        public static TextParser<Unit> BlockStringToken
        {
            get
            {
                var delimmiter = Span.EqualTo("\"\"\"").Value(Unit.Value);
                return i =>
                {
                    Debug.Assert(delimmiter != null, nameof(delimmiter) + " != null");
                    var begin = delimmiter(i);
                    if (!begin.HasValue)
                    {
                        return begin;
                    }

                    var content = begin.Remainder;

                    while (!content.IsAtEnd)
                    {
                        // ReSharper disable once PossibleNullReferenceException
                        content = Span.EqualTo("\\\"\"\"").Value(Unit.Value).Try()(content).Remainder;
                        var end = delimmiter(content);
                        if (end.HasValue)
                        {
                            return end;
                        }

                        content = content.ConsumeChar().Remainder;
                    }

                    return delimmiter(content); // Will fail, because we're at the end-of-input.
                };
            }
        }

        [NotNull]
        [SuppressMessage("ReSharper", "AssignNullToNotNullAttribute")]
        [SuppressMessage("ReSharper", "PossibleNullReferenceException")]
        public static Tokenizer<TokenKind> Instance { get; } = new TokenizerBuilder<TokenKind>()
            .Ignore(Span.WhiteSpace)
            .Ignore(Character.EqualTo(','))
            .Match(Comment.ShellStyle, TokenKind.Comment)
            .Match(Identifier.CStyle, TokenKind.Name)
            .Match(BlockStringToken, TokenKind.BlockString)
            .Match(QuotedString.CStyle, TokenKind.String)
            .Match(FloatValueToken, TokenKind.FloatValue)
            .Match(Numerics.IntegerInt64, TokenKind.IntValue)
            .Match(Character.EqualTo('!'), TokenKind.Bang)
            .Match(Character.EqualTo('$'), TokenKind.Dollar)
            .Match(Character.EqualTo('&'), TokenKind.Ampersand)
            .Match(Character.EqualTo('('), TokenKind.LeftParen)
            .Match(Character.EqualTo(')'), TokenKind.RightParen)
            .Match(Character.EqualTo(':'), TokenKind.Colon)
            .Match(Character.EqualTo('='), TokenKind.Assignment)
            .Match(Character.EqualTo('@'), TokenKind.At)
            .Match(Character.EqualTo('['), TokenKind.LeftBracket)
            .Match(Character.EqualTo(']'), TokenKind.RightBracket)
            .Match(Character.EqualTo('{'), TokenKind.LeftBrace)
            .Match(Character.EqualTo('|'), TokenKind.Pipe)
            .Match(Character.EqualTo('}'), TokenKind.RightBrace)
            .Match(Span.EqualTo("..."), TokenKind.Spread)
            .Build();
    }
}