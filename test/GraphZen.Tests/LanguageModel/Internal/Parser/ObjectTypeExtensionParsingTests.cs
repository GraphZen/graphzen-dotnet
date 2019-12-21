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
    public class ObjectTypeExtensionParsingTests : ParserTestBase
    {
        [Fact]
        public void ObjectTypeExtension()
        {
            var result = ParseDocument(@"
extend type Foo {
  seven(argument: [String]): Type
}");
            var expected = SyntaxFactory.Document(new ObjectTypeExtensionSyntax(SyntaxFactory.Name("Foo"), null, null,
                new[]
                {
                    new FieldDefinitionSyntax(SyntaxFactory.Name("seven"),
                        SyntaxFactory.NamedType(SyntaxFactory.Name("Type")), null, new[]
                        {
                            new InputValueDefinitionSyntax(SyntaxFactory.Name("argument"),
                                SyntaxFactory.ListType(SyntaxFactory.NamedType(SyntaxFactory.Name("String"))))
                        })
                }));


            Assert.Equal(expected, result);
            Assert.Equal(expected, PrintAndParse(result));
        }

        [Fact]
        public void TypeExtensionWithDirective()
        {
            var result = ParseDocument("extend type Foo @onType");
            var expected =
                SyntaxFactory.Document(new ObjectTypeExtensionSyntax(SyntaxFactory.Name("Foo"), null,
                    new[] {SyntaxFactory.Directive(SyntaxFactory.Name("onType"))}));
            Assert.Equal(expected, result);
            Assert.Equal(expected, PrintAndParse(result));
        }

        [Fact]
        public void TypeExtensionWithFields()
        {
            var result = ParseDocument(@"
extend type Foo {
  seven(argument: [String]): Type
}");
            var expected = SyntaxFactory.Document(new ObjectTypeExtensionSyntax(SyntaxFactory.Name("Foo"), null, null,
                new[]
                {
                    new FieldDefinitionSyntax(SyntaxFactory.Name("seven"),
                        SyntaxFactory.NamedType(SyntaxFactory.Name("Type")), null, new[]
                        {
                            SyntaxFactory.InputValueDefinition(SyntaxFactory.Name("argument"),
                                SyntaxFactory.ListType(SyntaxFactory.NamedType(SyntaxFactory.Name("String"))))
                        })
                }));
            Assert.Equal(expected, result);
            Assert.Equal(expected, PrintAndParse(result));
        }
    }
}