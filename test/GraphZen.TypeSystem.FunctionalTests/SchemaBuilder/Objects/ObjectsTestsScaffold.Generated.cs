// Copyright (c) GraphZen LLC. All rights reserved.
// Licensed under the GraphZen Community License. See the LICENSE file in the project root for license information.

#nullable enable

using System.Diagnostics.CodeAnalysis;
using GraphZen.Infrastructure;
using JetBrains.Annotations;
using Xunit;
using static GraphZen.TypeSystem.FunctionalTests.Specs.TypeSystemSpecs;

// ReSharper disable All
namespace GraphZen.TypeSystem.FunctionalTests.SchemaBuilder.Objects
{
    [NoReorder]
    public abstract class ObjectsTestsScaffold
    {
        [Spec(nameof(UniquelyInputOutputTypeCollectionSpecs
            .named_item_cannot_be_added_if_name_conflicts_with_type_identity_of_opposite_io))]
        [Fact(Skip = "TODO")]
        public void named_item_cannot_be_added_if_name_conflicts_with_type_identity_of_opposite_io_()
        {
            var schema = Schema.Create(_ => { });
        }


        [Spec(nameof(UniquelyInputOutputTypeCollectionSpecs
            .named_item_cannot_be_renamed_to_name_conflicts_with_type_identity_of_opposite_io))]
        [Fact(Skip = "TODO")]
        public void named_item_cannot_be_renamed_to_name_conflicts_with_type_identity_of_opposite_io_()
        {
            var schema = Schema.Create(_ => { });
        }


        [Spec(nameof(UniquelyInputOutputTypeCollectionSpecs
            .clr_typed_item_cannot_be_renamed_if_name_conflicts_with_type_identity_of_opposite_io))]
        [Fact(Skip = "TODO")]
        public void clr_typed_item_cannot_be_renamed_if_name_conflicts_with_type_identity_of_opposite_io_()
        {
            var schema = Schema.Create(_ => { });
        }


        [Spec(nameof(UniquelyInputOutputTypeCollectionSpecs
            .DEPRECATED_subsequently_clr_typed_item_cannot_have_custom_name_removed_if_clr_type_name_conflicts_with_type_identity_of_opposite_io
        ))]
        [Fact(Skip = "TODO")]
        public void
            subsequently_clr_typed_item_cannot_have_custom_name_removed_if_clr_type_name_conflicts_with_type_identity_of_opposite_io_()
        {
            var schema = Schema.Create(_ => { });
        }


        [Spec(nameof(UniquelyInputOutputTypeCollectionSpecs
            .DEPRECATED_subsequently_clr_typed_item_cannot_have_custom_name_removed_if_clr_type_name_annotation_conflicts_with_type_identity_of_opposite_io
        ))]
        [Fact(Skip = "TODO")]
        public void
            subsequently_clr_typed_item_cannot_have_custom_name_removed_if_clr_type_name_annotation_conflicts_with_type_identity_of_opposite_io_()
        {
            var schema = Schema.Create(_ => { });
        }


        [Spec(nameof(UniquelyInputOutputTypeCollectionSpecs
            .clr_typed_item_with_name_attribute_cannot_be_added_if_name_attribute_conflicts_with_type_identity_of_opposite_io
        ))]
        [Fact(Skip = "TODO")]
        public void
            clr_typed_item_with_name_attribute_cannot_be_added_if_name_attribute_conflicts_with_type_identity_of_opposite_io_()
        {
            var schema = Schema.Create(_ => { });
        }


        [Spec(nameof(ClrTypedCollectionSpecs.clr_typed_item_can_be_added_with_custom_name))]
        [Fact(Skip = "TODO")]
        public void clr_typed_item_can_be_added_with_custom_name_()
        {
            var schema = Schema.Create(_ => { });
        }


        [Spec(nameof(ClrTypedCollectionSpecs.clr_typed_item_can_be_added_via_type_param_with_custom_name))]
        [Fact(Skip = "TODO")]
        public void clr_typed_item_can_be_added_via_type_param_with_custom_name_()
        {
            var schema = Schema.Create(_ => { });
        }


        [Spec(nameof(ClrTypedCollectionSpecs.custom_named_clr_typed_item_with_type_removed_should_retain_custom_name))]
        [Fact(Skip = "TODO")]
        public void custom_named_clr_typed_item_with_type_removed_should_retain_custom_name_()
        {
            var schema = Schema.Create(_ => { });
        }


        [Spec(nameof(ClrTypedCollectionSpecs.adding_clr_type_to_item_changes_name))]
        [Fact(Skip = "TODO")]
        public void adding_clr_type_to_item_changes_name_()
        {
            var schema = Schema.Create(_ => { });
        }


        [Spec(nameof(ClrTypedCollectionSpecs.adding_clr_type_to_item_with_name_changes_name_from_param))]
        [Fact(Skip = "TODO")]
        public void adding_clr_type_to_item_with_name_changes_name_from_param_()
        {
            var schema = Schema.Create(_ => { });
        }


        [Spec(nameof(ClrTypedCollectionSpecs
            .adding_clr_type_with_name_annotation_to_item_with_name_param_changes_name_from_param))]
        [Fact(Skip = "TODO")]
        public void adding_clr_type_with_name_annotation_to_item_with_name_param_changes_name_from_param_()
        {
            var schema = Schema.Create(_ => { });
        }


        [Spec(nameof(ClrTypedCollectionSpecs.cannot_add_clr_type_to_item_with_custom_name_if_name_conflicts))]
        [Fact(Skip = "TODO")]
        public void cannot_add_clr_type_to_item_with_custom_name_if_name_conflicts_()
        {
            var schema = Schema.Create(_ => { });
        }
    }
}
// Source Hash Code: 1710815592573858258