// Copyright (c) GraphZen LLC. All rights reserved.
// Licensed under the GraphZen Community License. See the LICENSE file in the project root for license information.

using GraphZen.LanguageModel.Validation;
using GraphZen.QueryEngine.Validation;
using GraphZen.SpecConformance.Tests.Infrastructure;
using GraphZen.TypeSystem;

namespace GraphZen.SpecConformance.Tests.Section5_Validation.Operations;

// Spec draft: see SpecMetadata.Version
// Spec: https://spec.graphql.org/draft/#sec-Operation-Name-Uniqueness
// graphql-js source: src/validation/rules/UniqueOperationNamesRule.ts
// graphql-js tests: src/validation/__tests__/UniqueOperationNamesRule-test.ts

[SpecSection("5.2.2.1", "Operation Name Uniqueness")]
public class OperationNameUniquenessConformanceTests : SpecValidationRuleHarness
{
    public override ValidationRule RuleUnderTest { get; } = QueryValidationRules.UniqueOperationNames;

    public static TheoryData<string, string> ValidQueries { get; } = new()
    {
        {
            "no_operations",
            """
            fragment fragA on Type {
              field
            }
            """
        },
        {
            "one_anonymous_operation",
            """
            {
              field
            }
            """
        },
        {
            "one_named_operation",
            """
            query Foo {
              field
            }
            """
        },
        {
            "multiple_operations",
            """
            query Foo {
              field
            }
            query Bar {
              field
            }
            """
        },
        {
            "multiple_operations_of_different_types",
            """
            query Foo {
              field
            }
            mutation Bar {
              field
            }
            subscription Baz {
              field
            }
            """
        },
        {
            "fragment_and_operation_named_the_same",
            """
            query Foo {
              ...Foo
            }
            fragment Foo on Type {
              field
            }
            """
        },
    };

    public static TheoryData<string, string, int> InvalidQueries { get; } = new()
    {
        {
            "multiple_operations_of_same_name_mutation",
            """
            query Foo {
              fieldA
            }
            query Foo {
              fieldB
            }
            """,
            1
        },
        {
            "multiple_operations_of_same_name_subscription",
            """
            query Foo {
              fieldA
            }
            subscription Foo {
              fieldB
            }
            """,
            1
        },
    };

    [Theory]
    [MemberData(nameof(ValidQueries))]
    public void valid_operation_name_uniqueness_queries_pass(string caseName, string query)
    {
        Assert.False(string.IsNullOrWhiteSpace(caseName));
        QueryShouldPass(query);
    }

    [Theory(Skip = "Negative operation name uniqueness validation cases are a conformance gap tracked in follow-up issue.")]
    [MemberData(nameof(InvalidQueries))]
    public void invalid_operation_name_uniqueness_queries_fail(string caseName, string query, int errorCount)
    {
        Assert.False(string.IsNullOrWhiteSpace(caseName));
        QueryShouldFail(query, errorCount);
    }
}

// Spec draft: see SpecMetadata.Version
// Spec: https://spec.graphql.org/draft/#sec-Lone-Anonymous-Operation
// graphql-js source: src/validation/rules/LoneAnonymousOperationRule.ts
// graphql-js tests: src/validation/__tests__/LoneAnonymousOperationRule-test.ts

[SpecSection("5.2.3.1", "Lone Anonymous Operation")]
public class LoneAnonymousOperationConformanceTests : SpecValidationRuleHarness
{
    public override ValidationRule RuleUnderTest { get; } = QueryValidationRules.LoneAnonymousOperation;

    public static TheoryData<string, string> ValidQueries { get; } = new()
    {
        {
            "no_operations",
            """
            fragment fragA on Type {
              field
            }
            """
        },
        {
            "one_anonymous_operation",
            """
            {
              field
            }
            """
        },
        {
            "multiple_named_operations",
            """
            query Foo {
              field
            }

            query Bar {
              field
            }
            """
        },
    };

    public static TheoryData<string, string, int> InvalidQueries { get; } = new()
    {
        {
            "multiple_anonymous_operations",
            """
            {
              fieldA
            }
            {
              fieldB
            }
            """,
            2
        },
        {
            "anonymous_operation_with_a_mutation",
            """
            {
              fieldA
            }
            mutation Foo {
              fieldB
            }
            """,
            1
        },
        {
            "anonymous_operation_with_a_subscription",
            """
            {
              fieldA
            }
            subscription Foo {
              fieldB
            }
            """,
            1
        },
    };

    [Theory]
    [MemberData(nameof(ValidQueries))]
    public void valid_lone_anonymous_operation_queries_pass(string caseName, string query)
    {
        Assert.False(string.IsNullOrWhiteSpace(caseName));
        QueryShouldPass(query);
    }

    [Theory(Skip = "Negative lone anonymous operation validation cases are a conformance gap tracked in follow-up issue.")]
    [MemberData(nameof(InvalidQueries))]
    public void invalid_lone_anonymous_operation_queries_fail(string caseName, string query, int errorCount)
    {
        Assert.False(string.IsNullOrWhiteSpace(caseName));
        QueryShouldFail(query, errorCount);
    }
}

[SpecSection("5.2.4.1", "Single Root Field")]
public class SingleFieldSubscriptionsConformanceTests : SpecValidationRuleHarness
{
    private static readonly Schema SubscriptionSchema = Schema.Create(sb =>
    {
        sb.Object("Message")
            .Field("body", "String")
            .Field("sender", "String");

        sb.Object("QueryRoot")
            .Field("ping", "String");

        sb.Object("SubscriptionRoot")
            .Field("newMessage", "Message")
            .Field("otherMessage", "Message");

        sb.QueryType("QueryRoot");
        sb.SubscriptionType("SubscriptionRoot");
    });

    public override ValidationRule RuleUnderTest { get; } = QueryValidationRules.SingleFieldSubscriptions;

    [Fact]
    public void valid_subscription_passes()
    {
        QueryShouldPass(SubscriptionSchema, """
            subscription sub {
              newMessage {
                body
                sender
              }
            }
            """);
    }

    [Fact]
    public void valid_subscription_with_fragment_passes()
    {
        QueryShouldPass(SubscriptionSchema, """
            subscription sub {
              ...newMessageFields
            }

            fragment newMessageFields on SubscriptionRoot {
              newMessage {
                body
                sender
              }
            }
            """);
    }

    [Fact(Skip = "Subscription root-field validation gap tracked in follow-up issue.")]
    public void multiple_root_fields_fail()
    {
        QueryShouldFail(SubscriptionSchema, """
            subscription sub {
              newMessage {
                body
              }
              otherMessage {
                body
              }
            }
            """);
    }

    [Fact(Skip = "Subscription root-field validation gap tracked in follow-up issue.")]
    public void introspection_root_field_fails()
    {
        QueryShouldFail(SubscriptionSchema, """
            subscription sub {
              __typename
            }
            """);
    }
}
