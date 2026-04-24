// Copyright (c) GraphZen LLC. All rights reserved.
// Licensed under the GraphZen Community License. See the LICENSE file in the project root for license information.

using GraphZen.SpecConformance.Tests.Infrastructure;
using static GraphZen.QueryEngine.Validation.QueryValidationRules;
using static GraphZen.SpecConformance.Tests.Infrastructure.SpecValidation;

namespace GraphZen.SpecConformance.Tests.Section5_Validation.Operations;

/// <seealso href="https://spec.graphql.org/draft/#sec-Operation-Name-Uniqueness" />
[SpecSection("5.2.2.1", "Operation Name Uniqueness")]
public class OperationNameUniquenessConformanceTests
{
    [Fact]
    public void no_operations() =>
        ExpectValid(UniqueOperationNames, """
                                          fragment fragA on Type {
                                            field
                                          }
                                          """);

    [Fact]
    public void one_anonymous_operation() =>
        ExpectValid(UniqueOperationNames, """
                                          {
                                            field
                                          }
                                          """);

    [Fact]
    public void one_named_operation() =>
        ExpectValid(UniqueOperationNames, """
                                          query Foo {
                                            field
                                          }
                                          """);

    [Fact]
    public void multiple_operations() =>
        ExpectValid(UniqueOperationNames, """
                                          query Foo {
                                            field
                                          }
                                          query Bar {
                                            field
                                          }
                                          """);

    [Fact]
    public void multiple_operations_of_different_types() =>
        ExpectValid(UniqueOperationNames, """
                                          query Foo {
                                            field
                                          }
                                          mutation Bar {
                                            field
                                          }
                                          subscription Baz {
                                            field
                                          }
                                          """);

    [Fact]
    public void fragment_and_operation_named_the_same() =>
        ExpectValid(UniqueOperationNames, """
                                          query Foo {
                                            ...Foo
                                          }
                                          fragment Foo on Type {
                                            field
                                          }
                                          """);

    [Fact(Skip = "GraphZen does not reject duplicate operation names.")]
    public void multiple_operations_of_same_name_mutation()
    {
        _ = """
            query Foo {
              fieldA
            }
            query Foo {
              fieldB
            }
            """;
        throw new NotImplementedException(
            "Expected error assertions for UniqueOperationNames rule need to be ported from graphql-js.");
    }

    [Fact(Skip = "GraphZen does not reject duplicate operation names.")]
    public void multiple_operations_of_same_name_subscription()
    {
        _ = """
            query Foo {
              fieldA
            }
            subscription Foo {
              fieldB
            }
            """;
        throw new NotImplementedException(
            "Expected error assertions for UniqueOperationNames rule need to be ported from graphql-js.");
    }
}
