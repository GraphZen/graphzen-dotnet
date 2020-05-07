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

[Spec(nameof(UniquelyInputOutputTypeCollectionSpecs.named_item_cannot_be_added_if_name_conflicts_with_type_identity_of_opposite_io))]
[Fact(Skip="TODO")]
public void named_item_cannot_be_added_if_name_conflicts_with_type_identity_of_opposite_io_() {
    var schema = Schema.Create(_ => {

    });
}



[Spec(nameof(UniquelyInputOutputTypeCollectionSpecs.named_item_cannot_be_renamed_to_name_conflicts_with_type_identity_of_opposite_io))]
[Fact(Skip="TODO")]
public void named_item_cannot_be_renamed_to_name_conflicts_with_type_identity_of_opposite_io_() {
    var schema = Schema.Create(_ => {

    });
}



[Spec(nameof(UniquelyInputOutputTypeCollectionSpecs.clr_typed_item_cannot_be_renamed_if_name_conflicts_with_type_identity_of_opposite_io))]
[Fact(Skip="TODO")]
public void clr_typed_item_cannot_be_renamed_if_name_conflicts_with_type_identity_of_opposite_io_() {
    var schema = Schema.Create(_ => {

    });
}



[Spec(nameof(UniquelyInputOutputTypeCollectionSpecs.clr_typed_item_with_name_attribute_cannot_be_added_if_name_attribute_conflicts_with_type_identity_of_opposite_io))]
[Fact(Skip="TODO")]
public void clr_typed_item_with_name_attribute_cannot_be_added_if_name_attribute_conflicts_with_type_identity_of_opposite_io_() {
    var schema = Schema.Create(_ => {

    });
}


}
}
