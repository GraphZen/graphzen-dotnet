// Copyright (c) GraphZen LLC. All rights reserved.
// Licensed under the GraphZen Community License. See the LICENSE file in the project root for license information.

using GraphZen.SpecConformance.Tests.Infrastructure;
using static GraphZen.QueryEngine.Validation.QueryValidationRules;
using static GraphZen.SpecConformance.Tests.Infrastructure.SpecValidation;

namespace GraphZen.SpecConformance.Tests.Section5_Validation.Values;

/// <seealso href="https://spec.graphql.org/draft/#sec-Input-Object-Field-Names" />
[SpecSection("5.6.2", "Input Object Field Names")]
public class InputObjectFieldNamesConformanceTests
{
    [Fact]
    public void known_input_object_fields() =>
        ExpectValid(ValuesOfCorrectType, """
                                         {
                                           complicatedArgs {
                                             complexArgField(complexArg: { requiredField: true, intField: 4 })
                                           }
                                         }
                                         """);

    [Fact]
    public void partial_object_only_required() =>
        ExpectValid(ValuesOfCorrectType, """
                                         {
                                           complicatedArgs {
                                             complexArgField(complexArg: { requiredField: true })
                                           }
                                         }
                                         """);

    [Fact(Skip = "Error assertions need to be ported from graphql-js.")]
    public void unknown_input_object_field()
    {
        _ = """
            {
              complicatedArgs {
                complexArgField(complexArg: {
                  requiredField: true,
                  invalidField: "value"
                })
              }
            }
            """;
        throw new NotImplementedException(
            "Expected error assertions for ValuesOfCorrectType rule need to be ported from graphql-js.");
    }

    [Fact(Skip = "Error assertions need to be ported from graphql-js.")]
    public void misspelled_input_object_field()
    {
        _ = """
            {
              complicatedArgs {
                complexArgField(complexArg: {
                  requiredField: true,
                  intFeild: 4
                })
              }
            }
            """;
        throw new NotImplementedException(
            "Expected error assertions for ValuesOfCorrectType rule need to be ported from graphql-js.");
    }
}
