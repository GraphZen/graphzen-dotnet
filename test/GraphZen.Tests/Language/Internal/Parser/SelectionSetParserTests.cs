// Copyright (c) GraphZen LLC. All rights reserved.
// Licensed under the GraphZen Community License. See the LICENSE file in the project root for license information.

using GraphZen.Infrastructure;
using GraphZen.LanguageModel;
using GraphZen.LanguageModel.Internal;
using GraphZen.LanguageModel.Internal.Grammar;
using JetBrains.Annotations;
using Superpower;
using Xunit;
using static GraphZen.LanguageModel.SyntaxFactory;

namespace GraphZen.Language.Internal
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
            var testResult = Grammar.SelectionSet(tokens);
            var expectedResultValue = SelectionSet(
                Field(Name("foo")),
                FragmentSpread(Name("barFields")),
                new InlineFragmentSyntax(SelectionSet(Field(Name("baz"))),
                    NamedType(Name("User"))),
                new FieldSyntax(Name("address"),
                    SelectionSet(Field(Name("line1"))))
            );
            Assert.Equal(expectedResultValue, testResult.Value);
        }

        [Fact]
        public void SelectionSetWithField()
        {
            var tokens = _sut.Tokenize("{foo}");
            var testResult = Grammar.SelectionSet(tokens);
            var expectedResultValue = SelectionSet(Field(Name("foo")));
            Assert.Equal(expectedResultValue, testResult.Value);
        }

        [Fact]
        public void SelectionSetWithMultipleFields()
        {
            var tokens = _sut.Tokenize("{foo bar}");
            var testResult = Grammar.SelectionSet(tokens);
            var expectedResultValue = SelectionSet(
                Field(Name("foo")),
                Field(Name("bar"))
            );
            Assert.Equal(expectedResultValue, testResult.Value);
        }

        [Fact]
        public void SelectionSetWithMultipleFieldsWithCommas()
        {
            var tokens = _sut.Tokenize("{foo,bar}");
            var testResult = Grammar.SelectionSet(tokens);
            var expectedResultValue = SelectionSet(
                Field(Name("foo")),
                Field(Name("bar"))
            );
            Assert.Equal(expectedResultValue, testResult.Value);
        }
    }
}