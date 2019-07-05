// Copyright (c) GraphZen LLC. All rights reserved.
// Licensed under the GraphZen Community License. See the LICENSE file in the project root for license information.

using FluentAssertions;
using GraphZen.Infrastructure;
using Xunit;

namespace GraphZen.Infrastructure
{
    public class SuggestionListTests
    {
        [Fact]
        public void ReturnsEmptyArrayWhenThereAreNoOptions()
        {
            StringUtils.GetSuggestionList("input").Should().BeEquivalentTo();
        }

        [Fact]
        public void ReturnsOptionsSortedBasedOnSimilarity()
        {
            StringUtils.GetSuggestionList("abc", "a", "ab", "abc").Should().BeEquivalentTo("abc", "ab");
        }

        [Fact]
        public void ReturnsResultsWhenInputIsEmpty()
        {
            StringUtils.GetSuggestionList("", "a").Should().BeEquivalentTo("a");
        }
    }
}