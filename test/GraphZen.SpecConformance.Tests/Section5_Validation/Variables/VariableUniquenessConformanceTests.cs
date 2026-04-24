// Copyright (c) GraphZen LLC. All rights reserved.
// Licensed under the GraphZen Community License. See the LICENSE file in the project root for license information.

using GraphZen.SpecConformance.Tests.Infrastructure;
using static GraphZen.QueryEngine.Validation.QueryValidationRules;
using static GraphZen.SpecConformance.Tests.Infrastructure.SpecValidation;

namespace GraphZen.SpecConformance.Tests.Section5_Validation.Variables;

/// <seealso href="https://spec.graphql.org/draft/#sec-Variable-Uniqueness" />
[SpecSection("5.8.1", "Variable Uniqueness")]
public class VariableUniquenessConformanceTests
{
    [Fact]
    public void unique_variable_names_pass() =>
        ExpectValid(UniqueVariableNames, """
                                         query A($x: Int, $y: String) {
                                           __typename
                                         }

                                         query B($x: String, $y: Int) {
                                           __typename
                                         }
                                         """);

    [Fact(Skip = "GraphZen does not reject duplicate variable names.")]
    public void duplicate_variable_names_fail()
    {
        _ = """
            query A($x: Int, $x: Int, $x: String) {
              __typename
            }

            query B($x: String, $x: Int) {
              __typename
            }

            query C($x: Int, $x: Int) {
              __typename
            }
            """;
        throw new NotImplementedException(
            "Expected error assertions for UniqueVariableNames rule need to be ported from graphql-js.");
    }
}
