// Copyright (c) GraphZen LLC. All rights reserved.
// Licensed under the GraphZen Community License. See the LICENSE file in the project root for license information.

using System.Diagnostics.CodeAnalysis;
using FluentAssertions;
using GraphZen.Infrastructure;
using JetBrains.Annotations;
using Xunit;
using static GraphZen.TypeSystem.FunctionalTests.Specs.TypeSystemSpecs.SdlSpec;

namespace GraphZen.TypeSystem.FunctionalTests.Schema_.Interfaces.InterfaceType.Fields.Field.Arguments.ArgumentDefinition
{
    [NoReorder]
    public class SdlTests
    {
        [Spec(nameof(item_can_be_defined_by_sdl))]
        [Fact]
        public void named_item_can_be_added_via_sdl_()
        {
            var schema = Schema.Create(_ => _.FromSchema(@"interface Foo { foo(foo: String): String }"));
            schema.GetInterface("Foo").GetField("foo").HasArgument("foo").Should().BeTrue();
        }
    }
}