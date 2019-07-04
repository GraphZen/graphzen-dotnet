// Copyright (c) GraphZen LLC. All rights reserved.
// Licensed under the GraphZen Community License. See the LICENSE file in the project root for license information.

using GraphZen.Infrastructure;
using GraphZen.LanguageModel;
using JetBrains.Annotations;
using Xunit;
using static GraphZen.LanguageModel.SyntaxFactory;

namespace GraphZen.Language.Internal
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
            var expected = Document(new InterfaceTypeDefinitionSyntax(
                Name("AnnotatedInterface"), null, new[]
                {
                    Directive(Name("onInterface"))
                }, new[]
                {
                    new FieldDefinitionSyntax(Name("annotatedField"),
                        NamedType(Name("Type")), null, new[]
                        {
                            new InputValueDefinitionSyntax(Name("arg"),
                                NamedType(Name("Type")), null, null,
                                new[] {Directive(Name("onArg"))})
                        }, new[] {Directive(Name("onField"))})
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
            var expected = Document(new InterfaceTypeDefinitionSyntax(Name("Bar"), null,
                null, new[]
                {
                    FieldDefinition(Name("one"),
                        NamedType(Name("Type"))),
                    new FieldDefinitionSyntax(Name("four"),
                        NamedType(Name("String")), null, new[]
                        {
                            new InputValueDefinitionSyntax(Name("argument"),
                                NamedType(Name("String")), null,
                                StringValue("string")
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
                Document(InterfaceTypeDefinition(Name("UndefinedInterface")));
            Assert.Equal(expected, result);
            Assert.Equal(expected, PrintAndParse(result));
        }
    }
}