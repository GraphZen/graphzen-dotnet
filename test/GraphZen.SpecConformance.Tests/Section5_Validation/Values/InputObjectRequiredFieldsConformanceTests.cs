// Copyright (c) GraphZen LLC. All rights reserved.
// Licensed under the GraphZen Community License. See the LICENSE file in the project root for license information.

using GraphZen.SpecConformance.Tests.Infrastructure;
using static GraphZen.QueryEngine.Validation.QueryValidationRules;
using static GraphZen.SpecConformance.Tests.Infrastructure.SpecValidation;

namespace GraphZen.SpecConformance.Tests.Section5_Validation.Values;

/// <seealso href="https://spec.graphql.org/draft/#sec-Input-Object-Required-Fields" />
[SpecSection("5.6.4", "Input Object Required Fields")]
public class InputObjectRequiredFieldsConformanceTests
{
    [Fact]
    public void all_required_fields_provided() =>
        ExpectValid(ProvidedRequiredArguments, """
                                               {
                                                 complicatedArgs {
                                                   complexArgField(complexArg: { requiredField: true })
                                                 }
                                               }
                                               """);

    [Fact]
    public void required_and_optional_fields_provided() =>
        ExpectValid(ProvidedRequiredArguments, """
                                               {
                                                 complicatedArgs {
                                                   complexArgField(complexArg: { requiredField: true, intField: 4 })
                                                 }
                                               }
                                               """);

    [Fact(Skip = "Error assertions need to be ported from graphql-js.")]
    public void missing_required_field()
    {
        _ = """
            {
              complicatedArgs {
                complexArgField(complexArg: { intField: 4 })
              }
            }
            """;
        throw new NotImplementedException(
            "Expected error assertions for ProvidedRequiredArguments rule need to be ported from graphql-js.");
    }

    [Fact(Skip = "Error assertions need to be ported from graphql-js.")]
    public void missing_required_field_with_null()
    {
        _ = """
            {
              complicatedArgs {
                complexArgField(complexArg: { requiredField: null, intField: 4 })
              }
            }
            """;
        throw new NotImplementedException(
            "Expected error assertions for ProvidedRequiredArguments rule need to be ported from graphql-js.");
    }
}
