// Copyright (c) GraphZen LLC. All rights reserved.
// Licensed under the GraphZen Community License. See the LICENSE file in the project root for license information.

#nullable enable

// ReSharper disable All
using System.Diagnostics.CodeAnalysis;
using FluentAssertions;
using GraphZen.Infrastructure;
using JetBrains.Annotations;
using Xunit;
using static GraphZen.TypeSystem.FunctionalTests.Specs.TypeSystemSpecs.DirectiveAnnotationSpecs;

namespace GraphZen.TypeSystem.FunctionalTests.Schema_.DirectiveAnnotations
{
    [NoReorder]
    public abstract class DirectiveAnnotationTestsScaffold
    {
        [Spec(nameof(directive_annotations_are_removed_when_directive_is_ignored))]
        [Fact(Skip = "TODO")]
        public void directive_annotations_are_removed_when_directive_is_ignored_()
        {
            // var schema = Schema.Create(_ => { });
        }
    }
}
// Source Hash Code: 2441915996384749288