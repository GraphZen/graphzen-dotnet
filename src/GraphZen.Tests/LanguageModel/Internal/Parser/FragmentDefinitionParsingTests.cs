#nullable disable
// Copyright (c) GraphZen LLC. All rights reserved.
// Licensed under the GraphZen Community License. See the LICENSE file in the project root for license information.

using GraphZen.Infrastructure;
using Xunit;

namespace GraphZen.LanguageModel.Internal.Parser
{
    public class FragmentDefinitionParsingTests : ParserTestBase
    {
        [Fact]
        public void FragmentDefinition()
        {
            var query = @"
fragment frag on Friend {
  foo(size: $size, bar: $b, obj: {key: ""value"", block: """"""

      block string uses \""""""

  """"""})
}";
            var result = ParseDocument(query);
            var expected = SyntaxFactory.Document(new FragmentDefinitionSyntax(SyntaxFactory.Name("frag"),
                SyntaxFactory.NamedType(SyntaxFactory.Name("Friend")), SyntaxFactory.SelectionSet(
                    new FieldSyntax(SyntaxFactory.Name("foo"), null, new[]
                    {
                        SyntaxFactory.Argument(SyntaxFactory.Name("size"),
                            SyntaxFactory.Variable(SyntaxFactory.Name("size"))),
                        SyntaxFactory.Argument(SyntaxFactory.Name("bar"),
                            SyntaxFactory.Variable(SyntaxFactory.Name("b"))),
                        SyntaxFactory.Argument(SyntaxFactory.Name("obj"), SyntaxFactory.ObjectValue(
                            SyntaxFactory.ObjectField(SyntaxFactory.Name("key"), SyntaxFactory.StringValue("value")),
                            SyntaxFactory.ObjectField(
                                SyntaxFactory.Name("block"),
                                SyntaxFactory.StringValue(@"block string uses \""""""", true)))
                        )
                    }))));

            Assert.Equal(expected, result);
            Assert.Equal(expected, PrintAndParse(result));
        }
    }
}