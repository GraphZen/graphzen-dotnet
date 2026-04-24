// Copyright (c) GraphZen LLC. All rights reserved.
// Licensed under the GraphZen Community License. See the LICENSE file in the project root for license information.

using GraphZen.SpecConformance.Tests.Infrastructure;
using static GraphZen.QueryEngine.Validation.QueryValidationRules;
using static GraphZen.SpecConformance.Tests.Infrastructure.SpecValidation;

namespace GraphZen.SpecConformance.Tests.Section5_Validation.Directives;

/// <seealso href="https://spec.graphql.org/draft/#sec-Directives-Are-Unique-Per-Location" />
[SpecSection("5.7.3", "Directives Are Unique per Location")]
public class DirectivesAreUniquePerLocationConformanceTests
{
    [Fact]
    public void same_directives_in_different_locations() =>
        ExpectValid(UniqueDirectivesPerLocation, """
                                                 {
                                                   cat @skip(if: false) {
                                                     nickname @skip(if: true)
                                                   }
                                                 }
                                                 """);

    [Fact]
    public void unknown_directives_are_ignored() =>
        ExpectValid(UniqueDirectivesPerLocation, """
                                                 {
                                                   cat @unknown {
                                                     nickname @unknown
                                                   }
                                                 }
                                                 """);

    [Fact(Skip = "GraphZen does not reject duplicate directives per location.")]
    public void duplicate_directives_in_one_location()
    {
        _ = """
            {
              cat @skip(if: false) @skip(if: true) {
                nickname
              }
            }
            """;
        throw new NotImplementedException(
            "Expected error assertions for UniqueDirectivesPerLocation rule need to be ported from graphql-js.");
    }

    [Fact(Skip = "GraphZen does not reject duplicate directives per location.")]
    public void different_duplicate_directives_in_one_location()
    {
        _ = """
            {
              cat @skip(if: false) @skip(if: true) @include(if: true) @include(if: false) {
                nickname
              }
            }
            """;
        throw new NotImplementedException(
            "Expected error assertions for UniqueDirectivesPerLocation rule need to be ported from graphql-js.");
    }
}
