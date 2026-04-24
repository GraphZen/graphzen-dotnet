// Copyright (c) GraphZen LLC. All rights reserved.
// Licensed under the GraphZen Community License. See the LICENSE file in the project root for license information.

using GraphZen.SpecConformance.Tests.Infrastructure;
using GraphZen.TypeSystem;
using static GraphZen.QueryEngine.Validation.QueryValidationRules;
using static GraphZen.SpecConformance.Tests.Infrastructure.SpecValidation;

namespace GraphZen.SpecConformance.Tests.Section5_Validation.Operations;

/// <seealso href="https://spec.graphql.org/draft/#sec-Single-Root-Field" />
[SpecSection("5.2.4.1", "Single Root Field")]
public class SingleRootFieldConformanceTests
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

    [Fact]
    public void valid_subscription_passes() =>
        ExpectValid(SubscriptionSchema, SingleFieldSubscriptions, """
                                                                  subscription sub {
                                                                    newMessage {
                                                                      body
                                                                      sender
                                                                    }
                                                                  }
                                                                  """);

    [Fact]
    public void valid_subscription_with_fragment_passes() =>
        ExpectValid(SubscriptionSchema, SingleFieldSubscriptions, """
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

    [Fact(Skip = "GraphZen does not validate single root field in subscriptions.")]
    public void multiple_root_fields_fail()
    {
        _ = """
            subscription sub {
              newMessage {
                body
              }
              otherMessage {
                body
              }
            }
            """;
        throw new NotImplementedException(
            "Expected error assertions for SingleFieldSubscriptions rule need to be ported from graphql-js.");
    }

    [Fact(Skip = "GraphZen does not validate single root field in subscriptions.")]
    public void introspection_root_field_fails()
    {
        _ = """
            subscription sub {
              __typename
            }
            """;
        throw new NotImplementedException(
            "Expected error assertions for SingleFieldSubscriptions rule need to be ported from graphql-js.");
    }
}
