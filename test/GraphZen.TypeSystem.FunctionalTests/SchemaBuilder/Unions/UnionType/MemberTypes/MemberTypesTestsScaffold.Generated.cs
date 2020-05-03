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
namespace GraphZen.TypeSystem.FunctionalTests.SchemaBuilder.Unions.UnionType.MemberTypes
{
    [NoReorder]
    public abstract class MemberTypesTests
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


        [Spec(nameof(NamedTypeSetSpecs.set_item_can_be_added))]
        [Fact]
        public void set_item_can_be_added()
        {
            // Priority: Low
            var schema = Schema.Create(_ => { });
            throw new NotImplementedException();
        }


        [Spec(nameof(NamedTypeSetSpecs.set_item_can_be_removed))]
        [Fact]
        public void set_item_can_be_removed()
        {
            // Priority: Low
            var schema = Schema.Create(_ => { });
            throw new NotImplementedException();
        }


        [Spec(nameof(NamedTypeSetSpecs.set_item_must_be_valid_name))]
        [Fact]
        public void set_item_must_be_valid_name()
        {
            // Priority: Low
            var schema = Schema.Create(_ => { });
            throw new NotImplementedException();
        }
    }

// Move MemberTypesTests into a separate file to start writing tests
    [NoReorder]
    public class MemberTypesTestsScaffold
    {
    }
}