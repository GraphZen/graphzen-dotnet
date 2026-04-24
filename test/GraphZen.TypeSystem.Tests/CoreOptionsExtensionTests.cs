// Copyright (c) GraphZen LLC. All rights reserved.
// Licensed under the GraphZen Community License. See the LICENSE file in the project root for license information.

using GraphZen.Infrastructure;

namespace GraphZen.TypeSystem.Tests;

public class CoreOptionsExtensionTests
{
    [Fact]
    public void it_should_have_default_constructor()
    {
        Assert.NotNull(new CoreOptionsExtension());
    }
}
