#nullable disable
// Copyright (c) GraphZen LLC. All rights reserved.
// Licensed under the GraphZen Community License. See the LICENSE file in the project root for license information.

using System.Collections.Generic;
using FluentAssertions;
using GraphZen.Infrastructure;
using GraphZen.LanguageModel;
using Xunit;

namespace GraphZen.TypeSystem
{
    [NoReorder]
    public class SchemaToSyntaxTests
    {
        public static Schema Schema = Schema.Create(schema =>
        {
            schema.DirectiveAnnotation("onSchema");

            schema
                .Directive("customDirective");
            schema.Interface("Interface")
                .DirectiveAnnotation("onInterface")
                .Description("interface description")
                .Field("field", "String", _ => _.Description("field description"))
                .Field("nn", "String!")
                .Field("list", "[String]")
                .Field("nnList", "[String]!")
                .Field("nnListOfnn", "[String!]!");
            schema.Object("Object")
                .Description("object description").ImplementsInterface("Interface")
                .Field("field", "String", _ => _.Description("field description"))
                .Field("nn", "String!")
                .Field("list", "[String]")
                .Field("nnList", "[String]!")
                .Field("nnListOfnn", "[String!]!");
            schema.Scalar("CustomScalar").Description("scalar description");

            schema.Union("Union").OfTypes("Object").Description("union description");
            schema.Enum("Enum").Description("enum description")
                .Value("EnumValue", _ => _.Description("enum value description"));

            schema.InputObject("InputObject").Description("input object description")
                .Field("field", "String", _ => _.Description("field description"))
                .Field("nn", "String!")
                .Field("list", "[String]")
                .Field("nnList", "[String]!")
                .Field("nnListOfnn", "[String!]!");
        });

        [Fact]
        public void scalar_type_to_syntax_node()
        {
            var scalar = Schema.GetType("CustomScalar");
            var expected =
                new ScalarTypeDefinitionSyntax(SyntaxFactory.Name("CustomScalar"),
                    SyntaxFactory.StringValue("scalar description", true));
            scalar.ToSyntaxNode().Should().Be(expected);
        }


        [Fact]
        public void object_type_to_sytnax_node()
        {
            var objectType = Schema.GetType("Object");
            var expected = new ObjectTypeDefinitionSyntax(
                SyntaxFactory.Name("Object"),
                SyntaxHelpers.Description("object description")
                , new[]
                {
                    SyntaxFactory.NamedType(SyntaxFactory.Name("Interface"))
                }, null, new[]
                {
                    new FieldDefinitionSyntax(SyntaxFactory.Name("field"),
                        SyntaxFactory.NamedType(SyntaxFactory.Name("String")),
                        SyntaxFactory.StringValue("field description", true)),
                    SyntaxFactory.FieldDefinition(SyntaxFactory.Name("nn"),
                        SyntaxFactory.NonNull(SyntaxFactory.NamedType(SyntaxFactory.Name("String")))),
                    SyntaxFactory.FieldDefinition(SyntaxFactory.Name("list"),
                        SyntaxFactory.ListType(SyntaxFactory.NamedType(SyntaxFactory.Name("String")))),
                    SyntaxFactory.FieldDefinition(SyntaxFactory.Name("nnList"),
                        SyntaxFactory.NonNull(
                            SyntaxFactory.ListType(SyntaxFactory.NamedType(SyntaxFactory.Name("String"))))),
                    SyntaxFactory.FieldDefinition(SyntaxFactory.Name("nnListOfnn"),
                        SyntaxFactory.NonNull(SyntaxFactory.ListType(
                            SyntaxFactory.NonNull(SyntaxFactory.NamedType(SyntaxFactory.Name("String"))))))
                });
            objectType.ToSyntaxNode().ToSyntaxString().Should().Be(expected.ToSyntaxString());
            objectType.ToSyntaxNode().Should().Be(expected);

        }

