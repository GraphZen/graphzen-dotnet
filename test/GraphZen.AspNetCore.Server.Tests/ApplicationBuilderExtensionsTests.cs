// Copyright (c) GraphZen LLC. All rights reserved.
// Licensed under the GraphZen Community License. See the LICENSE file in the project root for license information.

using GraphZen.Infrastructure;
using Microsoft.AspNetCore.Builder;

namespace GraphZen.AspNetCore.Server.Tests;

[NoReorder]
public class ApplicationBuilderExtensionsTests
{
    [Fact]
    public void should_share_IApplicationBuilder_namespace() =>
        Assert.Equal(typeof(IApplicationBuilder).Namespace,
            typeof(GraphZenApplicationBuilderExtensions).Namespace);
}