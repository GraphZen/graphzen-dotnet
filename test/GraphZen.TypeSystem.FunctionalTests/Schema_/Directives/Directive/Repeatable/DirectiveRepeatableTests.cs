// Copyright (c) GraphZen LLC. All rights reserved.
// Licensed under the GraphZen Community License. See the LICENSE file in the project root for license information.

using System;
using System.Diagnostics.CodeAnalysis;
using FluentAssertions;
using GraphZen.Infrastructure;
using JetBrains.Annotations;
using Xunit;
using static GraphZen.TypeSystem.FunctionalTests.Specs.TypeSystemSpecs.DirectiveRepeatableSpecs;

namespace GraphZen.TypeSystem.FunctionalTests.Schema_.Directives.Directive.Repeatable
{
    [NoReorder]
    public class DirectiveRepeatableTests
    {
        public class PlainAttribute : Attribute
        {
        }

        [Spec(nameof(is_initially_false))]
        [Fact]
        public void is_initially_false_()
        {
            var schema = Schema.Create(_ => { _.Directive("Foo"); });
            schema.GetDirective("Foo").Repeatable.Should().BeFalse();
        }


        [Spec(nameof(can_be_set_to_true))]
        [Fact]
        public void can_be_set_to_true_()
        {
            var schema = Schema.Create(_ => { _.Directive("Foo").Repeatable(true); });
            schema.GetDirective("Foo").Repeatable.Should().BeTrue();
        }


        [Spec(nameof(can_be_set_to_false))]
        [Fact]
        public void can_be_set_to_false_()
        {
            var schema = Schema.Create(_ => { _.Directive("Foo").Repeatable(true).Repeatable(false); });
            schema.GetDirective("Foo").Repeatable.Should().BeFalse();
        }
    }
}