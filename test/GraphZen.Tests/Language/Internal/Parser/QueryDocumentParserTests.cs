// Copyright (c) GraphZen LLC. All rights reserved.
// Licensed under the GraphZen Community License. See the LICENSE file in the project root for license information.

using GraphZen.Infrastructure;
using JetBrains.Annotations;
using Superpower;
using Xunit;
using static GraphZen.Language.SyntaxFactory;

namespace GraphZen.Language.Internal
{
    public class QueryDocumentParserTests : ParserTestBase
    {
        private readonly Tokenizer<TokenKind> _sut = SuperPowerTokenizer.Instance;

        [Fact]
        public void ListOfDefinitions()
        {
            var source = @"
query testQuery($var1: String! = ""Foo"") @queryDirective {
    story {
        likeCount
    }
}";


            var tokens = _sut.Tokenize(source);

            var test = Grammar.Document(tokens);
            var expectedValue = Document(new OperationDefinitionSyntax(OperationType.Query,
                SelectionSet(new FieldSyntax(Name("story"),
                    SelectionSet(Field(Name("likeCount"))))),
                Name("testQuery"),
                new[]
                {
                    VariableDefinition(Variable(Name("var1")),
                        NonNull(NamedType(Name("String"))),
                        StringValue("Foo"))
                }, new[]
                {
                    Directive(Name("queryDirective"))
                }));
            Assert.Equal(expectedValue, test.Value);
        }

        [Fact]
        public void QueryShorthand()
        {
            var source = @" 
{
    story {
        likeCount
    }
}
";


            var test = ParseDocument(source);
            var expectedValue = Document(new OperationDefinitionSyntax(OperationType.Query,
                SelectionSet(new FieldSyntax(Name("story"),
                    SelectionSet(Field(Name("likeCount")))))));
            Assert.Equal(expectedValue, test);
        }
    }
}