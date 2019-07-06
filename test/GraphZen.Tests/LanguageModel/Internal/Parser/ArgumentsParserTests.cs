// Copyright (c) GraphZen LLC. All rights reserved.
// Licensed under the GraphZen Community License. See the LICENSE file in the project root for license information.

using GraphZen.Infrastructure;
using Superpower;
using Xunit;

namespace GraphZen.LanguageModel.Internal.Parser
{
    public class ArgumentsParserTests
    {
        private readonly Tokenizer<TokenKind> _sut = SuperPowerTokenizer.Instance;


        [Fact]
        public void MultipleArguments()
        {
            var source = @"(booleanParam: false, stringParam: ""foo"", intParam: -1, floatParam: 4.123e-3)";
            var tokens = _sut.Tokenize(source);
            var test = Grammar.Grammar.Arguments(tokens);
            var expectedValue = new[]
            {
                SyntaxFactory.Argument(SyntaxFactory.Name("booleanParam"), SyntaxFactory.BooleanValue(false)),
                SyntaxFactory.Argument(SyntaxFactory.Name("stringParam"), SyntaxFactory.StringValue("foo")),
                SyntaxFactory.Argument(SyntaxFactory.Name("intParam"), SyntaxFactory.IntValue(-1)),
                SyntaxFactory.Argument(SyntaxFactory.Name("floatParam"), SyntaxFactory.FloatValue("4.123e-3"))
            };
            Assert.Equal(expectedValue, test.Value);
        }
    }
}