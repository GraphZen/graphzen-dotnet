#nullable enable

using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Diagnostics.CodeAnalysis;
using GraphZen.Infrastructure;
using JetBrains.Annotations;

using Xunit;
using static GraphZen.TypeSystem.FunctionalTests.Specs.TypeSystemSpecs;
namespace GraphZen.TypeSystem.FunctionalTests.SchemaBuilder.Objects.ObjectType.Fields.Field.Description {
[NoReorder]
public abstract  class DescriptionTests {

[Spec(nameof(SdlSpec.element_can_be_defined_via_sdl))]
[Fact]
public void element_can_be_defined_via_sdl() {
    // Priority: High
    var schema = Schema.Create(_ => {

    });
    throw new NotImplementedException();
}



[Spec(nameof(SdlSpec.element_can_be_defined_via_sdl_extension))]
[Fact]
public void element_can_be_defined_via_sdl_extension() {
    // Priority: High
    var schema = Schema.Create(_ => {

    });
    throw new NotImplementedException();
}



[Spec(nameof(OptionalSpecs.optional_item_can_be_removed))]
[Fact]
public void optional_item_can_be_removed() {
    // Priority: High
    var schema = Schema.Create(_ => {

    });
    throw new NotImplementedException();
}



[Spec(nameof(OptionalSpecs.parent_can_be_created_without_optional_item))]
[Fact]
public void parent_can_be_created_without_optional_item() {
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
// Move DescriptionTests into a separate file to start writing tests
[NoReorder] 
public  class DescriptionTestsScaffold {
}
}
