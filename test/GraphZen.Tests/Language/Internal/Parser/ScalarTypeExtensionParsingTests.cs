// Copyright (c) GraphZen LLC. All rights reserved.
// Licensed under the GraphZen Community License. See the LICENSE file in the project root for license information.

using GraphZen.Infrastructure;
using GraphZen.LanguageModel;
using JetBrains.Annotations;
using Xunit;
using static GraphZen.LanguageModel.SyntaxFactory;

namespace GraphZen.Language.Internal
{
    public class ScalarTypeExtensionParsingTests : ParserTestBase
    {
        [Fact]
        public void AnnotatedScalarExtension()
        {
            var result = ParseDocument("extend scalar CustomScalar @onScalar");
            var expected =
                Document(new ScalarTypeExtensionSyntax(Name("CustomScalar"),
                    new[] {Directive(Name("onScalar"))}));
            Assert.Equal(expected, result);
            Assert.Equal(expected, PrintAndParse(result));
        }
    }
}