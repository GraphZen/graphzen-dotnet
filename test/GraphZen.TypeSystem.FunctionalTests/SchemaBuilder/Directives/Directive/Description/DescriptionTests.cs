// Copyright (c) GraphZen LLC. All rights reserved.
// Licensed under the GraphZen Community License. See the LICENSE file in the project root for license information.

using System.Diagnostics.CodeAnalysis;
using FluentAssertions;
using GraphZen.Infrastructure;
using GraphZen.TypeSystem.FunctionalTests.Specs;
using JetBrains.Annotations;
using Xunit;

namespace GraphZen.TypeSystem.FunctionalTests.SchemaBuilder.Directives.Directive.Description
{
    [NoReorder]
    public class DescriptionTests
    {
        [Spec(nameof(TypeSystemSpecs.DescriptionSpecs.description_can_be_updated))]
        [Fact]
        public void updateable_item_can_be_updated_()
        {
            var schema = Schema.Create(_ =>
            {
                _.Directive("Foo").Description("foo directive description");
            });
            schema.GetDirective("Foo").Description.Should().Be("foo directive description");
        }
    }
}