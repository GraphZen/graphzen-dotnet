// Copyright (c) GraphZen LLC. All rights reserved.
// Licensed under the GraphZen Community License. See the LICENSE file in the project root for license information.

using GraphZen.Infrastructure;
using JetBrains.Annotations;
using Xunit;
using static GraphZen.Language.SyntaxFactory;

namespace GraphZen.Language.Internal
{
    public class ObjectTypeDefinitionParsingTests : ParserTestBase
    {
        [Fact]
        public void AnnotatedObjectDefinition()
        {
            var gql = @"
type AnnotatedObject @onObject(arg: ""value"") {
  annotatedField(arg: Type = ""default"" @onArg): Type @onField
}";
            var result = ParseDocument(gql);
            var expected = Document(new ObjectTypeDefinitionSyntax(Name("AnnotatedObject"),
                null, null, new[]
                {
                    new DirectiveSyntax(Name("onObject"),
                        new[] {Argument(Name("arg"), StringValue("value"))})
                }, new[]
                {
                    new FieldDefinitionSyntax(Name("annotatedField"),
                        NamedType(Name("Type")), null, new[]
                        {
                            new InputValueDefinitionSyntax(Name("arg"),
                                NamedType(Name("Type")), null,
                                StringValue("default"), new[]
                                {
                                    Directive(Name("onArg"))
                                })
                        }, new[] {Directive(Name("onField"))})
                }));
            Assert.Equal(expected, result);
            Assert.Equal(expected, PrintAndParse(result));
        }

        [Fact]
        public void TypeDefinitionNode()
        {
            var gql = @"
""""""
This is a description
of the `Foo` type.
""""""
type Foo implements Bar & Baz {
  one: Type
  """"""
  This is a description of the `two` field.
  """"""
  two(
    """"""
    This is a description of the `argument` argument.
    """"""
    argument: InputType!
  ): Type
  three(argument: InputType, other: String): Int
  four(argument: String = ""string""): String
  five(argument: [String] = [""string"", ""string""]): String
  six(argument: InputType = {key: ""value""}): Type
  seven(argument: Int = null): Type
}
  

";
            var result = ParseDocument(gql);

            var expected = Document(new ObjectTypeDefinitionSyntax(Name("Foo"),
                StringValue(@"This is a description
of the `Foo` type.", true), new[]
                {
                    NamedType(Name("Bar")),
                    NamedType(Name("Baz"))
                }, null, new[]
                {
                    FieldDefinition(Name("one"),
                        NamedType(Name("Type"))),
                    new FieldDefinitionSyntax(Name("two"),
                        NamedType(Name("Type")),
                        StringValue("This is a description of the `two` field.", true), new[]
                        {
                            new InputValueDefinitionSyntax(Name("argument"),
                                NonNull(NamedType(Name("InputType"))),
                                StringValue("This is a description of the `argument` argument.", true))
                        }),
                    new FieldDefinitionSyntax(Name("three"),
                        NamedType(Name("Int")), null, new[]
                        {
                            InputValueDefinition(Name("argument"),
                                NamedType(Name("InputType"))),
                            InputValueDefinition(Name("other"),
                                NamedType(Name("String")))
                        }),
                    new FieldDefinitionSyntax(Name("four"),
                        NamedType(Name("String")), null, new[]
                        {
                            new InputValueDefinitionSyntax(Name("argument"),
                                NamedType(Name("String")), null,
                                StringValue("string"))
                        }),
                    new FieldDefinitionSyntax(Name("five"),
                        NamedType(Name("String")), null, new[]
                        {
                            new InputValueDefinitionSyntax(Name("argument"),
                                ListType(NamedType(Name("String"))),
                                null,
                                ListValue(StringValue("string"),
                                    StringValue("string")))
                        }),
                    new FieldDefinitionSyntax(Name("six"),
                        NamedType(Name("Type")), null, new[]
                        {
                            new InputValueDefinitionSyntax(Name("argument"),
                                NamedType(Name("InputType")), null,
                                ObjectValue(ObjectField(Name("key"),
                                    StringValue("value"))))
                        }),
                    new FieldDefinitionSyntax(Name("seven"),
                        NamedType(Name("Type")), null, new[]
                        {
                            new InputValueDefinitionSyntax(Name("argument"),
                                NamedType(Name("Int")), null,
                                NullValue())
                        })
                }));

            Assert.Equal(expected, result);
            Assert.Equal(expected, PrintAndParse(result));
        }

        [Fact]
        public void UndefinedType()
        {
            var result = ParseDocument("type UndefinedType");
            var expected =
                Document(ObjectTypeDefinition(Name("UndefinedType")));
            Assert.Equal(expected, result);
            Assert.Equal(expected, PrintAndParse(result));
        }
    }
}