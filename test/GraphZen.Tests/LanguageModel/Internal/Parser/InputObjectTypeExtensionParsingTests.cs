// Copyright (c) GraphZen LLC. All rights reserved.
// Licensed under the GraphZen Community License. See the LICENSE file in the project root for license information.

using System.Diagnostics.CodeAnalysis;
using GraphZen.Infrastructure;
using GraphZen.LanguageModel;
using JetBrains.Annotations;
using Xunit;

#nullable disable


namespace GraphZen.Tests.LanguageModel.Internal.Parser
{
    public class InputObjectTypeExtensionParsingTests : ParserTestBase
    {
        [Fact]
        public void ExtendInputObjectWithField()
        {
            var result = ParseDocument(@"
extend input InputType {
  other: Float = 1.23e4
}");
            var expected = SyntaxFactory.Document(new InputObjectTypeExtensionSyntax(SyntaxFactory.Name("InputType"),
                null, new[]
                {
                    new InputValueDefinitionSyntax(SyntaxFactory.Name("other"),
                        SyntaxFactory.NamedType(SyntaxFactory.Name("Float")), null, SyntaxFactory.FloatValue("1.23e4"))
                }));
            Assert.Equal(expected, result);
            var printResult = PrintAndParse(result);
            Assert.Equal(expected, printResult);
        }

        [Fact]
        public void ExtendInputTypeWithDirective()
        {
            var result = ParseDocument(@"extend input InputType @onInputObject");
            var expected =
                SyntaxFactory.Document(
                    new InputObjectTypeExtensionSyntax(SyntaxFactory.Name("InputType"),
                        new[] {SyntaxFactory.Directive(SyntaxFactory.Name("onInputObject"))}));
            Assert.Equal(expected, result);
            Assert.Equal(expected, PrintAndParse(result));
        }
    }
}