// Copyright (c) GraphZen LLC. All rights reserved.
// Licensed under the GraphZen Community License. See the LICENSE file in the project root for license information.

using System.Diagnostics.CodeAnalysis;
using FluentAssertions;
using GraphZen.Infrastructure;
using GraphZen.LanguageModel;
using JetBrains.Annotations;
using Xunit;
using static GraphZen.TypeSystem.FunctionalTests.Specs.TypeSystemSpecs.DirectiveLocationsSpecs;

namespace GraphZen.TypeSystem.FunctionalTests.Schema_.Directives.Directive.Locations
{
    [NoReorder]
    public class DirectiveLocationsTests
    {
        [Spec(nameof(location_can_be_added))]
        [Fact]
        public void location_can_be_added_()
        {
            var schema = Schema.Create(_ => { _.Directive("Foo").AddLocation(DirectiveLocation.Enum); });
            schema.GetDirective("Foo").Locations.Should().Contain(DirectiveLocation.Enum);
        }


        [Spec(nameof(location_can_be_removed))]
        [Fact]
        public void location_can_be_removed_()
        {
            var schema = Schema.Create(_ =>
            {
                _.Directive("Foo").AddLocation(DirectiveLocation.Enum).RemoveLocation(DirectiveLocation.Enum);
            });
            schema.GetDirective("Foo").Locations.Should().BeEmpty();
        }


        [Spec(nameof(multiple_locations_can_be_set))]
        [Fact]
        public void locations_can_be_set_()
        {
            var schema = Schema.Create(_ =>
            {
                _.Directive("Foo").Locations(DirectiveLocation.Enum, DirectiveLocation.ArgumentDefinition);
            });
            schema.GetDirective("Foo").Locations.Should().Contain(DirectiveLocation.Enum).And
                .Contain(DirectiveLocation.ArgumentDefinition);
        }


        [Spec(nameof(locations_can_be_removed))]
        [Fact]
        public void locations_can_be_removed_()
        {
            var schema = Schema.Create(_ =>
            {
                _.Directive("Foo").Locations(DirectiveLocation.Enum, DirectiveLocation.ArgumentDefinition)
                    .RemoveLocations();
            });
            schema.GetDirective("Foo").Locations.Should().BeEmpty();
        }
    }
}