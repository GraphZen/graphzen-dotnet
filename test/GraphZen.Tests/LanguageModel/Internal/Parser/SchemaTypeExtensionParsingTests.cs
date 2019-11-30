// Copyright (c) GraphZen LLC. All rights reserved.
// Licensed under the GraphZen Community License. See the LICENSE file in the project root for license information.

using System.Diagnostics.CodeAnalysis;
using GraphZen.Infrastructure;
using JetBrains.Annotations;
using Xunit;

#nullable disable


namespace GraphZen.LanguageModel.Internal.Parser
{
    public class SchemaTypeExtensionParsingTests : ParserTestBase
    {
        [Fact]
        public void SchemaExtendedWithDirective()
        {
            var result = ParseDocument("extend schema @onSchema");
            var expected =
                SyntaxFactory.Document(new SchemaExtensionSyntax(new[]
                    {SyntaxFactory.Directive(SyntaxFactory.Name("onSchema"))}));
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
            var expected = SyntaxFactory.Document(new SchemaExtensionSyntax(
                new[] { SyntaxFactory.Directive(SyntaxFactory.Name("onSchema")) },
                new[]
                {
                    new OperationTypeDefinitionSyntax(OperationType.Subscription,
                        SyntaxFactory.NamedType(SyntaxFactory.Name("SubscriptionType")))
                }));
            Assert.Equal(expected, result);
            Assert.Equal(expected, PrintAndParse(result));
        }
    }
}