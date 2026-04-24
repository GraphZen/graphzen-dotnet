// Copyright (c) GraphZen LLC. All rights reserved.
// Licensed under the GraphZen Community License. See the LICENSE file in the project root for license information.

using GraphZen.Infrastructure;

namespace GraphZen.TypeSystem.Tests;

public class GraphQLContextOptionsBuilderTests
{
    [Fact]
    public void it_should_be_created_with_default_constructor()
    {
        var sut = new GraphQLContextOptionsBuilder();
        Assert.Equal(typeof(GraphQLContextOptions<GraphQLContext>), sut.Options.GetType());
    }

    [Fact]
    public void it_should_be_created_with_custom_options()
    {
        var options = new GraphQLContextOptions<GraphQLContext>();
        var sut = new GraphQLContextOptionsBuilder(options);
        Assert.Equal(options, sut.Options);
    }

    [Fact]
    public void it_should_be_created_with_default_constructor_from_custom_context()
    {
        var sut = new GraphQLContextOptionsBuilder<CustomContext>();
        Assert.Equal(typeof(GraphQLContextOptions<CustomContext>), sut.Options.GetType());
    }

    [Fact]
    public void it_should_be_created_with_custom_options_from_custom_context()
    {
        var options = new GraphQLContextOptions<CustomContext>();
        var sut = new GraphQLContextOptionsBuilder<CustomContext>(options);
        Assert.Equal(options, sut.Options);
    }

    public class CustomContext : GraphQLContext
    {
    }
}