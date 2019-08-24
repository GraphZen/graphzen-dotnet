// Copyright (c) GraphZen LLC. All rights reserved.
// Licensed under the GraphZen Community License. See the LICENSE file in the project root for license information.
using JetBrains.Annotations;
#nullable disable


using System.Linq;
using GraphZen.Infrastructure;
using GraphZen.LanguageModel.Internal;
using Superpower;
using Xunit;

namespace GraphZen.LanguageModel
{
    public class PunctuatorsSpec
    {
        private readonly Tokenizer<TokenKind> _sut = SuperPowerTokenizer.Instance;

        [Theory]
        [InlineData("!", TokenKind.Bang, "!")]
        [InlineData("$", TokenKind.Dollar, "$")]
        [InlineData("(", TokenKind.LeftParen, "(")]
        [InlineData(")", TokenKind.RightParen, ")")]
        [InlineData("...", TokenKind.Spread, "...")]
        [InlineData(":", TokenKind.Colon, ":")]
        [InlineData("=", TokenKind.Assignment, "=")]
        [InlineData("@", TokenKind.At, "@")]
        [InlineData("[", TokenKind.LeftBracket, "[")]
        [InlineData("]", TokenKind.RightBracket, "]")]
        [InlineData("{", TokenKind.LeftBrace, "{")]
        [InlineData("|", TokenKind.Pipe, "|")]
        [InlineData("}", TokenKind.RightBrace, "}")]
        internal void Punctuators(string input, TokenKind kind, string stringValue)
        {
            var result = _sut.Tokenize(input).First();
            Assert.Equal(kind, result.Kind);
            Assert.Equal(stringValue, result.ToStringValue());
        }
    }
}