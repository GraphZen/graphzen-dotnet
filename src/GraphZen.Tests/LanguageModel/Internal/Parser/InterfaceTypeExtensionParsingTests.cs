// Copyright (c) GraphZen LLC. All rights reserved.
// Licensed under the GraphZen Community License. See the LICENSE file in the project root for license information.

using System.Diagnostics.CodeAnalysis;
using GraphZen.Infrastructure;
using JetBrains.Annotations;
using Xunit;

#nullable disable


namespace GraphZen.LanguageModel.Internal.Parser
{
    public class InterfaceTypeExtensionParsingTests : ParserTestBase
    {
        [Fact]
        public void InterfaceExtendedWithDirective()
        {
            var result = ParseDocument("extend interface Bar @onInterface ");
            var expected =
                SyntaxFactory.Document(new InterfaceTypeExtensionSyntax(SyntaxFactory.Name("Bar"),
                    new[] { SyntaxFactory.Directive(SyntaxFactory.Name("onInterface")) }));
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
            var expected = SyntaxFactory.Document(new InterfaceTypeExtensionSyntax(SyntaxFactory.Name("Bar"), null,
                new[]
                {
                    new FieldDefinitionSyntax(SyntaxFactory.Name("two"),
                        SyntaxFactory.NamedType(SyntaxFactory.Name("Type")), null,
                        new[]
                        {
                            SyntaxFactory.InputValueDefinition(SyntaxFactory.Name("argument"),
                                SyntaxFactory.NonNull(SyntaxFactory.NamedType(SyntaxFactory.Name("InputType"))))
                        })
                }));
            Assert.Equal(expected, result);
            Assert.Equal(expected, PrintAndParse(result));
        }
    }
}