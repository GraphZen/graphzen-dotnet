// Copyright (c) GraphZen LLC. All rights reserved.
// Licensed under the GraphZen Community License. See the LICENSE file in the project root for license information.

using System.Diagnostics.CodeAnalysis;
using GraphZen.Infrastructure;
using JetBrains.Annotations;
using Xunit;
using Xunit.Sdk;

namespace GraphZen.Infrastructure
{
    public class StringAssertTests
    {
        [Theory]
        [InlineData("a", "a")]
        public void it_should_not_fail_for_equal_strings(string actual, string expected)
        {
            StringAssert.Equal(actual, expected, true);
            StringAssert.Equal(actual, expected, new StringDiffOptions());
            StringAssert.Equal(actual, expected, opt => { });
        }

        [Theory]
        [InlineData("a", "b")]
        [InlineData("a", "A")]
        public void it_should_fail_for_unequal_strings(string actual, string expected)
        {
            var expectedDiff = actual.GetDiff(expected);
            Assert.NotNull(expectedDiff);
            Assert.NotEmpty(expectedDiff);
            var ex1 = Assert.Throws<XunitException>(() => StringAssert.Equal(actual, expected, true));
            Assert.Equal(expectedDiff, ex1.Message);
            var ex2 = Assert.Throws<XunitException>(() => StringAssert.Equal(actual, expected, new StringDiffOptions()));
            Assert.Equal(expectedDiff, ex2.Message);
            var ex3 = Assert.Throws<XunitException>(() => StringAssert.Equal(actual, expected, opt => { }));
            Assert.Equal(expectedDiff, ex3.Message);
        }
    }
}
