#nullable disable
// Copyright (c) GraphZen LLC. All rights reserved.
// Licensed under the GraphZen Community License. See the LICENSE file in the project root for license information.

using GraphZen.Infrastructure;
using Superpower;
using Xunit;

namespace GraphZen.LanguageModel.Internal.Parser
{
    [NoReorder]
    public class FragmentParserTests
    {
        private readonly Tokenizer<TokenKind> _sut = SuperPowerTokenizer.Instance;

        [Fact]
        public void FragmentSpreadSimple()
        {
            var source = "... address";
            var tokens = _sut.Tokenize(source);
            var test = Grammar.Grammar.FragmentSpread(tokens);
            var expectedValue = SyntaxFactory.FragmentSpread(SyntaxFactory.Name("address"));
            Assert.Equal(expectedValue, test.Value);
        }


        [Fact]
        public void FragmentSpreadWithDirectives()
        {
            var source = "... address @include";
            var tokens = _sut.Tokenize(source);
            var test = Grammar.Grammar.FragmentSpread(tokens);
            var expectedValue = new FragmentSpreadSyntax(SyntaxFactory.Name("address"),
                new[]
                {
                    SyntaxFactory.Directive(SyntaxFactory.Name("include"))
                });
            Assert.Equal(expectedValue, test.Value);
        }

        [Fact]
        public void InlineFragmentFull()
        {
            var source = " ... on Bar @include(times: 1) {foo}";
            var tokens = _sut.Tokenize(source);
            var test = Grammar.Grammar.InlineFragment(tokens);
            var expectedValue = new InlineFragmentSyntax(
                SyntaxFactory.SelectionSet(SyntaxFactory.Field(SyntaxFactory.Name("foo"))),
                SyntaxFactory.NamedType(SyntaxFactory.Name("Bar")),
                new[]
                {
                    new DirectiveSyntax(SyntaxFactory.Name("include"), new[]
                    {
                        SyntaxFactory.Argument(SyntaxFactory.Name("times"), SyntaxFactory.IntValue(1))
                    })
                });
            Assert.Equal(expectedValue, test.Value);
        }


        [Fact]
        public void InlineFragmentFuller()
        {
            var source = @"
  {
    alias: field1(first: 10, after:$foo) @include(if: $foo) {
      id,
      ...frag
    },
    alias: field1(first: 10, after:$foo) @include(if: $foo) {
      id,
      ...frag
    }
  }
";
            var tokens = _sut.Tokenize(source);
            var test = Grammar.Grammar.SelectionSet(tokens);
            test.ThrowOnParserError();
        }

        [Fact]
        public void InlineFragmentSimple()
        {
            var source = "... {foo}";
            var tokens = _sut.Tokenize(source);
            var test = Grammar.Grammar.InlineFragment(tokens);
            var expectedValue =
                new InlineFragmentSyntax(SyntaxFactory.SelectionSet(SyntaxFactory.Field(SyntaxFactory.Name("foo"))));
            Assert.Equal(expectedValue, test.Value);
        }

        [Fact]
        public void SimpleFragmentDefinition()
        {
            var source = @"
fragment address on User @directive {
    line1
}";
            var tokens = _sut.Tokenize(source);
            var test = Grammar.Grammar.FragmentDefinition(tokens);
            var expectedValue = new FragmentDefinitionSyntax(SyntaxFactory.Name("address"),
                SyntaxFactory.NamedType(SyntaxFactory.Name("User")),
                SyntaxFactory.SelectionSet(SyntaxFactory.Field(SyntaxFactory.Name("line1"))), new[]
                {
                    SyntaxFactory.Directive(SyntaxFactory.Name("directive"))
                });
            Assert.Equal(expectedValue, test.Value);
        }
    }
}