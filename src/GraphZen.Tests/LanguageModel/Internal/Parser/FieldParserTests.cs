#nullable disable
// Copyright (c) GraphZen LLC. All rights reserved.
// Licensed under the GraphZen Community License. See the LICENSE file in the project root for license information.

using GraphZen.Infrastructure;
using Superpower;
using Xunit;

namespace GraphZen.LanguageModel.Internal.Parser
{
    public class FieldParserTests
    {
        private readonly Tokenizer<TokenKind> _sut = SuperPowerTokenizer.Instance;

        [Fact]
        public void AliasedField()
        {
            var source = "bar: foo";
            var tokens = _sut.Tokenize(source);
            var test = Grammar.Grammar.Field(tokens);
            var expectedValue = new FieldSyntax(SyntaxFactory.Name("foo"), SyntaxFactory.Name("bar"));
            Assert.True(expectedValue.Equals(test.Value));
        }

        [Fact]
        public void FieldWithArguments()
        {
            var source = @"bar: foo(booleanParam: false, stringParam: ""foo"", intParam: -1, floatParam: 4.123e-3)";
            var tokens = _sut.Tokenize(source);
            var test = Grammar.Grammar.Field(tokens);
            var expectedValue = new FieldSyntax(SyntaxFactory.Name("foo"),
                arguments: new[]
                {
                    SyntaxFactory.Argument(SyntaxFactory.Name("booleanParam"), SyntaxFactory.BooleanValue(false)),
                    SyntaxFactory.Argument(SyntaxFactory.Name("stringParam"), SyntaxFactory.StringValue("foo")),
                    SyntaxFactory.Argument(SyntaxFactory.Name("intParam"), SyntaxFactory.IntValue(-1)),
                    SyntaxFactory.Argument(SyntaxFactory.Name("floatParam"), SyntaxFactory.FloatValue("4.123e-3"))
                },
                alias: SyntaxFactory.Name("bar"));
            Assert.True(expectedValue.Equals(test.Value));
        }

        [Fact]
        public void NameOnlyField()
        {
            var source = "foo";
            var tokens = _sut.Tokenize(source);
            var test = Grammar.Grammar.Field(tokens);
            var expectedValue = SyntaxFactory.Field(SyntaxFactory.Name("foo"));
            Assert.True(expectedValue.Equals(test.Value));
        }
    }
}