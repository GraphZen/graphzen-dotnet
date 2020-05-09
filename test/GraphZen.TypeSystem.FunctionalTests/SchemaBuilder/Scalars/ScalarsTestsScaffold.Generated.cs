// Copyright (c) GraphZen LLC. All rights reserved.
// Licensed under the GraphZen Community License. See the LICENSE file in the project root for license information.

#nullable enable

using System.Diagnostics.CodeAnalysis;
using GraphZen.Infrastructure;
using JetBrains.Annotations;
using Xunit;
using static GraphZen.TypeSystem.FunctionalTests.Specs.TypeSystemSpecs;

// ReSharper disable All
namespace GraphZen.TypeSystem.FunctionalTests.SchemaBuilder.Scalars
{
    [NoReorder]
    public abstract class ScalarsTests
    {
        [Spec(nameof(InputAndOutputTypeCollectionSpecs.named_item_can_be_added_if_name_matches_input_type_identity))]
        [Fact(Skip = "TODO")]
        public void named_item_can_be_added_if_name_matches_input_type_identity_()
        {
            var schema = Schema.Create(_ => { });
        }


        [Spec(nameof(InputAndOutputTypeCollectionSpecs.named_item_can_be_added_if_name_matches_output_type_identity))]
        [Fact(Skip = "TODO")]
        public void named_item_can_be_added_if_name_matches_output_type_identity_()
        {
            var schema = Schema.Create(_ => { });
        }


        [Spec(nameof(InputAndOutputTypeCollectionSpecs.named_item_can_be_renamed_to_name_with_input_type_identity))]
        [Fact(Skip = "TODO")]
        public void named_item_can_be_renamed_to_name_with_input_type_identity_()
        {
            var schema = Schema.Create(_ => { });
        }


        [Spec(nameof(InputAndOutputTypeCollectionSpecs.named_item_can_be_renamed_to_name_with_output_type_identity))]
        [Fact(Skip = "TODO")]
        public void named_item_can_be_renamed_to_name_with_output_type_identity_()
        {
            var schema = Schema.Create(_ => { });
        }


        [Spec(nameof(InputAndOutputTypeCollectionSpecs
            .clr_typed_item_can_be_renamed_if_name_matches_input_type_identity))]
        [Fact(Skip = "TODO")]
        public void clr_typed_item_can_be_renamed_if_name_matches_input_type_identity_()
        {
            var schema = Schema.Create(_ => { });
        }


        [Spec(nameof(InputAndOutputTypeCollectionSpecs
            .clr_typed_item_can_be_renamed_if_name_matches_output_type_identity))]
        [Fact(Skip = "TODO")]
        public void clr_typed_item_can_be_renamed_if_name_matches_output_type_identity_()
        {
            var schema = Schema.Create(_ => { });
        }


        [Spec(nameof(InputAndOutputTypeCollectionSpecs
            .clr_typed_item_with_name_attribute_can_be_added_if_name_attribute_matches_with_input_type_identity))]
        [Fact(Skip = "TODO")]
        public void
            clr_typed_item_with_name_attribute_can_be_added_if_name_attribute_matches_with_input_type_identity_()
        {
            var schema = Schema.Create(_ => { });
        }


        [Spec(nameof(InputAndOutputTypeCollectionSpecs
            .clr_typed_item_with_name_attribute_can_be_added_if_name_attribute_matches_with_output_type_identity))]
        [Fact(Skip = "TODO")]
        public void
            clr_typed_item_with_name_attribute_can_be_added_if_name_attribute_matches_with_output_type_identity_()
        {
            var schema = Schema.Create(_ => { });
        }


        [Spec(nameof(NamedCollectionSpecs.named_item_can_be_added_via_sdl))]
        [Fact(Skip = "TODO")]
        public void named_item_can_be_added_via_sdl_()
        {
            var schema = Schema.Create(_ => { });
        }


        [Spec(nameof(NamedCollectionSpecs.named_item_can_be_added_via_sdl_extension))]
        [Fact(Skip = "TODO")]
        public void named_item_can_be_added_via_sdl_extension_()
        {
            var schema = Schema.Create(_ => { });
        }


        [Spec(nameof(NamedCollectionSpecs.named_item_can_be_added))]
        [Fact(Skip = "TODO")]
        public void named_item_can_be_added_()
        {
            var schema = Schema.Create(_ => { });
        }


        [Spec(nameof(NamedCollectionSpecs.named_item_cannot_be_added_with_null_value))]
        [Fact(Skip = "TODO")]
        public void named_item_cannot_be_added_with_null_value_()
        {
            var schema = Schema.Create(_ => { });
        }


