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
namespace GraphZen.TypeSystem.FunctionalTests.SchemaBuilder.Interfaces.InterfaceType.Fields.Field.Arguments.ArgumentDefinition.DefaultValue {
[NoReorder]
public abstract  class DefaultValueTests {

[Spec(nameof(SdlSpec.element_can_be_defined_via_sdl))]
[Fact(Skip="TODO")]
public void defaultvalue__element_can_be_defined_via_sdl() {
    // Priority: Low
    var schema = Schema.Create(_ => {

    });
    throw new NotImplementedException();
}



[Spec(nameof(SdlSpec.element_can_be_defined_via_sdl_extension))]
[Fact(Skip="TODO")]
public void defaultvalue__element_can_be_defined_via_sdl_extension() {
    // Priority: Low
    var schema = Schema.Create(_ => {

    });
    throw new NotImplementedException();
}



[Spec(nameof(OptionalSpecs.optional_item_can_be_removed))]
[Fact(Skip="TODO")]
public void defaultvalue__optional_item_can_be_removed() {
    // Priority: Low
    var schema = Schema.Create(_ => {

    });
    throw new NotImplementedException();
}



[Spec(nameof(OptionalSpecs.parent_can_be_created_without_optional_item))]
[Fact(Skip="TODO")]
public void defaultvalue__parent_can_be_created_without_optional_item() {
    // Priority: Low
    var schema = Schema.Create(_ => {

    });
    throw new NotImplementedException();
}


}
// Move DefaultValueTests into a separate file to start writing tests
[NoReorder] 
public  class DefaultValueTestsScaffold {
}
}
