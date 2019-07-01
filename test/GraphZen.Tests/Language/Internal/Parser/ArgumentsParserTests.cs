// Copyright (c) GraphZen LLC. All rights reserved.
// Licensed under the GraphZen Community License. See the LICENSE file in the project root for license information.

using GraphZen.Infrastructure;
using JetBrains.Annotations;
using Superpower;
using Xunit;
using static GraphZen.Language.SyntaxFactory;

namespace GraphZen.Language.Internal
{
    public class ArgumentsParserTests
    {
        private readonly Tokenizer<TokenKind> _sut = SuperPowerTokenizer.Instance;


        [Fact]
        public void MultipleArguments()
        {
            var source = @"(booleanParam: false, stringParam: ""foo"", intParam: -1, floatParam: 4.123e-3)";
            var tokens = _sut.Tokenize(source);
            var test = Grammar.Arguments(tokens);
            var expectedValue = new[]
            {
                Argument(Name("booleanParam"), BooleanValue(false)),
                Argument(Name("stringParam"), StringValue("foo")),
                Argument(Name("intParam"), IntValue(-1)),
                Argument(Name("floatParam"), FloatValue("4.123e-3"))
            };
            Assert.Equal(expectedValue, test.Value);
        }
    }
}