#nullable enable

using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Diagnostics.CodeAnalysis;
using GraphZen.Infrastructure;
using JetBrains.Annotations;

using Xunit;
using static GraphZen.TypeSystem.FunctionalTests.Specs.TypeSystemSpecs;
namespace GraphZen.TypeSystem.FunctionalTests.SchemaBuilder.Interfaces.InterfaceType.DirectiveAnnotations.DirectiveAnnotation.Arguments.Argument.Value {
public partial class ArgumentValueTests {

// Priority: Low
// Subject Name: Value
[Spec(nameof(UpdateableSpecs.it_can_be_updated))]
[Fact]
public void it_can_be_updated() {
    var schema = Schema.Create(_ => {

    });
}



// Priority: Low
// Subject Name: Value
[Spec(nameof(RequiredSpecs.required_item_cannot_be_removed))]
[Fact]
public void required_item_cannot_be_removed() {
    var schema = Schema.Create(_ => {

    });
}


}
}
