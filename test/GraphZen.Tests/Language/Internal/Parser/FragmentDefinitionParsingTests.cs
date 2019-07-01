// Copyright (c) GraphZen LLC. All rights reserved.
// Licensed under the GraphZen Community License. See the LICENSE file in the project root for license information.

using GraphZen.Infrastructure;
using JetBrains.Annotations;
using Xunit;
using static GraphZen.Language.SyntaxFactory;

namespace GraphZen.Language.Internal
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
            var expected = Document(new FragmentDefinitionSyntax(Name("frag"),
                NamedType(Name("Friend")), SelectionSet(
                    new FieldSyntax(Name("foo"), null, new[]
                    {
                        Argument(Name("size"),
                            Variable(Name("size"))),
                        Argument(Name("bar"),
                            Variable(Name("b"))),
                        Argument(Name("obj"), ObjectValue(
                            ObjectField(Name("key"), StringValue("value")),
                            ObjectField(
                                Name("block"),
                                StringValue(@"block string uses \""""""", true)))
                        )
                    }))));

            Assert.Equal(expected, result);
            Assert.Equal(expected, PrintAndParse(result));
        }
    }
}