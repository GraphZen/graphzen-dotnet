// Copyright (c) GraphZen LLC. All rights reserved.
// Licensed under the GraphZen Community License. See the LICENSE file in the project root for license information.

using GraphZen.SpecConformance.Tests.Infrastructure;
using static GraphZen.QueryEngine.Validation.QueryValidationRules;
using static GraphZen.SpecConformance.Tests.Infrastructure.SpecValidation;

namespace GraphZen.SpecConformance.Tests.Section5_Validation.Fields;

/// <seealso href="https://spec.graphql.org/draft/#sec-Field-Selections" />
[SpecSection("5.3.1", "Field Selections")]
public class FieldSelectionsConformanceTests
{
    [Fact]
    public void object_field_selection() =>
        ExpectValid(FieldsOnCorrectType, """
                                         fragment objectFieldSelection on Dog {
                                           __typename
                                           name
                                         }
                                         """);

    [Fact]
    public void aliased_object_field_selection() =>
        ExpectValid(FieldsOnCorrectType, """
                                         fragment aliasedObjectFieldSelection on Dog {
                                           tn : __typename
                                           otherName : name
                                         }
                                         """);

    [Fact]
    public void interface_field_selection() =>
        ExpectValid(FieldsOnCorrectType, """
                                         fragment interfaceFieldSelection on Pet {
                                           __typename
                                           name
                                         }
                                         """);

    [Fact]
    public void aliased_interface_field_selection() =>
        ExpectValid(FieldsOnCorrectType, """
                                         fragment interfaceFieldSelection on Pet {
                                           otherName : name
                                         }
                                         """);

    [Fact]
    public void lying_alias_selection() =>
        ExpectValid(FieldsOnCorrectType, """
                                         fragment lyingAliasSelection on Dog {
                                           name : nickname
                                         }
                                         """);

    [Fact]
    public void ignores_fields_on_unknown_type() =>
        ExpectValid(FieldsOnCorrectType, """
                                         fragment unknownSelection on UnknownType {
                                           unknownField
                                         }
                                         """);

    [Fact]
    public void meta_field_selection_on_union() =>
        ExpectValid(FieldsOnCorrectType, """
                                         fragment directFieldSelectionOnUnion on CatOrDog {
                                           __typename
                                         }
                                         """);

    [Fact]
    public void valid_field_in_inline_fragment() =>
        ExpectValid(FieldsOnCorrectType, """
                                         fragment objectFieldSelection on Pet {
                                           ... on Dog {
                                             name
                                           }
                                           ... {
                                             name
                                           }
                                         }
                                         """);

    [Fact(Skip = "GraphZen does not reject fields that are not defined on the target type.")]
    public void reports_errors_when_type_is_known_again()
    {
        _ = """
            fragment typeKnownAgain on Pet {
              unknown_pet_field {
                ... on Cat {
                  unknown_cat_field
                }
              }
            }
            """;
        throw new NotImplementedException(
            "Expected error assertions for FieldsOnCorrectType rule need to be ported from graphql-js.");
    }

    [Fact(Skip = "GraphZen does not reject fields that are not defined on the target type.")]
    public void ignores_deeply_unknown_field()
    {
        _ = """
            fragment deepFieldNotDefined on Dog {
              unknown_field {
                deeper_unknown_field
              }
            }
            """;
        throw new NotImplementedException(
            "Expected error assertions for FieldsOnCorrectType rule need to be ported from graphql-js.");
    }

    [Fact(Skip = "GraphZen does not reject fields that are not defined on the target type.")]
    public void sub_field_not_defined()
    {
        _ = """
            fragment subFieldNotDefined on Human {
              pets {
                unknown_field
              }
            }
            """;
        throw new NotImplementedException(
            "Expected error assertions for FieldsOnCorrectType rule need to be ported from graphql-js.");
    }

    [Fact(Skip = "GraphZen does not reject fields that are not defined on the target type.")]
    public void field_not_defined_on_inline_fragment()
    {
        _ = """
            fragment fieldNotDefined on Pet {
              ... on Dog {
                meowVolume
              }
            }
            """;
        throw new NotImplementedException(
            "Expected error assertions for FieldsOnCorrectType rule need to be ported from graphql-js.");
    }

    [Fact(Skip = "GraphZen does not reject fields that are not defined on the target type.")]
    public void aliased_field_target_not_defined()
    {
        _ = """
            fragment aliasedFieldTargetNotDefined on Dog {
              volume : mooVolume
            }
            """;
        throw new NotImplementedException(
            "Expected error assertions for FieldsOnCorrectType rule need to be ported from graphql-js.");
    }

    [Fact(Skip = "GraphZen does not reject fields that are not defined on the target type.")]
    public void not_defined_on_interface()
    {
        _ = """
            fragment notDefinedOnInterface on Pet {
              tailLength
            }
            """;
        throw new NotImplementedException(
            "Expected error assertions for FieldsOnCorrectType rule need to be ported from graphql-js.");
    }

    [Fact(Skip = "GraphZen does not reject fields that are not defined on the target type.")]
    public void defined_on_implementors_but_not_on_interface()
    {
        _ = """
            fragment definedOnImplementorsButNotInterface on Pet {
              nickname
            }
            """;
        throw new NotImplementedException(
            "Expected error assertions for FieldsOnCorrectType rule need to be ported from graphql-js.");
    }

    [Fact(Skip = "GraphZen does not reject fields that are not defined on the target type.")]
    public void direct_field_selection_on_union()
    {
        _ = """
            fragment directFieldSelectionOnUnion on CatOrDog {
              directField
            }
            """;
        throw new NotImplementedException(
            "Expected error assertions for FieldsOnCorrectType rule need to be ported from graphql-js.");
    }

    [Fact(Skip = "GraphZen does not reject fields that are not defined on the target type.")]
    public void defined_on_implementors_queried_on_union()
    {
        _ = """
            fragment definedOnImplementorsQueriedOnUnion on CatOrDog {
              name
            }
            """;
        throw new NotImplementedException(
            "Expected error assertions for FieldsOnCorrectType rule need to be ported from graphql-js.");
    }
}
