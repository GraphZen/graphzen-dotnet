#nullable enable

using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Diagnostics.CodeAnalysis;
using GraphZen.Infrastructure;
using JetBrains.Annotations;

using Xunit;
using static GraphZen.TypeSystem.FunctionalTests.Specs.TypeSystemSpecs;
namespace GraphZen.TypeSystem.FunctionalTests.SchemaBuilder.InputObjects.InputObjectType.Fields.InputField.DirectiveAnnotations {
[NoReorder]
public  class InputFieldDirectiveAnnotationsTests {

[Spec(nameof(NamedCollectionSpecs.named_item_can_be_added))]
[Fact(Skip = "generated")]
public void named_item_can_be_added() {
    // Priority: Low
    var schema = Schema.Create(_ => {

    });
}



[Spec(nameof(NamedCollectionSpecs.named_item_can_be_removed))]
[Fact(Skip = "generated")]
public void named_item_can_be_removed() {
    // Priority: Low
    var schema = Schema.Create(_ => {

    });
}



[Spec(nameof(NamedCollectionSpecs.named_item_can_be_renamed))]
[Fact(Skip = "generated")]
public void named_item_can_be_renamed() {
    // Priority: Low
    var schema = Schema.Create(_ => {

    });
}



[Spec(nameof(NamedCollectionSpecs.named_item_cannot_be_renamed_if_name_already_exists))]
[Fact(Skip = "generated")]
public void named_item_cannot_be_renamed_if_name_already_exists() {
    // Priority: Low
    var schema = Schema.Create(_ => {

    });
}



[Spec(nameof(NamedCollectionSpecs.named_item_name_must_be_valid_name))]
[Fact(Skip = "generated")]
public void named_item_name_must_be_valid_name() {
    // Priority: Low
    var schema = Schema.Create(_ => {

    });
}


}
// Move InputFieldDirectiveAnnotationsTests into a separate file to start writing tests
[NoReorder] 
public  class InputFieldDirectiveAnnotationsTestsScaffold {
}
}
