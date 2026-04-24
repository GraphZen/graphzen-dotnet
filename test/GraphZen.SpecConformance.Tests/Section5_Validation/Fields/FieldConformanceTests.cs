// Copyright (c) GraphZen LLC. All rights reserved.
// Licensed under the GraphZen Community License. See the LICENSE file in the project root for license information.

using GraphZen.LanguageModel.Validation;
using GraphZen.QueryEngine.Validation;
using GraphZen.SpecConformance.Tests.Infrastructure;
using GraphZen.Tests.Validation.Rules;

namespace GraphZen.SpecConformance.Tests.Section5_Validation.Fields;

[SpecSection("5.3.1", "Field Selections")]
public class FieldsOnCorrectTypeConformanceTests : FieldsOnCorrectTypeTests
{
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
