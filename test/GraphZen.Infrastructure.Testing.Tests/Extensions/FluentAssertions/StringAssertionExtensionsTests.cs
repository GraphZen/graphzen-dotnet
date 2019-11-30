// Copyright (c) GraphZen LLC. All rights reserved.
// Licensed under the GraphZen Community License. See the LICENSE file in the project root for license information.

using System;
using System.Diagnostics.CodeAnalysis;
using FluentAssertions;
using GraphZen.Infrastructure;
using JetBrains.Annotations;
using Xunit;
using Xunit.Sdk;

namespace GraphZen.Infrastructure
{
    public class StringAssertionExtensionsTests
    {
        [Theory]
        [InlineData("a", "a")]
        public void it_should_not_fail_for_equal_strings(string actual, string expected)
        {
            actual.Should().Be(expected, true);
            actual.Should().Be(expected, new StringDiffOptions());
            actual.Should().Be(expected, opt => { });
        }

        [Theory]
        [InlineData("a", "b")]
        [InlineData("a", "A")]
        public void it_should_fail_for_unequal_strings(string actual, string expected)
        {
            var expectedDiff = actual.GetDiff(expected);
            expectedDiff.Should().NotBeNullOrEmpty();
            Action booleanOverload = () => actual.Should().Be(expected, true);
            Action optionsOverload = () => actual.Should().Be(expected, new StringDiffOptions());
            Action optionsActionOverload = () => actual.Should().Be(expected, opt => { });
            booleanOverload.Should().Throw<XunitException>().WithMessage(expectedDiff);
            optionsOverload.Should().Throw<XunitException>().WithMessage(expectedDiff);
            optionsActionOverload.Should().Throw<XunitException>().WithMessage(expectedDiff);
        }
    }
}