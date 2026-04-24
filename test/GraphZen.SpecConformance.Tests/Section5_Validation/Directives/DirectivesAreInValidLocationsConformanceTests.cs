// Copyright (c) GraphZen LLC. All rights reserved.
// Licensed under the GraphZen Community License. See the LICENSE file in the project root for license information.

using GraphZen.SpecConformance.Tests.Infrastructure;
using static GraphZen.QueryEngine.Validation.QueryValidationRules;
using static GraphZen.SpecConformance.Tests.Infrastructure.SpecValidation;

namespace GraphZen.SpecConformance.Tests.Section5_Validation.Directives;

/// <seealso href="https://spec.graphql.org/draft/#sec-Directives-Are-In-Valid-Locations" />
[SpecSection("5.7.2", "Directives Are in Valid Locations")]
public class DirectivesAreInValidLocationsConformanceTests
{
    [Fact]
    public void well_placed_directives() =>
        ExpectValid(KnownDirectives, """
                                     query Foo($var: Boolean) @onQuery {
                                       name @include(if: $var)
                                       ...Frag @include(if: true)
                                       skippedField @skip(if: true)
                                       ...SkippedFrag @skip(if: true)
                                     }

                                     mutation Bar @onMutation {
                                       someField
                                     }
                                     """);

    [Fact(Skip = "GraphZen does not reject directives in invalid locations.")]
    public void with_misplaced_directives()
    {
        _ = """
            query Foo($var: Boolean) @include(if: true) {
              name @onQuery @include(if: $var)
              ...Frag @onQuery
            }

            mutation Bar @onQuery {
              someField
            }
            """;
        throw new NotImplementedException(
            "Expected error assertions for KnownDirectives rule need to be ported from graphql-js.");
    }
}
