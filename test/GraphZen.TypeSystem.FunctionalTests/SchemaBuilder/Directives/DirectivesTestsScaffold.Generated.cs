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
namespace GraphZen.TypeSystem.FunctionalTests.SchemaBuilder.Directives
{
    [NoReorder]
    public abstract class DirectivesTests
    {
        [Spec(nameof(ClrTypedCollectionSpecs.adding_clr_type_to_item_does_not_change_name))]
        [Fact]
        public void adding_clr_type_to_item_does_not_change_name()
        {
            // Priority: Low
            var schema = Schema.Create(_ => { });
            throw new NotImplementedException();
        }


        [Spec(nameof(ClrTypedCollectionSpecs.adding_clr_type_with_name_annotation_to_item_does_not_change_name))]
        [Fact]
        public void adding_clr_type_with_name_annotation_to_item_does_not_change_name()
        {
            // Priority: Low
            var schema = Schema.Create(_ => { });
            throw new NotImplementedException();
        }


        [Spec(nameof(ClrTypedCollectionSpecs.clr_typed_item_can_be_added))]
        [Fact]
        public void clr_typed_item_can_be_added()
        {
            // Priority: Low
            var schema = Schema.Create(_ => { });
            throw new NotImplementedException();
        }


        [Spec(nameof(ClrTypedCollectionSpecs.clr_typed_item_can_be_added_via_type_param))]
        [Fact]
        public void clr_typed_item_can_be_added_via_type_param()
        {
            // Priority: Low
            var schema = Schema.Create(_ => { });
            throw new NotImplementedException();
        }


        [Spec(nameof(ClrTypedCollectionSpecs.clr_typed_item_can_be_removed))]
        [Fact]
        public void clr_typed_item_can_be_removed()
        {
            // Priority: Low
            var schema = Schema.Create(_ => { });
            throw new NotImplementedException();
        }


        [Spec(nameof(ClrTypedCollectionSpecs.clr_typed_item_can_be_removed_via_type_param))]
        [Fact]
        public void clr_typed_item_can_be_removed_via_type_param()
        {
            // Priority: Low
            var schema = Schema.Create(_ => { });
            throw new NotImplementedException();
        }


        [Spec(nameof(ClrTypedCollectionSpecs.clr_typed_item_can_be_renamed))]
        [Fact]
        public void clr_typed_item_can_be_renamed()
        {
            // Priority: Low
            var schema = Schema.Create(_ => { });
            throw new NotImplementedException();
        }


        [Spec(nameof(ClrTypedCollectionSpecs.clr_typed_item_cannot_be_added_with_invalid_name_attribute))]
        [Fact]
        public void clr_typed_item_cannot_be_added_with_invalid_name_attribute()
        {
            // Priority: Low
            var schema = Schema.Create(_ => { });
            throw new NotImplementedException();
        }


        [Spec(nameof(ClrTypedCollectionSpecs.clr_typed_item_cannot_be_added_with_null_value))]
        [Fact]
        public void clr_typed_item_cannot_be_added_with_null_value()
        {
            // Priority: Low
            var schema = Schema.Create(_ => { });
            throw new NotImplementedException();
        }


        [Spec(nameof(ClrTypedCollectionSpecs.clr_typed_item_cannot_be_removed_with_null_value))]
        [Fact]
        public void clr_typed_item_cannot_be_removed_with_null_value()
        {
            // Priority: Low
            var schema = Schema.Create(_ => { });
            throw new NotImplementedException();
        }


        [Spec(nameof(ClrTypedCollectionSpecs.clr_typed_item_cannot_be_renamed_if_name_already_exists))]
        [Fact]
        public void clr_typed_item_cannot_be_renamed_if_name_already_exists()
        {
            // Priority: Low
            var schema = Schema.Create(_ => { });
            throw new NotImplementedException();
        }


        [Spec(nameof(ClrTypedCollectionSpecs.clr_typed_item_cannot_be_renamed_with_an_invalid_name))]
        [Fact]
        public void clr_typed_item_cannot_be_renamed_with_an_invalid_name()
        {
            // Priority: Low
            var schema = Schema.Create(_ => { });
            throw new NotImplementedException();
        }


        [Spec(nameof(ClrTypedCollectionSpecs.clr_typed_item_cannot_be_renamed_with_null_value))]
        [Fact]
        public void clr_typed_item_cannot_be_renamed_with_null_value()
        {
            // Priority: Low
            var schema = Schema.Create(_ => { });
            throw new NotImplementedException();
        }


        [Spec(nameof(ClrTypedCollectionSpecs.clr_typed_item_with_name_attribute_can_be_renamed))]
        [Fact]
        public void clr_typed_item_with_name_attribute_can_be_renamed()
        {
            // Priority: Low
            var schema = Schema.Create(_ => { });
            throw new NotImplementedException();
        }


