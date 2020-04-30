#nullable enable

using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Diagnostics.CodeAnalysis;
using GraphZen.Infrastructure;
using JetBrains.Annotations;

using Xunit;
using static GraphZen.TypeSystem.FunctionalTests.Specs.TypeSystemSpecs;
namespace GraphZen.TypeSystem.FunctionalTests.SchemaBuilder.Objects {
[NoReorder]
public abstract  class SchemaBuilderObjectsTestsScaffold {

[Spec(nameof(NamedCollectionSpecs.named_item_cannot_be_added_with_null_value))]
[Fact]
public void named_item_cannot_be_added_with_null_value() {
    // Priority: High
    var schema = Schema.Create(_ => {

    });
    throw new NotImplementedException();
}



[Spec(nameof(NamedCollectionSpecs.named_item_cannot_be_removed_with_invalid_name))]
[Fact]
public void named_item_cannot_be_removed_with_invalid_name() {
    // Priority: High
    var schema = Schema.Create(_ => {

    });
    throw new NotImplementedException();
}



[Spec(nameof(NamedCollectionSpecs.named_item_cannot_be_removed_with_null_value))]
[Fact]
public void named_item_cannot_be_removed_with_null_value() {
    // Priority: High
    var schema = Schema.Create(_ => {

    });
    throw new NotImplementedException();
}



[Spec(nameof(NamedCollectionSpecs.named_item_cannot_be_renamed_with_an_invalid_name))]
[Fact]
public void named_item_cannot_be_renamed_with_an_invalid_name() {
    // Priority: High
    var schema = Schema.Create(_ => {

    });
    throw new NotImplementedException();
}



[Spec(nameof(NamedCollectionSpecs.named_item_cannot_be_renamed_with_null_value))]
[Fact]
public void named_item_cannot_be_renamed_with_null_value() {
    // Priority: High
    var schema = Schema.Create(_ => {

    });
    throw new NotImplementedException();
}


}
}
