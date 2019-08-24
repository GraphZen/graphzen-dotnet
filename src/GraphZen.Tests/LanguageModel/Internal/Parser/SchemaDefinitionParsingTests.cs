// Copyright (c) GraphZen LLC. All rights reserved.
// Licensed under the GraphZen Community License. See the LICENSE file in the project root for license information.
using JetBrains.Annotations;
#nullable disable


using GraphZen.Infrastructure;
using Xunit;

namespace GraphZen.LanguageModel.Internal.Parser
{
    public class SchemaDefinitionParsingTests : ParserTestBase
    {
        [Fact]
        public void SchemaDefinitionNode()
        {
            var gql = @"
schema {
  query: QueryType
  mutation: MutationType
}
";
            var result = ParseDocument(gql);

            var expected = SyntaxFactory.Document(new SchemaDefinitionSyntax(new[]
            {
                new OperationTypeDefinitionSyntax(OperationType.Query,
                    SyntaxFactory.NamedType(SyntaxFactory.Name("QueryType"))),
                new OperationTypeDefinitionSyntax(OperationType.Mutation,
                    SyntaxFactory.NamedType(SyntaxFactory.Name("MutationType")))
            }));

            Assert.Equal(expected, result);
            Assert.Equal(expected, PrintAndParse(result));
        }
    }
}