#nullable enable

using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Diagnostics.CodeAnalysis;
using GraphZen.Infrastructure;
using JetBrains.Annotations;

using Xunit;
using static GraphZen.TypeSystem.FunctionalTests.Specs.TypeSystemSpecs;
namespace GraphZen.TypeSystem.FunctionalTests.SchemaBuilder.Enums.EnumType.DirectiveAnnotations.DirectiveAnnotation.Name {
[NoReorder]
public abstract  class DirectiveAnnotationNameTests {

[Spec(nameof(UpdateableSpecs.updateable_item_can_be_updated))]
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
// Move DirectiveAnnotationNameTests into a separate file to start writing tests
[NoReorder] 
public  class DirectiveAnnotationNameTestsScaffold {
}
}
