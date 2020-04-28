#nullable enable

using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Diagnostics.CodeAnalysis;
using GraphZen.Infrastructure;
using JetBrains.Annotations;

using Xunit;
namespace GraphZen.TypeSystem.FunctionalTests.SchemaBuilder.InputObjects.InputObjectType.DirectiveAnnotations {
public partial class InputObjectTypeDirectiveAnnotationsTests {

[Spec("item_can_be_removed")]
[Fact]
public void item_can_be_removed() {
    var schema = Schema.Create(_ => {

    });
}

// SpecId: item_can_be_removed
// Priority: Low




[Spec("item_can_be_added")]
[Fact]
public void item_can_be_added() {
    var schema = Schema.Create(_ => {

    });
}

// SpecId: item_can_be_added
// Priority: Low




[Spec("item_cannot_be_renamed_if_name_already_exists")]
[Fact]
public void item_cannot_be_renamed_if_name_already_exists() {
    var schema = Schema.Create(_ => {

    });
}

// SpecId: item_cannot_be_renamed_if_name_already_exists
// Priority: Low




[Spec("item_name_must_be_valid_name")]
[Fact]
public void item_name_must_be_valid_name() {
    var schema = Schema.Create(_ => {

    });
}

// SpecId: item_name_must_be_valid_name
// Priority: Low




[Spec("item_can_be_renamed")]
[Fact]
public void item_can_be_renamed() {
    var schema = Schema.Create(_ => {

    });
}

// SpecId: item_can_be_renamed
// Priority: Low



}
}
