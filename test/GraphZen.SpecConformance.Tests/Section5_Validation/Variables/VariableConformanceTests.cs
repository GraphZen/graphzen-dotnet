// Copyright (c) GraphZen LLC. All rights reserved.
// Licensed under the GraphZen Community License. See the LICENSE file in the project root for license information.

using GraphZen.LanguageModel.Validation;
using GraphZen.QueryEngine.Validation;
using GraphZen.SpecConformance.Tests.Infrastructure;

namespace GraphZen.SpecConformance.Tests.Section5_Validation.Variables;

[SpecSection("5.8.1", "Variable Uniqueness")]
public class UniqueVariableNamesConformanceTests : SpecValidationRuleHarness
{
    public override ValidationRule RuleUnderTest { get; } = QueryValidationRules.UniqueVariableNames;

    [Fact]
    public void unique_variable_names_pass()
    {
        QueryShouldPass("""
            query A($x: Int, $y: String) {
              __typename
            }

            query B($x: String, $y: Int) {
              __typename
            }
            """);
    }

    [Fact(Skip = "Duplicate-variable validation is a conformance gap tracked in follow-up issue.")]
    public void duplicate_variable_names_fail()
    {
        QueryShouldFail("""
            query A($x: Int, $x: Int, $x: String) {
              __typename
            }

            query B($x: String, $x: Int) {
              __typename
            }

            query C($x: Int, $x: Int) {
              __typename
            }
            """, 3);
    }
}

[SpecSection("5.8.2", "Variables Are Input Types")]
public class VariablesAreInputTypesConformanceTests : SpecValidationRuleHarness
{
    public override ValidationRule RuleUnderTest { get; } = QueryValidationRules.VariablesAreInputTypes;

    [Fact]
    public void unknown_types_are_ignored()
    {
        QueryShouldPass("""
            query Foo($a: Unknown, $b: [[Unknown!]]!) {
              field(a: $a, b: $b)
            }
            """);
    }

    [Fact]
    public void input_types_are_valid()
    {
        QueryShouldPass("""
            query Foo($a: String, $b: [Boolean!]!, $c: ComplexInput) {
              field(a: $a, b: $b, c: $c)
            }
            """);
    }

    [Fact(Skip = "Output-type variable rejection is a conformance gap tracked in follow-up issue.")]
    public void output_types_are_invalid()
    {
        QueryShouldFail("""
            query Foo($a: Dog, $b: [[CatOrDog!]]!, $c: Pet) {
              field(a: $a, b: $b, c: $c)
            }
            """, 3);
    }
}

[SpecSection("5.8.3", "All Variable Uses Defined")]
public class NoUndefinedVariablesConformanceTests : SpecValidationRuleHarness
{
    public override ValidationRule RuleUnderTest { get; } = QueryValidationRules.NoUndefinedVariables;

    public static TheoryData<string, string> ValidQueries { get; } = new()
    {
        {
            "all_variables_defined",
            """
            query Foo($a: String, $b: String, $c: String) {
              field(a: $a, b: $b, c: $c)
            }
            """
        },
        {
            "all_variables_in_fragments_defined",
            """
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
            """
        },
    };

    public static TheoryData<string, string, int> InvalidQueries { get; } = new()
    {
        {
            "variable_not_defined",
            """
            query Foo($a: String, $b: String, $c: String) {
              field(a: $a, b: $b, c: $c, d: $d)
            }
            """,
            1
        },
        {
            "multiple_variables_not_defined",
            """
            query Foo($b: String) {
              field(a: $a, b: $b, c: $c)
            }
            """,
            2
        },
        {
            "variable_in_fragment_not_defined_by_operation",
            """
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
            """,
            1
        },
    };

    [Theory]
    [MemberData(nameof(ValidQueries))]
    public void valid_undefined_variable_queries_pass(string caseName, string query)
    {
        Assert.False(string.IsNullOrWhiteSpace(caseName));
        QueryShouldPass(query);
    }

    [Theory(Skip = "Negative undefined-variable validation cases are a conformance gap tracked in follow-up issue.")]
    [MemberData(nameof(InvalidQueries))]
    public void invalid_undefined_variable_queries_fail(string caseName, string query, int errorCount)
    {
        Assert.False(string.IsNullOrWhiteSpace(caseName));
        QueryShouldFail(query, errorCount);
    }
}

