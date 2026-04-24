// Copyright (c) GraphZen LLC. All rights reserved.
// Licensed under the GraphZen Community License. See the LICENSE file in the project root for license information.

using GraphZen.LanguageModel.Validation;
using GraphZen.QueryEngine.Validation;
using GraphZen.SpecConformance.Tests.Infrastructure;

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

// Spec draft: see SpecMetadata.Version
// Spec: https://spec.graphql.org/draft/#sec-Fragment-Spread-Type-Existence
// graphql-js source: src/validation/rules/KnownTypeNamesRule.ts
// graphql-js tests: src/validation/__tests__/KnownTypeNamesRule-test.ts

[SpecSection("5.5.1.2", "Fragment Spread Type Existence")]
public class FragmentSpreadTypeExistenceConformanceTests : SpecValidationRuleHarness
{
    public override ValidationRule RuleUnderTest { get; } = QueryValidationRules.KnownTypeNames;

    public static TheoryData<string, string> ValidQueries { get; } = new()
    {
        {
            "known_type_names_are_valid",
            """
            query Foo($var: String, $required: [String!]!) {
              user(id: 4) {
                pets { ... on Pet { name }, ...PetFields, ... { name } }
              }
            }

            fragment PetFields on Pet {
              name
            }
            """
        },
    };

    public static TheoryData<string, string, int> InvalidQueries { get; } = new()
    {
        {
            "unknown_type_names_are_invalid",
            """
            query Foo($var: JumbledUpLetters) {
              user(id: 4) {
                name
                pets { ... on Badger { name }, ...PetFields }
              }
            }

            fragment PetFields on Peettt {
              name
            }
            """,
            3
        },
        {
            "ignores_type_definitions",
            """
            type NotInTheSchema {
              field: FooBar
            }
            interface FooBar {
              field: NotInTheSchema
            }
            union U = A | B
            input Blob {
              field: UnknownType
            }
            query Foo($var: NotInTheSchema) {
              user(id: $var) {
                id
              }
            }
            """,
            1
        },
    };

    [Theory]
    [MemberData(nameof(ValidQueries))]
    public void valid_known_type_name_queries_pass(string caseName, string query)
    {
        Assert.False(string.IsNullOrWhiteSpace(caseName));
        QueryShouldPass(query);
    }

    [Theory(Skip = "Negative known-type-name validation cases are a conformance gap tracked in follow-up issue.")]
    [MemberData(nameof(InvalidQueries))]
    public void invalid_known_type_name_queries_fail(string caseName, string query, int errorCount)
    {
        Assert.False(string.IsNullOrWhiteSpace(caseName));
        QueryShouldFail(query, errorCount);
    }
}

// Spec draft: see SpecMetadata.Version
// Spec: https://spec.graphql.org/draft/#sec-Fragments-on-Object-Interface-or-Union-Types
// graphql-js source: src/validation/rules/FragmentsOnCompositeTypesRule.ts
// graphql-js tests: src/validation/__tests__/FragmentsOnCompositeTypesRule-test.ts

[SpecSection("5.5.1.3", "Fragments on Object, Interface or Union Types")]
public class FragmentsOnCompositeTypesConformanceTests : SpecValidationRuleHarness
{
    public override ValidationRule RuleUnderTest { get; } = QueryValidationRules.FragmentsOnCompositeTypes;

    public static TheoryData<string, string> ValidQueries { get; } = new()
    {
        {
            "object_is_valid_fragment_type",
            """
            fragment validFragment on Dog {
              barks
            }
            """
        },
        {
            "interface_is_valid_fragment_type",
            """
            fragment validFragment on Pet {
              name
            }
            """
        },
        {
            "object_is_valid_inline_fragment_type",
            """
            fragment validFragment on Pet {
              ... on Dog {
                barks
              }
            }
            """
        },
        {
            "inline_fragment_without_type_is_valid",
            """
            fragment validFragment on Pet {
              ... {
                name
              }
            }
            """
        },
        {
            "union_is_valid_fragment_type",
            """
            fragment validFragment on CatOrDog {
              __typename
            }
            """
        },
    };

    public static TheoryData<string, string, int> InvalidQueries { get; } = new()
    {
        {
            "scalar_is_invalid_fragment_type",
            """
            fragment scalarFragment on Boolean {
              bad
            }
            """,
            1
        },
        {
            "enum_is_invalid_fragment_type",
            """
            fragment scalarFragment on FurColor {
              bad
            }
            """,
            1
        },
        {
            "input_object_is_invalid_fragment_type",
            """
            fragment inputFragment on ComplexInput {
              stringField
            }
            """,
            1
        },
        {
            "scalar_is_invalid_inline_fragment_type",
            """
            fragment invalidFragment on Pet {
              ... on String {
                barks
              }
            }
            """,
            1
        },
    };

    [Theory]
    [MemberData(nameof(ValidQueries))]
    public void valid_composite_type_queries_pass(string caseName, string query)
    {
        Assert.False(string.IsNullOrWhiteSpace(caseName));
        QueryShouldPass(query);
    }

    [Theory(Skip = "Negative composite-type validation cases are a conformance gap tracked in follow-up issue.")]
    [MemberData(nameof(InvalidQueries))]
    public void invalid_composite_type_queries_fail(string caseName, string query, int errorCount)
    {
        Assert.False(string.IsNullOrWhiteSpace(caseName));
        QueryShouldFail(query, errorCount);
    }
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

// Spec draft: see SpecMetadata.Version
// Spec: https://spec.graphql.org/draft/#sec-Fragment-Spread-Target-Defined
// graphql-js source: src/validation/rules/KnownFragmentNamesRule.ts
// graphql-js tests: src/validation/__tests__/KnownFragmentNamesRule-test.ts

[SpecSection("5.5.2.1", "Fragment Spread Target Defined")]
public class FragmentSpreadTargetDefinedConformanceTests : SpecValidationRuleHarness
{
    public override ValidationRule RuleUnderTest { get; } = QueryValidationRules.KnownFragmentNames;

    public static TheoryData<string, string> ValidQueries { get; } = new()
    {
        {
            "known_fragment_names_are_valid",
            """
            {
              human(id: 4) {
                ...HumanFields1
                ... on Human {
                  ...HumanFields2
                }
                ... {
                  name
                }
              }
            }
            fragment HumanFields1 on Human {
              name
              ...HumanFields3
            }
            fragment HumanFields2 on Human {
              name
            }
            fragment HumanFields3 on Human {
              name
            }
            """
        },
    };

    public static TheoryData<string, string, int> InvalidQueries { get; } = new()
    {
        {
            "unknown_fragment_names",
            """
            {
              human(id: 4) {
                ...UnknownFragment1
                ... on Human {
                  ...UnknownFragment2
                }
              }
            }

            fragment HumanFields on Human {
              name
              ...UnknownFragment3
            }
            """,
            3
        },
    };

    [Theory]
    [MemberData(nameof(ValidQueries))]
    public void valid_fragment_spread_target_queries_pass(string caseName, string query)
    {
        Assert.False(string.IsNullOrWhiteSpace(caseName));
        QueryShouldPass(query);
    }

    [Theory(Skip = "Negative fragment-spread-target validation cases are a conformance gap tracked in follow-up issue.")]
    [MemberData(nameof(InvalidQueries))]
    public void invalid_fragment_spread_target_queries_fail(string caseName, string query, int errorCount)
    {
        Assert.False(string.IsNullOrWhiteSpace(caseName));
        QueryShouldFail(query, errorCount);
    }
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
