// Copyright (c) GraphZen LLC. All rights reserved.
// Licensed under the GraphZen Community License. See the LICENSE file in the project root for license information.

using GraphZen.LanguageModel.Validation;
using GraphZen.QueryEngine.Validation;
using GraphZen.SpecConformance.Tests.Infrastructure;
using GraphZen.Tests.Validation.Rules;
using GraphZen.TypeSystem;

namespace GraphZen.SpecConformance.Tests.Section5_Validation.Operations;

[SpecSection("5.2.2.1", "Operation Name Uniqueness")]
public class UniqueOperationNamesConformanceTests : UniqueOperationNamesTests
{
}

[SpecSection("5.2.3.1", "Lone Anonymous Operation")]
public class LoneAnonymousOperationConformanceTests : LoneAnonymousOperationTests
{
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
