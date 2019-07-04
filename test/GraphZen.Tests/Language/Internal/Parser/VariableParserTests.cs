// Copyright (c) GraphZen LLC. All rights reserved.
// Licensed under the GraphZen Community License. See the LICENSE file in the project root for license information.

using FluentAssertions;
using GraphZen.Infrastructure;
using GraphZen.LanguageModel.Internal;
using GraphZen.LanguageModel.Internal.Grammar;
using JetBrains.Annotations;
using Superpower;
using Xunit;
using static GraphZen.LanguageModel.SyntaxFactory;

namespace GraphZen.Language.Internal
{
    public class VariableParserTests : ParserTestBase
    {
        private readonly Tokenizer<TokenKind> _sut = SuperPowerTokenizer.Instance;

        [Fact]
        public void ParseVariable()
        {
            ParseValue("$name").Should().Be(Variable(Name("name")));
        }

        [Fact(Skip = "Seems identical to one above - should revisit")]
        public void VariableDefinition()
        {
            var tokens = _sut.Tokenize("$name");
            var test = Grammar.Variable(tokens);
            var expectedValue = Variable(Name("name"));
            Assert.Equal(expectedValue, test.Value);
        }
    }
}