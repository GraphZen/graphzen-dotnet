#nullable enable

using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Diagnostics.CodeAnalysis;
using GraphZen.Infrastructure;
using JetBrains.Annotations;

using Xunit;
using static GraphZen.TypeSystem.FunctionalTests.Specs.TypeSystemSpecs;
// ReSharper disable All
namespace GraphZen.TypeSystem.FunctionalTests.SchemaBuilder.Interfaces {
[NoReorder]
public abstract  class InterfacesTestsScaffold {

[Spec(nameof(UniquelyInputOutputTypeCollectionSpecs.clr_type_cannot_be_added_with_custom_name_if_name_conflicts_with_type_identity_of_opposite_io))]
[Fact(Skip="TODO")]
public void clr_type_cannot_be_added_with_custom_name_if_name_conflicts_with_type_identity_of_opposite_io_() {
    // var schema = Schema.Create(_ => { });
}



[Spec(nameof(UniquelyInputOutputTypeCollectionSpecs.clr_type_cannot_be_added_via_type_param_with_custom_name_if_name_conflicts_with_type_identity_of_opposite_io))]
[Fact(Skip="TODO")]
public void clr_type_cannot_be_added_via_type_param_with_custom_name_if_name_conflicts_with_type_identity_of_opposite_io_() {
    // var schema = Schema.Create(_ => { });
}



[Spec(nameof(UniquelyInputOutputTypeCollectionSpecs.clr_typed_item_cannot_be_added_with_custom_name_if_name_conflicts_with_type_identity_of_opposite_io))]
[Fact(Skip="TODO")]
public void clr_typed_item_cannot_be_added_with_custom_name_if_name_conflicts_with_type_identity_of_opposite_io_() {
    // var schema = Schema.Create(_ => { });
}



[Spec(nameof(UniquelyInputOutputTypeCollectionSpecs.clr_type_cannot_be_changed_with_custom_name_if_name_conflicts_with_type_identity_of_opposite_io))]
[Fact(Skip="TODO")]
public void clr_type_cannot_be_changed_with_custom_name_if_name_conflicts_with_type_identity_of_opposite_io_() {
    // var schema = Schema.Create(_ => { });
}



[Spec(nameof(ClrTypedCollectionSpecs.clr_typed_item_can_be_added_via_type_param))]
[Fact(Skip="TODO")]
public void clr_typed_item_can_be_added_via_type_param_() {
    // var schema = Schema.Create(_ => { });
}



[Spec(nameof(ClrTypedCollectionSpecs.clr_typed_item_with_conflicting_name_can_be_added_with_custom_name))]
[Fact(Skip="TODO")]
public void clr_typed_item_with_conflicting_name_can_be_added_with_custom_name_() {
    // var schema = Schema.Create(_ => { });
}



[Spec(nameof(ClrTypedCollectionSpecs.clr_typed_item_with_conflicting_name_can_be_added_via_type_param_with_custom_name))]
[Fact(Skip="TODO")]
public void clr_typed_item_with_conflicting_name_can_be_added_via_type_param_with_custom_name_() {
    // var schema = Schema.Create(_ => { });
}



[Spec(nameof(ClrTypedCollectionSpecs.clr_typed_item_with_conflicting_name_annotation_can_be_added_with_custom_name))]
[Fact(Skip="TODO")]
public void clr_typed_item_with_conflicting_name_annotation_can_be_added_with_custom_name_() {
    // var schema = Schema.Create(_ => { });
}



[Spec(nameof(ClrTypedCollectionSpecs.clr_typed_item_with_conflicting_name_annotation_can_be_added_via_type_param_with_custom_name))]
[Fact(Skip="TODO")]
public void clr_typed_item_with_conflicting_name_annotation_can_be_added_via_type_param_with_custom_name_() {
    // var schema = Schema.Create(_ => { });
}



[Spec(nameof(ClrTypedCollectionSpecs.clr_typed_item_with_invalid_name_annotation_can_be_added_with_custom_name))]
[Fact(Skip="TODO")]
public void clr_typed_item_with_invalid_name_annotation_can_be_added_with_custom_name_() {
    // var schema = Schema.Create(_ => { });
}



[Spec(nameof(ClrTypedCollectionSpecs.clr_typed_item_with_invalid_name_annotation_can_be_added_via_type_param_with_custom_name))]
[Fact(Skip="TODO")]
public void clr_typed_item_with_invalid_name_annotation_can_be_added_via_type_param_with_custom_name_() {
    // var schema = Schema.Create(_ => { });
}



[Spec(nameof(ClrTypedCollectionSpecs.clr_typed_item_can_have_clr_type_changed_via_type_param))]
[Fact(Skip="TODO")]
public void clr_typed_item_can_have_clr_type_changed_via_type_param_() {
    // var schema = Schema.Create(_ => { });
}



[Spec(nameof(ClrTypedCollectionSpecs.custom_named_clr_typed_item_with_type_removed_should_retain_custom_name))]
[Fact(Skip="TODO")]
public void custom_named_clr_typed_item_with_type_removed_should_retain_custom_name_() {
    // var schema = Schema.Create(_ => { });
}



[Spec(nameof(ClrTypedCollectionSpecs.adding_clr_type_to_item_does_not_change_name))]
[Fact(Skip="TODO")]
public void adding_clr_type_to_item_does_not_change_name_() {
    // var schema = Schema.Create(_ => { });
}



[Spec(nameof(ClrTypedCollectionSpecs.adding_clr_type_to_item_via_type_param_does_not_change_name))]
[Fact(Skip="TODO")]
public void adding_clr_type_to_item_via_type_param_does_not_change_name_() {
    // var schema = Schema.Create(_ => { });
}



[Spec(nameof(ClrTypedCollectionSpecs.adding_clr_type_with_name_annotation_to_item_via_type_param_does_not_change_name))]
[Fact(Skip="TODO")]
public void adding_clr_type_with_name_annotation_to_item_via_type_param_does_not_change_name_() {
    // var schema = Schema.Create(_ => { });
}



[Spec(nameof(ClrTypedCollectionSpecs.adding_clr_type_to_item_changes_name))]
[Fact(Skip="TODO")]
public void adding_clr_type_to_item_changes_name_() {
    // var schema = Schema.Create(_ => { });
}



[Spec(nameof(ClrTypedCollectionSpecs.adding_clr_type_to_item_via_type_param_changes_name))]
[Fact(Skip="TODO")]
public void adding_clr_type_to_item_via_type_param_changes_name_() {
    // var schema = Schema.Create(_ => { });
}



[Spec(nameof(ClrTypedCollectionSpecs.adding_clr_type_with_name_annotation_to_item_changes_name))]
[Fact(Skip="TODO")]
public void adding_clr_type_with_name_annotation_to_item_changes_name_() {
    // var schema = Schema.Create(_ => { });
}



[Spec(nameof(ClrTypedCollectionSpecs.adding_clr_type_with_name_annotation_to_item_via_type_pram_changes_name))]
[Fact(Skip="TODO")]
public void adding_clr_type_with_name_annotation_to_item_via_type_pram_changes_name_() {
    // var schema = Schema.Create(_ => { });
}



[Spec(nameof(ClrTypedCollectionSpecs.clr_type_with_conflicting_name_can_be_added_using_custom_name))]
[Fact(Skip="TODO")]
public void clr_type_with_conflicting_name_can_be_added_using_custom_name_() {
    // var schema = Schema.Create(_ => { });
}



[Spec(nameof(ClrTypedCollectionSpecs.clr_type_with_conflicting_name_annotation_can_be_added_using_custom_name))]
[Fact(Skip="TODO")]
public void clr_type_with_conflicting_name_annotation_can_be_added_using_custom_name_() {
    // var schema = Schema.Create(_ => { });
}



[Spec(nameof(ClrTypedCollectionSpecs.clr_type_with_conflicting_name_cannot_be_added_using_custom_name_if_name_null))]
[Fact(Skip="TODO")]
public void clr_type_with_conflicting_name_cannot_be_added_using_custom_name_if_name_null_() {
    // var schema = Schema.Create(_ => { });
}



[Spec(nameof(ClrTypedCollectionSpecs.clr_type_with_conflicting_name_annotation_cannot_be_added_using_custom_name_if_name_null))]
[Fact(Skip="TODO")]
public void clr_type_with_conflicting_name_annotation_cannot_be_added_using_custom_name_if_name_null_() {
    // var schema = Schema.Create(_ => { });
}



[Spec(nameof(ClrTypedCollectionSpecs.clr_type_with_conflicting_name_cannot_be_added_using_custom_name_if_name_invalid))]
[Fact(Skip="TODO")]
public void clr_type_with_conflicting_name_cannot_be_added_using_custom_name_if_name_invalid_() {
    // var schema = Schema.Create(_ => { });
}



[Spec(nameof(ClrTypedCollectionSpecs.clr_type_with_conflicting_name_annotation_cannot_be_added_using_custom_name_if_name_invalid))]
[Fact(Skip="TODO")]
public void clr_type_with_conflicting_name_annotation_cannot_be_added_using_custom_name_if_name_invalid_() {
    // var schema = Schema.Create(_ => { });
}



[Spec(nameof(ClrTypedCollectionSpecs.clr_type_with_conflicting_name_cannot_be_added_using_custom_name_if_name_conflicting))]
[Fact(Skip="TODO")]
public void clr_type_with_conflicting_name_cannot_be_added_using_custom_name_if_name_conflicting_() {
    // var schema = Schema.Create(_ => { });
}



[Spec(nameof(ClrTypedCollectionSpecs.clr_type_with_conflicting_name_annotation_cannot_be_added_using_custom_name_if_name_conflicting))]
[Fact(Skip="TODO")]
public void clr_type_with_conflicting_name_annotation_cannot_be_added_using_custom_name_if_name_conflicting_() {
    // var schema = Schema.Create(_ => { });
}



[Spec(nameof(ClrTypedCollectionSpecs.clr_type_with_conflicting_name_can_be_added_via_type_param_using_custom_name))]
[Fact(Skip="TODO")]
public void clr_type_with_conflicting_name_can_be_added_via_type_param_using_custom_name_() {
    // var schema = Schema.Create(_ => { });
}



[Spec(nameof(ClrTypedCollectionSpecs.clr_type_with_conflicting_name_annotation_can_be_added_via_type_param_using_custom_name))]
[Fact(Skip="TODO")]
public void clr_type_with_conflicting_name_annotation_can_be_added_via_type_param_using_custom_name_() {
    // var schema = Schema.Create(_ => { });
}



[Spec(nameof(ClrTypedCollectionSpecs.cannot_add_clr_type_to_item_with_custom_name_if_name_conflicts))]
[Fact(Skip="TODO")]
public void cannot_add_clr_type_to_item_with_custom_name_if_name_conflicts_() {
    // var schema = Schema.Create(_ => { });
}



[Spec(nameof(ClrTypedCollectionSpecs.cannot_add_clr_type_to_item_via_type_param_with_custom_name_if_name_conflicts))]
[Fact(Skip="TODO")]
public void cannot_add_clr_type_to_item_via_type_param_with_custom_name_if_name_conflicts_() {
    // var schema = Schema.Create(_ => { });
}



[Spec(nameof(ClrTypedCollectionSpecs.cannot_add_clr_type_to_item_with_custom_name_if_name_invalid))]
[Fact(Skip="TODO")]
public void cannot_add_clr_type_to_item_with_custom_name_if_name_invalid_() {
    // var schema = Schema.Create(_ => { });
}



[Spec(nameof(ClrTypedCollectionSpecs.cannot_add_clr_type_to_item_via_type_param_with_custom_name_if_name_invalid))]
[Fact(Skip="TODO")]
public void cannot_add_clr_type_to_item_via_type_param_with_custom_name_if_name_invalid_() {
    // var schema = Schema.Create(_ => { });
}



[Spec(nameof(ClrTypedCollectionSpecs.cannot_add_clr_type_to_item_with_custom_name_if_name_null))]
[Fact(Skip="TODO")]
public void cannot_add_clr_type_to_item_with_custom_name_if_name_null_() {
    // var schema = Schema.Create(_ => { });
}



[Spec(nameof(ClrTypedCollectionSpecs.cannot_add_clr_type_to_item_via_type_param_with_custom_name_if_name_null))]
[Fact(Skip="TODO")]
public void cannot_add_clr_type_to_item_via_type_param_with_custom_name_if_name_null_() {
    // var schema = Schema.Create(_ => { });
}


}
}
// Source Hash Code: 2544252814998589158