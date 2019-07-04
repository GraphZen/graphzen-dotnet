// Copyright (c) GraphZen LLC. All rights reserved.
// Licensed under the GraphZen Community License. See the LICENSE file in the project root for license information.

using GraphZen.Infrastructure;
using GraphZen.LanguageModel;
using JetBrains.Annotations;
using Xunit;
using static GraphZen.LanguageModel.SyntaxFactory;

namespace GraphZen.Language.Internal
{
    public class ScalarTypeDefinitionParsingTests : ParserTestBase
    {
        [Fact]
        public void AnnotatedScalar()
        {
            var result = ParseDocument("scalar AnnotatedScalar @onScalar");
            var expected =
                Document(new ScalarTypeDefinitionSyntax(Name("AnnotatedScalar"), null,
                    new[] {Directive(Name("onScalar"))}));
            Assert.Equal(expected, result);
            Assert.Equal(expected, PrintAndParse(result));
        }

        [Fact]
        public void SimpleScalar()
        {
            var result = ParseDocument("scalar CustomScalar");
            var expected =
                Document(ScalarTypeDefinition(Name("CustomScalar")));
            Assert.Equal(expected, result);
            Assert.Equal(expected, PrintAndParse(result));
        }
    }
}