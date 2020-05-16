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
namespace GraphZen.TypeSystem.FunctionalTests.Schema_.Objects.ObjectType.ClrType {

// testFile: .\test\GraphZen.TypeSystem.FunctionalTests\Schema_\Objects\ObjectType\ClrType\ClrTypeTests.cs
// testFileExists: True
// fileDir: .\test\GraphZen.TypeSystem.FunctionalTests\Schema_\Objects\ObjectType\ClrType

[NoReorder]
public abstract  class ClrTypeTestsScaffold {


// SpecId: clr_type_can_be_added
[Spec(nameof(ClrTypeSpecs.clr_type_can_be_added))]
[Fact(Skip="TODO")]
public void clr_type_can_be_added_() {
    // var schema = Schema.Create(_ => { });
}




// SpecId: clr_type_can_be_added_with_custom_name
[Spec(nameof(ClrTypeSpecs.clr_type_can_be_added_with_custom_name))]
[Fact(Skip="TODO")]
public void clr_type_can_be_added_with_custom_name_() {
    // var schema = Schema.Create(_ => { });
}




// SpecId: clr_type_can_be_added_via_type_param
[Spec(nameof(ClrTypeSpecs.clr_type_can_be_added_via_type_param))]
[Fact(Skip="TODO")]
public void clr_type_can_be_added_via_type_param_() {
    // var schema = Schema.Create(_ => { });
}




// SpecId: clr_type_can_be_added_via_type_param_with_custom_name
[Spec(nameof(ClrTypeSpecs.clr_type_can_be_added_via_type_param_with_custom_name))]
[Fact(Skip="TODO")]
public void clr_type_can_be_added_via_type_param_with_custom_name_() {
    // var schema = Schema.Create(_ => { });
}




// SpecId: clr_type_can_be_changed_with_custom_name
[Spec(nameof(ClrTypeSpecs.clr_type_can_be_changed_with_custom_name))]
[Fact(Skip="TODO")]
public void clr_type_can_be_changed_with_custom_name_() {
    // var schema = Schema.Create(_ => { });
}




// SpecId: clr_type_can_be_changed_via_type_param
[Spec(nameof(ClrTypeSpecs.clr_type_can_be_changed_via_type_param))]
[Fact(Skip="TODO")]
public void clr_type_can_be_changed_via_type_param_() {
    // var schema = Schema.Create(_ => { });
}




// SpecId: clr_type_can_be_changed_via_type_param_with_custom_name
[Spec(nameof(ClrTypeSpecs.clr_type_can_be_changed_via_type_param_with_custom_name))]
[Fact(Skip="TODO")]
public void clr_type_can_be_changed_via_type_param_with_custom_name_() {
    // var schema = Schema.Create(_ => { });
}




// SpecId: clr_type_name_should_be_unique
[Spec(nameof(ClrTypeSpecs.clr_type_name_should_be_unique))]
[Fact(Skip="TODO")]
public void clr_type_name_should_be_unique_() {
    // var schema = Schema.Create(_ => { });
}




// SpecId: clr_type_name_annotation_should_be_unique
[Spec(nameof(ClrTypeSpecs.clr_type_name_annotation_should_be_unique))]
[Fact(Skip="TODO")]
public void clr_type_name_annotation_should_be_unique_() {
    // var schema = Schema.Create(_ => { });
}




// SpecId: clr_type_name_annotation_should_be_valid
[Spec(nameof(ClrTypeSpecs.clr_type_name_annotation_should_be_valid))]
[Fact(Skip="TODO")]
public void clr_type_name_annotation_should_be_valid_() {
    // var schema = Schema.Create(_ => { });
}




// SpecId: custom_name_should_be_unique
[Spec(nameof(ClrTypeSpecs.custom_name_should_be_unique))]
[Fact(Skip="TODO")]
public void custom_name_should_be_unique_() {
    // var schema = Schema.Create(_ => { });
}




// SpecId: custom_name_should_be_valid
[Spec(nameof(ClrTypeSpecs.custom_name_should_be_valid))]
[Fact(Skip="TODO")]
public void custom_name_should_be_valid_() {
    // var schema = Schema.Create(_ => { });
}




// SpecId: custom_name_cannot_be_null
[Spec(nameof(ClrTypeSpecs.custom_name_cannot_be_null))]
[Fact(Skip="TODO")]
public void custom_name_cannot_be_null_() {
    // var schema = Schema.Create(_ => { });
}




// SpecId: changing_clr_type_changes_name
[Spec(nameof(ClrTypeSpecs.changing_clr_type_changes_name))]
[Fact(Skip="TODO")]
public void changing_clr_type_changes_name_() {
    // var schema = Schema.Create(_ => { });
}




// SpecId: changing_clr_type_with_name_annotation_changes_name
[Spec(nameof(ClrTypeSpecs.changing_clr_type_with_name_annotation_changes_name))]
[Fact(Skip="TODO")]
public void changing_clr_type_with_name_annotation_changes_name_() {
    // var schema = Schema.Create(_ => { });
}




// SpecId: clr_type_with_conflicting_name_can_be_added_using_custom_name
[Spec(nameof(ClrTypeSpecs.clr_type_with_conflicting_name_can_be_added_using_custom_name))]
[Fact(Skip="TODO")]
public void clr_type_with_conflicting_name_can_be_added_using_custom_name_() {
    // var schema = Schema.Create(_ => { });
}




// SpecId: clr_type_with_conflicting_name_annotation_can_be_added_using_custom_name
[Spec(nameof(ClrTypeSpecs.clr_type_with_conflicting_name_annotation_can_be_added_using_custom_name))]
[Fact(Skip="TODO")]
public void clr_type_with_conflicting_name_annotation_can_be_added_using_custom_name_() {
    // var schema = Schema.Create(_ => { });
}




// SpecId: clr_typed_item_with_name_annotation_when_clr_type_removed_should_retain_annotated_name
[Spec(nameof(ClrTypeSpecs.clr_typed_item_with_name_annotation_when_clr_type_removed_should_retain_annotated_name))]
[Fact(Skip="TODO")]
public void clr_typed_item_with_name_annotation_when_clr_type_removed_should_retain_annotated_name_() {
    // var schema = Schema.Create(_ => { });
}




// SpecId: custom_named_clr_typed_item_when_type_removed_should_retain_custom_name
[Spec(nameof(ClrTypeSpecs.custom_named_clr_typed_item_when_type_removed_should_retain_custom_name))]
[Fact(Skip="TODO")]
public void custom_named_clr_typed_item_when_type_removed_should_retain_custom_name_() {
    // var schema = Schema.Create(_ => { });
}


}
}
// Source Hash Code: 6829790794486411965