// Copyright (c) GraphZen LLC. All rights reserved.
// Licensed under the GraphZen Community License. See the LICENSE file in the project root for license information.
using JetBrains.Annotations;
#nullable disable


using GraphZen.Infrastructure;
using Xunit;

namespace GraphZen.LanguageModel.Internal.Parser
{
    public class EnumTypeExtensionParserTests : ParserTestBase
    {
        [Fact]
        public void ExtendEnumDirectives()
        {
            var result = ParseDocument("extend enum Site @onEnum");
            var expected = SyntaxFactory.Document(new EnumTypeExtensionSyntax(SyntaxFactory.Name("Site"),
                new[] { SyntaxFactory.Directive(SyntaxFactory.Name("onEnum")) }));
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
            var expected = SyntaxFactory.Document(new EnumTypeExtensionSyntax(SyntaxFactory.Name("Site"), null, new[]
            {
                SyntaxFactory.EnumValueDefinition(SyntaxFactory.EnumValue(SyntaxFactory.Name("VR")))
            }));

            Assert.Equal(expected, result);
            Assert.Equal(expected, PrintAndParse(result));
        }
    }
}