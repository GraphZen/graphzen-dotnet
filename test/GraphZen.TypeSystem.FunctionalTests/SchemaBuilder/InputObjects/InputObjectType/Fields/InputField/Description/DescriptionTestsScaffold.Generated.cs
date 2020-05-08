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
namespace GraphZen.TypeSystem.FunctionalTests.SchemaBuilder.InputObjects.InputObjectType.Fields.InputField.Description {
[NoReorder]
public abstract  class DescriptionTests {

[Spec(nameof(SdlSpec.element_can_be_defined_via_sdl))]
[Fact(Skip="TODO")]
public void element_can_be_defined_via_sdl_() {
    var schema = Schema.Create(_ => {

    });
}



[Spec(nameof(SdlSpec.element_can_be_defined_via_sdl_extension))]
[Fact(Skip="TODO")]
public void element_can_be_defined_via_sdl_extension_() {
    var schema = Schema.Create(_ => {

    });
}



[Spec(nameof(UpdateableSpecs.updateable_item_can_be_updated))]
[Fact(Skip="TODO")]
public void updateable_item_can_be_updated_() {
    var schema = Schema.Create(_ => {

    });
}



[Spec(nameof(OptionalSpecs.optional_item_can_be_removed))]
[Fact(Skip="TODO")]
public void optional_item_can_be_removed_() {
    var schema = Schema.Create(_ => {

    });
}



[Spec(nameof(OptionalSpecs.parent_can_be_created_without_optional_item))]
[Fact(Skip="TODO")]
public void parent_can_be_created_without_optional_item_() {
    var schema = Schema.Create(_ => {

    });
}


}
// Move DescriptionTests into a separate file to start writing tests
[NoReorder] 
public  class DescriptionTestsScaffold {
}
}
// Source Hash Code: 13808431015123762218