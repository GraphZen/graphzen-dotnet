// Copyright (c) GraphZen LLC. All rights reserved.
// Licensed under the GraphZen Community License. See the LICENSE file in the project root for license information.

using GraphZen.SpecConformance.Tests.Infrastructure;
using static GraphZen.QueryEngine.Validation.QueryValidationRules;
using static GraphZen.SpecConformance.Tests.Infrastructure.SpecValidation;

namespace GraphZen.SpecConformance.Tests.Section5_Validation.Fragments;

/// <seealso href="https://spec.graphql.org/draft/#sec-Fragment-Spread-Type-Existence" />
[SpecSection("5.5.1.2", "Fragment Spread Type Existence")]
public class FragmentSpreadTypeExistenceConformanceTests
{
    [Fact]
    public void known_type_names_are_valid() =>
        ExpectValid(KnownTypeNames, """
                                    query Foo($var: String, $required: [String!]!) {
                                      user(id: 4) {
                                        pets { ... on Pet { name }, ...PetFields, ... { name } }
                                      }
                                    }

                                    fragment PetFields on Pet {
                                      name
                                    }
                                    """);

    [Fact(Skip = "GraphZen does not reject spreads on unknown type names.")]
    public void unknown_type_names_are_invalid()
    {
        _ = """
            query Foo($var: JumbledUpLetters) {
              user(id: 4) {
                name
                pets { ... on Badger { name }, ...PetFields }
              }
            }

            fragment PetFields on Peettt {
              name
            }
            """;
        throw new NotImplementedException(
            "Expected error assertions for KnownTypeNames rule need to be ported from graphql-js.");
    }

    [Fact(Skip = "GraphZen does not reject spreads on unknown type names.")]
    public void ignores_type_definitions()
    {
        _ = """
            type NotInTheSchema {
              field: FooBar
            }
            interface FooBar {
              field: NotInTheSchema
            }
            union U = A | B
            input Blob {
              field: UnknownType
            }
            query Foo($var: NotInTheSchema) {
              user(id: $var) {
                id
              }
            }
            """;
        throw new NotImplementedException(
            "Expected error assertions for KnownTypeNames rule need to be ported from graphql-js.");
    }
}
