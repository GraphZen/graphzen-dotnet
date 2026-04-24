// Copyright (c) GraphZen LLC. All rights reserved.
// Licensed under the GraphZen Community License. See the LICENSE file in the project root for license information.

using GraphZen.Infrastructure;
using static GraphZen.Infrastructure.StringUtils;

namespace GraphZen.Tests.Infrastructure.StringUtilsTests;

public class QuotedOrListTests
{
    [Fact]
    public void DoesNotAcceptAnEmptyList()
    {
        Assert.ThrowsAny<Exception>(() => QuotedOrList());
    }


    [Fact]
    public void LimitsToFiveItems()
    {
        Assert.Equal("\"A\", \"B\", \"C\", \"D\", or \"E\"", QuotedOrList("A", "B", "C", "D", "E", "F"));
    }

    [Fact]
    public void ReturnsCommaSeperatedManyItemsList()
    {
        Assert.Equal("\"A\", \"B\", or \"C\"", QuotedOrList("A", "B", "C"));
    }

    [Fact]
    public void ReturnsSingleQuotedItem()
    {
        Assert.Equal(@"""A""", QuotedOrList("A"));
    }

    [Fact]
    public void ReturnsTwoItemList()
    {
        Assert.Equal("\"A\" or \"B\"", QuotedOrList("A", "B"));
    }
}