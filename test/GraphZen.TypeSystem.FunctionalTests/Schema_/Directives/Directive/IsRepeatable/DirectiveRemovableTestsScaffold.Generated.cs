// Copyright (c) GraphZen LLC. All rights reserved.
// Licensed under the GraphZen Community License. See the LICENSE file in the project root for license information.

#nullable enable

// ReSharper disable All
using System.Diagnostics.CodeAnalysis;
using FluentAssertions;
using GraphZen.Infrastructure;
using JetBrains.Annotations;
using Xunit;
using static GraphZen.TypeSystem.FunctionalTests.Specs.TypeSystemSpecs.DirectiveIsRepeatableSpecs;

namespace GraphZen.TypeSystem.FunctionalTests.Schema_.Directives.Directive.IsRepeatable
{
    [NoReorder]
    public abstract class DirectiveRemovableTests
    {
        [Spec(nameof(is_initially_false))]
        [Fact(Skip = "TODO")]
        public void is_initially_false_()
        {
            // var schema = Schema.Create(_ => { });
        }


        [Spec(nameof(can_be_set_to_true))]
        [Fact(Skip = "TODO")]
        public void can_be_set_to_true_()
        {
            // var schema = Schema.Create(_ => { });
        }


        [Spec(nameof(can_be_set_to_false))]
        [Fact(Skip = "TODO")]
        public void can_be_set_to_false_()
        {
            // var schema = Schema.Create(_ => { });
        }
    }

// Move DirectiveRemovableTests into a separate file to start writing tests
    [NoReorder]
    public class DirectiveRemovableTestsScaffold
    {
    }
}
// Source Hash Code: 2476160055252296534