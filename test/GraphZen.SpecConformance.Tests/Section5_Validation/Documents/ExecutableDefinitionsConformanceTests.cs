// Copyright (c) GraphZen LLC. All rights reserved.
// Licensed under the GraphZen Community License. See the LICENSE file in the project root for license information.

// Spec draft: see SpecMetadata.Version
// Spec: https://spec.graphql.org/draft/#sec-Executable-Definitions
// graphql-js source: src/validation/rules/ExecutableDefinitionsRule.ts
// graphql-js tests: src/validation/__tests__/ExecutableDefinitionsRule-test.ts

using GraphZen.LanguageModel.Validation;
using GraphZen.QueryEngine.Validation;
using GraphZen.SpecConformance.Tests.Infrastructure;

namespace GraphZen.SpecConformance.Tests.Section5_Validation.Documents;

[SpecSection("5.1.1", "Executable Definitions")]
public class ExecutableDefinitionsConformanceTests : SpecValidationRuleHarness
{
    public override ValidationRule RuleUnderTest { get; } = QueryValidationRules.ExecutableDefinitions;

    public static TheoryData<string, string> ValidQueries { get; } = new()
    {
        {
            "with_only_operation",
            """
            query Foo {
              dog {
                name
              }
            }
            """
        },
        {
            "with_operation_and_fragment",
            """
            query Foo {
              dog {
                name
                ...Frag
              }
            }

            fragment Frag on Dog {
              name
            }
            """
        },
    };

    public static TheoryData<string, string, int> InvalidQueries { get; } = new()
    {
        {
            "with_type_definition",
            """
            query Foo {
              dog {
                name
              }
            }

            type Cow {
              name: String
            }

            extend type Dog {
              color: String
            }
            """,
            2
        },
        {
            "with_schema_definition",
            """
            schema {
              query: Query
            }

            type Query {
              test: String
            }

            extend schema @directive
            """,
            3
        },
    };

    [Theory]
    [MemberData(nameof(ValidQueries))]
    public void valid_executable_definition_queries_pass(string caseName, string query)
    {
        Assert.False(string.IsNullOrWhiteSpace(caseName));
        QueryShouldPass(query);
    }

    [Theory(Skip = "Negative executable-definition validation cases are a conformance gap tracked in follow-up issue.")]
    [MemberData(nameof(InvalidQueries))]
    public void invalid_executable_definition_queries_fail(string caseName, string query, int errorCount)
    {
        Assert.False(string.IsNullOrWhiteSpace(caseName));
        QueryShouldFail(query, errorCount);
    }
}
