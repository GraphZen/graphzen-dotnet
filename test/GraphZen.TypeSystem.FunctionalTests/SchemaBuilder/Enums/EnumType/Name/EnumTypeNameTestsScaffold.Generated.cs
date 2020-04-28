#nullable enable

using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Diagnostics.CodeAnalysis;
using GraphZen.Infrastructure;
using JetBrains.Annotations;

using Xunit;
using static GraphZen.TypeSystem.FunctionalTests.Specs.TypeSystemSpecs;
namespace GraphZen.TypeSystem.FunctionalTests.SchemaBuilder.Enums.EnumType.Name {
[NoReorder]
public abstract  class EnumTypeNameTests {

[Spec(nameof(UpdateableSpecs.it_can_be_updated))]
[Fact(Skip = "generated")]
public void it_can_be_updated() {
    // Priority: Low
    var schema = Schema.Create(_ => {

    });
    throw new NotImplementedException();
}



[Spec(nameof(RequiredSpecs.required_item_cannot_be_removed))]
[Fact(Skip = "generated")]
public void required_item_cannot_be_removed() {
    // Priority: Low
    var schema = Schema.Create(_ => {

    });
    throw new NotImplementedException();
}


}
// Move EnumTypeNameTests into a separate file to start writing tests
[NoReorder] 
public  class EnumTypeNameTestsScaffold {
}
}