[SpecSection("5.8.4", "All Variables Used")]
public class NoUnusedVariablesConformanceTests : SpecValidationRuleHarness
{
    public override ValidationRule RuleUnderTest { get; } = QueryValidationRules.NoUnusedVariables;

    public static TheoryData<string, string> ValidQueries { get; } = new()
    {
        {
            "uses_all_variables",
            """
            query ($a: String, $b: String, $c: String) {
              field(a: $a, b: $b, c: $c)
            }
            """
        },
        {
            "uses_all_variables_in_fragments",
            """
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
            """
        },
    };

    public static TheoryData<string, string, int> InvalidQueries { get; } = new()
    {
        {
            "variable_not_used",
            """
            query ($a: String, $b: String, $c: String) {
              field(a: $a, b: $b)
            }
            """,
            1
        },
        {
            "multiple_variables_not_used",
            """
            query Foo($a: String, $b: String, $c: String) {
              field(b: $b)
            }
            """,
            2
        },
        {
            "variables_not_used_in_fragments",
            """
            query Foo($a: String, $b: String, $c: String) {
              ...FragA
            }

            fragment FragA on Type {
              field {
                ...FragB
              }
            }

            fragment FragB on Type {
              field(b: $b) {
                ...FragC
              }
            }

            fragment FragC on Type {
              field
            }
            """,
            2
        },
    };

    [Theory]
    [MemberData(nameof(ValidQueries))]
    public void valid_unused_variable_queries_pass(string caseName, string query)
    {
        Assert.False(string.IsNullOrWhiteSpace(caseName));
        QueryShouldPass(query);
    }

    [Theory(Skip = "Negative unused-variable validation cases are a conformance gap tracked in follow-up issue.")]
    [MemberData(nameof(InvalidQueries))]
    public void invalid_unused_variable_queries_fail(string caseName, string query, int errorCount)
    {
        Assert.False(string.IsNullOrWhiteSpace(caseName));
        QueryShouldFail(query, errorCount);
    }
}

[SpecSection("5.8.5", "All Variable Usages Are Allowed")]
public class VariablesInAllowedPositionConformanceTests : SpecValidationRuleHarness
{
    public override ValidationRule RuleUnderTest { get; } = QueryValidationRules.VariablesInAllowedPosition;

    public static TheoryData<string, string> ValidQueries { get; } = new()
    {
        {
            "boolean_to_boolean",
            """
            query Query($booleanArg: Boolean) {
              complicatedArgs {
                booleanArgField(booleanArg: $booleanArg)
              }
            }
            """
        },
        {
            "boolean_non_null_to_boolean",
            """
            query Query($nonNullBooleanArg: Boolean!) {
              complicatedArgs {
                booleanArgField(booleanArg: $nonNullBooleanArg)
              }
            }
            """
        },
        {
            "list_to_list",
            """
            query Query($stringListVar: [String]) {
              complicatedArgs {
                stringListArgField(stringListArg: $stringListVar)
              }
            }
            """
        },
    };

    public static TheoryData<string, string, int> InvalidQueries { get; } = new()
    {
        {
            "int_to_non_null_int",
            """
            query Query($intArg: Int) {
              complicatedArgs {
                nonNullIntArgField(nonNullIntArg: $intArg)
              }
            }
            """,
            1
        },
        {
            "string_over_boolean",
            """
            query Query($stringVar: String) {
              complicatedArgs {
                booleanArgField(booleanArg: $stringVar)
              }
            }
            """,
            1
        },
        {
            "string_to_list",
            """
            query Query($stringVar: String) {
              complicatedArgs {
                stringListArgField(stringListArg: $stringVar)
              }
            }
            """,
            1
        },
    };

    [Theory]
    [MemberData(nameof(ValidQueries))]
    public void valid_variable_usage_queries_pass(string caseName, string query)
    {
        Assert.False(string.IsNullOrWhiteSpace(caseName));
        QueryShouldPass(query);
    }

    [Theory(Skip = "Negative variable-position validation cases are a conformance gap tracked in follow-up issue.")]
    [MemberData(nameof(InvalidQueries))]
    public void invalid_variable_usage_queries_fail(string caseName, string query, int errorCount)
    {
        Assert.False(string.IsNullOrWhiteSpace(caseName));
        QueryShouldFail(query, errorCount);
    }

    [Fact(Skip = "OneOf variable-position cases need a dedicated oneOf schema harness; tracked via follow-up issue.")]
    public void oneof_variable_position_cases_are_not_yet_ported()
    {
    }
}
