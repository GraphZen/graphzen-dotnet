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
    public class ScalarTypeDefinitionParsingTests : ParserTestBase
    {
        [Fact]
        public void AnnotatedScalar()
        {
            var result = ParseDocument("scalar AnnotatedScalar @onScalar");
            var expected =
                SyntaxFactory.Document(new ScalarTypeDefinitionSyntax(SyntaxFactory.Name("AnnotatedScalar"), null,
                    new[] { SyntaxFactory.Directive(SyntaxFactory.Name("onScalar")) }));
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