// Copyright (c) GraphZen LLC. All rights reserved.
// Licensed under the GraphZen Community License. See the LICENSE file in the project root for license information.

using GraphZen.Infrastructure;
using JetBrains.Annotations;
using Xunit;
using static GraphZen.Language.SyntaxFactory;

namespace GraphZen.Language.Internal
{
    public class EnumTypeExtensionParserTests : ParserTestBase
    {
        [Fact]
        public void ExtendEnumDirectives()
        {
            var result = ParseDocument("extend enum Site @onEnum");
            var expected = Document(new EnumTypeExtensionSyntax(Name("Site"),
                new[] {Directive(Name("onEnum"))}));
            Assert.Equal(expected, result);
            Assert.Equal(expected, PrintAndParse(result));
        }

        [Fact]
        public void ExtendEnumValue()
        {
            var result = ParseDocument(@"
extend enum Site {
  VR
}");
            var expected = Document(new EnumTypeExtensionSyntax(Name("Site"), null, new[]
            {
                EnumValueDefinition(EnumValue(Name("VR")))
            }));

            Assert.Equal(expected, result);
            Assert.Equal(expected, PrintAndParse(result));
        }
    }
}