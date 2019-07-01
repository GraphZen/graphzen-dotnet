// Copyright (c) GraphZen LLC. All rights reserved.
// Licensed under the GraphZen Community License. See the LICENSE file in the project root for license information.

using GraphZen.Infrastructure;
using JetBrains.Annotations;
using Xunit;
using static GraphZen.Language.SyntaxFactory;

namespace GraphZen.Language.Internal
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

            var expected = Document(new SchemaDefinitionSyntax(new[]
            {
                new OperationTypeDefinitionSyntax(OperationType.Query,
                    NamedType(Name("QueryType"))),
                new OperationTypeDefinitionSyntax(OperationType.Mutation,
                    NamedType(Name("MutationType")))
            }));

            Assert.Equal(expected, result);
            Assert.Equal(expected, PrintAndParse(result));
        }
    }
}