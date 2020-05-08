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
namespace GraphZen.TypeSystem.FunctionalTests.SchemaBuilder.InputObjects {
[NoReorder]
public abstract  class InputObjectsTestsScaffold {

[Spec(nameof(UniquelyInputOutputTypeCollectionSpecs.subsequently_clr_typed_item_cannot_have_custom_name_removed_if_clr_type_name_conflicts_with_type_identity_of_opposite_io))]
[Fact(Skip="TODO")]
public void subsequently_clr_typed_item_cannot_have_custom_name_removed_if_clr_type_name_conflicts_with_type_identity_of_opposite_io_() {
    var schema = Schema.Create(_ => {

    });
}



[Spec(nameof(UniquelyInputOutputTypeCollectionSpecs.subsequently_clr_typed_item_cannot_have_custom_name_removed_if_clr_type_name_annotation_conflicts_with_type_identity_of_opposite_io))]
[Fact(Skip="TODO")]
public void subsequently_clr_typed_item_cannot_have_custom_name_removed_if_clr_type_name_annotation_conflicts_with_type_identity_of_opposite_io_() {
    var schema = Schema.Create(_ => {

    });
}


}
}
// Source Hash Code: 1932284580