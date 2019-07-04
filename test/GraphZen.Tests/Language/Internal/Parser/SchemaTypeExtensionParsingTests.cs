// Copyright (c) GraphZen LLC. All rights reserved.
// Licensed under the GraphZen Community License. See the LICENSE file in the project root for license information.

using GraphZen.Infrastructure;
using GraphZen.LanguageModel;
using JetBrains.Annotations;
using Xunit;
using static GraphZen.LanguageModel.SyntaxFactory;

namespace GraphZen.Language.Internal
{
    public class SchemaTypeExtensionParsingTests : ParserTestBase
    {
        [Fact]
        public void SchemaExtendedWithDirective()
        {
            var result = ParseDocument("extend schema @onSchema");
            var expected =
                Document(new SchemaExtensionSyntax(new[]
                    {Directive(Name("onSchema"))}));
            Assert.Equal(expected, result);
            Assert.Equal(expected, PrintAndParse(result));
        }

        [Fact]
        public void SchemaExtendedWithOperationType()
        {
            var result = ParseDocument(@"
extend schema @onSchema {
  subscription: SubscriptionType
}");
            var expected = Document(new SchemaExtensionSyntax(
                new[] {Directive(Name("onSchema"))},
                new[]
                {
                    new OperationTypeDefinitionSyntax(OperationType.Subscription,
                        NamedType(Name("SubscriptionType")))
                }));
            Assert.Equal(expected, result);
            Assert.Equal(expected, PrintAndParse(result));
        }
    }
}