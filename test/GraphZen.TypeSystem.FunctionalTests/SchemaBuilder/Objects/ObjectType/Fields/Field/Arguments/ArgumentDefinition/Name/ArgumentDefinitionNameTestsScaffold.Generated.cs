#nullable enable

using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Diagnostics.CodeAnalysis;
using GraphZen.Infrastructure;
using JetBrains.Annotations;

using Xunit;
using static GraphZen.TypeSystem.FunctionalTests.Specs.TypeSystemSpecs;
namespace GraphZen.TypeSystem.FunctionalTests.SchemaBuilder.Objects.ObjectType.Fields.Field.Arguments.ArgumentDefinition.Name {
[NoReorder]
public abstract  class ArgumentDefinitionNameTests {

[Spec(nameof(RequiredSpecs.required_item_cannot_be_removed))]
[Fact]
public void required_item_cannot_be_removed() {
    // Priority: High
    var schema = Schema.Create(_ => {

    });
    throw new NotImplementedException();
}



[Spec(nameof(UpdateableSpecs.updateable_item_can_be_updated))]
[Fact]
public void updateable_item_can_be_updated() {
    // Priority: High
    var schema = Schema.Create(_ => {

    });
    throw new NotImplementedException();
}


}
// Move ArgumentDefinitionNameTests into a separate file to start writing tests
[NoReorder] 
public  class ArgumentDefinitionNameTestsScaffold {
}
}
