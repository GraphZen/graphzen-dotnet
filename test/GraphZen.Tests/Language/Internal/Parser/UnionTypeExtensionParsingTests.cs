// Copyright (c) GraphZen LLC. All rights reserved.
// Licensed under the GraphZen Community License. See the LICENSE file in the project root for license information.

using GraphZen.Infrastructure;
using JetBrains.Annotations;
using Xunit;
using static GraphZen.Language.SyntaxFactory;

namespace GraphZen.Language.Internal
{
    public class UnionTypeExtensionParsingTests : ParserTestBase
    {
        [Fact]
        public void UnionExtendedWithDirective()
        {
            var result = ParseDocument("extend union Feed @onUnion");
            var expected = Document(new UnionTypeExtensionSyntax(Name("Feed"),
                new[] {Directive(Name("onUnion"))}));
            Assert.Equal(expected, result);
            Assert.Equal(expected, PrintAndParse(result));
        }

        [Fact]
        public void UnionWithExtendedTypes()
        {
            var result = ParseDocument("extend union Feed = Photo | Video");
            var expected = Document(new UnionTypeExtensionSyntax(Name("Feed"), null, new[]
            {
                NamedType(Name("Photo")),
                NamedType(Name("Video"))
            }));
            Assert.Equal(expected, result);
            Assert.Equal(expected, PrintAndParse(result));
        }
    }
}