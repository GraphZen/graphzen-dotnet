// Copyright (c) GraphZen LLC. All rights reserved.
// Licensed under the GraphZen Community License. See the LICENSE file in the project root for license information.

using GraphZen.Infrastructure;
using GraphZen.LanguageModel;
using JetBrains.Annotations;
using Xunit;
using static GraphZen.LanguageModel.SyntaxFactory;

namespace GraphZen.Language.Internal
{
    public class InterfaceTypeExtensionParsingTests : ParserTestBase
    {
        [Fact]
        public void InterfaceExtendedWithDirective()
        {
            var result = ParseDocument("extend interface Bar @onInterface ");
            var expected =
                Document(new InterfaceTypeExtensionSyntax(Name("Bar"),
                    new[] {Directive(Name("onInterface"))}));
            Assert.Equal(expected, result);
            Assert.Equal(expected, PrintAndParse(result));
        }

        [Fact]
        public void InterfaceExtendedWithFields()
        {
            var result = ParseDocument(@"
extend interface Bar {
  two(argument: InputType!): Type
}");
            var expected = Document(new InterfaceTypeExtensionSyntax(Name("Bar"), null,
                new[]
                {
                    new FieldDefinitionSyntax(Name("two"),
                        NamedType(Name("Type")), null,
                        new[]
                        {
                            InputValueDefinition(Name("argument"),
                                NonNull(NamedType(Name("InputType"))))
                        })
                }));
            Assert.Equal(expected, result);
            Assert.Equal(expected, PrintAndParse(result));
        }
    }
}