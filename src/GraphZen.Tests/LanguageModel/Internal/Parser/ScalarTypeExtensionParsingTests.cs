// Copyright (c) GraphZen LLC. All rights reserved.
// Licensed under the GraphZen Community License. See the LICENSE file in the project root for license information.
using JetBrains.Annotations;
#nullable disable


using GraphZen.Infrastructure;
using Xunit;

namespace GraphZen.LanguageModel.Internal.Parser
{
    public class ScalarTypeExtensionParsingTests : ParserTestBase
    {
        [Fact]
        public void AnnotatedScalarExtension()
        {
            var result = ParseDocument("extend scalar CustomScalar @onScalar");
            var expected =
                SyntaxFactory.Document(new ScalarTypeExtensionSyntax(SyntaxFactory.Name("CustomScalar"),
                    new[] { SyntaxFactory.Directive(SyntaxFactory.Name("onScalar")) }));
            Assert.Equal(expected, result);
            Assert.Equal(expected, PrintAndParse(result));
        }
    }
}