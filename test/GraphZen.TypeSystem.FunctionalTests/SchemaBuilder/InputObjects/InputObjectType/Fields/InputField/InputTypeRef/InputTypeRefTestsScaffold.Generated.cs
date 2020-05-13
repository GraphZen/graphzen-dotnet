// Copyright (c) GraphZen LLC. All rights reserved.
// Licensed under the GraphZen Community License. See the LICENSE file in the project root for license information.

#nullable enable

using System.Diagnostics.CodeAnalysis;
using GraphZen.Infrastructure;
using JetBrains.Annotations;
using Xunit;
using static GraphZen.TypeSystem.FunctionalTests.Specs.TypeSystemSpecs;

// ReSharper disable All
namespace GraphZen.TypeSystem.FunctionalTests.SchemaBuilder.InputObjects.InputObjectType.Fields.InputField.InputTypeRef
{
    [NoReorder]
    public abstract class InputTypeRefTests
    {
        [Spec(nameof(SdlSpec.element_can_be_defined_via_sdl))]
        [Fact(Skip = "TODO")]
        public void element_can_be_defined_via_sdl_()
        {
            // var schema = Schema.Create(_ => { });
        }


        [Spec(nameof(SdlSpec.element_can_be_defined_via_sdl_extension))]
        [Fact(Skip = "TODO")]
        public void element_can_be_defined_via_sdl_extension_()
        {
            // var schema = Schema.Create(_ => { });
        }


        [Spec(nameof(UpdateableSpecs.updateable_item_can_be_updated))]
        [Fact(Skip = "TODO")]
        public void updateable_item_can_be_updated_()
        {
            // var schema = Schema.Create(_ => { });
        }


        [Spec(nameof(RequiredSpecs.required_item_cannot_be_set_with_null_value))]
        [Fact(Skip = "TODO")]
        public void required_item_cannot_be_removed_()
        {
            // var schema = Schema.Create(_ => { });
        }
    }

// Move InputTypeRefTests into a separate file to start writing tests
    [NoReorder]
    public class InputTypeRefTestsScaffold
    {
    }
}
// Source Hash Code: 18067429450472615750