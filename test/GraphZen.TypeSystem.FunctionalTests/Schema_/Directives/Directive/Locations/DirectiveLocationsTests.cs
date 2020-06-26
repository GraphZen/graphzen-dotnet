// Copyright (c) GraphZen LLC. All rights reserved.
// Licensed under the GraphZen Community License. See the LICENSE file in the project root for license information.

using System.Diagnostics.CodeAnalysis;
using GraphZen.Infrastructure;
using GraphZen.TypeSystem.FunctionalTests.Specs;
using JetBrains.Annotations;
using Xunit;

namespace GraphZen.TypeSystem.FunctionalTests.Schema_.Directives.Directive.Locations
{
    [NoReorder]
    public class DirectiveLocationsTests
    {


        [Spec(nameof(TypeSystemSpecs.DirectiveLocationsSpecs.location_can_be_added))]
        [Fact()]
        public void location_can_be_added_()
        {
            var schema = Schema.Create(_ =>
            {
                _.Directive("Foo");
            });
        }




        [Spec(nameof(TypeSystemSpecs.DirectiveLocationsSpecs.location_can_be_removed))]
        [Fact(Skip = "TODO")]
        public void location_can_be_removed_()
        {
            // var schema = Schema.Create(_ => { });
        }




        [Spec(nameof(TypeSystemSpecs.DirectiveLocationsSpecs.locations_can_be_set))]
        [Fact(Skip = "TODO")]
        public void locations_can_be_set_()
        {
            // var schema = Schema.Create(_ => { });
        }




        [Spec(nameof(TypeSystemSpecs.DirectiveLocationsSpecs.locations_can_be_removed))]
        [Fact(Skip = "TODO")]
        public void locations_can_be_removed_()
        {
            // var schema = Schema.Create(_ => { });
        }


    }
}