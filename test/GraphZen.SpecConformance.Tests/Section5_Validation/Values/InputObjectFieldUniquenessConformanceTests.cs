// Copyright (c) GraphZen LLC. All rights reserved.
// Licensed under the GraphZen Community License. See the LICENSE file in the project root for license information.

using GraphZen.SpecConformance.Tests.Infrastructure;
using static GraphZen.QueryEngine.Validation.QueryValidationRules;
using static GraphZen.SpecConformance.Tests.Infrastructure.SpecValidation;

namespace GraphZen.SpecConformance.Tests.Section5_Validation.Values;

/// <seealso href="https://spec.graphql.org/draft/#sec-Input-Object-Field-Uniqueness" />
[SpecSection("5.6.3", "Input Object Field Uniqueness")]
public class InputObjectFieldUniquenessConformanceTests
{
    [Fact]
    public void multiple_input_object_fields() =>
        ExpectValid(UniqueInputFieldNames, """
                                           {
                                             field(arg: { f1: "value", f2: "value", f3: "value" })
                                           }
                                           """);

    [Fact]
    public void same_input_object_within_two_args() =>
        ExpectValid(UniqueInputFieldNames, """
                                           {
                                             field(arg1: { f: true }, arg2: { f: true })
                                           }
                                           """);

    [Fact(Skip = "GraphZen does not reject duplicate input object field names.")]
    public void duplicate_input_object_fields()
    {
        _ = """
            {
              field(arg: { f1: "value", f1: "value" })
            }
            """;
        throw new NotImplementedException(
            "Expected error assertions for UniqueInputFieldNames rule need to be ported from graphql-js.");
    }

    [Fact(Skip = "GraphZen does not reject duplicate input object field names.")]
    public void many_duplicate_input_object_fields()
    {
        _ = """
            {
              field(arg: { f1: "value", f1: "value", f1: "value" })
            }
            """;
        throw new NotImplementedException(
            "Expected error assertions for UniqueInputFieldNames rule need to be ported from graphql-js.");
    }

    [Fact(Skip = "GraphZen does not reject duplicate input object field names.")]
    public void nested_duplicate_input_object_fields()
    {
        _ = """
            {
              field(arg: { f1: { f2: "value", f2: "value" } })
            }
            """;
        throw new NotImplementedException(
            "Expected error assertions for UniqueInputFieldNames rule need to be ported from graphql-js.");
    }
}
