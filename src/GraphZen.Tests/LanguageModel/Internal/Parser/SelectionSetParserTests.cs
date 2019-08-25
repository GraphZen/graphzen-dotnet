// Copyright (c) GraphZen LLC. All rights reserved.
// Licensed under the GraphZen Community License. See the LICENSE file in the project root for license information.

using System.Diagnostics.CodeAnalysis;
using GraphZen.Infrastructure;
using JetBrains.Annotations;
using Superpower;
using Xunit;
#nullable disable


namespace GraphZen.LanguageModel.Internal.Parser
{
    public class SelectionSetParserTests
    {
        private readonly Tokenizer<TokenKind> _sut = SuperPowerTokenizer.Instance;


        [Fact]
        public void SelectionSetWithAllSelectionTypes()
        {
            var source = @"
{
    foo
    ...barFields,
    ... on User {
        baz
     }
    address {
        line1
    }
}
";

            var tokens = _sut.Tokenize(source);
            var testResult = Grammar.Grammar.SelectionSet(tokens);
            var expectedResultValue = SyntaxFactory.SelectionSet(
                SyntaxFactory.Field(SyntaxFactory.Name("foo")),
                SyntaxFactory.FragmentSpread(SyntaxFactory.Name("barFields")),
                new InlineFragmentSyntax(SyntaxFactory.SelectionSet(SyntaxFactory.Field(SyntaxFactory.Name("baz"))),
                    SyntaxFactory.NamedType(SyntaxFactory.Name("User"))),
                new FieldSyntax(SyntaxFactory.Name("address"),
                    SyntaxFactory.SelectionSet(SyntaxFactory.Field(SyntaxFactory.Name("line1"))))
            );
            Assert.Equal(expectedResultValue, testResult.Value);
        }

        [Fact]
        public void SelectionSetWithField()
        {
            var tokens = _sut.Tokenize("{foo}");
            var testResult = Grammar.Grammar.SelectionSet(tokens);
            var expectedResultValue = SyntaxFactory.SelectionSet(SyntaxFactory.Field(SyntaxFactory.Name("foo")));
            Assert.Equal(expectedResultValue, testResult.Value);
        }

        [Fact]
        public void SelectionSetWithMultipleFields()
        {
            var tokens = _sut.Tokenize("{foo bar}");
            var testResult = Grammar.Grammar.SelectionSet(tokens);
            var expectedResultValue = SyntaxFactory.SelectionSet(
                SyntaxFactory.Field(SyntaxFactory.Name("foo")),
                SyntaxFactory.Field(SyntaxFactory.Name("bar"))
            );
            Assert.Equal(expectedResultValue, testResult.Value);
        }

        [Fact]
        public void SelectionSetWithMultipleFieldsWithCommas()
        {
            var tokens = _sut.Tokenize("{foo,bar}");
            var testResult = Grammar.Grammar.SelectionSet(tokens);
            var expectedResultValue = SyntaxFactory.SelectionSet(
                SyntaxFactory.Field(SyntaxFactory.Name("foo")),
                SyntaxFactory.Field(SyntaxFactory.Name("bar"))
            );
            Assert.Equal(expectedResultValue, testResult.Value);
        }
    }
}