        [Spec(nameof(NamedCollectionSpecs.named_item_cannot_be_added_with_invalid_name))]
        [Fact(Skip = "TODO")]
        public void named_item_cannot_be_added_with_invalid_name_()
        {
            var schema = Schema.Create(_ => { });
        }


        [Spec(nameof(NamedCollectionSpecs.named_item_can_be_renamed))]
        [Fact(Skip = "TODO")]
        public void named_item_can_be_renamed_()
        {
            var schema = Schema.Create(_ => { });
        }


        [Spec(nameof(NamedCollectionSpecs.named_item_cannot_be_renamed_with_null_value))]
        [Fact(Skip = "TODO")]
        public void named_item_cannot_be_renamed_with_null_value_()
        {
            var schema = Schema.Create(_ => { });
        }


        [Spec(nameof(NamedCollectionSpecs.named_item_cannot_be_renamed_with_an_invalid_name))]
        [Fact(Skip = "TODO")]
        public void named_item_cannot_be_renamed_with_an_invalid_name_()
        {
            var schema = Schema.Create(_ => { });
        }


        [Spec(nameof(NamedCollectionSpecs.named_item_cannot_be_renamed_if_name_already_exists))]
        [Fact(Skip = "TODO")]
        public void named_item_cannot_be_renamed_if_name_already_exists_()
        {
            var schema = Schema.Create(_ => { });
        }


        [Spec(nameof(NamedCollectionSpecs.named_item_can_be_removed))]
        [Fact(Skip = "TODO")]
        public void named_item_can_be_removed_()
        {
            var schema = Schema.Create(_ => { });
        }


        [Spec(nameof(NamedCollectionSpecs.named_item_cannot_be_removed_with_null_value))]
        [Fact(Skip = "TODO")]
        public void named_item_cannot_be_removed_with_null_value_()
        {
            var schema = Schema.Create(_ => { });
        }


        [Spec(nameof(NamedCollectionSpecs.named_item_cannot_be_removed_with_invalid_name))]
        [Fact(Skip = "TODO")]
        public void named_item_cannot_be_removed_with_invalid_name_()
        {
            var schema = Schema.Create(_ => { });
        }


        [Spec(nameof(ClrTypedCollectionSpecs.clr_typed_item_can_be_added))]
        [Fact(Skip = "TODO")]
        public void clr_typed_item_can_be_added_()
        {
            var schema = Schema.Create(_ => { });
        }


        [Spec(nameof(ClrTypedCollectionSpecs.clr_typed_item_can_be_added_via_type_param))]
        [Fact(Skip = "TODO")]
        public void clr_typed_item_can_be_added_via_type_param_()
        {
            var schema = Schema.Create(_ => { });
        }


        [Spec(nameof(ClrTypedCollectionSpecs.clr_typed_item_cannot_be_added_with_null_value))]
        [Fact(Skip = "TODO")]
        public void clr_typed_item_cannot_be_added_with_null_value_()
        {
            var schema = Schema.Create(_ => { });
        }


        [Spec(nameof(ClrTypedCollectionSpecs.clr_typed_item_cannot_be_added_with_invalid_name_attribute))]
        [Fact(Skip = "TODO")]
        public void clr_typed_item_cannot_be_added_with_invalid_name_attribute_()
        {
            var schema = Schema.Create(_ => { });
        }


        [Spec(nameof(ClrTypedCollectionSpecs.clr_typed_item_can_be_removed))]
        [Fact(Skip = "TODO")]
        public void clr_typed_item_can_be_removed_()
        {
            var schema = Schema.Create(_ => { });
        }


        [Spec(nameof(ClrTypedCollectionSpecs.clr_typed_item_can_be_removed_via_type_param))]
        [Fact(Skip = "TODO")]
        public void clr_typed_item_can_be_removed_via_type_param_()
        {
            var schema = Schema.Create(_ => { });
        }


        [Spec(nameof(ClrTypedCollectionSpecs.clr_typed_item_cannot_be_removed_with_null_value))]
        [Fact(Skip = "TODO")]
        public void clr_typed_item_cannot_be_removed_with_null_value_()
        {
            var schema = Schema.Create(_ => { });
        }


        [Spec(nameof(ClrTypedCollectionSpecs.clr_typed_item_can_have_clr_type_changed))]
        [Fact(Skip = "TODO")]
        public void clr_typed_item_can_have_clr_type_changed_()
        {
            var schema = Schema.Create(_ => { });
        }


