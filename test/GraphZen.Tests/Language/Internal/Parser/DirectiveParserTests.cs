// Copyright (c) GraphZen LLC. All rights reserved.
// Licensed under the GraphZen Community License. See the LICENSE file in the project root for license information.

using GraphZen.Infrastructure;
using JetBrains.Annotations;
using Superpower;
using Xunit;
using static GraphZen.Language.SyntaxFactory;

namespace GraphZen.Language.Internal
{
    public class DirectiveParserTests
    {
        private readonly Tokenizer<TokenKind> _tokenizer = SuperPowerTokenizer.Instance;

        [Fact]
        public void NameOnly()
        {
            var tokens = _tokenizer.Tokenize("@skip");
            var testResult = Grammar.Directives(tokens);
            var expectedValue = new[]
            {
                Directive(Name("skip"))
            };
            Assert.Equal(expectedValue, testResult.Value);
        }

        [Fact]
        public void NameWithArguments()
        {
            var tokens = _tokenizer.Tokenize("@skip(count: 1)");
            var testResult = Grammar.Directives(tokens);
            var expectedValue =
                new[]
                {
                    new DirectiveSyntax(Name("skip"),
                        new[]
                        {
                            Argument(Name("count"), IntValue(1))
                        })
                };
            Assert.Equal(expectedValue, testResult.Value);
        }
    }
}