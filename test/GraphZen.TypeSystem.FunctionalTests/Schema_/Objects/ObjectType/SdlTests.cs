// Copyright (c) GraphZen LLC. All rights reserved.
// Licensed under the GraphZen Community License. See the LICENSE file in the project root for license information.

using System.Diagnostics.CodeAnalysis;
using FluentAssertions;
using GraphZen.Infrastructure;
using GraphZen.TypeSystem.FunctionalTests.Specs;
using JetBrains.Annotations;
using Xunit;

namespace GraphZen.TypeSystem.FunctionalTests.Schema_.Objects.ObjectType
{
    [NoReorder]
    public class SdlTests
    {
        [Spec(nameof(TypeSystemSpecs.SdlSpec.item_can_be_defined_by_sdl))]
        [Fact]
        public void named_item_can_be_added_via_sdl_()
        {
            var schema = Schema.Create(_ => { _.FromSchema(@"type Foo"); });
            schema.HasObject("Foo").Should().BeTrue();
        }
    }
}