using System.Diagnostics.CodeAnalysis;
using FluentAssertions;
using GraphZen.Infrastructure;
using JetBrains.Annotations;
using Xunit;

namespace GraphZen.TypeSystem.Tests
{
    public class CoreOptionsExtensionTests
    {
        [Fact]
        public void it_should_have_default_constructor()
        {
            new CoreOptionsExtension().Should().NotBeNull();
        }
    }
}