// Copyright (c) GraphZen LLC. All rights reserved.
// Licensed under the GraphZen Community License. See the LICENSE file in the project root for license information.

using System;
using System.Diagnostics.CodeAnalysis;
using FluentAssertions;
using GraphZen.Infrastructure;
using JetBrains.Annotations;
using Microsoft.Extensions.DependencyInjection;
using Xunit;

namespace GraphZen.AspNetCore.Server.Tests
{
    [NoReorder]
    public class ServiceCollectionExtensionTests
    {
        [Fact]
        public void should_share_IServiceCollection_namespace() =>
            typeof(GraphZenServiceCollectionExtensions).Namespace
                .Should().Be(typeof(IServiceCollection).Namespace);

        [Fact]
        public void add_default_graphql_context_resolves()
        {
            var services = new ServiceCollection();

            services.AddGraphQLContext<TestGraphQLContext>();

            var serviceProvider = services.BuildServiceProvider();
            GraphQLContext context1;
            GraphQLContext context2;

            using (var scope = serviceProvider.GetRequiredService<IServiceScopeFactory>().CreateScope())
            {
                context1 = scope.ServiceProvider.GetService<GraphQLContext>();
                context1.Should().NotBeNull();
            }

            using (var scope = serviceProvider.GetRequiredService<IServiceScopeFactory>().CreateScope())
            {
                context2 = scope.ServiceProvider.GetService<GraphQLContext>();
                context2.Should().NotBeSameAs(context1);
            }

            context1.Schema.Should().BeSameAs(context2.Schema);
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
            GraphQLContext context1;
            GraphQLContext context2;
            GraphQLContext context3;
            GraphQLContext context4;

            using (var scope = serviceProvider.GetRequiredService<IServiceScopeFactory>().CreateScope())
            {
                context1 = scope.ServiceProvider.GetService<GraphQLContext>();
                context1.Should().NotBeNull();
                context2 = scope.ServiceProvider.GetService<TestGraphQLContext>();
                context2.Should().NotBeNull();
                context1.Should().BeSameAs(context2);
            }

            using (var scope = serviceProvider.GetRequiredService<IServiceScopeFactory>().CreateScope())
            {
                context3 = scope.ServiceProvider.GetService<GraphQLContext>();
                context4 = scope.ServiceProvider.GetService<TestGraphQLContext>();
                context3.Should().BeSameAs(context4);
                context3.Should().NotBeSameAs(context1);
                context4.Should().NotBeSameAs(context1);
            }

            context1.Schema.Should().NotBeNull();
            context3.Schema.Should().NotBeNull();
            context1.Schema.Should().BeSameAs(context3.Schema);
        }


        public class GraphQLContextNoOptionsConstructor : GraphQLContext
        {
        }

        [Fact]
        public void add_custom_context_with_no_options_ctor_with_options_throws_exception()
        {
            var services = new ServiceCollection();
            Action addContext = () =>
            {
                services.AddGraphQLContext<GraphQLContextNoOptionsConstructor>(options => { });
            };
            addContext.Should().Throw<ArgumentException>().WithMessage(
                $"AddGraphQLContext was called with configuration, but the context type '{typeof(GraphQLContextNoOptionsConstructor)}' only declares a parameterless constructor. This means that the configuration passed to AddGraphQLContext will never be used. If configuration is passed to AddGraphQLContext, then '{typeof(GraphQLContextNoOptionsConstructor)}' should declare a constructor that accepts a GraphQLContextOptions<GraphQLContextNoOptionsConstructor> and must pass it to the base constructor for GraphQLContext.");
        }
    }
}