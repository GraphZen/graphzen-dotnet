#nullable enable

// ReSharper disable All
using System.Diagnostics.CodeAnalysis;
using FluentAssertions;
using GraphZen.Infrastructure;
using JetBrains.Annotations;
using Xunit;
using static GraphZen.TypeSystem.FunctionalTests.Specs.TypeSystemSpecs.DirectiveAnnotationSpecs;

namespace GraphZen.TypeSystem.FunctionalTests.Schema_.Objects.ObjectType.DirectiveAnnotations
{
    [NoReorder]
    public abstract class DirectiveAnnotationTestsScaffold
    {
        [Spec(nameof(directive_annotations_are_renamed_when_directive_is_renamed))]
        [Fact(Skip = "TODO")]
        public void directive_annotations_are_renamed_when_directive_is_renamed_()
        {
            // var schema = Schema.Create(_ => { });
        }
    }
}
// Source Hash Code: 13124561500620689138