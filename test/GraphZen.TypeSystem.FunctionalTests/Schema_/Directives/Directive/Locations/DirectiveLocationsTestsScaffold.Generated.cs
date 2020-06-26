// Copyright (c) GraphZen LLC. All rights reserved.
// Licensed under the GraphZen Community License. See the LICENSE file in the project root for license information.

#nullable enable

// ReSharper disable All
using System.Diagnostics.CodeAnalysis;
using FluentAssertions;
using GraphZen.Infrastructure;
using JetBrains.Annotations;
using Xunit;
using static GraphZen.TypeSystem.FunctionalTests.Specs.TypeSystemSpecs.DirectiveLocationsSpecs;

namespace GraphZen.TypeSystem.FunctionalTests.Schema_.Directives.Directive.Locations
{
    [NoReorder]
    public abstract class DirectiveLocationsTestsScaffold
    {
        [Spec(nameof(single_location_can_be_set))]
        [Fact(Skip = "TODO")]
        public void single_location_can_be_set_()
        {
            // var schema = Schema.Create(_ => { });
        }


        [Spec(nameof(setting_multiple_locations_removes_other_locations))]
        [Fact(Skip = "TODO")]
        public void setting_multiple_locations_removes_other_locations_()
        {
            // var schema = Schema.Create(_ => { });
        }


        [Spec(nameof(setting_multiple_locations_of_same_kind_are_made_distinct))]
        [Fact(Skip = "TODO")]
        public void setting_multiple_locations_of_same_kind_are_made_distinct_()
        {
            // var schema = Schema.Create(_ => { });
        }


        [Spec(nameof(location_added_twice_results_in_single))]
        [Fact(Skip = "TODO")]
        public void location_added_twice_results_in_single_()
        {
            // var schema = Schema.Create(_ => { });
        }
    }
}
// Source Hash Code: 16518556958600798205