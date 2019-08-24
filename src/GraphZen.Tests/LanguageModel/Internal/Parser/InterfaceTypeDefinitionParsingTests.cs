#nullable disable
// Copyright (c) GraphZen LLC. All rights reserved.
// Licensed under the GraphZen Community License. See the LICENSE file in the project root for license information.

using GraphZen.Infrastructure;
using Xunit;

namespace GraphZen.LanguageModel.Internal.Parser
{
    public class InterfaceTypeDefinitionParsingTests : ParserTestBase
    {
        [Fact]
        public void AnnotatedInterface()
        {
            var result = ParseDocument(@"

interface AnnotatedInterface @onInterface {
  annotatedField(arg: Type @onArg): Type @onField
}
");
            var expected = SyntaxFactory.Document(new InterfaceTypeDefinitionSyntax(
                SyntaxFactory.Name("AnnotatedInterface"), null, new[]
                {
                    SyntaxFactory.Directive(SyntaxFactory.Name("onInterface"))
                }, new[]
                {
                    new FieldDefinitionSyntax(SyntaxFactory.Name("annotatedField"),
                        SyntaxFactory.NamedType(SyntaxFactory.Name("Type")), null, new[]
                        {
                            new InputValueDefinitionSyntax(SyntaxFactory.Name("arg"),
                                SyntaxFactory.NamedType(SyntaxFactory.Name("Type")), null, null,
                                new[] {SyntaxFactory.Directive(SyntaxFactory.Name("onArg"))})
                        }, new[] {SyntaxFactory.Directive(SyntaxFactory.Name("onField"))})
                }));
            Assert.Equal(expected, result);
            Assert.Equal(expected, PrintAndParse(result));
        }

        [Fact]
        public void Interface()
        {
            var result = ParseDocument(@"
interface Bar {
  one: Type
  four(argument: String = ""string""): String
}");
            var expected = SyntaxFactory.Document(new InterfaceTypeDefinitionSyntax(SyntaxFactory.Name("Bar"), null,
                null, new[]
                {
                    SyntaxFactory.FieldDefinition(SyntaxFactory.Name("one"),
                        SyntaxFactory.NamedType(SyntaxFactory.Name("Type"))),
                    new FieldDefinitionSyntax(SyntaxFactory.Name("four"),
                        SyntaxFactory.NamedType(SyntaxFactory.Name("String")), null, new[]
                        {
                            new InputValueDefinitionSyntax(SyntaxFactory.Name("argument"),
                                SyntaxFactory.NamedType(SyntaxFactory.Name("String")), null,
                                SyntaxFactory.StringValue("string")
                            )
                        })
                }));
            Assert.Equal(expected, result);
            Assert.Equal(expected, PrintAndParse(result));
        }

        [Fact]
        public void UndefinedInterface()
        {
            var result = ParseDocument("interface UndefinedInterface ");
            var expected =
                SyntaxFactory.Document(SyntaxFactory.InterfaceTypeDefinition(SyntaxFactory.Name("UndefinedInterface")));
            Assert.Equal(expected, result);
            Assert.Equal(expected, PrintAndParse(result));
        }
    }
}