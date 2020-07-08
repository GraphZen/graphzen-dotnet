// Copyright (c) GraphZen LLC. All rights reserved.
// Licensed under the GraphZen Community License. See the LICENSE file in the project root for license information.

using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using FluentAssertions;
using GraphZen.Infrastructure;
using GraphZen.LanguageModel;
using JetBrains.Annotations;
using Xunit;
using static GraphZen.TypeSystem.Internal.SyntaxHelpers;
using static GraphZen.LanguageModel.SyntaxFactory;


namespace GraphZen.TypeSystem.Tests
{
    [NoReorder]
    public class SchemaToSyntaxTests
    {
        private Schema Schema { get; } = Schema.Create(schema =>
        {
            schema.Directive("customDirective");
            schema.Directive("onSchema").Locations(DirectiveLocation.Schema);
            schema.Directive("onInterface").Locations(DirectiveLocation.Interface);

            schema.AddDirectiveAnnotation("onSchema");

            schema.Interface("Interface")
                .AddDirectiveAnnotation("onInterface")
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
                new ScalarTypeDefinitionSyntax(Name("CustomScalar"),
                    StringValue("scalar description", true));
            scalar.ToSyntaxNode().Should().Be(expected);
        }


        [Fact]
        public void object_type_to_sytnax_node()
        {
            var objectType = Schema.GetType("Object");
            var expected = new ObjectTypeDefinitionSyntax(
                Name("Object"),
                Description("object description")
                , new[]
                {
                    NamedType(Name("Interface"))
                }, null, new[]
                {
                    new FieldDefinitionSyntax(Name("field"),
                        NamedType(Name("String")),
                        StringValue("field description", true)),
                    FieldDefinition(Name("nn"),
                        NonNullType(NamedType(Name("String")))),
                    FieldDefinition(Name("list"),
                        ListType(NamedType(Name("String")))),
                    FieldDefinition(Name("nnList"),
                        NonNullType(
                            ListType(NamedType(Name("String"))))),
                    FieldDefinition(Name("nnListOfnn"),
                        NonNullType(ListType(
                            NonNullType(NamedType(Name("String"))))))
                });
            objectType.ToSyntaxNode().ToSyntaxString().Should().Be(expected.ToSyntaxString());
            objectType.ToSyntaxNode().Should().Be(expected);
        }

        [Fact]
        public void interface_type_to_syntax_node()
        {
            var objectType = Schema.GetType("Interface");
            var expected = new InterfaceTypeDefinitionSyntax(
                Name("Interface"),
                StringValue("interface description", true), new[]
                {
                    Directive(Name("onInterface"))
                }, new[]
                {
                    new FieldDefinitionSyntax(Name("field"),
                        NamedType(Name("String")),
                        StringValue("field description", true)),
                    FieldDefinition(Name("nn"),
                        NonNullType(NamedType(Name("String")))),
                    FieldDefinition(Name("list"),
                        ListType(NamedType(Name("String")))),
                    FieldDefinition(Name("nnList"),
                        NonNullType(
                            ListType(NamedType(Name("String"))))),
                    FieldDefinition(Name("nnListOfnn"),
                        NonNullType(ListType(
                            NonNullType(NamedType(Name("String"))))))
                });
            objectType.ToSyntaxNode().Should().Be(expected);
        }

        [Fact]
        public void union_type_to_syntax_node()
        {
            var union = Schema.GetType("Union");
            var expected = new UnionTypeDefinitionSyntax(Name("Union"),
                Description("union description"),
                null,
                new[] { NamedType(Name("Object")) });
            union.ToSyntaxNode().Should().Be(expected);
        }

        [Fact]
        public void enum_type_to_syntax_node()
        {
            var enumType = Schema.GetType("Enum");
            var expected = new EnumTypeDefinitionSyntax(Name("Enum"),
                Description("enum description"),
                null, new List<EnumValueDefinitionSyntax>
                {
                    new EnumValueDefinitionSyntax(EnumValue(Name("EnumValue")),
                        Description("enum value description"))
                });
            enumType.ToSyntaxNode().Should().Be(expected);
        }

        [Fact]
        public void input_object_type_to_syntax_mode()
        {
            var inputObject = Schema.GetType("InputObject");
            var expected = new InputObjectTypeDefinitionSyntax(Name("InputObject"),
                Description("input object description"), null, new[]
                {
                    new InputValueDefinitionSyntax(Name("field"),
                        NamedType(Name("String")),
                        StringValue("field description", true)),
                    InputValueDefinition(Name("nn"),
                        NonNullType(NamedType(Name("String")))),
                    InputValueDefinition(Name("list"),
                        ListType(NamedType(Name("String")))),
                    InputValueDefinition(Name("nnList"),
                        NonNullType(
                            ListType(NamedType(Name("String"))))),
                    InputValueDefinition(Name("nnListOfnn"),
                        NonNullType(ListType(
                            NonNullType(NamedType(Name("String"))))))
                });
            inputObject.ToSyntaxNode().Should().Be(expected);
        }
    }
}