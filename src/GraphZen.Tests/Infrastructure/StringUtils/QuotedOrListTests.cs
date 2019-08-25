// Copyright (c) GraphZen LLC. All rights reserved.
// Licensed under the GraphZen Community License. See the LICENSE file in the project root for license information.

#nullable disable
using System;
using System.Diagnostics.CodeAnalysis;
using FluentAssertions;
using GraphZen.Infrastructure;
using JetBrains.Annotations;
using Xunit;
using static GraphZen.Infrastructure.StringUtils;

namespace GraphZen.Infrastructure
{
    public class QuotedOrListTests
    {
        [Fact]
        public void DoesNotAcceptAnEmptyList()
        {
            ((Action)(() => QuotedOrList())).Should().Throw<Exception>();
        }


        [Fact]
        public void LimitsToFiveItems()
        {
            QuotedOrList("A", "B", "C", "D", "E", "F")
                .Should().Be("\"A\", \"B\", \"C\", \"D\", or \"E\"");
        }

        [Fact]
        public void ReturnsCommaSeperatedManyItemsList()
        {
            QuotedOrList("A", "B", "C").Should().Be("\"A\", \"B\", or \"C\"");
        }

        [Fact]
        public void ReturnsSingleQuotedItem()
        {
            QuotedOrList("A").Should().Be(@"""A""");
        }

        [Fact]
        public void ReturnsTwoItemList()
        {
            QuotedOrList("A", "B").Should().Be("\"A\" or \"B\"");
        }
    }
}