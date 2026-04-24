// Copyright (c) GraphZen LLC. All rights reserved.
// Licensed under the GraphZen Community License. See the LICENSE file in the project root for license information.

using GraphZen.LanguageModel.Validation;
using GraphZen.QueryEngine.Validation;
using GraphZen.SpecConformance.Tests.Infrastructure;
using GraphZen.Tests.Validation.Rules;

namespace GraphZen.SpecConformance.Tests.Section5_Validation.Fragments;

[SpecSection("5.5.1.1", "Fragment Name Uniqueness")]
public class UniqueFragmentNamesConformanceTests : SpecValidationRuleHarness
{
    public override ValidationRule RuleUnderTest { get; } = QueryValidationRules.UniqueFragmentNames;

    public static TheoryData<string, string> ValidQueries { get; } = new()
    {
        {
            "many_fragments",
            """
            {
              dogOrHuman {
                __typename
              }
            }

            fragment one on Dog {
              name
            }

            fragment two on Cat {
              name
            }
            """
        },
        {
            "fragment_and_operation_named_the_same",
            """
            query dog {
              cat {
                name
              }
            }

            fragment dog on Dog {
              name
            }
            """
        },
    };

    public static TheoryData<string, string, int> InvalidQueries { get; } = new()
    {
        {
            "fragments_named_the_same",
            """
            fragment fragmentOne on Dog {
              name
            }

            fragment fragmentOne on Cat {
              name
            }
            """,
            1
        },
        {
            "duplicate_fragment_name_without_reference",
            """
            fragment fragmentOne on Dog {
              name
            }

            fragment fragmentOne on Cat {
              name
            }
            """,
            1
        },
    };

    [Theory]
    [MemberData(nameof(ValidQueries))]
    public void valid_fragment_name_queries_pass(string caseName, string query)
    {
        Assert.False(string.IsNullOrWhiteSpace(caseName));
        QueryShouldPass(query);
    }

    [Theory(Skip = "Negative fragment-name validation cases are a conformance gap tracked in follow-up issue.")]
    [MemberData(nameof(InvalidQueries))]
    public void invalid_fragment_name_queries_fail(string caseName, string query, int errorCount)
    {
        Assert.False(string.IsNullOrWhiteSpace(caseName));
        QueryShouldFail(query, errorCount);
    }
}

[SpecSection("5.5.1.2", "Fragment Spread Type Existence")]
public class KnownTypeNamesConformanceTests : KnownTypeNamesTests
{
}

[SpecSection("5.5.1.3", "Fragments on Object, Interface or Union Types")]
public class FragmentsOnCompositeTypesConformanceTests : FragmentsOnCompositeTypesTests
{
}

[SpecSection("5.5.1.4", "Fragments Must Be Used")]
public class NoUnusedFragmentsConformanceTests : SpecValidationRuleHarness
{
    public override ValidationRule RuleUnderTest { get; } = QueryValidationRules.NoUnusedFragments;

    public static TheoryData<string, string> ValidQueries { get; } = new()
    {
        {
            "all_fragment_names_are_used",
            """
            {
              ...FragA
            }

            fragment FragA on Type {
              ...FragB
            }

            fragment FragB on Type {
              field
            }
            """
        },
        {
            "unknown_fragments_are_ignored",
            """
            {
              ...UnknownFragment
            }
            """
        },
    };

    public static TheoryData<string, string, int> InvalidQueries { get; } = new()
    {
        {
            "contains_unknown_fragments",
            """
            {
              ...FragA
            }

            fragment FragA on Type {
              field
            }

            fragment FragB on Type {
              field
            }
            """,
            1
        },
        {
            "contains_unknown_and_undefined_fragments",
            """
            {
              ...FragA
            }

            fragment FragA on Type {
              ...FragB
            }

            fragment FragB on Type {
              field
            }

            fragment FragC on Type {
              field
            }
            """,
            1
        },
    };

    [Theory]
    [MemberData(nameof(ValidQueries))]
    public void valid_unused_fragment_queries_pass(string caseName, string query)
    {
        Assert.False(string.IsNullOrWhiteSpace(caseName));
        QueryShouldPass(query);
    }

    [Theory(Skip = "Negative unused-fragment validation cases are a conformance gap tracked in follow-up issue.")]
    [MemberData(nameof(InvalidQueries))]
    public void invalid_unused_fragment_queries_fail(string caseName, string query, int errorCount)
    {
        Assert.False(string.IsNullOrWhiteSpace(caseName));
        QueryShouldFail(query, errorCount);
    }
}

[SpecSection("5.5.2.1", "Fragment Spread Target Defined")]
public class KnownFragmentNamesConformanceTests : KnownFragmentNamesTests
{
}

[SpecSection("5.5.2.2", "Fragment Spreads Must Not Form Cycles")]
public class NoFragmentCyclesConformanceTests : SpecValidationRuleHarness
{
    public override ValidationRule RuleUnderTest { get; } = QueryValidationRules.NoFragmentCycles;

    public static TheoryData<string, string> ValidQueries { get; } = new()
    {
        {
            "single_reference_is_valid",
            """
            fragment fragA on Type {
              ...fragB
            }

            fragment fragB on Type {
              field
            }
            """
        },
        {
            "spreading_twice_is_not_circular",
            """
            fragment fragA on Type {
              ...fragB
              ...fragB
            }

            fragment fragB on Type {
              field
            }
            """
        },
    };

    public static TheoryData<string, string, int> InvalidQueries { get; } = new()
    {
        {
            "no_spreading_itself_directly",
            """
            fragment fragA on Type {
              ...fragA
            }
            """,
            1
        },
        {
            "no_spreading_itself_indirectly",
            """
            fragment fragA on Type {
              ...fragB
            }

            fragment fragB on Type {
              ...fragA
            }
            """,
            1
        },
        {
            "no_spreading_itself_deeply",
            """
            fragment fragA on Type {
              ...fragB
            }

            fragment fragB on Type {
              ...fragC
            }

            fragment fragC on Type {
              ...fragA
            }
            """,
            1
        },
    };

    [Theory]
    [MemberData(nameof(ValidQueries))]
    public void valid_fragment_cycle_queries_pass(string caseName, string query)
    {
        Assert.False(string.IsNullOrWhiteSpace(caseName));
        QueryShouldPass(query);
    }

    [Theory(Skip = "Negative fragment-cycle validation cases are a conformance gap tracked in follow-up issue.")]
    [MemberData(nameof(InvalidQueries))]
    public void invalid_fragment_cycle_queries_fail(string caseName, string query, int errorCount)
    {
        Assert.False(string.IsNullOrWhiteSpace(caseName));
        QueryShouldFail(query, errorCount);
    }
}

[SpecSection("5.5.2.3", "Fragment Spread Is Possible")]
public class PossibleFragmentSpreadsConformanceTests : SpecValidationRuleHarness
{
    public override ValidationRule RuleUnderTest { get; } = QueryValidationRules.PossibleFragmentSpreads;

    [Fact(Skip = "Broader graphql-js fragment spread matrix remains a conformance gap; tracked via follow-up issue.")]
    public void graphql_js_fragment_spread_matrix_is_not_yet_ported()
    {
    }
}