        [Spec(nameof(NamedCollectionSpecs.named_item_can_be_added))]
        [Fact]
        public void named_item_can_be_added()
        {
            // Priority: Low
            var schema = Schema.Create(_ => { });
            throw new NotImplementedException();
        }


        [Spec(nameof(NamedCollectionSpecs.named_item_can_be_added_via_sdl))]
        [Fact]
        public void named_item_can_be_added_via_sdl()
        {
            // Priority: Low
            var schema = Schema.Create(_ => { });
            throw new NotImplementedException();
        }


        [Spec(nameof(NamedCollectionSpecs.named_item_can_be_added_via_sdl_extension))]
        [Fact]
        public void named_item_can_be_added_via_sdl_extension()
        {
            // Priority: Low
            var schema = Schema.Create(_ => { });
            throw new NotImplementedException();
        }


        [Spec(nameof(NamedCollectionSpecs.named_item_can_be_removed))]
        [Fact]
        public void named_item_can_be_removed()
        {
            // Priority: Low
            var schema = Schema.Create(_ => { });
            throw new NotImplementedException();
        }


        [Spec(nameof(NamedCollectionSpecs.named_item_can_be_renamed))]
        [Fact]
        public void named_item_can_be_renamed()
        {
            // Priority: Low
            var schema = Schema.Create(_ => { });
            throw new NotImplementedException();
        }


        [Spec(nameof(NamedCollectionSpecs.named_item_cannot_be_added_with_invalid_name))]
        [Fact]
        public void named_item_cannot_be_added_with_invalid_name()
        {
            // Priority: Low
            var schema = Schema.Create(_ => { });
            throw new NotImplementedException();
        }


        [Spec(nameof(NamedCollectionSpecs.named_item_cannot_be_added_with_null_value))]
        [Fact]
        public void named_item_cannot_be_added_with_null_value()
        {
            // Priority: Low
            var schema = Schema.Create(_ => { });
            throw new NotImplementedException();
        }


        [Spec(nameof(NamedCollectionSpecs.named_item_cannot_be_removed_with_invalid_name))]
        [Fact]
        public void named_item_cannot_be_removed_with_invalid_name()
        {
            // Priority: Low
            var schema = Schema.Create(_ => { });
            throw new NotImplementedException();
        }


        [Spec(nameof(NamedCollectionSpecs.named_item_cannot_be_removed_with_null_value))]
        [Fact]
        public void named_item_cannot_be_removed_with_null_value()
        {
            // Priority: Low
            var schema = Schema.Create(_ => { });
            throw new NotImplementedException();
        }


        [Spec(nameof(NamedCollectionSpecs.named_item_cannot_be_renamed_if_name_already_exists))]
        [Fact]
        public void named_item_cannot_be_renamed_if_name_already_exists()
        {
            // Priority: Low
            var schema = Schema.Create(_ => { });
            throw new NotImplementedException();
        }


        [Spec(nameof(NamedCollectionSpecs.named_item_cannot_be_renamed_with_an_invalid_name))]
        [Fact]
        public void named_item_cannot_be_renamed_with_an_invalid_name()
        {
            // Priority: Low
            var schema = Schema.Create(_ => { });
            throw new NotImplementedException();
        }


        [Spec(nameof(NamedCollectionSpecs.named_item_cannot_be_renamed_with_null_value))]
        [Fact]
        public void named_item_cannot_be_renamed_with_null_value()
        {
            // Priority: Low
            var schema = Schema.Create(_ => { });
            throw new NotImplementedException();
        }


        [Spec(nameof(ClrTypedCollectionSpecs.clr_typed_item_can_have_type_removed))]
        [Fact]
        public void typed_item_can_have_type_removed()
        {
            // Priority: Low
            var schema = Schema.Create(_ => { });
            throw new NotImplementedException();
        }


        [Spec(nameof(ClrTypedCollectionSpecs.untyped_item_can_have_clr_type_added))]
        [Fact]
        public void untyped_item_can_have_clr_type_added()
        {
            // Priority: Low
            var schema = Schema.Create(_ => { });
            throw new NotImplementedException();
        }


        [Spec(nameof(ClrTypedCollectionSpecs.untyped_item_cannot_have_clr_type_added_that_is_already_in_use))]
        [Fact]
        public void untyped_item_cannot_have_clr_type_added_that_is_already_in_use()
        {
            // Priority: Low
            var schema = Schema.Create(_ => { });
            throw new NotImplementedException();
        }
    }

// Move DirectivesTests into a separate file to start writing tests
    [NoReorder]
    public class DirectivesTestsScaffold
    {
    }
}