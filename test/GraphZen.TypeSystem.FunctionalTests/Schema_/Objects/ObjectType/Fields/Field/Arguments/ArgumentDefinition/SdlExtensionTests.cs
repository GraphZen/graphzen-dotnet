// Copyright (c) GraphZen LLC. All rights reserved.
// Licensed under the GraphZen Community License. See the LICENSE file in the project root for license information.

using System.Diagnostics.CodeAnalysis;
using FluentAssertions;
using GraphZen.Infrastructure;
using GraphZen.TypeSystem.FunctionalTests.Specs;
using JetBrains.Annotations;
using Xunit;

namespace GraphZen.TypeSystem.FunctionalTests.Schema_.Objects.ObjectType.Fields.Field.Arguments.ArgumentDefinition
{
    [NoReorder]
    public class SdlExtensionTests
    {
        [Spec(nameof(TypeSystemSpecs.SdlExtensionSpec.item_can_be_defined_by_sdl_extension))]
        [Fact(Skip = "needs impl")]
        public void named_item_can_be_added_via_sdl_extension_()
        {
            var schema = Schema.Create(_ => _.FromSchema(@"extend type Foo { foo(foo: String): String }"));
            schema.GetObject("Foo").GetField("foo").HasArgument("foo").Should().BeTrue();
        }
    }
}