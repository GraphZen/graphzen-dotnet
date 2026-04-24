// Copyright (c) GraphZen LLC. All rights reserved.
// Licensed under the GraphZen Community License. See the LICENSE file in the project root for license information.

using GraphZen.Infrastructure;

namespace GraphZen.TypeSystem.Tests;

public class GraphQLContextOptionsTests
{
    [Fact]
    public void it_should_be_created_with_default_context()
    {
        var sut = new GraphQLContextOptions<GraphQLContext>();
        Assert.NotNull(sut);
    }

    [Fact]
    public void it_should_be_created_with_custom_context()
    {
        var sut = new GraphQLContextOptions<CustomContext>();
        Assert.NotNull(sut);
    }

    public class CustomContext : GraphQLContext
    {
    }
}