// Copyright (c) GraphZen LLC. All rights reserved.
// Licensed under the GraphZen Community License. See the LICENSE file in the project root for license information.

using Microsoft.Extensions.DependencyInjection;

namespace GraphZen.AspNetCore.Server.Tests;

[NoReorder]
public class ServiceCollectionExtensionTests
{
    [Fact]
    public void should_share_IServiceCollection_namespace() =>
        Assert.Equal(typeof(IServiceCollection).Namespace,
            typeof(GraphZenServiceCollectionExtensions).Namespace);

    [Fact]
    public void add_default_graphql_context_resolves()
    {
        var services = new ServiceCollection();

        services.AddGraphQLContext<TestGraphQLContext>();

        var serviceProvider = services.BuildServiceProvider();
        GraphQLContext? context1;
        GraphQLContext? context2;

        using (var scope = serviceProvider.GetRequiredService<IServiceScopeFactory>().CreateScope())
        {
            context1 = scope.ServiceProvider.GetService<GraphQLContext>();
            Assert.NotNull(context1);
        }

        using (var scope = serviceProvider.GetRequiredService<IServiceScopeFactory>().CreateScope())
        {
            context2 = scope.ServiceProvider.GetService<GraphQLContext>();
            Assert.NotSame(context1, context2);
        }

        Assert.Same(context2!.Schema, context1!.Schema);
    }


    public class TestGraphQLContext : GraphQLContext
    {
    }

    [Fact]
    public void add_custom_graphql_context_resolves_correctly()
    {
        var services = new ServiceCollection();

        services.AddGraphQLContext<TestGraphQLContext>();

        var serviceProvider = services.BuildServiceProvider();
        GraphQLContext? context1;
        GraphQLContext? context2;
        GraphQLContext? context3;
        GraphQLContext? context4;

        using (var scope = serviceProvider.GetRequiredService<IServiceScopeFactory>().CreateScope())
        {
            context1 = scope.ServiceProvider.GetService<GraphQLContext>();
            Assert.NotNull(context1);
            context2 = scope.ServiceProvider.GetService<TestGraphQLContext>();
            Assert.NotNull(context2);
            Assert.Same(context2, context1);
        }

        using (var scope = serviceProvider.GetRequiredService<IServiceScopeFactory>().CreateScope())
        {
            context3 = scope.ServiceProvider.GetService<GraphQLContext>();
            context4 = scope.ServiceProvider.GetService<TestGraphQLContext>();
            Assert.Same(context4, context3);
            Assert.NotSame(context1, context3);
            Assert.NotSame(context1, context4);
        }

        Assert.NotNull(context1!.Schema);
        Assert.NotNull(context3!.Schema);
        Assert.Same(context3.Schema, context1.Schema);
    }


    public class GraphQLContextNoOptionsConstructor : GraphQLContext
    {
    }

    [Fact]
    public void add_custom_context_with_no_options_ctor_with_options_throws_exception()
    {
        var services = new ServiceCollection();
        var addContext = () => { services.AddGraphQLContext<GraphQLContextNoOptionsConstructor>(options => { }); };
        var ex = Assert.Throws<ArgumentException>(addContext);
        Assert.Equal(
            $"AddGraphQLContext was called with configuration, but the context type '{typeof(GraphQLContextNoOptionsConstructor)}' only declares a parameterless constructor. This means that the configuration passed to AddGraphQLContext will never be used. If configuration is passed to AddGraphQLContext, then '{typeof(GraphQLContextNoOptionsConstructor)}' should declare a constructor that accepts a GraphQLContextOptions<GraphQLContextNoOptionsConstructor> and must pass it to the base constructor for GraphQLContext.",
            ex.Message);
    }
}
