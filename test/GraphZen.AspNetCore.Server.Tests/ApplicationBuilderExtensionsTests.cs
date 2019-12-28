// Copyright (c) GraphZen LLC. All rights reserved.
// Licensed under the GraphZen Community License. See the LICENSE file in the project root for license information.

using System.Diagnostics.CodeAnalysis;
using FluentAssertions;
using GraphZen.Infrastructure;
using JetBrains.Annotations;
using Microsoft.AspNetCore.Builder;
using Xunit;

namespace GraphZen.AspNetCore.Server.Tests
{
    [NoReorder]
    public class ApplicationBuilderExtensionsTests
    {
        [Fact]
        public void should_share_IApplicationBuilder_namespace() =>
            typeof(GraphZenApplicationBuilderExtensions).Namespace
                .Should().Be(typeof(IApplicationBuilder).Namespace);
    }
}