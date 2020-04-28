#nullable enable

using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Diagnostics.CodeAnalysis;
using GraphZen.Infrastructure;
using JetBrains.Annotations;

using Xunit;
namespace GraphZen.TypeSystem.FunctionalTests.SchemaBuilder.Enums.EnumType.Values.EnumValue.Name {
public partial class EnumValueNameTests {

[Spec("it_cannot_be_removed")]
[Fact]
public void it_cannot_be_removed() {
    var schema = Schema.Create(_ => {

    });
}

// SpecId: it_cannot_be_removed
// Priority: Low




[Spec("it_can_be_updated")]
[Fact]
public void it_can_be_updated() {
    var schema = Schema.Create(_ => {

    });
}

// SpecId: it_can_be_updated
// Priority: Low



}
}
