// Copyright (c) GraphZen LLC. All rights reserved.
// Licensed under the GraphZen Community License. See the LICENSE file in the project root for license information.

using GraphZen.SpecConformance.Tests.Infrastructure;
using static GraphZen.QueryEngine.Validation.QueryValidationRules;
using static GraphZen.SpecConformance.Tests.Infrastructure.SpecValidation;

namespace GraphZen.SpecConformance.Tests.Section5_Validation.Operations;

/// <seealso href="https://spec.graphql.org/draft/#sec-Lone-Anonymous-Operation" />
[SpecSection("5.2.3.1", "Lone Anonymous Operation")]
public class LoneAnonymousOperationConformanceTests
{
    [Fact]
    public void no_operations() =>
        ExpectValid(LoneAnonymousOperation, """
                                            fragment fragA on Type {
                                              field
                                            }
                                            """);

    [Fact]
    public void one_anonymous_operation() =>
        ExpectValid(LoneAnonymousOperation, """
                                            {
                                              field
                                            }
                                            """);

    [Fact]
    public void multiple_named_operations() =>
        ExpectValid(LoneAnonymousOperation, """
                                            query Foo {
                                              field
                                            }

                                            query Bar {
                                              field
                                            }
                                            """);

    [Fact(Skip =
        "GraphZen does not reject multiple anonymous operations or anonymous operations alongside named ones.")]
    public void multiple_anonymous_operations()
    {
        _ = """
            {
              fieldA
            }
            {
              fieldB
            }
            """;
        throw new NotImplementedException(
            "Expected error assertions for LoneAnonymousOperation rule need to be ported from graphql-js.");
    }

    [Fact(Skip =
        "GraphZen does not reject multiple anonymous operations or anonymous operations alongside named ones.")]
    public void anonymous_operation_with_a_mutation()
    {
        _ = """
            {
              fieldA
            }
            mutation Foo {
              fieldB
            }
            """;
        throw new NotImplementedException(
            "Expected error assertions for LoneAnonymousOperation rule need to be ported from graphql-js.");
    }

    [Fact(Skip =
        "GraphZen does not reject multiple anonymous operations or anonymous operations alongside named ones.")]
    public void anonymous_operation_with_a_subscription()
    {
        _ = """
            {
              fieldA
            }
            subscription Foo {
              fieldB
            }
            """;
        throw new NotImplementedException(
            "Expected error assertions for LoneAnonymousOperation rule need to be ported from graphql-js.");
    }
}
