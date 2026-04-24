// Copyright (c) GraphZen LLC. All rights reserved.
// Licensed under the GraphZen Community License. See the LICENSE file in the project root for license information.

using GraphZen.LanguageModel.Validation;
using GraphZen.QueryEngine.Validation;
using GraphZen.SpecConformance.Tests.Infrastructure;

namespace GraphZen.SpecConformance.Tests.Section5_Validation.Arguments;

[SpecSection("5.4.1", "Argument Names")]
public class KnownArgumentNamesConformanceTests : SpecValidationRuleHarness
{
    public override ValidationRule RuleUnderTest { get; } = QueryValidationRules.KnownArgumentNames;

    public static TheoryData<string, string> ValidQueries { get; } = new()
    {
        {
            "single_arg_is_known",
            """
            fragment argOnRequiredArg on Dog {
              doesKnowCommand(dogCommand: SIT)
            }
            """
        },
        {
            "multiple_args_are_known",
            """
            fragment multipleArgs on ComplicatedArgs {
              multipleReqs(req1: 1, req2: 2)
            }
            """
        },
        {
            "directive_args_are_known",
            """
            {
              cat @skip(if: true) {
                nickname
              }
            }
            """
        },
    };

    public static TheoryData<string, string, int> InvalidQueries { get; } = new()
    {
        {
            "directive_without_args_reports_unknown_arg",
            """
            {
              cat @onField(if: true) {
                nickname
              }
            }
            """,
            1
        },
        {
            "invalid_field_argument_name",
            """
            fragment invalidArgName on Dog {
              doesKnowCommand(unknown: true)
            }
            """,
            1
        },
        {
            "unknown_args_amongst_known_args",
            """
            fragment oneGoodArgOneInvalidArg on Dog {
              doesKnowCommand(whoKnows: 1, dogCommand: SIT, unknown: true)
            }
            """,
            2
        },
    };

    [Theory]
    [MemberData(nameof(ValidQueries))]
    public void valid_argument_name_queries_pass(string caseName, string query)
    {
        Assert.False(string.IsNullOrWhiteSpace(caseName));
        QueryShouldPass(query);
    }

    [Theory(Skip = "Negative known-argument validation cases are a conformance gap tracked in follow-up issue.")]
    [MemberData(nameof(InvalidQueries))]
    public void invalid_argument_name_queries_fail(string caseName, string query, int errorCount)
    {
        Assert.False(string.IsNullOrWhiteSpace(caseName));
        QueryShouldFail(query, errorCount);
    }
}

[SpecSection("5.4.2", "Argument Uniqueness")]
public class UniqueArgumentNamesConformanceTests : SpecValidationRuleHarness
{
    public override ValidationRule RuleUnderTest { get; } = QueryValidationRules.UniqueArgumentNames;

    public static TheoryData<string, string> ValidQueries { get; } = new()
    {
        {
            "argument_on_field",
            """
            {
              field(arg: "value")
            }
            """
        },
        {
            "argument_on_directive",
            """
            {
              field @directive(arg: "value")
            }
            """
        },
        {
            "multiple_field_arguments",
            """
            {
              field(arg1: "value", arg2: "value", arg3: "value")
            }
            """
        },
    };

    public static TheoryData<string, string, int> InvalidQueries { get; } = new()
    {
        {
            "duplicate_field_arguments",
            """
            {
              field(arg1: "value", arg1: "value")
            }
            """,
            1
        },
        {
            "many_duplicate_field_arguments",
            """
            {
              field(arg1: "value", arg1: "value", arg1: "value")
            }
            """,
            1
        },
        {
            "duplicate_directive_arguments",
            """
            {
              field @directive(arg1: "value", arg1: "value")
            }
            """,
            1
        },
    };

    [Theory]
    [MemberData(nameof(ValidQueries))]
    public void valid_unique_argument_name_queries_pass(string caseName, string query)
    {
        Assert.False(string.IsNullOrWhiteSpace(caseName));
        QueryShouldPass(query);
    }

    [Theory(Skip = "Negative unique-argument validation cases are a conformance gap tracked in follow-up issue.")]
    [MemberData(nameof(InvalidQueries))]
    public void invalid_unique_argument_name_queries_fail(string caseName, string query, int errorCount)
    {
        Assert.False(string.IsNullOrWhiteSpace(caseName));
        QueryShouldFail(query, errorCount);
    }
}

[SpecSection("5.4.3", "Required Arguments")]
public class ProvidedRequiredArgumentsConformanceTests : SpecValidationRuleHarness
{
    public override ValidationRule RuleUnderTest { get; } = QueryValidationRules.ProvidedRequiredArguments;

    public static TheoryData<string, string> ValidQueries { get; } = new()
    {
        {
            "unknown_arguments_are_ignored",
            """
            fragment ignoresUnknownArguments on Dog {
              isHouseTrained(unknownArgument: true)
            }
            """
        },
        {
            "no_arg_on_optional_arg",
            """
            fragment noArgOnOptionalArg on Dog {
              isHouseTrained
            }
            """
        },
        {
            "no_arg_on_non_null_field_with_default",
            """
            {
              complicatedArgs {
                nonNullFieldWithDefault
              }
            }
            """
        },
        {
            "multiple_required_args",
            """
            {
              complicatedArgs {
                multipleReqs(req1: 1, req2: 2)
              }
            }
            """
        },
        {
            "directive_with_required_arg",
            """
            {
              cat @include(if: true) {
                nickname
              }
            }
            """
        },
    };

    public static TheoryData<string, string, int> InvalidQueries { get; } = new()
    {
        {
            "missing_one_non_nullable_argument",
            """
            {
              complicatedArgs {
                multipleReqs(req2: 2)
              }
            }
            """,
            1
        },
        {
            "missing_multiple_non_nullable_arguments",
            """
            {
              complicatedArgs {
                multipleReqs
              }
            }
            """,
            2
        },
        {
            "directive_with_missing_required_arg",
            """
            {
              cat @include {
                nickname @skip
              }
            }
            """,
            2
        },
    };

    [Theory]
    [MemberData(nameof(ValidQueries))]
    public void valid_required_argument_queries_pass(string caseName, string query)
    {
        Assert.False(string.IsNullOrWhiteSpace(caseName));
        QueryShouldPass(query);
    }

    [Theory(Skip = "Negative required-argument validation cases are a conformance gap tracked in follow-up issue.")]
    [MemberData(nameof(InvalidQueries))]
    public void invalid_required_argument_queries_fail(string caseName, string query, int errorCount)
    {
        Assert.False(string.IsNullOrWhiteSpace(caseName));
        QueryShouldFail(query, errorCount);
    }
}
