// Copyright (c) GraphZen LLC. All rights reserved.
// Licensed under the GraphZen Community License. See the LICENSE file in the project root for license information.

using System.Diagnostics.CodeAnalysis;
using GraphZen.Infrastructure;
using GraphZen.LanguageModel.Internal;
using JetBrains.Annotations;
using Xunit;
#nullable disable


namespace GraphZen.LanguageModel
{
    public class BlockStringValueTests
    {
        [Fact]
        public void DoesNotALterTrailingSpaces()
        {
            var rawValue = new[]
            {
                "               ",
                "    Hello,     ",
                "      World!   ",
                "               ",
                "    Yours,     ",
                "      GraphQL. ",
                "               "
            }.ToMultiLineString();

            var result = LanguageHelpers.BlockStringValue(rawValue);

            var expected = new[]
            {
                "Hello,     ",
                "  World!   ",
                "           ",
                "Yours,     ",
                "  GraphQL. "
            }.ToMultiLineString();

            Assert.Equal(expected, result);
        }

        [Fact]
        public void RemovesBlankLeadingAndTrailingLines()
        {
            var rawValue = new[]
            {
                "    ",
                "        ",
                "",
                "    Hello,",
                "      World!",
                "",
                "    Yours,",
                "      GraphQL.",
                "        ",
                "    "
            }.ToMultiLineString();

            var result = LanguageHelpers.BlockStringValue(rawValue);

            var expected = new[]
            {
                "Hello,", "  World!", "", "Yours,", "  GraphQL."
            }.ToMultiLineString();

            Assert.Equal(expected, result);
        }

        [Fact]
        public void RemovesEmptyLeadingAndTrailingLines()
        {
            var rawValue = new[]
            {
                "",
                "",
                "",
                "    Hello,",
                "      World!",
                "",
                "    Yours,",
                "      GraphQL.",
                "",
                ""
            }.ToMultiLineString();

            var result = LanguageHelpers.BlockStringValue(rawValue);

            var expected = new[]
            {
                "Hello,", "  World!", "", "Yours,", "  GraphQL."
            }.ToMultiLineString();

            Assert.Equal(expected, result);
        }

        [Fact]
        public void RemovesUniformIndentationFromAString()
        {
            var rawValue = new[]
            {
                "",
                "    Hello,",
                "      World!",
                "",
                "    Yours,",
                "      GraphQL."
            }.ToMultiLineString();

            var result = LanguageHelpers.BlockStringValue(rawValue);

            var expected = new[]
            {
                "Hello,", "  World!", "", "Yours,", "  GraphQL."
            }.ToMultiLineString();

            Assert.Equal(expected, result);
        }

        [Fact]
        public void RetainsIndentationFromFirstLine()
        {
            var rawValue = new[]
            {
                "    Hello,",
                "      World!",
                "",
                "    Yours,",
                "      GraphQL."
            }.ToMultiLineString();

            var result = LanguageHelpers.BlockStringValue(rawValue);

            var expected = new[]
            {
                "    Hello,", "  World!", "", "Yours,", "  GraphQL."
            }.ToMultiLineString();

            Assert.Equal(expected, result);
        }
    }
}