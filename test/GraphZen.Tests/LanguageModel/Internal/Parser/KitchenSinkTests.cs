// Copyright (c) GraphZen LLC. All rights reserved.
// Licensed under the GraphZen Community License. See the LICENSE file in the project root for license information.

using GraphZen.Infrastructure;
using Xunit;

namespace GraphZen.LanguageModel.Internal.Parser
{
    public class KitchenSinkTests : ParserTests
    {
        private static readonly string KitchenSinkSchema = @"
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
}

mutation likeStory {
  like(story: 123) @defer {
    story {
      id
    }
  }
}

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
}

fragment frag on Friend {
  foo(size: $size, bar: $b, obj: {key: ""value""})
}

{
  unnamed(truthy: true, falsey: false, nullish: null),
  query
}
";

        [Fact]
        public void CanParseKitchenSink()
        {
            ParseDocument(KitchenSinkSchema);
        }

        [Fact]
        public void CanRoundTripKitchenSink()
        {
            var doc = ParseDocument(KitchenSinkSchema);
            var fromPrintedDoc = ParseDocument(doc.ToSyntaxString());
            Assert.Equal(doc, fromPrintedDoc);
        }
    }
}