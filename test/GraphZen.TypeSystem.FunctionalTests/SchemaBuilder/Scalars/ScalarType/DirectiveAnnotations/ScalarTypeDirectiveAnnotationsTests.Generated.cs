#nullable enable

using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Diagnostics.CodeAnalysis;
using GraphZen.Infrastructure;
using JetBrains.Annotations;

using Xunit;
using static GraphZen.TypeSystem.FunctionalTests.Specs.TypeSystemSpecs;
namespace GraphZen.TypeSystem.FunctionalTests.SchemaBuilder.Scalars.ScalarType.DirectiveAnnotations {
public partial class ScalarTypeDirectiveAnnotationsTests {

// Priority: Low
// Subject Name: DirectiveAnnotations
[Spec(nameof(NamedCollectionSpecs.named_item_can_be_added))]
[Fact]
public void named_item_can_be_added() {
    var schema = Schema.Create(_ => {

    });
}



// Priority: Low
// Subject Name: DirectiveAnnotations
[Spec(nameof(NamedCollectionSpecs.named_item_can_be_renamed))]
[Fact]
public void named_item_can_be_renamed() {
    var schema = Schema.Create(_ => {

    });
}



// Priority: Low
// Subject Name: DirectiveAnnotations
[Spec(nameof(NamedCollectionSpecs.named_item_cannot_be_renamed_if_name_already_exists))]
[Fact]
public void named_item_cannot_be_renamed_if_name_already_exists() {
    var schema = Schema.Create(_ => {

    });
}



// Priority: Low
// Subject Name: DirectiveAnnotations
[Spec(nameof(NamedCollectionSpecs.named_item_can_be_removed))]
[Fact]
public void named_item_can_be_removed() {
    var schema = Schema.Create(_ => {

    });
}



// Priority: Low
// Subject Name: DirectiveAnnotations
[Spec(nameof(NamedCollectionSpecs.named_item_name_must_be_valid_name))]
[Fact]
public void named_item_name_must_be_valid_name() {
    var schema = Schema.Create(_ => {

    });
}


}
}
