#nullable enable

using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Diagnostics.CodeAnalysis;
using GraphZen.Infrastructure;
using JetBrains.Annotations;

using Xunit;
using static GraphZen.TypeSystem.FunctionalTests.Specs.TypeSystemSpecs;
// ReSharper disable PartialTypeWithSinglePart
namespace GraphZen.TypeSystem.FunctionalTests.SchemaBuilder.Interfaces.InterfaceType.Fields.Field.Arguments.ArgumentDefinition.InputTypeRef {
// Move ArgumentDefinitionInputTypeRefTests into a separate file to start writing tests
[NoReorder] 
public  class ArgumentDefinitionInputTypeRefTestsScaffold {
}
[NoReorder]
public partial class ArgumentDefinitionInputTypeRefTests {

// Priority: Low
// Subject Name: InputTypeRef
[Spec(nameof(UpdateableSpecs.it_can_be_updated))]
[Fact(Skip = "generated")]
public void it_can_be_updated() {
    var schema = Schema.Create(_ => {

    });
}



// Priority: Low
// Subject Name: InputTypeRef
[Spec(nameof(RequiredSpecs.required_item_cannot_be_removed))]
[Fact(Skip = "generated")]
public void required_item_cannot_be_removed() {
    var schema = Schema.Create(_ => {

    });
}


}
}
