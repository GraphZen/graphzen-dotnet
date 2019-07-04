// Copyright (c) GraphZen LLC. All rights reserved.
// Licensed under the GraphZen Community License. See the LICENSE file in the project root for license information.

using GraphZen.Infrastructure;
using GraphZen.LanguageModel;
using JetBrains.Annotations;
using Xunit;
using static GraphZen.LanguageModel.SyntaxFactory;

namespace GraphZen.Language.Internal
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
            var expected = Document(new ObjectTypeExtensionSyntax(Name("Foo"), null, null,
                new[]
                {
                    new FieldDefinitionSyntax(Name("seven"),
                        NamedType(Name("Type")), null, new[]
                        {
                            new InputValueDefinitionSyntax(Name("argument"),
                                ListType(NamedType(Name("String"))))
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
                Document(new ObjectTypeExtensionSyntax(Name("Foo"), null,
                    new[] {Directive(Name("onType"))}));
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
            var expected = Document(new ObjectTypeExtensionSyntax(Name("Foo"), null, null,
                new[]
                {
                    new FieldDefinitionSyntax(Name("seven"),
                        NamedType(Name("Type")), null, new[]
                        {
                            InputValueDefinition(Name("argument"),
                                ListType(NamedType(Name("String"))))
                        })
                }));
            Assert.Equal(expected, result);
            Assert.Equal(expected, PrintAndParse(result));
        }
    }
}