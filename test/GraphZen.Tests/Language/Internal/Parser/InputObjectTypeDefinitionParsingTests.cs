// Copyright (c) GraphZen LLC. All rights reserved.
// Licensed under the GraphZen Community License. See the LICENSE file in the project root for license information.

using GraphZen.Infrastructure;
using JetBrains.Annotations;
using Xunit;
using static GraphZen.Language.SyntaxFactory;

namespace GraphZen.Language.Internal
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
            var expected = Document(new InputObjectTypeDefinitionSyntax(
                Name("AnnotatedInput"), null,
                new[] {Directive(Name("onInputObject"))},
                new[]
                {
                    new InputValueDefinitionSyntax(Name("annotatedField"),
                        NamedType(Name("Type"))
                        , null, null,
                        new[] {Directive(Name("onField"))})
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
            var expected = Document(new InputObjectTypeDefinitionSyntax(Name("InputType"),
                null, null, new[]
                {
                    InputValueDefinition(Name("key"),
                        NonNull(NamedType(Name("String")))),
                    new InputValueDefinitionSyntax(Name("answer"),
                        NamedType(Name("Int"))
                        , null, IntValue(42))
                }));

            Assert.Equal(expected, result);
            Assert.Equal(expected, PrintAndParse(result));
        }

        [Fact]
        public void UndefinedInput()
        {
            var result = ParseDocument(@"input UndefinedInput");
            var expected =
                Document(InputObjectTypeDefinition(Name("UndefinedInput")));
            Assert.Equal(expected, result);
            Assert.Equal(expected, PrintAndParse(result));
        }
    }
}