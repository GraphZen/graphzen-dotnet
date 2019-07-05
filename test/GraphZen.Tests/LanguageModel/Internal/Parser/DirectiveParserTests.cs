﻿// Copyright (c) GraphZen LLC. All rights reserved.
// Licensed under the GraphZen Community License. See the LICENSE file in the project root for license information.

using GraphZen.Infrastructure;
using Superpower;
using Xunit;

namespace GraphZen.LanguageModel.Internal.Parser
{
    public class DirectiveParserTests
    {
        private readonly Tokenizer<TokenKind> _tokenizer = SuperPowerTokenizer.Instance;

        [Fact]
        public void NameOnly()
        {
            var tokens = _tokenizer.Tokenize("@skip");
            var testResult = Grammar.Grammar.Directives(tokens);
            var expectedValue = new[]
            {
                SyntaxFactory.Directive(SyntaxFactory.Name("skip"))
            };
            Assert.Equal(expectedValue, testResult.Value);
        }

        [Fact]
        public void NameWithArguments()
        {
            var tokens = _tokenizer.Tokenize("@skip(count: 1)");
            var testResult = Grammar.Grammar.Directives(tokens);
            var expectedValue =
                new[]
                {
                    new DirectiveSyntax(SyntaxFactory.Name("skip"),
                        new[]
                        {
                            SyntaxFactory.Argument(SyntaxFactory.Name("count"), SyntaxFactory.IntValue(1))
                        })
                };
            Assert.Equal(expectedValue, testResult.Value);
        }
    }
}