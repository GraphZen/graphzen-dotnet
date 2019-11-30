// Copyright (c) GraphZen LLC. All rights reserved.
// Licensed under the GraphZen Community License. See the LICENSE file in the project root for license information.

using System.Diagnostics.CodeAnalysis;
using FluentAssertions;
using GraphZen.Infrastructure;
using JetBrains.Annotations;
using Xunit;

namespace GraphZen
{
    public class GraphQLContextOptionsBuilderTests
    {
        public class CustomContext : GraphQLContext
        {
        }

        [Fact]
        public void it_should_be_created_with_default_constructor()
        {
            var sut = new GraphQLContextOptionsBuilder();
            sut.Options.GetType().Should().Be(typeof(GraphQLContextOptions<GraphQLContext>));
        }

        [Fact]
        public void it_should_be_created_with_custom_options()
        {
            var options = new GraphQLContextOptions<GraphQLContext>();
            var sut = new GraphQLContextOptionsBuilder(options);
            sut.Options.Should().Be(options);
        }

        [Fact]
        public void it_should_be_created_with_default_constructor_from_custom_context()
        {
            var sut = new GraphQLContextOptionsBuilder<CustomContext>();
            sut.Options.GetType().Should().Be(typeof(GraphQLContextOptions<CustomContext>));
        }

        [Fact]
        public void it_should_be_created_with_custom_options_from_custom_context()
        {
            var options = new GraphQLContextOptions<CustomContext>();
            var sut = new GraphQLContextOptionsBuilder<CustomContext>(options);
            sut.Options.Should().Be(options);
        }
    }
}