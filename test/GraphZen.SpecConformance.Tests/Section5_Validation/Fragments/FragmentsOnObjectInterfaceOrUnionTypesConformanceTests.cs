// Copyright (c) GraphZen LLC. All rights reserved.
// Licensed under the GraphZen Community License. See the LICENSE file in the project root for license information.

using GraphZen.SpecConformance.Tests.Infrastructure;
using static GraphZen.QueryEngine.Validation.QueryValidationRules;
using static GraphZen.SpecConformance.Tests.Infrastructure.SpecValidation;

namespace GraphZen.SpecConformance.Tests.Section5_Validation.Fragments;

/// <seealso href="https://spec.graphql.org/draft/#sec-Fragments-On-Composite-Types" />
[SpecSection("5.5.1.3", "Fragments on Object, Interface or Union Types")]
public class FragmentsOnObjectInterfaceOrUnionTypesConformanceTests
{
    [Fact]
    public void object_is_valid_fragment_type() =>
        ExpectValid(FragmentsOnCompositeTypes, """
                                               fragment validFragment on Dog {
                                                 barks
                                               }
                                               """);

    [Fact]
    public void interface_is_valid_fragment_type() =>
        ExpectValid(FragmentsOnCompositeTypes, """
                                               fragment validFragment on Pet {
                                                 name
                                               }
                                               """);

    [Fact]
    public void object_is_valid_inline_fragment_type() =>
        ExpectValid(FragmentsOnCompositeTypes, """
                                               fragment validFragment on Pet {
                                                 ... on Dog {
                                                   barks
                                                 }
                                               }
                                               """);

    [Fact]
    public void inline_fragment_without_type_is_valid() =>
        ExpectValid(FragmentsOnCompositeTypes, """
                                               fragment validFragment on Pet {
                                                 ... {
                                                   name
                                                 }
                                               }
                                               """);

    [Fact]
    public void union_is_valid_fragment_type() =>
        ExpectValid(FragmentsOnCompositeTypes, """
                                               fragment validFragment on CatOrDog {
                                                 __typename
                                               }
                                               """);

    [Fact(Skip = "GraphZen does not reject fragments on non-composite types.")]
    public void scalar_is_invalid_fragment_type()
    {
        _ = """
            fragment scalarFragment on Boolean {
              bad
            }
            """;
        throw new NotImplementedException(
            "Expected error assertions for FragmentsOnCompositeTypes rule need to be ported from graphql-js.");
    }

    [Fact(Skip = "GraphZen does not reject fragments on non-composite types.")]
    public void enum_is_invalid_fragment_type()
    {
        _ = """
            fragment scalarFragment on FurColor {
              bad
            }
            """;
        throw new NotImplementedException(
            "Expected error assertions for FragmentsOnCompositeTypes rule need to be ported from graphql-js.");
    }

    [Fact(Skip = "GraphZen does not reject fragments on non-composite types.")]
    public void input_object_is_invalid_fragment_type()
    {
        _ = """
            fragment inputFragment on ComplexInput {
              stringField
            }
            """;
        throw new NotImplementedException(
            "Expected error assertions for FragmentsOnCompositeTypes rule need to be ported from graphql-js.");
    }

    [Fact(Skip = "GraphZen does not reject fragments on non-composite types.")]
    public void scalar_is_invalid_inline_fragment_type()
    {
        _ = """
            fragment invalidFragment on Pet {
              ... on String {
                barks
              }
            }
            """;
        throw new NotImplementedException(
            "Expected error assertions for FragmentsOnCompositeTypes rule need to be ported from graphql-js.");
    }
}
