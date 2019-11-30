using System.Diagnostics.CodeAnalysis;
using FluentAssertions;
using GraphZen.Infrastructure;
using JetBrains.Annotations;
using Xunit;

namespace GraphZen
{
    public class GraphQLContextOptionsTests
    {
        public class CustomContext : GraphQLContext
        {
        }
        [Fact]
        public void it_should_be_created_with_default_context()
        {
            var sut = new GraphQLContextOptions<GraphQLContext>();
            sut.Should().NotBeNull();
        }

        [Fact]
        public void it_should_be_created_with_custom_context()
        {
            var sut = new GraphQLContextOptions<CustomContext>();
            sut.Should().NotBeNull();
        }
    }
}