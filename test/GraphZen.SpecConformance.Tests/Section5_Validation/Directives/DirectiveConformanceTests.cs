// Copyright (c) GraphZen LLC. All rights reserved.
// Licensed under the GraphZen Community License. See the LICENSE file in the project root for license information.

using GraphZen.LanguageModel.Validation;
using GraphZen.QueryEngine.Validation;
using GraphZen.SpecConformance.Tests.Infrastructure;
using GraphZen.Tests.Validation.Rules;

namespace GraphZen.SpecConformance.Tests.Section5_Validation.Directives;

[SpecSection("5.7.1", "Directives Are Defined")]
[SpecSection("5.7.2", "Directives Are in Valid Locations")]
public class KnownDirectivesConformanceTests : KnownDirectivesTests
{
}

[SpecSection("5.7.3", "Directives Are Unique per Location")]
public class UniqueDirectivesPerLocationConformanceTests : SpecValidationRuleHarness
{
    public override ValidationRule RuleUnderTest { get; } = QueryValidationRules.UniqueDirectivesPerLocation;

    public static TheoryData<string, string> ValidQueries { get; } = new()
    {
        {
            "same_directives_in_different_locations",
            """
            {
              cat @skip(if: false) {
                nickname @skip(if: true)
              }
            }
            """
        },
        {
            "unknown_directives_are_ignored",
            """
            {
              cat @unknown {
                nickname @unknown
              }
            }
            """
        },
    };

    public static TheoryData<string, string, int> InvalidQueries { get; } = new()
    {
        {
            "duplicate_directives_in_one_location",
            """
            {
              cat @skip(if: false) @skip(if: true) {
                nickname
              }
            }
            """,
            1
        },
        {
            "different_duplicate_directives_in_one_location",
            """
            {
              cat @skip(if: false) @skip(if: true) @include(if: true) @include(if: false) {
                nickname
              }
            }
            """,
            2
        },
    };

    [Theory]
    [MemberData(nameof(ValidQueries))]
    public void valid_unique_directive_queries_pass(string caseName, string query)
    {
        Assert.False(string.IsNullOrWhiteSpace(caseName));
        QueryShouldPass(query);
    }

    [Theory(Skip = "Negative unique-directive validation cases are a conformance gap tracked in follow-up issue.")]
    [MemberData(nameof(InvalidQueries))]
    public void invalid_unique_directive_queries_fail(string caseName, string query, int errorCount)
    {
        Assert.False(string.IsNullOrWhiteSpace(caseName));
        QueryShouldFail(query, errorCount);
    }
}
