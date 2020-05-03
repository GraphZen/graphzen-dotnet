#nullable enable

using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Diagnostics.CodeAnalysis;
using GraphZen.Infrastructure;
using JetBrains.Annotations;

using Xunit;
using static GraphZen.TypeSystem.FunctionalTests.Specs.TypeSystemSpecs;
namespace GraphZen.TypeSystem.FunctionalTests.SchemaBuilder.Enums.EnumType.DirectiveAnnotations.DirectiveAnnotation {
[NoReorder]
public abstract  class DirectiveAnnotationTests {

[Spec(nameof(SdlSpec.element_can_be_defined_via_sdl))]
[Fact]
public void item_can_be_defined_via_sdl() {
    // Priority: Low
    var schema = Schema.Create(_ => {

    });
    throw new NotImplementedException();
}


}
// Move DirectiveAnnotationTests into a separate file to start writing tests
[NoReorder] 
public  class DirectiveAnnotationTestsScaffold {
}
}
