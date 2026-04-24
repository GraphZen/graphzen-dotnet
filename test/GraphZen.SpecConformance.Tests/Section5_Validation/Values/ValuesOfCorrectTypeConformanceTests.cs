// Copyright (c) GraphZen LLC. All rights reserved.
// Licensed under the GraphZen Community License. See the LICENSE file in the project root for license information.

using GraphZen.SpecConformance.Tests.Infrastructure;
using static GraphZen.QueryEngine.Validation.QueryValidationRules;
using static GraphZen.SpecConformance.Tests.Infrastructure.SpecValidation;

namespace GraphZen.SpecConformance.Tests.Section5_Validation.Values;

/// <seealso href="https://spec.graphql.org/draft/#sec-Values-of-Correct-Type" />
[SpecSection("5.6.1", "Values of Correct Type")]
public class ValuesOfCorrectTypeConformanceTests
{
    [Fact]
    public void good_int_value() =>
        ExpectValid(ValuesOfCorrectType, """
                                         {
                                           complicatedArgs {
                                             intArgField(intArg: 2)
                                           }
                                         }
                                         """);

    [Fact]
    public void good_boolean_value() =>
        ExpectValid(ValuesOfCorrectType, """
                                         {
                                           complicatedArgs {
                                             booleanArgField(booleanArg: true)
                                           }
                                         }
                                         """);

    [Fact]
    public void good_string_value() =>
        ExpectValid(ValuesOfCorrectType, """
                                         {
                                           complicatedArgs {
                                             stringArgField(stringArg: "foo")
                                           }
                                         }
                                         """);

    [Fact]
    public void good_enum_value() =>
        ExpectValid(ValuesOfCorrectType, """
                                         {
                                           dog {
                                             doesKnowCommand(dogCommand: SIT)
                                           }
                                         }
                                         """);

    [Fact(Skip = "Error assertions need to be ported from graphql-js.")]
    public void string_into_int()
    {
        _ = """
            {
              complicatedArgs {
                intArgField(intArg: "3")
              }
            }
            """;
        throw new NotImplementedException(
            "Expected error assertions for ValuesOfCorrectType rule need to be ported from graphql-js.");
    }

    [Fact(Skip = "Error assertions need to be ported from graphql-js.")]
    public void float_into_int()
    {
        _ = """
            {
              complicatedArgs {
                intArgField(intArg: 3.333)
              }
            }
            """;
        throw new NotImplementedException(
            "Expected error assertions for ValuesOfCorrectType rule need to be ported from graphql-js.");
    }

    [Fact(Skip = "Error assertions need to be ported from graphql-js.")]
    public void int_into_string()
    {
        _ = """
            {
              complicatedArgs {
                stringArgField(stringArg: 1)
              }
            }
            """;
        throw new NotImplementedException(
            "Expected error assertions for ValuesOfCorrectType rule need to be ported from graphql-js.");
    }
}
