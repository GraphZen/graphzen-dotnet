// Copyright (c) GraphZen LLC. All rights reserved.
// Licensed under the GraphZen Community License. See the LICENSE file in the project root for license information.

using GraphZen.Infrastructure;

using Xunit;

namespace GraphZen.LanguageModel.Internal.Parser
{
    public class ScalarTypeDefinitionParsingTests : ParserTestBase
    {
        [Fact]
        public void AnnotatedScalar()
        {
            var result = ParseDocument("scalar AnnotatedScalar @onScalar");
            var expected =
                SyntaxFactory.Document(new ScalarTypeDefinitionSyntax(SyntaxFactory.Name("AnnotatedScalar"), null,
                    new[] {SyntaxFactory.Directive(SyntaxFactory.Name("onScalar"))}));
            Assert.Equal(expected, result);
            Assert.Equal(expected, PrintAndParse(result));
        }

        [Fact]
        public void SimpleScalar()
        {
            var result = ParseDocument("scalar CustomScalar");
            var expected =
                SyntaxFactory.Document(SyntaxFactory.ScalarTypeDefinition(SyntaxFactory.Name("CustomScalar")));
            Assert.Equal(expected, result);
            Assert.Equal(expected, PrintAndParse(result));
        }
    }
}