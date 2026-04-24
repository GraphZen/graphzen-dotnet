// Copyright (c) GraphZen LLC. All rights reserved.
// Licensed under the GraphZen Community License. See the LICENSE file in the project root for license information.

using GraphZen.LanguageModel.Validation;
using GraphZen.QueryEngine.Validation;
using GraphZen.SpecConformance.Tests.Infrastructure;

namespace GraphZen.SpecConformance.Tests.Section5_Validation.Values;

[SpecSection("5.6.1", "Values of Correct Type")]
[SpecSection("5.6.2", "Input Object Field Names")]
[SpecSection("5.6.4", "Input Object Required Fields")]
public class ValuesOfCorrectTypeConformanceTests : SpecValidationRuleHarness
{
    public override ValidationRule RuleUnderTest { get; } = QueryValidationRules.ValuesOfCorrectType;

    [Fact(Skip = "The graphql-js literal coercion matrix is not yet ported; tracked via follow-up issue.")]
    public void graphql_js_value_coercion_matrix_is_not_yet_ported()
    {
    }
}

[SpecSection("5.6.3", "Input Object Field Uniqueness")]
public class UniqueInputFieldNamesConformanceTests : SpecValidationRuleHarness
{
    public override ValidationRule RuleUnderTest { get; } = QueryValidationRules.UniqueInputFieldNames;

    public static TheoryData<string, string> ValidQueries { get; } = new()
    {
        {
            "multiple_input_object_fields",
            """
            {
              field(arg: { f1: "value", f2: "value", f3: "value" })
            }
            """
        },
        {
            "same_input_object_within_two_args",
            """
            {
              field(arg1: { f: true }, arg2: { f: true })
            }
            """
        },
    };

    public static TheoryData<string, string, int> InvalidQueries { get; } = new()
    {
        {
            "duplicate_input_object_fields",
            """
            {
              field(arg: { f1: "value", f1: "value" })
            }
            """,
            1
        },
        {
            "many_duplicate_input_object_fields",
            """
            {
              field(arg: { f1: "value", f1: "value", f1: "value" })
            }
            """,
            2
        },
        {
            "nested_duplicate_input_object_fields",
            """
            {
              field(arg: { f1: { f2: "value", f2: "value" } })
            }
            """,
            1
        },
    };

    [Theory]
    [MemberData(nameof(ValidQueries))]
    public void valid_unique_input_field_queries_pass(string caseName, string query)
    {
        Assert.False(string.IsNullOrWhiteSpace(caseName));
        QueryShouldPass(query);
    }

    [Theory(Skip = "Negative unique-input-field validation cases are a conformance gap tracked in follow-up issue.")]
    [MemberData(nameof(InvalidQueries))]
    public void invalid_unique_input_field_queries_fail(string caseName, string query, int errorCount)
    {
        Assert.False(string.IsNullOrWhiteSpace(caseName));
        QueryShouldFail(query, errorCount);
    }
}
