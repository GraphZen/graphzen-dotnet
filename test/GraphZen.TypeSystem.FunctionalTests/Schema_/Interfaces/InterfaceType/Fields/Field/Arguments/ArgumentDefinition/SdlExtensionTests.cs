// Copyright (c) GraphZen LLC. All rights reserved.
// Licensed under the GraphZen Community License. See the LICENSE file in the project root for license information.

using System.Diagnostics.CodeAnalysis;
using FluentAssertions;
using GraphZen.Infrastructure;
using GraphZen.TypeSystem.FunctionalTests.Specs;
using JetBrains.Annotations;
using Xunit;

namespace GraphZen.TypeSystem.FunctionalTests.Schema_.Interfaces.InterfaceType.Fields.Field.Arguments.ArgumentDefinition
{
    [NoReorder]
    public class SdlExtensionTests
    {
        [Spec(nameof(TypeSystemSpecs.SdlExtensionSpec.item_can_be_defined_by_sdl_extension))]
        [Fact(Skip = "TODO")]
        public void item_can_be_defined_by_sdl_extension_()
        {
            // var schema = Schema.Create(_ => { });
            var schema = Schema.Create(_ => _.FromSchema(@"extend interface Foo { foo(foo: String): String }"));
            schema.GetInterface("Foo").GetField("foo").HasArgument("foo").Should().BeTrue();
        }


    }
}