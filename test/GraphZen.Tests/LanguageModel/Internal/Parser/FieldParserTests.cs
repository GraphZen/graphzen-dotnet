// Copyright (c) GraphZen LLC. All rights reserved.
// Licensed under the GraphZen Community License. See the LICENSE file in the project root for license information.

using System.Diagnostics.CodeAnalysis;
using GraphZen.Infrastructure;
using GraphZen.LanguageModel;
using GraphZen.LanguageModel.Internal;
using JetBrains.Annotations;
using Superpower;
using Xunit;
using static GraphZen.LanguageModel.SyntaxFactory;

#nullable disable


namespace GraphZen.Tests.LanguageModel.Internal.Parser
{
    public class FieldParserTests
    {
        private readonly Tokenizer<TokenKind> _sut = SuperPowerTokenizer.Instance;

        [Fact]
        public void AliasedField()
        {
            var source = "bar: foo";
            var tokens = _sut.Tokenize(source);
            var test = Grammar.Field(tokens);
            var expectedValue = new FieldSyntax(Name("foo"), Name("bar"));
            Assert.True(expectedValue.Equals(test.Value));
        }

        [Fact]
        public void FieldWithArguments()
        {
            var source = @"bar: foo(booleanParam: false, stringParam: ""foo"", intParam: -1, floatParam: 4.123e-3)";
            var tokens = _sut.Tokenize(source);
            var test = Grammar.Field(tokens);
            var expectedValue = new FieldSyntax(Name("foo"),
                arguments: new[]
                {
                    Argument(Name("booleanParam"), BooleanValue(false)),
                    Argument(Name("stringParam"), StringValue("foo")),
                    Argument(Name("intParam"), IntValue(-1)),
                    Argument(Name("floatParam"), FloatValue("4.123e-3"))
                },
                alias: Name("bar"));
            Assert.True(expectedValue.Equals(test.Value));
        }

        [Fact]
        public void NameOnlyField()
        {
            var source = "foo";
            var tokens = _sut.Tokenize(source);
            var test = Grammar.Field(tokens);
            var expectedValue = Field(Name("foo"));
            Assert.True(expectedValue.Equals(test.Value));
        }
    }
}