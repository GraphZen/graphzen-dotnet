// Copyright (c) GraphZen LLC. All rights reserved.
// Licensed under the GraphZen Community License. See the LICENSE file in the project root for license information.

#nullable enable

using System.Diagnostics.CodeAnalysis;
using GraphZen.Infrastructure;
using JetBrains.Annotations;
using Xunit;
using static GraphZen.TypeSystem.FunctionalTests.Specs.TypeSystemSpecs;

// ReSharper disable All
namespace GraphZen.TypeSystem.FunctionalTests.SchemaBuilder.Enums.EnumType.ClrType
{
    [NoReorder]
    public abstract class ClrTypeTestsScaffold
    {
        [Spec(nameof(ClrTypeSpecs.untyped_item_can_have_clr_type_added))]
        [Fact(Skip = "TODO")]
        public void untyped_item_can_have_clr_type_added_()
        {
            // var schema = Schema.Create(_ => { });
        }


        [Spec(nameof(ClrTypeSpecs.untyped_item_cannot_have_clr_type_added_that_is_already_in_use))]
        [Fact(Skip = "TODO")]
        public void untyped_item_cannot_have_clr_type_added_that_is_already_in_use_()
        {
            // var schema = Schema.Create(_ => { });
        }


        [Spec(nameof(ClrTypeSpecs.clr_typed_item_can_have_clr_type_changed))]
        [Fact(Skip = "TODO")]
        public void clr_typed_item_can_have_clr_type_changed_()
        {
            // var schema = Schema.Create(_ => { });
        }


        [Spec(nameof(ClrTypeSpecs.clr_typed_item_can_have_clr_type_changed_via_type_param))]
        [Fact(Skip = "TODO")]
        public void clr_typed_item_can_have_clr_type_changed_via_type_param_()
        {
            // var schema = Schema.Create(_ => { });
        }


        [Spec(nameof(ClrTypeSpecs.clr_typed_item_cannot_have_clr_type_changed_with_null_value))]
        [Fact(Skip = "TODO")]
        public void clr_typed_item_cannot_have_clr_type_changed_with_null_value_()
        {
            // var schema = Schema.Create(_ => { });
        }


        [Spec(nameof(ClrTypeSpecs.adding_clr_type_to_item_changes_name))]
        [Fact(Skip = "TODO")]
        public void adding_clr_type_to_item_changes_name_()
        {
            // var schema = Schema.Create(_ => { });
        }


        [Spec(nameof(ClrTypeSpecs.adding_clr_type_to_item_via_type_param_changes_name))]
        [Fact(Skip = "TODO")]
        public void adding_clr_type_to_item_via_type_param_changes_name_()
        {
            // var schema = Schema.Create(_ => { });
        }


        [Spec(nameof(ClrTypeSpecs.adding_clr_type_with_name_annotation_to_item_changes_name))]
        [Fact(Skip = "TODO")]
        public void adding_clr_type_with_name_annotation_to_item_changes_name_()
        {
            // var schema = Schema.Create(_ => { });
        }


        [Spec(nameof(ClrTypeSpecs.adding_clr_type_with_name_annotation_to_item_via_type_pram_changes_name))]
        [Fact(Skip = "TODO")]
        public void adding_clr_type_with_name_annotation_to_item_via_type_pram_changes_name_()
        {
            // var schema = Schema.Create(_ => { });
        }


        [Spec(nameof(ClrTypeSpecs.clr_type_with_conflicting_name_can_be_added_using_custom_name))]
        [Fact(Skip = "TODO")]
        public void clr_type_with_conflicting_name_can_be_added_using_custom_name_()
        {
            // var schema = Schema.Create(_ => { });
        }


        [Spec(nameof(ClrTypeSpecs.clr_type_with_conflicting_name_can_be_added_via_type_param_using_custom_name))]
        [Fact(Skip = "TODO")]
        public void clr_type_with_conflicting_name_can_be_added_via_type_param_using_custom_name_()
        {
            // var schema = Schema.Create(_ => { });
        }


        [Spec(nameof(ClrTypeSpecs.clr_type_with_conflicting_name_annotation_can_be_added_using_custom_name))]
        [Fact(Skip = "TODO")]
        public void clr_type_with_conflicting_name_annotation_can_be_added_using_custom_name_()
        {
            // var schema = Schema.Create(_ => { });
        }


        [Spec(nameof(ClrTypeSpecs
            .clr_type_with_conflicting_name_annotation_can_be_added_via_type_param_using_custom_name))]
        [Fact(Skip = "TODO")]
        public void clr_type_with_conflicting_name_annotation_can_be_added_via_type_param_using_custom_name_()
        {
            // var schema = Schema.Create(_ => { });
        }


        [Spec(nameof(ClrTypeSpecs.cannot_add_clr_type_to_item_with_conflicting_custom_name))]
        [Fact(Skip = "TODO")]
        public void cannot_add_clr_type_to_item_with_conflicting_custom_name_()
        {
            // var schema = Schema.Create(_ => { });
        }


        [Spec(nameof(ClrTypeSpecs.cannot_add_clr_type_to_item_via_type_param_with_conflicting_custom_name))]
        [Fact(Skip = "TODO")]
        public void cannot_add_clr_type_to_item_via_type_param_with_conflicting_custom_name_()
        {
            // var schema = Schema.Create(_ => { });
        }


        [Spec(nameof(ClrTypeSpecs.cannot_add_clr_type_to_item_with_invalid_custom_name))]
        [Fact(Skip = "TODO")]
        public void cannot_add_clr_type_to_item_with_invalid_custom_name_()
        {
            // var schema = Schema.Create(_ => { });
        }


        [Spec(nameof(ClrTypeSpecs.cannot_add_clr_type_to_item_via_type_param_with_invalid_custom_name))]
        [Fact(Skip = "TODO")]
        public void cannot_add_clr_type_to_item_via_type_param_with_invalid_custom_name_()
        {
            // var schema = Schema.Create(_ => { });
        }


        [Spec(nameof(ClrTypeSpecs.cannot_add_clr_type_to_item_with_null_custom_name))]
        [Fact(Skip = "TODO")]
        public void cannot_add_clr_type_to_item_with_null_custom_name_()
        {
            // var schema = Schema.Create(_ => { });
        }


        [Spec(nameof(ClrTypeSpecs.cannot_add_clr_type_to_item_via_type_param_with_null_custom_name))]
        [Fact(Skip = "TODO")]
        public void cannot_add_clr_type_to_item_via_type_param_with_null_custom_name_()
        {
            // var schema = Schema.Create(_ => { });
        }
    }
}
// Source Hash Code: 16894242645783924579