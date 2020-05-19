#nullable enable

using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Diagnostics.CodeAnalysis;
using GraphZen.Infrastructure;
using JetBrains.Annotations;

// ReSharper disable All
using FluentAssertions;
using Xunit;
using static GraphZen.TypeSystem.FunctionalTests.Specs.TypeSystemSpecs.SdlSpec;

namespace GraphZen.TypeSystem.FunctionalTests.Schema_.Enums.EnumType.Values.EnumValue.Description {
[NoReorder]
public abstract  class SdlTests {


[Spec(nameof(item_can_be_defined_by_sdl))]
[Fact(Skip="TODO")]
public void item_can_be_defined_by_sdlschemaBuilder() {
    // var schema = Schema.Create(schemaBuilder => { });
}


}
// Move SdlTests into a separate file to start writing tests
[NoReorder] 
public  class SdlTestsScaffold {
}
}
// Source Hash Code: 10663349948341359576