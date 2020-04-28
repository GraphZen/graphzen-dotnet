#nullable enable

using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Diagnostics.CodeAnalysis;
using GraphZen.Infrastructure;
using JetBrains.Annotations;

using Xunit;
using static GraphZen.TypeSystem.FunctionalTests.Specs.TypeSystemSpecs;
namespace GraphZen.TypeSystem.FunctionalTests.SchemaBuilder.Directives {
[NoReorder]
public abstract  class SchemaBuilderDirectivesTests {

[Spec(nameof(NamedCollectionSpecs.named_item_can_be_added))]
[Fact(Skip = "generated")]
public void named_item_can_be_added() {
    // Priority: High
    var schema = Schema.Create(_ => {

    });
}



[Spec(nameof(NamedCollectionSpecs.named_item_can_be_removed))]
[Fact(Skip = "generated")]
public void named_item_can_be_removed() {
    // Priority: High
    var schema = Schema.Create(_ => {

    });
}



[Spec(nameof(NamedCollectionSpecs.named_item_can_be_renamed))]
[Fact(Skip = "generated")]
public void named_item_can_be_renamed() {
    // Priority: High
    var schema = Schema.Create(_ => {

    });
}



[Spec(nameof(NamedCollectionSpecs.named_item_cannot_be_renamed_if_name_already_exists))]
[Fact(Skip = "generated")]
public void named_item_cannot_be_renamed_if_name_already_exists() {
    // Priority: High
    var schema = Schema.Create(_ => {

    });
}



[Spec(nameof(NamedCollectionSpecs.named_item_name_must_be_valid_name))]
[Fact(Skip = "generated")]
public void named_item_name_must_be_valid_name() {
    // Priority: High
    var schema = Schema.Create(_ => {

    });
}


}
// Move SchemaBuilderDirectivesTests into a separate file to start writing tests
[NoReorder] 
public  class SchemaBuilderDirectivesTestsScaffold {
}
}
