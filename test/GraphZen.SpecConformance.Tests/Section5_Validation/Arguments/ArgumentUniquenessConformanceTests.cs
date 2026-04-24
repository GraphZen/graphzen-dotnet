// Copyright (c) GraphZen LLC. All rights reserved.
// Licensed under the GraphZen Community License. See the LICENSE file in the project root for license information.

using GraphZen.SpecConformance.Tests.Infrastructure;
using static GraphZen.QueryEngine.Validation.QueryValidationRules;
using static GraphZen.SpecConformance.Tests.Infrastructure.SpecValidation;

namespace GraphZen.SpecConformance.Tests.Section5_Validation.Arguments;

/// <seealso href="https://spec.graphql.org/draft/#sec-Argument-Uniqueness" />
[SpecSection("5.4.2", "Argument Uniqueness")]
public class ArgumentUniquenessConformanceTests
{
    [Fact]
    public void argument_on_field() =>
        ExpectValid(UniqueArgumentNames, """
                                         {
                                           field(arg: "value")
                                         }
                                         """);

    [Fact]
    public void argument_on_directive() =>
        ExpectValid(UniqueArgumentNames, """
                                         {
                                           field @directive(arg: "value")
                                         }
                                         """);

    [Fact]
    public void multiple_field_arguments() =>
        ExpectValid(UniqueArgumentNames, """
                                         {
                                           field(arg1: "value", arg2: "value", arg3: "value")
                                         }
                                         """);

    [Fact(Skip = "GraphZen does not reject duplicate argument names.")]
    public void duplicate_field_arguments()
    {
        _ = """
            {
              field(arg1: "value", arg1: "value")
            }
            """;
        throw new NotImplementedException(
            "Expected error assertions for UniqueArgumentNames rule need to be ported from graphql-js.");
    }

    [Fact(Skip = "GraphZen does not reject duplicate argument names.")]
    public void many_duplicate_field_arguments()
    {
        _ = """
            {
              field(arg1: "value", arg1: "value", arg1: "value")
            }
            """;
        throw new NotImplementedException(
            "Expected error assertions for UniqueArgumentNames rule need to be ported from graphql-js.");
    }

    [Fact(Skip = "GraphZen does not reject duplicate argument names.")]
    public void duplicate_directive_arguments()
    {
        _ = """
            {
              field @directive(arg1: "value", arg1: "value")
            }
            """;
        throw new NotImplementedException(
            "Expected error assertions for UniqueArgumentNames rule need to be ported from graphql-js.");
    }
}
