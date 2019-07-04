// Copyright (c) GraphZen LLC. All rights reserved.
// Licensed under the GraphZen Community License. See the LICENSE file in the project root for license information.

using GraphZen.Infrastructure;
using GraphZen.TypeSystem;
using JetBrains.Annotations;
using Xunit;

namespace GraphZen.LanguageModel.Internal.Parser
{
    public class EnumTypeDefinitionParsingTests : ParserTestBase
    {
        [Fact]
        public void AnnotatedEnum()
        {
            var result = ParseDocument(@"
enum AnnotatedEnum @onEnum {
  ANNOTATED_VALUE @onEnumValue
  OTHER_VALUE
}

");
            var expected = SyntaxFactory.Document(new EnumTypeDefinitionSyntax(SyntaxFactory.Name("AnnotatedEnum"),
                null,
                new[] {SyntaxFactory.Directive(SyntaxFactory.Name("onEnum"))}, new[]
                {
                    new EnumValueDefinitionSyntax(SyntaxFactory.EnumValue(SyntaxFactory.Name("ANNOTATED_VALUE")), null,
                        new[] {SyntaxFactory.Directive(SyntaxFactory.Name("onEnumValue"))}),
                    SyntaxFactory.EnumValueDefinition(SyntaxFactory.EnumValue(SyntaxFactory.Name("OTHER_VALUE")))
                }));

            Assert.Equal(expected, result);
            Assert.Equal(expected, PrintAndParse(result));
        }

        [Fact]
        public void SimpleEnum()
        {
            var result = ParseDocument(@"
enum Site {
  DESKTOP
  MOBILE
}
");
            var expected = SyntaxFactory.Document(new EnumTypeDefinitionSyntax(SyntaxFactory.Name("Site"), null, null,
                new[]
                {
                    SyntaxFactory.EnumValueDefinition(SyntaxFactory.EnumValue(SyntaxFactory.Name("DESKTOP"))),
                    SyntaxFactory.EnumValueDefinition(SyntaxFactory.EnumValue(SyntaxFactory.Name("MOBILE")))
                }));

            Assert.Equal(expected, result);
            Assert.Equal(expected, PrintAndParse(result));
        }

        [Fact]
        public void UndefinedEnum()
        {
            var result = ParseDocument("enum UndefinedEnum");
            var expected =
                SyntaxFactory.Document(SyntaxFactory.EnumTypeDefinition(SyntaxFactory.Name("UndefinedEnum")));
            Assert.Equal(expected, result);
            Assert.Equal(expected, PrintAndParse(result));
        }

        [Fact]
        public void WithDescriptions()
        {
            var parsed =
                ParseDocument(@"
                """"""Enum description""""""
                enum DescribedEnum {
                  UNDESCRIBED
                  """"""Enum value description""""""
                  DESCRIBED
                }
            ");
            var expected =
                SyntaxFactory.Document(new EnumTypeDefinitionSyntax(SyntaxFactory.Name("DescribedEnum"),
                    SyntaxHelpers.Description("Enum description"), null, new[
                    ]
                    {
                        SyntaxFactory.EnumValueDefinition(SyntaxFactory.EnumValue(SyntaxFactory.Name("UNDESCRIBED"))),
                        new EnumValueDefinitionSyntax(SyntaxFactory.EnumValue(SyntaxFactory.Name("DESCRIBED")),
                            SyntaxHelpers.Description("Enum value description"))
                    }));

            TestHelpers.AssertEquals(expected.ToSyntaxString(), parsed.ToSyntaxString());
        }
    }
}