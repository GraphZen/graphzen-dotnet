// Copyright (c) GraphZen LLC. All rights reserved.
// Licensed under the GraphZen Community License. See the LICENSE file in the project root for license information.

using GraphZen.SpecConformance.Tests.Infrastructure;
using static GraphZen.QueryEngine.Validation.QueryValidationRules;
using static GraphZen.SpecConformance.Tests.Infrastructure.SpecValidation;

namespace GraphZen.SpecConformance.Tests.Section5_Validation.Variables;

/// <seealso href="https://spec.graphql.org/draft/#sec-All-Variables-Used" />
[SpecSection("5.8.4", "All Variables Used")]
public class AllVariablesUsedConformanceTests
{
    [Fact]
    public void uses_all_variables() =>
        ExpectValid(NoUnusedVariables, """
                                       query ($a: String, $b: String, $c: String) {
                                         field(a: $a, b: $b, c: $c)
                                       }
                                       """);

    [Fact]
    public void uses_all_variables_in_fragments() =>
        ExpectValid(NoUnusedVariables, """
                                       query Foo($a: String, $b: String, $c: String) {
                                         ...FragA
                                       }

                                       fragment FragA on Type {
                                         field(a: $a) {
                                           ...FragB
                                         }
                                       }

                                       fragment FragB on Type {
                                         field(b: $b) {
                                           ...FragC
                                         }
                                       }

                                       fragment FragC on Type {
                                         field(c: $c)
                                       }
                                       """);

    [Fact(Skip = "GraphZen does not reject unused variable definitions.")]
    public void variable_not_used()
    {
        _ = """
            query ($a: String, $b: String, $c: String) {
              field(a: $a, b: $b)
            }
            """;
        throw new NotImplementedException(
            "Expected error assertions for NoUnusedVariables rule need to be ported from graphql-js.");
    }

    [Fact(Skip = "GraphZen does not reject unused variable definitions.")]
    public void multiple_variables_not_used()
    {
        _ = """
            query Foo($a: String, $b: String, $c: String) {
              field(b: $b)
            }
            """;
        throw new NotImplementedException(
            "Expected error assertions for NoUnusedVariables rule need to be ported from graphql-js.");
    }

    [Fact(Skip = "GraphZen does not reject unused variable definitions.")]
    public void variables_not_used_in_fragments()
    {
        _ = """
            query Foo($a: String, $b: String, $c: String) {
              ...FragA
            }

            fragment FragA on Type {
              field {
                ...FragB
              }
            }

            fragment FragB on Type {
              field(b: $b) {
                ...FragC
              }
            }

            fragment FragC on Type {
              field
            }
            """;
        throw new NotImplementedException(
            "Expected error assertions for NoUnusedVariables rule need to be ported from graphql-js.");
    }
}