        [Spec(nameof(ClrTypedCollectionSpecs.clr_typed_item_cannot_have_clr_type_changed_with_null_value))]
        [Fact(Skip = "TODO")]
        public void clr_typed_item_cannot_have_clr_type_changed_with_null_value_()
        {
            var schema = Schema.Create(_ => { });
        }


        [Spec(nameof(ClrTypedCollectionSpecs.clr_typed_item_can_have_clr_type_removed))]
        [Fact(Skip = "TODO")]
        public void clr_typed_item_can_have_clr_type_removed_()
        {
            var schema = Schema.Create(_ => { });
        }


        [Spec(nameof(ClrTypedCollectionSpecs.clr_typed_item_with_type_removed_should_retain_clr_type_name))]
        [Fact(Skip = "TODO")]
        public void clr_typed_item_with_type_removed_should_retain_clr_type_name_()
        {
            var schema = Schema.Create(_ => { });
        }


        [Spec(nameof(ClrTypedCollectionSpecs
            .clr_typed_item_with_name_annotation_type_removed_should_retain_annotated_name))]
        [Fact(Skip = "TODO")]
        public void clr_typed_item_with_name_annotation_type_removed_should_retain_annotated_name_()
        {
            var schema = Schema.Create(_ => { });
        }


        [Spec(nameof(ClrTypedCollectionSpecs.clr_typed_item_can_be_renamed))]
        [Fact(Skip = "TODO")]
        public void clr_typed_item_can_be_renamed_()
        {
            var schema = Schema.Create(_ => { });
        }


        [Spec(nameof(ClrTypedCollectionSpecs.clr_typed_item_with_name_attribute_can_be_renamed))]
        [Fact(Skip = "TODO")]
        public void clr_typed_item_with_name_attribute_can_be_renamed_()
        {
            var schema = Schema.Create(_ => { });
        }


        [Spec(nameof(ClrTypedCollectionSpecs.clr_typed_item_cannot_be_renamed_with_an_invalid_name))]
        [Fact(Skip = "TODO")]
        public void clr_typed_item_cannot_be_renamed_with_an_invalid_name_()
        {
            var schema = Schema.Create(_ => { });
        }


        [Spec(nameof(ClrTypedCollectionSpecs.clr_typed_item_cannot_be_renamed_if_name_already_exists))]
        [Fact(Skip = "TODO")]
        public void clr_typed_item_cannot_be_renamed_if_name_already_exists_()
        {
            var schema = Schema.Create(_ => { });
        }


        [Spec(nameof(ClrTypedCollectionSpecs.untyped_item_can_have_clr_type_added))]
        [Fact(Skip = "TODO")]
        public void untyped_item_can_have_clr_type_added_()
        {
            var schema = Schema.Create(_ => { });
        }


        [Spec(nameof(ClrTypedCollectionSpecs.untyped_item_cannot_have_clr_type_added_that_is_already_in_use))]
        [Fact(Skip = "TODO")]
        public void untyped_item_cannot_have_clr_type_added_that_is_already_in_use_()
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


        [Spec(nameof(ClrTypedCollectionSpecs.adding_clr_type_with_custom_name_will_change_name))]
        [Fact(Skip = "TODO")]
        public void adding_clr_type_with_custom_name_will_change_name_()
        {
            var schema = Schema.Create(_ => { });
        }


        [Spec(nameof(ClrTypedCollectionSpecs.adding_clr_type_with_custom_name_null_value_will_use_clr_name))]
        [Fact(Skip = "TODO")]
        public void adding_clr_type_with_custom_name_null_value_will_use_clr_name_()
        {
            var schema = Schema.Create(_ => { });
        }


        [Spec(nameof(ClrTypedCollectionSpecs
            .subsequently_clr_typed_item_cannot_have_custom_named_removed_if_clr_type_name_conflicts))]
        [Fact(Skip = "TODO")]
        public void subsequently_clr_typed_item_cannot_have_custom_named_removed_if_clr_type_name_conflicts_()
        {
            var schema = Schema.Create(_ => { });
        }


        [Spec(nameof(ClrTypedCollectionSpecs
            .subsequently_clr_typed_item_cannot_have_custom_named_removed_if_clr_type_name_annotation_conflicts))]
        [Fact(Skip = "TODO")]
        public void
            subsequently_clr_typed_item_cannot_have_custom_named_removed_if_clr_type_name_annotation_conflicts_()
        {
            var schema = Schema.Create(_ => { });
        }
    }

// Move ScalarsTests into a separate file to start writing tests
    [NoReorder]
    public class ScalarsTestsScaffold
    {
    }
}
// Source Hash Code: 194574779274164688