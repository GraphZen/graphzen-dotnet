#nullable disable
// Copyright (c) GraphZen LLC. All rights reserved.
// Licensed under the GraphZen Community License. See the LICENSE file in the project root for license information.

using GraphZen.Infrastructure;
using Superpower;
using Xunit;

namespace GraphZen.LanguageModel.Internal.Parser
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

            var test = Grammar.Grammar.Document(tokens);
            var expectedValue = SyntaxFactory.Document(new OperationDefinitionSyntax(OperationType.Query,
                SyntaxFactory.SelectionSet(new FieldSyntax(SyntaxFactory.Name("story"),
                    SyntaxFactory.SelectionSet(SyntaxFactory.Field(SyntaxFactory.Name("likeCount"))))),
                SyntaxFactory.Name("testQuery"),
                new[]
                {
                    SyntaxFactory.VariableDefinition(SyntaxFactory.Variable(SyntaxFactory.Name("var1")),
                        SyntaxFactory.NonNull(SyntaxFactory.NamedType(SyntaxFactory.Name("String"))),
                        SyntaxFactory.StringValue("Foo"))
                }, new[]
                {
                    SyntaxFactory.Directive(SyntaxFactory.Name("queryDirective"))
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
            var expectedValue = SyntaxFactory.Document(new OperationDefinitionSyntax(OperationType.Query,
                SyntaxFactory.SelectionSet(new FieldSyntax(SyntaxFactory.Name("story"),
                    SyntaxFactory.SelectionSet(SyntaxFactory.Field(SyntaxFactory.Name("likeCount")))))));
            Assert.Equal(expectedValue, test);
        }
    }
}