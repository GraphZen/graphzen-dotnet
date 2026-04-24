// Copyright (c) GraphZen LLC. All rights reserved.
// Licensed under the GraphZen Community License. See the LICENSE file in the project root for license information.

using GraphZen.SpecConformance.Tests.Infrastructure;
using static GraphZen.QueryEngine.Validation.QueryValidationRules;
using static GraphZen.SpecConformance.Tests.Infrastructure.SpecValidation;

namespace GraphZen.SpecConformance.Tests.Section5_Validation.Variables;

/// <seealso href="https://spec.graphql.org/draft/#sec-All-Variable-Usages-Are-Allowed" />
[SpecSection("5.8.5", "All Variable Usages Are Allowed")]
public class AllVariableUsagesAreAllowedConformanceTests
{
    [Fact]
    public void boolean_to_boolean() =>
        ExpectValid(VariablesInAllowedPosition, """
                                                query Query($booleanArg: Boolean) {
                                                  complicatedArgs {
                                                    booleanArgField(booleanArg: $booleanArg)
                                                  }
                                                }
                                                """);

    [Fact]
    public void boolean_non_null_to_boolean() =>
        ExpectValid(VariablesInAllowedPosition, """
                                                query Query($nonNullBooleanArg: Boolean!) {
                                                  complicatedArgs {
                                                    booleanArgField(booleanArg: $nonNullBooleanArg)
                                                  }
                                                }
                                                """);

    [Fact]
    public void list_to_list() =>
        ExpectValid(VariablesInAllowedPosition, """
                                                query Query($stringListVar: [String]) {
                                                  complicatedArgs {
                                                    stringListArgField(stringListArg: $stringListVar)
                                                  }
                                                }
                                                """);

    [Fact(Skip = "GraphZen does not reject variables used in incompatible type positions.")]
    public void int_to_non_null_int()
    {
        _ = """
            query Query($intArg: Int) {
              complicatedArgs {
                nonNullIntArgField(nonNullIntArg: $intArg)
              }
            }
            """;
        throw new NotImplementedException(
            "Expected error assertions for VariablesInAllowedPosition rule need to be ported from graphql-js.");
    }

    [Fact(Skip = "GraphZen does not reject variables used in incompatible type positions.")]
    public void string_over_boolean()
    {
        _ = """
            query Query($stringVar: String) {
              complicatedArgs {
                booleanArgField(booleanArg: $stringVar)
              }
            }
            """;
        throw new NotImplementedException(
            "Expected error assertions for VariablesInAllowedPosition rule need to be ported from graphql-js.");
    }

    [Fact(Skip = "GraphZen does not reject variables used in incompatible type positions.")]
    public void string_to_list()
    {
        _ = """
            query Query($stringVar: String) {
              complicatedArgs {
                stringListArgField(stringListArg: $stringVar)
              }
            }
            """;
        throw new NotImplementedException(
            "Expected error assertions for VariablesInAllowedPosition rule need to be ported from graphql-js.");
    }

    [Fact(Skip = "Error assertions need to be ported from graphql-js.")]
    public void nullable_variable_in_oneof_position()
    {
        _ = """
            query ($string: String) {
              complicatedArgs {
                oneOfArgField(oneOfArg: { stringField: $string })
              }
            }
            """;
        throw new NotImplementedException(
            "Expected error assertions for VariablesInAllowedPosition rule need to be ported from graphql-js.");
    }
}
