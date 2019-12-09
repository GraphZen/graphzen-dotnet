// Copyright (c) GraphZen LLC. All rights reserved.
// Licensed under the GraphZen Community License. See the LICENSE file in the project root for license information.

using System;
using System.Diagnostics.CodeAnalysis;
using FluentAssertions;
using GraphZen.Infrastructure;
using JetBrains.Annotations;
using Xunit;

namespace GraphZen.TypeSystem.FunctionalTests.Objects.Fields
{
    public class ObjectFieldBuilderTypeFunctionalTests
    {
        [Fact]
        public void invalid_field_type_should_throw_helpful_exception()
        {
            Schema.Create(_ =>
            {
                Action act = () => _.Object("Foo").Field("Bar", "List<>");
                act.Should().Throw<InvalidOperationException>()
                    .WithMessage(
                        "Invalid type reference: 'List<>' is not a valid type reference for object field 'Foo.Bar'.");
            });
        }
    }
}