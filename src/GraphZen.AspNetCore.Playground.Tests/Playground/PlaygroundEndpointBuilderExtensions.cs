// Copyright (c) GraphZen LLC. All rights reserved.
// Licensed under the GraphZen Community License. See the LICENSE file in the project root for license information.

using System.Diagnostics.CodeAnalysis;
using FluentAssertions;
using GraphZen.Infrastructure;
using JetBrains.Annotations;
using Microsoft.AspNetCore.Builder;
using Xunit;

namespace GraphZen.Playground
{
    public class PlaygroundEndpointBuilderExtensionTests
    {
        [Fact]
        public void playground_should_share_IApplicationBuilder_namespace() =>
            typeof(PlaygroundEndpointBuilderExtensions).Namespace
                .Should().Be(typeof(IApplicationBuilder).Namespace);
    }
}