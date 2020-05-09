// Copyright (c) GraphZen LLC. All rights reserved.
// Licensed under the GraphZen Community License. See the LICENSE file in the project root for license information.

#nullable enable

using System.Diagnostics.CodeAnalysis;
using GraphZen.Infrastructure;
using JetBrains.Annotations;
using Xunit;
using static GraphZen.TypeSystem.FunctionalTests.Specs.TypeSystemSpecs;

// ReSharper disable All
namespace GraphZen.TypeSystem.FunctionalTests.SchemaBuilder.Enums
{
    [NoReorder]
    public abstract class EnumsTestsScaffold
    {
        [Spec(nameof(ClrTypedCollectionSpecs.clr_typed_item_with_conflicting_name_can_be_added_with_custom_name))]
        [Fact(Skip = "TODO")]
        public void clr_typed_item_can_be_added_with_custom_name_()
        {
            // var schema = Schema.Create(_ => { });
        }


        [Spec(nameof(ClrTypedCollectionSpecs.clr_typed_item_with_conflicting_name_can_be_added_via_type_param_with_custom_name))]
        [Fact(Skip = "TODO")]
        public void clr_typed_item_can_be_added_via_type_param_with_custom_name_()
        {
            // var schema = Schema.Create(_ => { });
        }


        [Spec(nameof(ClrTypedCollectionSpecs.custom_named_clr_typed_item_with_type_removed_should_retain_custom_name))]
        [Fact(Skip = "TODO")]
        public void custom_named_clr_typed_item_with_type_removed_should_retain_custom_name_()
        {
            // var schema = Schema.Create(_ => { });
        }


        [Spec(nameof(ClrTypedCollectionSpecs.adding_clr_type_to_item_changes_name))]
        [Fact(Skip = "TODO")]
        public void adding_clr_type_to_item_changes_name_()
        {
            // var schema = Schema.Create(_ => { });
        }


        [Spec(nameof(ClrTypedCollectionSpecs.adding_clr_type_with_name_annotation_to_item_changes_name))]
        [Fact(Skip = "TODO")]
        public void adding_clr_type_with_name_annotation_to_item_changes_name_()
        {
            // var schema = Schema.Create(_ => { });
        }


        [Spec(nameof(ClrTypedCollectionSpecs.clr_type_with_conflicting_name_can_be_added_with_custom_name))]
        [Fact(Skip = "TODO")]
        public void adding_clr_type_to_item_with_name_changes_name_from_param_()
        {
            // var schema = Schema.Create(_ => { });
        }


        

        [Spec(nameof(ClrTypedCollectionSpecs.cannot_add_clr_type_to_item_with_custom_name_if_name_conflicts))]
        [Fact(Skip = "TODO")]
        public void cannot_add_clr_type_to_item_with_custom_name_if_name_conflicts_()
        {
            // var schema = Schema.Create(_ => { });
        }


        [Spec(nameof(ClrTypedCollectionSpecs.cannot_add_clr_type_to_item_with_custom_name_if_name_is_null))]
        [Fact(Skip = "TODO")]
        public void cannot_add_clr_type_to_item_with_custom_name_if_name_is_null_()
        {
            // var schema = Schema.Create(_ => { });
        }


        [Spec(nameof(ClrTypedCollectionSpecs.cannot_add_clr_type_to_item_with_custom_name_if_name_is_invalid))]
        [Fact(Skip = "TODO")]
        public void cannot_add_clr_type_to_item_with_custom_name_if_name_is_invalid_()
        {
            // var schema = Schema.Create(_ => { });
        }
    }
}
// Source Hash Code: 12689667266406048356