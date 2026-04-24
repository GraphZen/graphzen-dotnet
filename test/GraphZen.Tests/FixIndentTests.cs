// Copyright (c) GraphZen LLC. All rights reserved.
// Licensed under the GraphZen Community License. See the LICENSE file in the project root for license information.

using GraphZen.Infrastructure;

namespace GraphZen.Tests;

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

        Assert.Equal(expected, result);
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
        Assert.Equal(expected, result);
    }
}