// Copyright (c) GraphZen LLC. All rights reserved.
// Licensed under the GraphZen Community License. See the LICENSE file in the project root for license information.

using System.Diagnostics.CodeAnalysis;
using FluentAssertions;
using GraphZen.Infrastructure;
using JetBrains.Annotations;
using Xunit;
using static GraphZen.Infrastructure.StringUtils;

#nullable disable


namespace GraphZen.Infrastructure.StringUtilsTests
{
    public class SuggestionListTests
    {
        [Fact]
        public void ReturnsEmptyArrayWhenThereAreNoOptions()
        {
            GetSuggestionList("input").Should().BeEquivalentTo();
        }

        [Fact]
        public void ReturnsOptionsSortedBasedOnSimilarity()
        {
            GetSuggestionList("abc", "a", "ab", "abc").Should().BeEquivalentTo("abc", "ab");
        }

        [Fact]
        public void ReturnsResultsWhenInputIsEmpty()
        {
            GetSuggestionList("", "a").Should().BeEquivalentTo("a");
        }
    }
}