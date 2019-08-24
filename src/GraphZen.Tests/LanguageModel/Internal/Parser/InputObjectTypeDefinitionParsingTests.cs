#nullable disable
// Copyright (c) GraphZen LLC. All rights reserved.
// Licensed under the GraphZen Community License. See the LICENSE file in the project root for license information.

using GraphZen.Infrastructure;
using Xunit;

namespace GraphZen.LanguageModel.Internal.Parser
{
    public class InputObjectTypeDefinitionParsingTests : ParserTestBase
    {
        [Fact]
        public void AnnotatedInput()
        {
            var result = ParseDocument(@"
input AnnotatedInput @onInputObject {
  annotatedField: Type @onField
}");
            var expected = SyntaxFactory.Document(new InputObjectTypeDefinitionSyntax(
                SyntaxFactory.Name("AnnotatedInput"), null,
                new[] { SyntaxFactory.Directive(SyntaxFactory.Name("onInputObject")) },
                new[]
                {
                    new InputValueDefinitionSyntax(SyntaxFactory.Name("annotatedField"),
                        SyntaxFactory.NamedType(SyntaxFactory.Name("Type"))
                        , null, null,
                        new[] {SyntaxFactory.Directive(SyntaxFactory.Name("onField"))})
                }));
            Assert.Equal(expected, result);
            Assert.Equal(expected, PrintAndParse(result));
        }

        [Fact]
        public void SimpleInputType()
        {
            var result = ParseDocument(@"
input InputType {
  key: String!
  answer: Int = 42
}
");
            var expected = SyntaxFactory.Document(new InputObjectTypeDefinitionSyntax(SyntaxFactory.Name("InputType"),
                null, null, new[]
                {
                    SyntaxFactory.InputValueDefinition(SyntaxFactory.Name("key"),
                        SyntaxFactory.NonNull(SyntaxFactory.NamedType(SyntaxFactory.Name("String")))),
                    new InputValueDefinitionSyntax(SyntaxFactory.Name("answer"),
                        SyntaxFactory.NamedType(SyntaxFactory.Name("Int"))
                        , null, SyntaxFactory.IntValue(42))
                }));

            Assert.Equal(expected, result);
            Assert.Equal(expected, PrintAndParse(result));
        }

        [Fact]
        public void UndefinedInput()
        {
            var result = ParseDocument(@"input UndefinedInput");
            var expected =
                SyntaxFactory.Document(SyntaxFactory.InputObjectTypeDefinition(SyntaxFactory.Name("UndefinedInput")));
            Assert.Equal(expected, result);
            Assert.Equal(expected, PrintAndParse(result));
        }
    }
}