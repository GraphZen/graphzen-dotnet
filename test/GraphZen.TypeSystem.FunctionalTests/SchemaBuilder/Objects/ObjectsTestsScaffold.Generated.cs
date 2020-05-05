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
namespace GraphZen.TypeSystem.FunctionalTests.SchemaBuilder.Objects {
[NoReorder]
public abstract  class ObjectsTestsScaffold {

[Spec(nameof(ClrTypedCollectionSpecs.adding_clr_type_to_item_via_type_param_does_not_change_name))]
[Fact]
public void adding_clr_type_to_item_via_type_param_does_not_change_name() {
    // Priority: High
    var schema = Schema.Create(_ => {

    });
    throw new NotImplementedException();
}



[Spec(nameof(ClrTypedCollectionSpecs.adding_clr_type_with_name_annotation_to_item_via_type_param_does_not_change_name))]
[Fact]
public void adding_clr_type_with_name_annotation_to_item_via_type_param_does_not_change_name() {
    // Priority: High
    var schema = Schema.Create(_ => {

    });
    throw new NotImplementedException();
}



[Spec(nameof(ClrTypedCollectionSpecs.clr_typed_item_can_have_clr_type_changed))]
[Fact]
public void clr_typed_item_can_have_clr_type_changed() {
    // Priority: High
    var schema = Schema.Create(_ => {

    });
    throw new NotImplementedException();
}



[Spec(nameof(ClrTypedCollectionSpecs.clr_typed_item_can_have_clr_type_changed_via_type_param))]
[Fact]
public void clr_typed_item_can_have_clr_type_changed_via_type_param() {
    // Priority: High
    var schema = Schema.Create(_ => {

    });
    throw new NotImplementedException();
}



[Spec(nameof(ClrTypedCollectionSpecs.clr_typed_item_with_name_annotation_type_removed_should_retain_annotated_name))]
[Fact]
public void clr_typed_item_with_name_annotation_type_removed_should_retain_annotated_name() {
    // Priority: High
    var schema = Schema.Create(_ => {

    });
    throw new NotImplementedException();
}



[Spec(nameof(ClrTypedCollectionSpecs.clr_typed_item_with_type_removed_should_retain_clr_type_name))]
[Fact]
public void clr_typed_item_with_type_removed_should_retain_clr_type_name() {
    // Priority: High
    var schema = Schema.Create(_ => {

    });
    throw new NotImplementedException();
}



[Spec(nameof(ClrTypedCollectionSpecs.subsequently_clr_typed_item_can_have_custom_named_removed))]
[Fact]
public void subsequently_clr_typed_item_can_have_custom_named_removed() {
    // Priority: High
    var schema = Schema.Create(_ => {

    });
    throw new NotImplementedException();
}



[Spec(nameof(ClrTypedCollectionSpecs.subsequently_clr_typed_item_cannot_have_custom_named_removed_if_clr_type_name_annotation_conflicts))]
[Fact]
public void subsequently_clr_typed_item_cannot_have_custom_named_removed_if_clr_type_name_annotation_conflicts() {
    // Priority: High
    var schema = Schema.Create(_ => {

    });
    throw new NotImplementedException();
}



[Spec(nameof(ClrTypedCollectionSpecs.subsequently_clr_typed_item_cannot_have_custom_named_removed_if_clr_type_name_conflicts))]
[Fact]
public void subsequently_clr_typed_item_cannot_have_custom_named_removed_if_clr_type_name_conflicts() {
    // Priority: High
    var schema = Schema.Create(_ => {

    });
    throw new NotImplementedException();
}



[Spec(nameof(ClrTypedCollectionSpecs.untyped_item_can_have_clr_type_added_via_type_param))]
[Fact]
public void untyped_item_can_have_clr_type_added_via_type_param() {
    // Priority: High
    var schema = Schema.Create(_ => {

    });
    throw new NotImplementedException();
}


}
}
