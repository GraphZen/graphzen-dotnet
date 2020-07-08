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
using static GraphZen.TypeSystem.FunctionalTests.Specs.TypeSystemSpecs.DirectiveAnnotationSpecs;

namespace GraphZen.TypeSystem.FunctionalTests.Schema_.Scalars.ScalarType.DirectiveAnnotations {
[NoReorder]
public abstract  class DirectiveAnnotationTestsScaffold {


[Spec(nameof(directive_annotations_are_removed_when_directive_is_ignored))]
[Fact(Skip="TODO")]
public void directive_annotations_are_removed_when_directive_is_ignored_() {
    // var schema = Schema.Create(_ => { });
}


}
}
// Source Hash Code: 7850447853311149112