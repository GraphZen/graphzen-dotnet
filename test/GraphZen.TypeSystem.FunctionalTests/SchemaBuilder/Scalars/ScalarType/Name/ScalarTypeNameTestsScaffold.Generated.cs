#nullable enable

using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Diagnostics.CodeAnalysis;
using GraphZen.Infrastructure;
using JetBrains.Annotations;

using Xunit;
using static GraphZen.TypeSystem.FunctionalTests.Specs.TypeSystemSpecs;
namespace GraphZen.TypeSystem.FunctionalTests.SchemaBuilder.Scalars.ScalarType.Name {
[NoReorder]
public abstract  class ScalarTypeNameTests {

[Spec(nameof(UpdateableSpecs.it_can_be_updated))]
[Fact]
public void it_can_be_updated() {
    // Priority: Low
    var schema = Schema.Create(_ => {

    });
    throw new NotImplementedException();
}



[Spec(nameof(RequiredSpecs.required_item_cannot_be_removed))]
[Fact]
public void required_item_cannot_be_removed() {
    // Priority: Low
    var schema = Schema.Create(_ => {

    });
    throw new NotImplementedException();
}


}
// Move ScalarTypeNameTests into a separate file to start writing tests
[NoReorder] 
public  class ScalarTypeNameTestsScaffold {
}
}