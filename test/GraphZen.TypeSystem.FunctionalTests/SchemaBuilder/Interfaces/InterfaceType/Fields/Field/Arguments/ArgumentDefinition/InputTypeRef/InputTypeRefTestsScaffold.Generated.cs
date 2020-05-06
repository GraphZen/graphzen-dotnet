#nullable enable

using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Diagnostics.CodeAnalysis;
using GraphZen.Infrastructure;
using JetBrains.Annotations;

using Xunit;
using static GraphZen.TypeSystem.FunctionalTests.Specs.TypeSystemSpecs;
// ReSharper disable All
namespace GraphZen.TypeSystem.FunctionalTests.SchemaBuilder.Interfaces.InterfaceType.Fields.Field.Arguments.ArgumentDefinition.InputTypeRef {
[NoReorder]
public abstract  class InputTypeRefTests {

[Spec(nameof(SdlSpec.element_can_be_defined_via_sdl_extension))]
[Fact(Skip="TODO")]
public void element_can_be_defined_via_sdl_extension_() {
    // Priority: Low
    var schema = Schema.Create(_ => {

    });
    throw new NotImplementedException();
}



[Spec(nameof(SdlSpec.element_can_be_defined_via_sdl))]
[Fact(Skip="TODO")]
public void element_can_be_defined_via_sdl_() {
    // Priority: Low
    var schema = Schema.Create(_ => {

    });
    throw new NotImplementedException();
}



[Spec(nameof(RequiredSpecs.required_item_cannot_be_removed))]
[Fact(Skip="TODO")]
public void required_item_cannot_be_removed_() {
    // Priority: Low
    var schema = Schema.Create(_ => {

    });
    throw new NotImplementedException();
}



[Spec(nameof(UpdateableSpecs.updateable_item_can_be_updated))]
[Fact(Skip="TODO")]
public void updateable_item_can_be_updated_() {
    // Priority: Low
    var schema = Schema.Create(_ => {

    });
    throw new NotImplementedException();
}


}
// Move InputTypeRefTests into a separate file to start writing tests
[NoReorder] 
public  class InputTypeRefTestsScaffold {
}
}
