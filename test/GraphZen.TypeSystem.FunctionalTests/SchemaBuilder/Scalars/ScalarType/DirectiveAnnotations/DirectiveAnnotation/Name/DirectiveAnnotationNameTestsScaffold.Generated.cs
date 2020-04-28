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
namespace GraphZen.TypeSystem.FunctionalTests.SchemaBuilder.Scalars.ScalarType.DirectiveAnnotations.DirectiveAnnotation.Name {
[NoReorder] //
public  class DirectiveAnnotationNameTests {
// Move me into a separate file to start writing tests
}
[NoReorder]
public partial class DirectiveAnnotationNameTestsScaffold {

// Priority: Low
// Subject Name: Name
[Spec(nameof(UpdateableSpecs.it_can_be_updated))]
[Fact(Skip = "generated")]
public void it_can_be_updated() {
    var schema = Schema.Create(_ => {

    });
}



// Priority: Low
// Subject Name: Name
[Spec(nameof(RequiredSpecs.required_item_cannot_be_removed))]
[Fact(Skip = "generated")]
public void required_item_cannot_be_removed() {
    var schema = Schema.Create(_ => {

    });
}


}
}
