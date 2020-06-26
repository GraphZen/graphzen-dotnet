// Copyright (c) GraphZen LLC. All rights reserved.
// Licensed under the GraphZen Community License. See the LICENSE file in the project root for license information.

using System;
using System.Diagnostics.CodeAnalysis;
using FluentAssertions;
using GraphZen.Infrastructure;
using JetBrains.Annotations;
using Xunit;
using static GraphZen.TypeSystem.FunctionalTests.Specs.TypeSystemSpecs.DirectiveRepeatableSpecs;

namespace GraphZen.TypeSystem.FunctionalTests.Schema_.Directives.Directive.IsRepeatable
{
    [NoReorder]
    public class DirectiveRepeatableTests
    {
        // ReSharper disable once RedundantAttributeUsageProperty
        [AttributeUsage(AttributeTargets.All, AllowMultiple = false)]
        public class DisallowMultipleAttribute : Attribute
        {
        }

        [AttributeUsage(AttributeTargets.All, AllowMultiple = true)]
        public class AllowMultipleAttribute : Attribute
        {
        }


        [Spec(nameof(is_initially_false))]
        [Fact]
        public void is_initially_false_()
        {
            var schema = Schema.Create(_ => { _.Directive("Foo"); });
            schema.GetDirective("Foo").IsRepeatable.Should().BeFalse();
        }


        [Spec(nameof(can_be_set_to_true))]
        [Fact]
        public void can_be_set_to_true_()
        {
            var schema = Schema.Create(_ => { _.Directive("Foo").Repeatable(true); });
            schema.GetDirective("Foo").IsRepeatable.Should().BeTrue();
        }


        [Spec(nameof(can_be_set_to_false))]
        [Fact]
        public void can_be_set_to_false_()
        {
            var schema = Schema.Create(_ => { _.Directive("Foo").Repeatable(true).Repeatable(false); });
            schema.GetDirective("Foo").IsRepeatable.Should().BeFalse();
        }

        [Spec(nameof(when_created_via_clr_attribute_honors_allow_multiple_true))]
        [Fact]
        public void when_created_via_clr_attribute_honors_allow_multiple_true_()
        {
            var schema = Schema.Create(_ => { _.Directive<AllowMultipleAttribute>(); });
            schema.GetDirective<AllowMultipleAttribute>().IsRepeatable.Should().BeTrue();
        }


        [Spec(nameof(when_created_via_clr_attribute_honors_allow_multiple_false))]
        [Fact]
        public void when_created_via_clr_attribute_honors_allow_multiple_false_()
        {
            var schema = Schema.Create(_ => { _.Directive<DisallowMultipleAttribute>(); });
            schema.GetDirective<DisallowMultipleAttribute>().IsRepeatable.Should().BeFalse();
        }


        [Spec(nameof(when_clr_attribute_set_honors_allow_multiple_true))]
        [Fact]
        public void when_clr_attribute_set_honors_allow_multiple_true_()
        {
            var schema = Schema.Create(_ => { _.Directive("Foo").ClrType<AllowMultipleAttribute>(); });
            schema.GetDirective<AllowMultipleAttribute>().IsRepeatable.Should().BeTrue();
        }


        [Spec(nameof(when_clr_attribute_set_honors_allow_multiple_false))]
        [Fact]
        public void when_clr_attribute_set_honors_allow_multiple_false_()
        {
            var schema = Schema.Create(_ => { _.Directive("Foo").ClrType<DisallowMultipleAttribute>(); });
            schema.GetDirective<DisallowMultipleAttribute>().IsRepeatable.Should().BeFalse();
        }

        [Spec(nameof(cannot_be_set_to_true_when_clr_attribute_allow_multiple_false))]
        [Fact]
        public void cannot_be_set_to_true_when_clr_attribute_allow_multiple_false_()
        {
            Schema.Create(_ =>
            {
                var directive = _.Directive<DisallowMultipleAttribute>();
                Action set = () => directive.Repeatable(true);
                set.Should().Throw<InvalidOperationException>().WithMessage(
                    "Cannot set directive DisallowMultipleAttribute.IsRepeatable to 'true': the directive CLR type 'DisallowMultipleAttribute' has an AttributeUsageAttribute with an AllowMultiple value of 'false'.");
            });
        }

        [Spec(nameof(cannot_be_set_to_false_when_clr_attribute_allow_multiple_true))]
        [Fact]
        public void cannot_be_set_to_false_when_clr_attribute_allow_multiple_true_()
        {
            Schema.Create(_ =>
            {
                var directive = _.Directive<AllowMultipleAttribute>();
                Action set = () => directive.Repeatable(false);
                set.Should().Throw<InvalidOperationException>().WithMessage(
                    "Cannot set directive AllowMultipleAttribute.IsRepeatable to 'false': the directive CLR type 'AllowMultipleAttribute' has an AttributeUsageAttribute with an AllowMultiple value of 'true'.");
            });
        }
    }
}