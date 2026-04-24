// Copyright (c) GraphZen LLC. All rights reserved.
// Licensed under the GraphZen Community License. See the LICENSE file in the project root for license information.

using GraphZen.LanguageModel.Validation;
using GraphZen.QueryEngine.Validation;
using GraphZen.SpecConformance.Tests.Infrastructure;

namespace GraphZen.SpecConformance.Tests.Section5_Validation.Directives;

// Spec draft: see SpecMetadata.Version
// Spec: https://spec.graphql.org/draft/#sec-Directives-Are-Defined
// graphql-js source: src/validation/rules/KnownDirectivesRule.ts
// graphql-js tests: src/validation/__tests__/KnownDirectivesRule-test.ts

[SpecSection("5.7.1", "Directives Are Defined")]
public class DirectivesAreDefinedConformanceTests : SpecValidationRuleHarness
{
    public override ValidationRule RuleUnderTest { get; } = QueryValidationRules.KnownDirectives;

    public static TheoryData<string, string> ValidQueries { get; } = new()
    {
        {
            "no_directives",
            """
            query Foo {
              name
              ...Frag
            }

            fragment Frag on Dog {
              name
            }
            """
        },
        {
            "with_known_directives",
            """
            {
              dog @include(if: true) {
                name
              }
              human @skip(if: false) {
                name
              }
            }
            """
        },
    };

    public static TheoryData<string, string, int> InvalidQueries { get; } = new()
    {
        {
            "with_unknown_directive",
            """
            {
              dog @unknown(directive: "value") {
                name
              }
            }
            """,
            1
        },
        {
            "with_many_unknown_directives",
            """
            {
              dog @unknown(directive: "value") {
                name
              }
              human @unknown(directive: "value") {
                name
                pets @unknown(directive: "value") {
                  name
                }
              }
            }
            """,
            3
        },
    };

    [Theory]
    [MemberData(nameof(ValidQueries))]
    public void valid_directives_are_defined_queries_pass(string caseName, string query)
    {
        Assert.False(string.IsNullOrWhiteSpace(caseName));
        QueryShouldPass(query);
    }

    [Theory(Skip = "Negative directives-are-defined validation cases are a conformance gap tracked in follow-up issue.")]
    [MemberData(nameof(InvalidQueries))]
    public void invalid_directives_are_defined_queries_fail(string caseName, string query, int errorCount)
    {
        Assert.False(string.IsNullOrWhiteSpace(caseName));
        QueryShouldFail(query, errorCount);
    }
}

// Spec draft: see SpecMetadata.Version
// Spec: https://spec.graphql.org/draft/#sec-Directives-Are-in-Valid-Locations
// graphql-js source: src/validation/rules/KnownDirectivesRule.ts
// graphql-js tests: src/validation/__tests__/KnownDirectivesRule-test.ts

[SpecSection("5.7.2", "Directives Are in Valid Locations")]
public class DirectivesAreInValidLocationsConformanceTests : SpecValidationRuleHarness
{
    public override ValidationRule RuleUnderTest { get; } = QueryValidationRules.KnownDirectives;

    public static TheoryData<string, string> ValidQueries { get; } = new()
    {
        {
            "well_placed_directives",
            """
            query Foo($var: Boolean) @onQuery {
              name @include(if: $var)
              ...Frag @include(if: true)
              skippedField @skip(if: true)
              ...SkippedFrag @skip(if: true)
            }

            mutation Bar @onMutation {
              someField
            }
            """
        },
    };

    public static TheoryData<string, string, int> InvalidQueries { get; } = new()
    {
        {
            "with_misplaced_directives",
            """
            query Foo($var: Boolean) @include(if: true) {
              name @onQuery @include(if: $var)
              ...Frag @onQuery
            }

            mutation Bar @onQuery {
              someField
            }
            """,
            4
        },
    };

    [Theory]
    [MemberData(nameof(ValidQueries))]
    public void valid_directives_in_valid_locations_queries_pass(string caseName, string query)
    {
        Assert.False(string.IsNullOrWhiteSpace(caseName));
        QueryShouldPass(query);
    }

    [Theory(Skip = "Negative directives-in-valid-locations validation cases are a conformance gap tracked in follow-up issue.")]
    [MemberData(nameof(InvalidQueries))]
    public void invalid_directives_in_valid_locations_queries_fail(string caseName, string query, int errorCount)
    {
        Assert.False(string.IsNullOrWhiteSpace(caseName));
        QueryShouldFail(query, errorCount);
    }
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
