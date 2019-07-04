// Copyright (c) GraphZen LLC. All rights reserved.
// Licensed under the GraphZen Community License. See the LICENSE file in the project root for license information.

using FluentAssertions;
using GraphZen.Infrastructure;

using Xunit;

namespace GraphZen
{
    public class FixIndentTests
    {
        [Fact]
        public void RemovesIndentationInTypicalUsage()
        {
            var result = @"
                type Query {
                  me: User
                }

                type User {
                  id: ID
                  name: String
                }
            ".Dedent();


            var expected = new[]
            {
                "type Query {",
                "  me: User",
                "}",
                "",
                "type User {",
                "  id: ID",
                "  name: String",
                "}",
                ""
            }.ToMultiLineString();

            result.Should().Be(expected);
        }

        [Fact]
        public void RemovesOnlyFirstLevelOfIndentation()
        {
            var result = @"
            qux
              quux
                quuux
                  quuuux".Dedent();

            var expected = new[]
            {
                "qux", "  quux", "    quuux", "      quuuux"
            }.ToMultiLineString();
            result.Should().Be(expected);
        }
    }
}