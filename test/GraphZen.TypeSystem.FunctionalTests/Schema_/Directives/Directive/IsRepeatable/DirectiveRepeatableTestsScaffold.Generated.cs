// Copyright (c) GraphZen LLC. All rights reserved.
// Licensed under the GraphZen Community License. See the LICENSE file in the project root for license information.

#nullable enable

// ReSharper disable All
using System.Diagnostics.CodeAnalysis;
using FluentAssertions;
using GraphZen.Infrastructure;
using JetBrains.Annotations;
using Xunit;
using static GraphZen.TypeSystem.FunctionalTests.Specs.TypeSystemSpecs.DirectiveRepeatableSpecs;

namespace GraphZen.TypeSystem.FunctionalTests.Schema_.Directives.Directive.IsRepeatable
{
    [NoReorder]
    public abstract class DirectiveRepeatableTestsScaffold
    {
        [Spec(nameof(cannot_be_set_to_true_when_clr_attribute_allow_multiple_false))]
        [Fact(Skip = "TODO")]
        public void cannot_be_set_to_true_when_clr_attribute_allow_multiple_false_()
        {
            // var schema = Schema.Create(_ => { });
        }


        [Spec(nameof(cannot_be_set_to_false_when_clr_attribute_allow_multiple_true))]
        [Fact(Skip = "TODO")]
        public void cannot_be_set_to_false_when_clr_attribute_allow_multiple_true_()
        {
            // var schema = Schema.Create(_ => { });
        }
    }
}
// Source Hash Code: 14170773858591568064