// Copyright (c) GraphZen LLC. All rights reserved.
// Licensed under the GraphZen Community License. See the LICENSE file in the project root for license information.

using System.Diagnostics.CodeAnalysis;
using GraphZen.Infrastructure;
using GraphZen.LanguageModel;
using JetBrains.Annotations;
using Xunit;
using static GraphZen.LanguageModel.SyntaxFactory;

#nullable disable


namespace GraphZen.Tests.LanguageModel.Internal.Parser
{
    public class DirectiveDefinitionParsingTests : ParserTestBase
    {
        [Fact]
        public void InlineDirectiveDefinition()
        {
            var result = ParseDocument("directive @skip(if: Boolean!) on FIELD | FRAGMENT_SPREAD | INLINE_FRAGMENT");
            var expected = Document(new DirectiveDefinitionSyntax(
                Name(
                    "skip"), Names("FIELD", "FRAGMENT_SPREAD", "INLINE_FRAGMENT"),
                new[]
                {
                    InputValueDefinition(
                        Name("if"),
                        NonNullType(NamedType(Name("Boolean"))))
                }));
            Assert.Equal(expected, result);
            Assert.Equal(expected, PrintAndParse(result));
        }

        [Fact]
        public void Multiline()
        {
            var result = ParseDocument(@"
directive @include(if: Boolean!)
  on FIELD
   | FRAGMENT_SPREAD
   | INLINE_FRAGMENT
");
            var expected = Document(new DirectiveDefinitionSyntax(
                Name(
                    "include"),
                Names("FIELD", "FRAGMENT_SPREAD", "INLINE_FRAGMENT"),
                new[]
                {
                    InputValueDefinition(Name("if"),
                        NonNullType(NamedType(Name("Boolean"))))
                }));
            Assert.Equal(expected, result);
            Assert.Equal(expected, PrintAndParse(result));
        }

        [Fact]
        public void MultilinePipePrefix()
        {
            var result = ParseDocument(@"
directive @include2(if: Boolean!) on 
   | FIELD
   | FRAGMENT_SPREAD
   | INLINE_FRAGMENT
");
            var expected = Document(new DirectiveDefinitionSyntax(Name("include2"),
                Names("FIELD", "FRAGMENT_SPREAD", "INLINE_FRAGMENT"),
                new[]
                {
                    InputValueDefinition(Name("if"),
                        NonNullType(NamedType(Name("Boolean"))))
                }));
            Assert.Equal(expected, result);
            Assert.Equal(expected, PrintAndParse(result));
        }
    }
}