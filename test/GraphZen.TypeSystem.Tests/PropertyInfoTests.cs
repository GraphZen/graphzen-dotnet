// Copyright (c) GraphZen LLC. All rights reserved.
// Licensed under the GraphZen Community License. See the LICENSE file in the project root for license information.

using GraphZen.Infrastructure;
using GraphZen.TypeSystem.Internal;

namespace GraphZen.TypeSystem.Tests;

[NoReorder]
public class PropertyInfoTests
{
    public abstract class FooBase
    {
        public string BaseProperty { get; set; } = null!;

        [GraphQLCanBeNull] public string? NullableBaseProperty { get; set; }
    }


    public class Foo : FooBase
    {
        public string Bar { get; set; } = null!;

        [GraphQLCanBeNull] public string? NullableBar { get; set; }
    }

    [Theory]
    [InlineData(nameof(Foo.Bar), false)]
    [InlineData(nameof(Foo.NullableBar), true)]
    [InlineData(nameof(Foo.BaseProperty), false)]
    [InlineData(nameof(Foo.NullableBaseProperty), true)]
    public void PropertyNullability(string propertyName, bool excpectCanBeNull)
    {
        var property = typeof(Foo).GetProperty(propertyName)!;
        var canBeNull = property.CanBeNull();
        Assert.Equal(excpectCanBeNull, canBeNull);
    }
}