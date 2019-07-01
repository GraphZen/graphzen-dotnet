// Copyright (c) GraphZen LLC. All rights reserved.
// Licensed under the GraphZen Community License. See the LICENSE file in the project root for license information.

using GraphZen.Infrastructure;
using JetBrains.Annotations;
using Superpower;
using Xunit;
using static GraphZen.Language.SyntaxFactory;

namespace GraphZen.Language.Internal
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
            var test = Grammar.FragmentSpread(tokens);
            var expectedValue = FragmentSpread(Name("address"));
            Assert.Equal(expectedValue, test.Value);
        }


        [Fact]
        public void FragmentSpreadWithDirectives()
        {
            var source = "... address @include";
            var tokens = _sut.Tokenize(source);
            var test = Grammar.FragmentSpread(tokens);
            var expectedValue = new FragmentSpreadSyntax(Name("address"),
                new[]
                {
                    Directive(Name("include"))
                });
            Assert.Equal(expectedValue, test.Value);
        }

        [Fact]
        public void InlineFragmentFull()
        {
            var source = " ... on Bar @include(times: 1) {foo}";
            var tokens = _sut.Tokenize(source);
            var test = Grammar.InlineFragment(tokens);
            var expectedValue = new InlineFragmentSyntax(
                SelectionSet(Field(Name("foo"))),
                NamedType(Name("Bar")),
                new[]
                {
                    new DirectiveSyntax(Name("include"), new[]
                    {
                        Argument(Name("times"), IntValue(1))
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
            var test = Grammar.SelectionSet(tokens);
            test.ThrowOnParserError();
        }

        [Fact]
        public void InlineFragmentSimple()
        {
            var source = "... {foo}";
            var tokens = _sut.Tokenize(source);
            var test = Grammar.InlineFragment(tokens);
            var expectedValue =
                new InlineFragmentSyntax(SelectionSet(Field(Name("foo"))));
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
            var test = Grammar.FragmentDefinition(tokens);
            var expectedValue = new FragmentDefinitionSyntax(Name("address"),
                NamedType(Name("User")),
                SelectionSet(Field(Name("line1"))), new[]
                {
                    Directive(Name("directive"))
                });
            Assert.Equal(expectedValue, test.Value);
        }
    }
}