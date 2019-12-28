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
    public class OperationDefinitionParsingTests : ParserTestBase
    {
        [Fact]
        public void ComplexQueryOperation()
        {
            var query = @"
query queryName($foo: ComplexType, $site: Site = MOBILE) {
  whoever123is: node(id: [123, 456]) {
    id ,
    ... on User @defer {
      field2 {
        id ,
        alias: field1(first:10, after:$foo,) @include(if: $foo) {
          id,
          ...frag
        }
      }
    }
    ... @skip(unless: $foo) {
      id
    }
    ... {
      id
    }
  }
}";

            var result = ParseDocument(query);

            var expected = SyntaxFactory.Document(new OperationDefinitionSyntax(OperationType.Query,
                SyntaxFactory.SelectionSet(new FieldSyntax(SyntaxFactory.Name("node"),
                    SyntaxFactory.Name("whoever123is"), new[]
                    {
                        SyntaxFactory.Argument(SyntaxFactory.Name("id"),
                            SyntaxFactory.ListValue(SyntaxFactory.IntValue(123), SyntaxFactory.IntValue(456)))
                    }, null, SyntaxFactory.SelectionSet(SyntaxFactory.Field(SyntaxFactory.Name("id")),
                        new InlineFragmentSyntax(SyntaxFactory.SelectionSet(new FieldSyntax(
                                SyntaxFactory.Name("field2"),
                                SyntaxFactory.SelectionSet(SyntaxFactory.Field(SyntaxFactory.Name("id")),
                                    new FieldSyntax(
                                        SyntaxFactory.Name("field1"), SyntaxFactory.Name("alias"), new[]
                                        {
                                            SyntaxFactory.Argument(SyntaxFactory.Name("first"),
                                                SyntaxFactory.IntValue(10)),
                                            SyntaxFactory.Argument(SyntaxFactory.Name("after"),
                                                SyntaxFactory.Variable(SyntaxFactory.Name("foo")))
                                        }, new[]
                                        {
                                            new DirectiveSyntax(SyntaxFactory.Name("include"), new[]
                                            {
                                                SyntaxFactory.Argument(SyntaxFactory.Name("if"),
                                                    SyntaxFactory.Variable(SyntaxFactory.Name("foo")))
                                            })
                                        },
                                        SyntaxFactory.SelectionSet(SyntaxFactory.Field(SyntaxFactory.Name("id")),
                                            SyntaxFactory.FragmentSpread(SyntaxFactory.Name("frag"))))))),
                            SyntaxFactory.NamedType(SyntaxFactory.Name("User")), new[]
                            {
                                SyntaxFactory.Directive(SyntaxFactory.Name("defer"))
                            }), new InlineFragmentSyntax(
                            SyntaxFactory.SelectionSet(SyntaxFactory.Field(SyntaxFactory.Name("id"))), null, new[]
                            {
                                new DirectiveSyntax(SyntaxFactory.Name("skip"),
                                    new[]
                                    {
                                        SyntaxFactory.Argument(SyntaxFactory.Name("unless"),
                                            SyntaxFactory.Variable(SyntaxFactory.Name("foo")))
                                    })
                            }),
                        new InlineFragmentSyntax(
                            SyntaxFactory.SelectionSet(SyntaxFactory.Field(SyntaxFactory.Name("id"))))))),
                SyntaxFactory.Name("queryName"), new[]
                {
                    SyntaxFactory.VariableDefinition(SyntaxFactory.Variable(SyntaxFactory.Name("foo")),
                        SyntaxFactory.NamedType(SyntaxFactory.Name("ComplexType"))),
                    SyntaxFactory.VariableDefinition(SyntaxFactory.Variable(SyntaxFactory.Name("site")),
                        SyntaxFactory.NamedType(SyntaxFactory.Name("Site")),
                        SyntaxFactory.EnumValue(SyntaxFactory.Name("MOBILE")))
                }));
            Assert.Equal(expected, result);
            Assert.Equal(expected, PrintAndParse(result));
        }


        [Fact]
        public void MutationOperation()
        {
            var query = @"
mutation likeStory {
  like(story: 123) @defer {
    story {
      id
    }
  }
}";
            var result = ParseDocument(query);
            var expected = SyntaxFactory.Document(new OperationDefinitionSyntax(OperationType.Mutation,
                SyntaxFactory.SelectionSet(new FieldSyntax(
                    SyntaxFactory.Name("like"), null, new[]
                    {
                        SyntaxFactory.Argument(SyntaxFactory.Name("story"), SyntaxFactory.IntValue(123))
                    }, new[]
                    {
                        SyntaxFactory.Directive(SyntaxFactory.Name("defer"))
                    },
                    SyntaxFactory.SelectionSet(new FieldSyntax(SyntaxFactory.Name("story"),
                        SyntaxFactory.SelectionSet(SyntaxFactory.Field(SyntaxFactory.Name("id"))))))),
                SyntaxFactory.Name("likeStory")));


            Assert.Equal(expected, result);
            Assert.Equal(expected, PrintAndParse(result));
        }

        [Fact]
        public void SubscriptionOperation()
        {
            var query = @"
subscription StoryLikeSubscription($input: StoryLikeSubscribeInput) {
  storyLikeSubscribe(input: $input) {
    story {
      likers {
        count
      }
      likeSentence {
        text
      }
    }
  }
}";
            var result = ParseDocument(query);
            var expected = SyntaxFactory.Document(new OperationDefinitionSyntax(OperationType.Subscription,
                SyntaxFactory.SelectionSet(
                    new FieldSyntax(SyntaxFactory.Name("storyLikeSubscribe"), null, new[]
                        {
                            SyntaxFactory.Argument(SyntaxFactory.Name("input"),
                                SyntaxFactory.Variable(SyntaxFactory.Name("input")))
                        }, null,
                        SyntaxFactory.SelectionSet(new FieldSyntax(SyntaxFactory.Name("story"),
                            SyntaxFactory.SelectionSet(
                                new FieldSyntax(SyntaxFactory.Name("likers"),
                                    SyntaxFactory.SelectionSet(SyntaxFactory.Field(SyntaxFactory.Name("count")))),
                                new FieldSyntax(SyntaxFactory.Name("likeSentence"),
                                    SyntaxFactory.SelectionSet(SyntaxFactory.Field(SyntaxFactory.Name("text")))))))
                    )), SyntaxFactory.Name("StoryLikeSubscription"), new[]
                {
                    SyntaxFactory.VariableDefinition(SyntaxFactory.Variable(SyntaxFactory.Name("input")),
                        SyntaxFactory.NamedType(SyntaxFactory.Name("StoryLikeSubscribeInput")))
                }));

            Assert.Equal(expected, result);
            Assert.Equal(expected, PrintAndParse(result));
        }
    }
}