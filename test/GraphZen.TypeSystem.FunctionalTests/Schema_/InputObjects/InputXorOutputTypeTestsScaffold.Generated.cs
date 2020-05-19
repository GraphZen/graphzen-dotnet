#nullable enable

using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Diagnostics.CodeAnalysis;
using GraphZen.Infrastructure;
using JetBrains.Annotations;

// ReSharper disable All
using FluentAssertions;
using Xunit;
using static GraphZen.TypeSystem.FunctionalTests.Specs.TypeSystemSpecs.InputXorOutputTypeSpecs;

namespace GraphZen.TypeSystem.FunctionalTests.Schema_.InputObjects {
[NoReorder]
public abstract  class InputXorOutputTypeTestsScaffold {


[Spec(nameof(clr_type_cannot_be_added_with_custom_name_if_name_conflicts_with_type_identity_of_opposite_io))]
[Fact(Skip="TODO")]
public void clr_type_cannot_be_added_with_custom_name_if_name_conflicts_with_type_identity_of_opposite_ioschemaBuilder() {
    // var schema = Schema.Create(schemaBuilder => { });
}




[Spec(nameof(clr_type_cannot_be_added_via_type_param_with_custom_name_if_name_conflicts_with_type_identity_of_opposite_io))]
[Fact(Skip="TODO")]
public void clr_type_cannot_be_added_via_type_param_with_custom_name_if_name_conflicts_with_type_identity_of_opposite_ioschemaBuilder() {
    // var schema = Schema.Create(schemaBuilder => { });
}




[Spec(nameof(clr_typed_item_cannot_be_added_with_custom_name_if_name_conflicts_with_type_identity_of_opposite_io))]
[Fact(Skip="TODO")]
public void clr_typed_item_cannot_be_added_with_custom_name_if_name_conflicts_with_type_identity_of_opposite_ioschemaBuilder() {
    // var schema = Schema.Create(schemaBuilder => { });
}




[Spec(nameof(clr_type_cannot_be_changed_with_custom_name_if_name_conflicts_with_type_identity_of_opposite_io))]
[Fact(Skip="TODO")]
public void clr_type_cannot_be_changed_with_custom_name_if_name_conflicts_with_type_identity_of_opposite_ioschemaBuilder() {
    // var schema = Schema.Create(schemaBuilder => { });
}


}
}
// Source Hash Code: 6686800278693175362