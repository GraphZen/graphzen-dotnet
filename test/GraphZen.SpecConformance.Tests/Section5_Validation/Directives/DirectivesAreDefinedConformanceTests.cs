// Copyright (c) GraphZen LLC. All rights reserved.
// Licensed under the GraphZen Community License. See the LICENSE file in the project root for license information.

using GraphZen.SpecConformance.Tests.Infrastructure;
using static GraphZen.QueryEngine.Validation.QueryValidationRules;
using static GraphZen.SpecConformance.Tests.Infrastructure.SpecValidation;

namespace GraphZen.SpecConformance.Tests.Section5_Validation.Directives;

/// <seealso href="https://spec.graphql.org/draft/#sec-Directives-Are-Defined" />
[SpecSection("5.7.1", "Directives Are Defined")]
public class DirectivesAreDefinedConformanceTests
{
    [Fact]
    public void no_directives() =>
        ExpectValid(KnownDirectives, """
                                     query Foo {
                                       name
                                       ...Frag
                                     }

                                     fragment Frag on Dog {
                                       name
                                     }
                                     """);

    [Fact]
    public void with_known_directives() =>
        ExpectValid(KnownDirectives, """
                                     {
                                       dog @include(if: true) {
                                         name
                                       }
                                       human @skip(if: false) {
                                         name
                                       }
                                     }
                                     """);

    [Fact(Skip = "GraphZen does not reject unknown directives.")]
    public void with_unknown_directive()
    {
        _ = """
            {
              dog @unknown(directive: "value") {
                name
              }
            }
            """;
        throw new NotImplementedException(
            "Expected error assertions for KnownDirectives rule need to be ported from graphql-js.");
    }

    [Fact(Skip = "GraphZen does not reject unknown directives.")]
    public void with_many_unknown_directives()
    {
        _ = """
            {
              dog @unknown(directive: "value") {
                name
              }
              human @unknown(directive: "value") {
                name
                pets @unknown(directive: "value") {
                  name
                }
              }
            }
            """;
        throw new NotImplementedException(
            "Expected error assertions for KnownDirectives rule need to be ported from graphql-js.");
    }
}
