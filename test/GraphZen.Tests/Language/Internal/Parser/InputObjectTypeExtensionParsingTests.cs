// Copyright (c) GraphZen LLC. All rights reserved.
// Licensed under the GraphZen Community License. See the LICENSE file in the project root for license information.

using GraphZen.Infrastructure;
using GraphZen.LanguageModel;
using JetBrains.Annotations;
using Xunit;
using static GraphZen.LanguageModel.SyntaxFactory;

namespace GraphZen.Language.Internal
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
            var expected = Document(new InputObjectTypeExtensionSyntax(Name("InputType"),
                null, new[]
                {
                    new InputValueDefinitionSyntax(Name("other"),
                        NamedType(Name("Float")), null, FloatValue("1.23e4"))
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
                Document(
                    new InputObjectTypeExtensionSyntax(Name("InputType"),
                        new[] {Directive(Name("onInputObject"))}));
            Assert.Equal(expected, result);
            Assert.Equal(expected, PrintAndParse(result));
        }
    }
}