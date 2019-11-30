// Copyright (c) GraphZen LLC. All rights reserved.
// Licensed under the GraphZen Community License. See the LICENSE file in the project root for license information.

using System.Diagnostics.CodeAnalysis;
using FluentAssertions;
using GraphZen.Infrastructure;
using GraphZen.TypeSystem;
using JetBrains.Annotations;
using Xunit;
using static GraphZen.LanguageModel.SyntaxFactory;

#nullable disable


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
            var expected = Document(new EnumTypeDefinitionSyntax(Name("AnnotatedEnum"),
                null,
                new[] { Directive(Name("onEnum")) }, new[]
                {
                    new EnumValueDefinitionSyntax(EnumValue(Name("ANNOTATED_VALUE")), null,
                        new[] {Directive(Name("onEnumValue"))}),
                    EnumValueDefinition(EnumValue(Name("OTHER_VALUE")))
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
            var expected = Document(new EnumTypeDefinitionSyntax(Name("Site"), null, null,
                new[]
                {
                    EnumValueDefinition(EnumValue(Name("DESKTOP"))),
                    EnumValueDefinition(EnumValue(Name("MOBILE")))
                }));

            Assert.Equal(expected, result);
            Assert.Equal(expected, PrintAndParse(result));
        }

        [Fact]
        public void UndefinedEnum()
        {
            var result = ParseDocument("enum UndefinedEnum");
            var expected =
                Document(EnumTypeDefinition(Name("UndefinedEnum")));
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
                Document(new EnumTypeDefinitionSyntax(Name("DescribedEnum"),
                    SyntaxHelpers.Description("Enum description"), null, new[
                    ]
                    {
                        EnumValueDefinition(EnumValue(Name("UNDESCRIBED"))),
                        new EnumValueDefinitionSyntax(EnumValue(Name("DESCRIBED")),
                            SyntaxHelpers.Description("Enum value description"))
                    }));

            parsed.ToSyntaxString().Should().Be(expected.ToSyntaxString());
        }
    }
}