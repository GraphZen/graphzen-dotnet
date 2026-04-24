// Copyright (c) GraphZen LLC. All rights reserved.
// Licensed under the GraphZen Community License. See the LICENSE file in the project root for license information.

using GraphZen.SpecConformance.Tests.Infrastructure;
using static GraphZen.QueryEngine.Validation.QueryValidationRules;
using static GraphZen.SpecConformance.Tests.Infrastructure.SpecValidation;

namespace GraphZen.SpecConformance.Tests.Section5_Validation.Variables;

/// <seealso href="https://spec.graphql.org/draft/#sec-Variables-Are-Input-Types" />
[SpecSection("5.8.2", "Variables Are Input Types")]
public class VariablesAreInputTypesConformanceTests
{
    [Fact]
    public void unknown_types_are_ignored() =>
        ExpectValid(VariablesAreInputTypes, """
                                            query Foo($a: Unknown, $b: [[Unknown!]]!) {
                                              field(a: $a, b: $b)
                                            }
                                            """);

    [Fact]
    public void input_types_are_valid() =>
        ExpectValid(VariablesAreInputTypes, """
                                            query Foo($a: String, $b: [Boolean!]!, $c: ComplexInput) {
                                              field(a: $a, b: $b, c: $c)
                                            }
                                            """);

    [Fact(Skip = "GraphZen does not reject output types used as variable types.")]
    public void output_types_are_invalid()
    {
        _ = """
            query Foo($a: Dog, $b: [[CatOrDog!]]!, $c: Pet) {
              field(a: $a, b: $b, c: $c)
            }
            """;
        throw new NotImplementedException(
            "Expected error assertions for VariablesAreInputTypes rule need to be ported from graphql-js.");
    }
}
