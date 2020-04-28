#nullable enable

using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Diagnostics.CodeAnalysis;
using GraphZen.Infrastructure;
using JetBrains.Annotations;

using Xunit;
using static GraphZen.TypeSystem.FunctionalTests.Specs.TypeSystemSpecs;
namespace GraphZen.TypeSystem.FunctionalTests.SchemaBuilder.Enums.EnumType.Values.EnumValue.Description {
[NoReorder]
public abstract  class EnumValueDescriptionTests {

[Spec(nameof(UpdateableSpecs.it_can_be_updated))]
[Fact(Skip = "generated")]
public void it_can_be_updated() {
    // Priority: Low
    var schema = Schema.Create(_ => {

    });
}



[Spec(nameof(OptionalSpecs.optional_item_can_be_removed))]
[Fact(Skip = "generated")]
public void optional_item_can_be_removed() {
    // Priority: Low
    var schema = Schema.Create(_ => {

    });
}



[Spec(nameof(OptionalSpecs.parent_can_be_created_without))]
[Fact(Skip = "generated")]
public void parent_can_be_created_without() {
    // Priority: Low
    var schema = Schema.Create(_ => {

    });
}


}
// Move EnumValueDescriptionTests into a separate file to start writing tests
[NoReorder] 
public  class EnumValueDescriptionTestsScaffold {
}
}
