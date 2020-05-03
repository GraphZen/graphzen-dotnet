// Copyright (c) GraphZen LLC. All rights reserved.
// Licensed under the GraphZen Community License. See the LICENSE file in the project root for license information.

#nullable enable

using System;
using System.Diagnostics.CodeAnalysis;
using GraphZen.Infrastructure;
using JetBrains.Annotations;
using Xunit;
using static GraphZen.TypeSystem.FunctionalTests.Specs.TypeSystemSpecs;

// ReSharper disable All
namespace GraphZen.TypeSystem.FunctionalTests.SchemaBuilder.MutationType
{
    [NoReorder]
    public abstract class MutationTypeTests
    {
        [Spec(nameof(SdlSpec.element_can_be_defined_via_sdl))]
        [Fact]
        public void element_can_be_defined_via_sdl()
        {
            // Priority: Low
            var schema = Schema.Create(_ => { });
            throw new NotImplementedException();
        }


        [Spec(nameof(SdlSpec.element_can_be_defined_via_sdl_extension))]
        [Fact]
        public void element_can_be_defined_via_sdl_extension()
        {
            // Priority: Low
            var schema = Schema.Create(_ => { });
            throw new NotImplementedException();
        }


        [Spec(nameof(OptionalSpecs.optional_item_can_be_removed))]
        [Fact]
        public void optional_item_can_be_removed()
        {
            // Priority: Low
            var schema = Schema.Create(_ => { });
            throw new NotImplementedException();
        }


        [Spec(nameof(OptionalSpecs.parent_can_be_created_without_optional_item))]
        [Fact]
        public void parent_can_be_created_without_optional_item()
        {
            // Priority: Low
            var schema = Schema.Create(_ => { });
            throw new NotImplementedException();
        }


        [Spec(nameof(UpdateableSpecs.updateable_item_can_be_updated))]
        [Fact]
        public void updateable_item_can_be_updated()
        {
            // Priority: Low
            var schema = Schema.Create(_ => { });
            throw new NotImplementedException();
        }
    }

// Move MutationTypeTests into a separate file to start writing tests
    [NoReorder]
    public class MutationTypeTestsScaffold
    {
    }
}