// Copyright (c) GraphZen LLC. All rights reserved.
// Licensed under the GraphZen Community License. See the LICENSE file in the project root for license information.

#nullable enable

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
        [Fact(Skip = "TODO")]
        public void element_can_be_defined_via_sdl_()
        {
            var schema = Schema.Create(_ => { });
        }


        [Spec(nameof(SdlSpec.element_can_be_defined_via_sdl_extension))]
        [Fact(Skip = "TODO")]
        public void element_can_be_defined_via_sdl_extension_()
        {
            var schema = Schema.Create(_ => { });
        }


        [Spec(nameof(NamedTypeSetSpecs.set_item_can_be_added))]
        [Fact(Skip = "TODO")]
        public void set_item_can_be_added_()
        {
            var schema = Schema.Create(_ => { });
        }


        [Spec(nameof(NamedTypeSetSpecs.set_item_can_be_removed))]
        [Fact(Skip = "TODO")]
        public void set_item_can_be_removed_()
        {
            var schema = Schema.Create(_ => { });
        }


        [Spec(nameof(NamedTypeSetSpecs.set_item_must_be_valid_name))]
        [Fact(Skip = "TODO")]
        public void set_item_must_be_valid_name_()
        {
            var schema = Schema.Create(_ => { });
        }
    }

// Move MemberTypesTests into a separate file to start writing tests
    [NoReorder]
    public class MemberTypesTestsScaffold
    {
    }
}
// Source Hash Code: 3735338925937189431