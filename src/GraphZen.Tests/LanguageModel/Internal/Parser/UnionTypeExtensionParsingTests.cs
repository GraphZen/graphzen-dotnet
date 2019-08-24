// Copyright (c) GraphZen LLC. All rights reserved.
// Licensed under the GraphZen Community License. See the LICENSE file in the project root for license information.

using GraphZen.Infrastructure;
using Xunit;

namespace GraphZen.LanguageModel.Internal.Parser
{
    public class UnionTypeExtensionParsingTests : ParserTestBase
    {
        [Fact]
        public void UnionExtendedWithDirective()
        {
            var result = ParseDocument("extend union Feed @onUnion");
            var expected = SyntaxFactory.Document(new UnionTypeExtensionSyntax(SyntaxFactory.Name("Feed"),
                new[] { SyntaxFactory.Directive(SyntaxFactory.Name("onUnion")) }));
            Assert.Equal(expected, result);
            Assert.Equal(expected, PrintAndParse(result));
        }

        [Fact]
        public void UnionWithExtendedTypes()
        {
            var result = ParseDocument("extend union Feed = Photo | Video");
            var expected = SyntaxFactory.Document(new UnionTypeExtensionSyntax(SyntaxFactory.Name("Feed"), null, new[]
            {
                SyntaxFactory.NamedType(SyntaxFactory.Name("Photo")),
                SyntaxFactory.NamedType(SyntaxFactory.Name("Video"))
            }));
            Assert.Equal(expected, result);
            Assert.Equal(expected, PrintAndParse(result));
        }
    }
}