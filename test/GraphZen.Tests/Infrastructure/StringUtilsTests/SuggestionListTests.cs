// Copyright (c) GraphZen LLC. All rights reserved.
// Licensed under the GraphZen Community License. See the LICENSE file in the project root for license information.

using GraphZen.Infrastructure;
using static GraphZen.Infrastructure.StringUtils;


namespace GraphZen.Tests.Infrastructure.StringUtilsTests;

public class SuggestionListTests
{
    [Fact]
    public void ReturnsEmptyArrayWhenThereAreNoOptions()
    {
        Assert.Empty(GetSuggestionList("input"));
    }

    [Fact]
    public void ReturnsOptionsSortedBasedOnSimilarity()
    {
        Assert.Equivalent(new[] { "abc", "ab" }, GetSuggestionList("abc", "a", "ab", "abc"));
    }

    [Fact]
    public void ReturnsResultsWhenInputIsEmpty()
    {
        Assert.Equivalent(new[] { "a" }, GetSuggestionList("", "a"));
    }
}