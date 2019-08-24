#nullable disable
// Copyright (c) GraphZen LLC. All rights reserved.
// Licensed under the GraphZen Community License. See the LICENSE file in the project root for license information.

using FluentAssertions;
using GraphZen.Infrastructure;
using Xunit;

namespace GraphZen.Infrastructure
{
    public class StringExtensionTests
    {
        [Theory]
        [InlineData("f", "F")]
        [InlineData("firstTest", "FirstTest")]
        [InlineData("", "")]
        [InlineData("Howdy", "Howdy")]
        public void FirstCharToUpper(string input, string expectation)
        {
            input.FirstCharToUpper().Should().Be(expectation);
        }


        [Theory]
        [InlineData("F", "f")]
        [InlineData("FirstTest", "firstTest")]
        public void FirstCharToLower(string input, string expectation)
        {
            input.FirstCharToLower().Should().Be(expectation);
        }

        [Theory]
        [InlineData("firstTest", "FIRST_TEST")]
        [InlineData("FirstTest", "FIRST_TEST")]
        [InlineData("Howdy", "HOWDY")]
        [InlineData("Foo bar baz", "FOO_BAR_BAZ")]
        [InlineData("foo_bar", "FOO_BAR")]
        [InlineData("foo-bar", "FOO_BAR")]
        [InlineData("FOO_BAR", "FOO_BAR")]
        [InlineData("FOO-BAR", "FOO_BAR")]
        public void ToUpperSnakeCase(string input, string expectation)
        {
            input.ToUpperSnakeCase().Should().Be(expectation);
        }
    }
}