        [Fact]
        public void interface_type_to_syntax_node()
        {
            var objectType = Schema.GetType("Interface");
            var expected = new InterfaceTypeDefinitionSyntax(
                SyntaxFactory.Name("Interface"),
                SyntaxFactory.StringValue("interface description", true), new[]
                {
                    SyntaxFactory.Directive(SyntaxFactory.Name("onInterface"))
                }, new[]
                {
                    new FieldDefinitionSyntax(SyntaxFactory.Name("field"),
                        SyntaxFactory.NamedType(SyntaxFactory.Name("String")),
                        SyntaxFactory.StringValue("field description", true)),
                    SyntaxFactory.FieldDefinition(SyntaxFactory.Name("nn"),
                        SyntaxFactory.NonNull(SyntaxFactory.NamedType(SyntaxFactory.Name("String")))),
                    SyntaxFactory.FieldDefinition(SyntaxFactory.Name("list"),
                        SyntaxFactory.ListType(SyntaxFactory.NamedType(SyntaxFactory.Name("String")))),
                    SyntaxFactory.FieldDefinition(SyntaxFactory.Name("nnList"),
                        SyntaxFactory.NonNull(
                            SyntaxFactory.ListType(SyntaxFactory.NamedType(SyntaxFactory.Name("String"))))),
                    SyntaxFactory.FieldDefinition(SyntaxFactory.Name("nnListOfnn"),
                        SyntaxFactory.NonNull(SyntaxFactory.ListType(
                            SyntaxFactory.NonNull(SyntaxFactory.NamedType(SyntaxFactory.Name("String"))))))
                });
            objectType.ToSyntaxNode().Should().Be(expected);
        }

        [Fact]
        public void union_type_to_syntax_node()
        {
            var union = Schema.GetType("Union");
            var expected = new UnionTypeDefinitionSyntax(SyntaxFactory.Name("Union"),
                SyntaxHelpers.Description("union description"),
                null,
                new[] { SyntaxFactory.NamedType(SyntaxFactory.Name("Object")) });
            union.ToSyntaxNode().Should().Be(expected);
        }

        [Fact]
        public void enum_type_to_syntax_node()
        {
            var enumType = Schema.GetType("Enum");
            var expected = new EnumTypeDefinitionSyntax(SyntaxFactory.Name("Enum"),
                SyntaxHelpers.Description("enum description"),
                null, new List<EnumValueDefinitionSyntax>
                {
                    new EnumValueDefinitionSyntax(SyntaxFactory.EnumValue(SyntaxFactory.Name("EnumValue")),
                        SyntaxHelpers.Description("enum value description"))
                });
            enumType.ToSyntaxNode().Should().Be(expected);
        }

        [Fact]
        public void input_object_type_to_syntax_mode()
        {
            var inputObject = Schema.GetType("InputObject");
            var expected = new InputObjectTypeDefinitionSyntax(SyntaxFactory.Name("InputObject"),
                SyntaxHelpers.Description("input object description"), null, new[]
                {
                    new InputValueDefinitionSyntax(SyntaxFactory.Name("field"),
                        SyntaxFactory.NamedType(SyntaxFactory.Name("String")),
                        SyntaxFactory.StringValue("field description", true)),
                    SyntaxFactory.InputValueDefinition(SyntaxFactory.Name("nn"),
                        SyntaxFactory.NonNull(SyntaxFactory.NamedType(SyntaxFactory.Name("String")))),
                    SyntaxFactory.InputValueDefinition(SyntaxFactory.Name("list"),
                        SyntaxFactory.ListType(SyntaxFactory.NamedType(SyntaxFactory.Name("String")))),
                    SyntaxFactory.InputValueDefinition(SyntaxFactory.Name("nnList"),
                        SyntaxFactory.NonNull(
                            SyntaxFactory.ListType(SyntaxFactory.NamedType(SyntaxFactory.Name("String"))))),
                    SyntaxFactory.InputValueDefinition(SyntaxFactory.Name("nnListOfnn"),
                        SyntaxFactory.NonNull(SyntaxFactory.ListType(
                            SyntaxFactory.NonNull(SyntaxFactory.NamedType(SyntaxFactory.Name("String"))))))
                });
            inputObject.ToSyntaxNode().Should().Be(expected);
        }
    }
}