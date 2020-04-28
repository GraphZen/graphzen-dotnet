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
namespace GraphZen.TypeSystem.FunctionalTests.SchemaBuilder.Interfaces.InterfaceType.Fields.Field.Arguments.ArgumentDefinition.DefaultValue {
public partial class ArgumentDefinitionDefaultValueTests {

// Priority: Low
// Subject Name: DefaultValue
[Spec(nameof(OptionalSpecs.optional_item_can_be_removed))]
[Fact]
public void optional_item_can_be_removed() {
    var schema = Schema.Create(_ => {

    });
}



// Priority: Low
// Subject Name: DefaultValue
[Spec(nameof(OptionalSpecs.parent_can_be_created_without))]
[Fact]
public void parent_can_be_created_without() {
    var schema = Schema.Create(_ => {

    });
}


}
}
