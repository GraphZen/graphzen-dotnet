// Copyright (c) GraphZen LLC. All rights reserved.
// Licensed under the GraphZen Community License. See the LICENSE file in the project root for license information.

using System.Diagnostics.CodeAnalysis;
using FluentAssertions;
using GraphZen.Infrastructure;
using GraphZen.TypeSystem.Internal;
using JetBrains.Annotations;
using Xunit;

#nullable disable


namespace GraphZen.TypeSystem
{
    [NoReorder]
    public class PropertyInfoTests
    {
        public abstract class FooBase
        {
            public string BaseProperty { get; set; }

            [GraphQLCanBeNull] public string NullableBaseProperty { get; set; }
        }


        public class Foo : FooBase
        {
            public string Bar { get; set; }

            [GraphQLCanBeNull] public string NullableBar { get; set; }
        }

        [Theory]
        [InlineData(nameof(Foo.Bar), false)]
        [InlineData(nameof(Foo.NullableBar), true)]
        [InlineData(nameof(Foo.BaseProperty), false)]
        [InlineData(nameof(Foo.NullableBaseProperty), true)]
        public void PropertyNullability(string propertyName, bool excpectCanBeNull)
        {
            var property = typeof(Foo).GetProperty(propertyName);
            var canBeNull = property.CanBeNull();
            canBeNull.Should().Be(excpectCanBeNull,
                $"{propertyName} {(excpectCanBeNull ? "CAN be null" : "should NOT be null")}");
        }
    }
}