// Copyright (c) GraphZen LLC. All rights reserved.
// Licensed under the GraphZen Community License. See the LICENSE file in the project root for license information.

using GraphZen.Infrastructure;
using GraphZen.LanguageModel;
using JetBrains.Annotations;
using Xunit;
using static GraphZen.LanguageModel.SyntaxFactory;
using OperationDefinitionSyntax = GraphZen.LanguageModel.OperationDefinitionSyntax;

namespace GraphZen.Language.Internal
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

            var expected = Document(new OperationDefinitionSyntax(OperationType.Query,
                SelectionSet(new FieldSyntax(Name("node"),
                    Name("whoever123is"), new[]
                    {
                        Argument(Name("id"),
                            ListValue(IntValue(123), IntValue(456)))
                    }, null, SelectionSet(Field(Name("id")),
                        new InlineFragmentSyntax(SelectionSet(new FieldSyntax(
                                Name("field2"),
                                SelectionSet(Field(Name("id")),
                                    new FieldSyntax(
                                        Name("field1"), Name("alias"), new[]
                                        {
                                            Argument(Name("first"),
                                                IntValue(10)),
                                            Argument(Name("after"),
                                                Variable(Name("foo")))
                                        }, new[]
                                        {
                                            new DirectiveSyntax(Name("include"), new[]
                                            {
                                                Argument(Name("if"),
                                                    Variable(Name("foo")))
                                            })
                                        },
                                        SelectionSet(Field(Name("id")),
                                            FragmentSpread(Name("frag"))))))),
                            NamedType(Name("User")), new[]
                            {
                                Directive(Name("defer"))
                            }), new InlineFragmentSyntax(
                            SelectionSet(Field(Name("id"))), null, new[]
                            {
                                new DirectiveSyntax(Name("skip"),
                                    new[]
                                    {
                                        Argument(Name("unless"),
                                            Variable(Name("foo")))
                                    })
                            }),
                        new InlineFragmentSyntax(
                            SelectionSet(Field(Name("id"))))))),
                Name("queryName"), new[]
                {
                    VariableDefinition(Variable(Name("foo")),
                        NamedType(Name("ComplexType"))),
                    VariableDefinition(Variable(Name("site")),
                        NamedType(Name("Site")),
                        EnumValue(Name("MOBILE")))
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
            var expected = Document(new OperationDefinitionSyntax(OperationType.Mutation,
                SelectionSet(new FieldSyntax(
                    Name("like"), null, new[]
                    {
                        Argument(Name("story"), IntValue(123))
                    }, new[]
                    {
                        Directive(Name("defer"))
                    },
                    SelectionSet(new FieldSyntax(Name("story"),
                        SelectionSet(Field(Name("id"))))))),
                Name("likeStory")));


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
            var expected = Document(new OperationDefinitionSyntax(OperationType.Subscription,
                SelectionSet(
                    new FieldSyntax(Name("storyLikeSubscribe"), null, new[]
                        {
                            Argument(Name("input"),
                                Variable(Name("input")))
                        }, null,
                        SelectionSet(new FieldSyntax(Name("story"),
                            SelectionSet(
                                new FieldSyntax(Name("likers"),
                                    SelectionSet(Field(Name("count")))),
                                new FieldSyntax(Name("likeSentence"),
                                    SelectionSet(Field(Name("text")))))))
                    )), Name("StoryLikeSubscription"), new[]
                {
                    VariableDefinition(Variable(Name("input")),
                        NamedType(Name("StoryLikeSubscribeInput")))
                }));

            Assert.Equal(expected, result);
            Assert.Equal(expected, PrintAndParse(result));
        }
    }
}