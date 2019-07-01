// Copyright (c) GraphZen LLC. All rights reserved.
// Licensed under the GraphZen Community License. See the LICENSE file in the project root for license information.

using GraphZen.Infrastructure;
using JetBrains.Annotations;
using Xunit;
using static GraphZen.Language.SyntaxFactory;

namespace GraphZen.Language.Internal
{
    public class UnionTypeDefnitionParsingTests : ParserTestBase
    {
        [Fact]
        public void SimpleUnion()
        {
            var result = ParseDocument("union Feed = Story | Article | Advert");
            var expected = Document(new UnionTypeDefinitionSyntax(Name("Feed"), null, null,
                new[]
                {
                    NamedType(Name("Story")),
                    NamedType(Name("Article")),
                    NamedType(Name("Advert"))
                }));

            Assert.Equal(expected, result);
            Assert.Equal(expected, PrintAndParse(result));
        }

        [Fact]
        public void UndefinedUnion()
        {
            var result = ParseDocument("union UndefinedUnion");
            var expected =
                Document(UnionTypeDefiniton(Name("UndefinedUnion")));
            Assert.Equal(expected, result);
            Assert.Equal(expected, PrintAndParse(result));
        }
    }
}