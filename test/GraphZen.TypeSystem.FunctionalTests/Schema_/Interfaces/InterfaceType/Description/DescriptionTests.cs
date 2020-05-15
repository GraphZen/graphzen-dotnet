// Copyright (c) GraphZen LLC. All rights reserved.
// Licensed under the GraphZen Community License. See the LICENSE file in the project root for license information.

using System;
using System.Diagnostics.CodeAnalysis;
using FluentAssertions;
using GraphZen.Infrastructure;
using JetBrains.Annotations;
using Xunit;
using static GraphZen.TypeSystem.FunctionalTests.Specs.TypeSystemSpecs.DescriptionSpecs;

namespace GraphZen.TypeSystem.FunctionalTests.Schema_.Interfaces.InterfaceType.Description
{
    [NoReorder]
    public class DescriptionTests
    {
        [Spec(nameof(description_can_be_updated))]
        [Fact]
        public void updateable_item_can_be_updated_()
        {
            var schema = Schema.Create(_ => { _.Interface("Foo").Description("desc"); });
            schema.GetInterface("Foo").Description.Should().Be("desc");
        }

        [Spec(nameof(description_cannot_be_null))]
        [Fact]
        public void description_cannot_be_null_()
        {
            Schema.Create(_ =>
            {
                var foo = _.Interface("Foo");
                Action add = () => foo.Description(null!);
                add.Should().ThrowArgumentNullException("description");
            });
        }


        [Spec(nameof(description_can_be_removed))]
        [Fact]
        public void description_can_be_removed_()
        {
            var schema = Schema.Create(_ => { _.Interface("Foo").Description("desc").RemoveDescription(); });
            schema.GetInterface("Foo").Description.Should().BeNull();
        }
    }
}