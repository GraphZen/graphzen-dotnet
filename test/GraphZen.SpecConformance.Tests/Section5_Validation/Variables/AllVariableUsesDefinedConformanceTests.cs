// Copyright (c) GraphZen LLC. All rights reserved.
// Licensed under the GraphZen Community License. See the LICENSE file in the project root for license information.

using GraphZen.SpecConformance.Tests.Infrastructure;
using static GraphZen.QueryEngine.Validation.QueryValidationRules;
using static GraphZen.SpecConformance.Tests.Infrastructure.SpecValidation;

namespace GraphZen.SpecConformance.Tests.Section5_Validation.Variables;

/// <seealso href="https://spec.graphql.org/draft/#sec-All-Variable-Uses-Defined" />
[SpecSection("5.8.3", "All Variable Uses Defined")]
public class AllVariableUsesDefinedConformanceTests
{
    [Fact]
    public void all_variables_defined() =>
        ExpectValid(NoUndefinedVariables, """
                                          query Foo($a: String, $b: String, $c: String) {
                                            field(a: $a, b: $b, c: $c)
                                          }
                                          """);

    [Fact]
    public void all_variables_in_fragments_defined() =>
        ExpectValid(NoUndefinedVariables, """
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

    [Fact(Skip = "GraphZen does not reject uses of undefined variables.")]
    public void variable_not_defined()
    {
        _ = """
            query Foo($a: String, $b: String, $c: String) {
              field(a: $a, b: $b, c: $c, d: $d)
            }
            """;
        throw new NotImplementedException(
            "Expected error assertions for NoUndefinedVariables rule need to be ported from graphql-js.");
    }

    [Fact(Skip = "GraphZen does not reject uses of undefined variables.")]
    public void multiple_variables_not_defined()
    {
        _ = """
            query Foo($b: String) {
              field(a: $a, b: $b, c: $c)
            }
            """;
        throw new NotImplementedException(
            "Expected error assertions for NoUndefinedVariables rule need to be ported from graphql-js.");
    }

    [Fact(Skip = "GraphZen does not reject uses of undefined variables.")]
    public void variable_in_fragment_not_defined_by_operation()
    {
        _ = """
            query Foo($a: String, $b: String) {
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
            """;
        throw new NotImplementedException(
            "Expected error assertions for NoUndefinedVariables rule need to be ported from graphql-js.");
    }
}
