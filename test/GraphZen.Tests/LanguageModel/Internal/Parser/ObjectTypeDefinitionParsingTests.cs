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
            var expected = SyntaxFactory.Document(new ObjectTypeDefinitionSyntax(SyntaxFactory.Name("AnnotatedObject"),
                null, null, new[]
                {
                    new DirectiveSyntax(SyntaxFactory.Name("onObject"),
                        new[] {SyntaxFactory.Argument(SyntaxFactory.Name("arg"), SyntaxFactory.StringValue("value"))})
                }, new[]
                {
                    new FieldDefinitionSyntax(SyntaxFactory.Name("annotatedField"),
                        SyntaxFactory.NamedType(SyntaxFactory.Name("Type")), null, new[]
                        {
                            new InputValueDefinitionSyntax(SyntaxFactory.Name("arg"),
                                SyntaxFactory.NamedType(SyntaxFactory.Name("Type")), null,
                                SyntaxFactory.StringValue("default"), new[]
                                {
                                    SyntaxFactory.Directive(SyntaxFactory.Name("onArg"))
                                })
                        }, new[] {SyntaxFactory.Directive(SyntaxFactory.Name("onField"))})
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

            var expected = SyntaxFactory.Document(new ObjectTypeDefinitionSyntax(SyntaxFactory.Name("Foo"),
                SyntaxFactory.StringValue(@"This is a description
of the `Foo` type.", true), new[]
                {
                    SyntaxFactory.NamedType(SyntaxFactory.Name("Bar")),
                    SyntaxFactory.NamedType(SyntaxFactory.Name("Baz"))
                }, null, new[]
                {
                    SyntaxFactory.FieldDefinition(SyntaxFactory.Name("one"),
                        SyntaxFactory.NamedType(SyntaxFactory.Name("Type"))),
                    new FieldDefinitionSyntax(SyntaxFactory.Name("two"),
                        SyntaxFactory.NamedType(SyntaxFactory.Name("Type")),
                        SyntaxFactory.StringValue("This is a description of the `two` field.", true), new[]
                        {
                            new InputValueDefinitionSyntax(SyntaxFactory.Name("argument"),
                                SyntaxFactory.NonNull(SyntaxFactory.NamedType(SyntaxFactory.Name("InputType"))),
                                SyntaxFactory.StringValue("This is a description of the `argument` argument.", true))
                        }),
                    new FieldDefinitionSyntax(SyntaxFactory.Name("three"),
                        SyntaxFactory.NamedType(SyntaxFactory.Name("Int")), null, new[]
                        {
                            SyntaxFactory.InputValueDefinition(SyntaxFactory.Name("argument"),
                                SyntaxFactory.NamedType(SyntaxFactory.Name("InputType"))),
                            SyntaxFactory.InputValueDefinition(SyntaxFactory.Name("other"),
                                SyntaxFactory.NamedType(SyntaxFactory.Name("String")))
                        }),
                    new FieldDefinitionSyntax(SyntaxFactory.Name("four"),
                        SyntaxFactory.NamedType(SyntaxFactory.Name("String")), null, new[]
                        {
                            new InputValueDefinitionSyntax(SyntaxFactory.Name("argument"),
                                SyntaxFactory.NamedType(SyntaxFactory.Name("String")), null,
                                SyntaxFactory.StringValue("string"))
                        }),
                    new FieldDefinitionSyntax(SyntaxFactory.Name("five"),
                        SyntaxFactory.NamedType(SyntaxFactory.Name("String")), null, new[]
                        {
                            new InputValueDefinitionSyntax(SyntaxFactory.Name("argument"),
                                SyntaxFactory.ListType(SyntaxFactory.NamedType(SyntaxFactory.Name("String"))),
                                null,
                                SyntaxFactory.ListValue(SyntaxFactory.StringValue("string"),
                                    SyntaxFactory.StringValue("string")))
                        }),
                    new FieldDefinitionSyntax(SyntaxFactory.Name("six"),
                        SyntaxFactory.NamedType(SyntaxFactory.Name("Type")), null, new[]
                        {
                            new InputValueDefinitionSyntax(SyntaxFactory.Name("argument"),
                                SyntaxFactory.NamedType(SyntaxFactory.Name("InputType")), null,
                                SyntaxFactory.ObjectValue(SyntaxFactory.ObjectField(SyntaxFactory.Name("key"),
                                    SyntaxFactory.StringValue("value"))))
                        }),
                    new FieldDefinitionSyntax(SyntaxFactory.Name("seven"),
                        SyntaxFactory.NamedType(SyntaxFactory.Name("Type")), null, new[]
                        {
                            new InputValueDefinitionSyntax(SyntaxFactory.Name("argument"),
                                SyntaxFactory.NamedType(SyntaxFactory.Name("Int")), null,
                                SyntaxFactory.NullValue())
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
                SyntaxFactory.Document(SyntaxFactory.ObjectTypeDefinition(SyntaxFactory.Name("UndefinedType")));
            Assert.Equal(expected, result);
            Assert.Equal(expected, PrintAndParse(result));
        }
    }
}