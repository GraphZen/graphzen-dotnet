// Copyright (c) GraphZen LLC. All rights reserved.
// Licensed under the GraphZen Community License. See the LICENSE file in the project root for license information.

using FluentAssertions;
using GraphZen.Infrastructure;

using Superpower;
using Xunit;

namespace GraphZen.LanguageModel.Internal.Parser
{
    public class VariableParserTests : ParserTestBase
    {
        private readonly Tokenizer<TokenKind> _sut = SuperPowerTokenizer.Instance;

        [Fact]
        public void ParseVariable()
        {
            ParseValue("$name").Should().Be(SyntaxFactory.Variable(SyntaxFactory.Name("name")));
        }

        [Fact(Skip = "Seems identical to one above - should revisit")]
        public void VariableDefinition()
        {
            var tokens = _sut.Tokenize("$name");
            var test = Grammar.Grammar.Variable(tokens);
            var expectedValue = SyntaxFactory.Variable(SyntaxFactory.Name("name"));
            Assert.Equal(expectedValue, test.Value);
        }
    }
}