// Copyright (c) GraphZen LLC. All rights reserved.
// Licensed under the GraphZen Community License. See the LICENSE file in the project root for license information.

using System.Diagnostics.CodeAnalysis;
using GraphZen.Infrastructure;
using JetBrains.Annotations;
using Superpower.Display;

#nullable disable


namespace GraphZen.LanguageModel.Internal
{
    public enum TokenKind
    {
        StartOfFile, // '<SOF>'
        EndOfFile, // '<EOF>'

        [Token(Category = "punctuator", Description = "bang", Example = "!")]
        Bang, // '!'

        [Token(Category = "punctuator", Description = "dollar sign", Example = "$")]
        Dollar, // '$'

        [Token(Category = "punctuator", Description = "left paren", Example = "(")]
        LeftParen, // '('

        [Token(Category = "punctuator", Description = "right paren", Example = ")")]
        RightParen, // ')'

        [Token(Category = "punctuator", Description = "spread", Example = "...")]
        Spread, // '...'

        [Token(Category = "punctuator", Description = "colon", Example = ":")]
        Colon, // ':'

        [Token(Category = "punctuator", Description = "assignment", Example = "=")]
        Assignment, // '='

        [Token(Category = "punctuator", Description = "at symbol", Example = "@")]
        At, // '@'

        [Token(Category = "punctuator", Description = "left bracket", Example = "[")]
        LeftBracket, // '['

        [Token(Category = "punctuator", Description = "right bracket", Example = "]")]
        RightBracket, // ']'

        [Token(Category = "punctuator", Description = "left brace", Example = "{")]
        LeftBrace, // '{'

        [Token(Category = "punctuator", Description = "right brace", Example = "}")]
        RightBrace, // '}'

        [Token(Category = "punctuator", Description = "pipe", Example = "|")]
        Pipe, // '|'

        [Token(Category = "token", Description = "name")]
        Name,

        [Token(Category = "token", Description = "int value", Example = "123")]
        IntValue, // 'IntValue'

        [Token(Category = "token", Description = "float value", Example = "1e50")]
        FloatValue, // 'FloatValue'

        [Token(Category = "token", Description = "string", Example = "\"foo\"")]
        String, // 'StringValue'

        [Token(Category = "token", Description = "block string", Example = "\"foo\"")]
        BlockString, // 'StringValue'

        [Token(Category = "ignored", Description = "comment", Example = "# comment example")]
        Comment,

        [Token(Category = "token", Description = "ampersand", Example = "&")]
        Ampersand
    }
}