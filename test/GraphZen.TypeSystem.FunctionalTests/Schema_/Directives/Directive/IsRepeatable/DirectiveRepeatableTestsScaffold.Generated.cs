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
    public abstract class DirectiveRepeatableTests
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


        [Spec(nameof(when_created_via_clr_attribute_honors_allow_multiple_true))]
        [Fact(Skip = "TODO")]
        public void when_created_via_clr_attribute_honors_allow_multiple_true_()
        {
            // var schema = Schema.Create(_ => { });
        }


        [Spec(nameof(when_created_via_clr_attribute_honors_allow_multiple_false))]
        [Fact(Skip = "TODO")]
        public void when_created_via_clr_attribute_honors_allow_multiple_false_()
        {
            // var schema = Schema.Create(_ => { });
        }


        [Spec(nameof(when_clr_attribute_set_honors_allow_multiple_true))]
        [Fact(Skip = "TODO")]
        public void when_clr_attribute_set_honors_allow_multiple_true_()
        {
            // var schema = Schema.Create(_ => { });
        }


        [Spec(nameof(when_clr_attribute_set_honors_allow_multiple_false))]
        [Fact(Skip = "TODO")]
        public void when_clr_attribute_set_honors_allow_multiple_false_()
        {
            // var schema = Schema.Create(_ => { });
        }
    }

// Move DirectiveRepeatableTests into a separate file to start writing tests
    [NoReorder]
    public class DirectiveRepeatableTestsScaffold
    {
    }
}
// Source Hash Code: 17123966632907472468