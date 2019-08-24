// Copyright (c) GraphZen LLC. All rights reserved.
// Licensed under the GraphZen Community License. See the LICENSE file in the project root for license information.
using JetBrains.Annotations;
#nullable disable


using GraphZen.Infrastructure;
using Xunit;

namespace GraphZen.LanguageModel.Internal.Parser
{
    public class UnionTypeDefnitionParsingTests : ParserTestBase
    {
        [Fact]
        public void SimpleUnion()
        {
            var result = ParseDocument("union Feed = Story | Article | Advert");
            var expected = SyntaxFactory.Document(new UnionTypeDefinitionSyntax(SyntaxFactory.Name("Feed"), null, null,
                new[]
                {
                    SyntaxFactory.NamedType(SyntaxFactory.Name("Story")),
                    SyntaxFactory.NamedType(SyntaxFactory.Name("Article")),
                    SyntaxFactory.NamedType(SyntaxFactory.Name("Advert"))
                }));

            Assert.Equal(expected, result);
            Assert.Equal(expected, PrintAndParse(result));
        }

        [Fact]
        public void UndefinedUnion()
        {
            var result = ParseDocument("union UndefinedUnion");
            var expected =
                SyntaxFactory.Document(SyntaxFactory.UnionTypeDefinition(SyntaxFactory.Name("UndefinedUnion")));
            Assert.Equal(expected, result);
            Assert.Equal(expected, PrintAndParse(result));
        }
    }
}