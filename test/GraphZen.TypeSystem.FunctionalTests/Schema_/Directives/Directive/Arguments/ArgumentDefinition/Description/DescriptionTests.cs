// Copyright (c) GraphZen LLC. All rights reserved.
// Licensed under the GraphZen Community License. See the LICENSE file in the project root for license information.

using System;
using System.Diagnostics.CodeAnalysis;
using FluentAssertions;
using GraphZen.Infrastructure;
using GraphZen.TypeSystem.FunctionalTests.Specs;
using JetBrains.Annotations;
using Xunit;

namespace GraphZen.TypeSystem.FunctionalTests.Schema_.Directives.Directive.Arguments.ArgumentDefinition.
    Description
{
    [NoReorder]
    public class DescriptionTests
    {
        [Spec(nameof(TypeSystemSpecs.DescriptionSpecs.description_can_be_updated))]
        [Fact]
        public void description_can_be_updated_()
        {
            var schema = Schema.Create(_ =>
            {
                _.Directive("foo").Argument("foo", "String", a => a.Description("desc"));
            });
            schema.GetDirective("foo").GetArgument("foo").Description.Should().Be("desc");
        }


        [Spec(nameof(TypeSystemSpecs.DescriptionSpecs.description_cannot_be_null))]
        [Fact]
        public void description_cannot_be_null_()
        {
            Schema.Create(_ =>
            {
                _.Directive("foo").Argument("foo", "String", a =>
                {
                    Action add = () => a.Description(null!);
                    add.Should().ThrowArgumentNullException("description");
                });
            });
        }


        [Spec(nameof(TypeSystemSpecs.DescriptionSpecs.description_can_be_removed))]
        [Fact]
        public void description_can_be_removed_()
        {
            var schema = Schema.Create(_ =>
            {
                _.Directive("foo").Argument("foo", "String", a => { a.Description("desc").RemoveDescription(); });
            });
            schema.GetDirective("foo").GetArgument("foo").Description.Should().BeNull();
        }
    }
}