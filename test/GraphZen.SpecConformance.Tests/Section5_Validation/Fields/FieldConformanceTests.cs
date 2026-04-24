// Copyright (c) GraphZen LLC. All rights reserved.
// Licensed under the GraphZen Community License. See the LICENSE file in the project root for license information.

using GraphZen.LanguageModel.Validation;
using GraphZen.QueryEngine.Validation;
using GraphZen.SpecConformance.Tests.Infrastructure;

namespace GraphZen.SpecConformance.Tests.Section5_Validation.Fields;

// Spec draft: see SpecMetadata.Version
// Spec: https://spec.graphql.org/draft/#sec-Field-Selections
// graphql-js source: src/validation/rules/FieldsOnCorrectTypeRule.ts
// graphql-js tests: src/validation/__tests__/FieldsOnCorrectTypeRule-test.ts

[SpecSection("5.3.1", "Field Selections")]
public class FieldsOnCorrectTypeConformanceTests : SpecValidationRuleHarness
{
    public override ValidationRule RuleUnderTest { get; } = QueryValidationRules.FieldsOnCorrectType;

    public static TheoryData<string, string> ValidQueries { get; } = new()
    {
        {
            "object_field_selection",
            """
            fragment objectFieldSelection on Dog {
              __typename
              name
            }
            """
        },
        {
            "aliased_object_field_selection",
            """
            fragment aliasedObjectFieldSelection on Dog {
              tn : __typename
              otherName : name
            }
            """
        },
        {
            "interface_field_selection",
            """
            fragment interfaceFieldSelection on Pet {
              __typename
              name
            }
            """
        },
        {
            "aliased_interface_field_selection",
            """
            fragment interfaceFieldSelection on Pet {
              otherName : name
            }
            """
        },
        {
            "lying_alias_selection",
            """
            fragment lyingAliasSelection on Dog {
              name : nickname
            }
            """
        },
        {
            "ignores_fields_on_unknown_type",
            """
            fragment unknownSelection on UnknownType {
              unknownField
            }
            """
        },
        {
            "meta_field_selection_on_union",
            """
            fragment directFieldSelectionOnUnion on CatOrDog {
              __typename
            }
            """
        },
        {
            "valid_field_in_inline_fragment",
            """
            fragment objectFieldSelection on Pet {
              ... on Dog {
                name
              }
              ... {
                name
              }
            }
            """
        },
    };

    public static TheoryData<string, string, int> InvalidQueries { get; } = new()
    {
        {
            "reports_errors_when_type_is_known_again",
            """
            fragment typeKnownAgain on Pet {
              unknown_pet_field {
                ... on Cat {
                  unknown_cat_field
                }
              }
            }
            """,
            2
        },
        {
            "ignores_deeply_unknown_field",
            """
            fragment deepFieldNotDefined on Dog {
              unknown_field {
                deeper_unknown_field
              }
            }
            """,
            1
        },
        {
            "sub_field_not_defined",
            """
            fragment subFieldNotDefined on Human {
              pets {
                unknown_field
              }
            }
            """,
            1
        },
        {
            "field_not_defined_on_inline_fragment",
            """
            fragment fieldNotDefined on Pet {
              ... on Dog {
                meowVolume
              }
            }
            """,
            1
        },
        {
            "aliased_field_target_not_defined",
            """
            fragment aliasedFieldTargetNotDefined on Dog {
              volume : mooVolume
            }
            """,
            1
        },
        {
            "not_defined_on_interface",
            """
            fragment notDefinedOnInterface on Pet {
              tailLength
            }
            """,
            1
        },
        {
            "defined_on_implementors_but_not_on_interface",
            """
            fragment definedOnImplementorsButNotInterface on Pet {
              nickname
            }
            """,
            1
        },
        {
            "direct_field_selection_on_union",
            """
            fragment directFieldSelectionOnUnion on CatOrDog {
              directField
            }
            """,
            1
        },
        {
            "defined_on_implementors_queried_on_union",
            """
            fragment definedOnImplementorsQueriedOnUnion on CatOrDog {
              name
            }
            """,
            1
        },
    };

    [Theory]
    [MemberData(nameof(ValidQueries))]
    public void valid_field_selection_queries_pass(string caseName, string query)
    {
        Assert.False(string.IsNullOrWhiteSpace(caseName));
        QueryShouldPass(query);
    }

    [Theory(Skip = "Negative field-selection validation cases are a conformance gap tracked in follow-up issue.")]
    [MemberData(nameof(InvalidQueries))]
    public void invalid_field_selection_queries_fail(string caseName, string query, int errorCount)
    {
        Assert.False(string.IsNullOrWhiteSpace(caseName));
        QueryShouldFail(query, errorCount);
    }
}

[SpecSection("5.3.2", "Field Selection Merging")]
public class OverlappingFieldsCanBeMergedConformanceTests : SpecValidationRuleHarness
{
    public override ValidationRule RuleUnderTest { get; } = QueryValidationRules.OverlappingFieldsCanBeMerged;

    [Fact(Skip = "Broader graphql-js overlap-port remains a conformance gap; tracked via follow-up issue.")]
    public void graphql_js_overlap_matrix_is_not_yet_ported()
    {
    }
}

[SpecSection("5.3.3", "Leaf Field Selections")]
public class ScalarLeafsConformanceTests : SpecValidationRuleHarness
{
    public override ValidationRule RuleUnderTest { get; } = QueryValidationRules.ScalarLeafs;

    public static TheoryData<string, string> ValidQueries { get; } = new()
    {
        {
            "valid_scalar_selection",
            """
            fragment scalarSelection on Dog {
              barks
            }
            """
        },
        {
            "valid_scalar_selection_with_args",
            """
            fragment scalarSelectionWithArgs on Dog {
              doesKnowCommand(dogCommand: SIT)
            }
            """
        },
    };

    public static TheoryData<string, string, int> InvalidQueries { get; } = new()
    {
        {
            "object_type_missing_selection",
            """
            query directQueryOnObjectWithoutSubFields {
              human
            }
            """,
            1
        },
        {
            "interface_type_missing_selection",
            """
            {
              human {
                pets
              }
            }
            """,
            1
        },
        {
            "scalar_selection_not_allowed_on_boolean",
            """
            fragment scalarSelectionsNotAllowedOnBoolean on Dog {
              barks {
                sinceWhen
              }
            }
            """,
            1
        },
        {
            "scalar_selection_not_allowed_on_enum",
            """
            fragment scalarSelectionsNotAllowedOnEnum on Cat {
              furColor {
                inHexDec
              }
            }
            """,
            1
        },
        {
            "scalar_selection_not_allowed_with_args",
            """
            fragment scalarSelectionsNotAllowedWithArgs on Dog {
              doesKnowCommand(dogCommand: SIT) {
                sinceWhen
              }
            }
            """,
            1
        },
    };

    [Theory]
    [MemberData(nameof(ValidQueries))]
    public void valid_scalar_leaf_queries_pass(string caseName, string query)
    {
        Assert.False(string.IsNullOrWhiteSpace(caseName));
        QueryShouldPass(query);
    }

    [Theory(Skip = "Negative scalar leaf validation cases are a conformance gap tracked in follow-up issue.")]
    [MemberData(nameof(InvalidQueries))]
    public void invalid_scalar_leaf_queries_fail(string caseName, string query, int errorCount)
    {
        Assert.False(string.IsNullOrWhiteSpace(caseName));
        QueryShouldFail(query, errorCount);
    }
}
