// Copyright (c) GraphZen LLC. All rights reserved.
// Licensed under the GraphZen Community License. See the LICENSE file in the project root for license information.

using GraphZen.SpecConformance.Tests.Infrastructure;
using static GraphZen.QueryEngine.Validation.QueryValidationRules;
using static GraphZen.SpecConformance.Tests.Infrastructure.SpecValidation;

namespace GraphZen.SpecConformance.Tests.Section5_Validation.Arguments;

/// <seealso href="https://spec.graphql.org/draft/#sec-Required-Arguments" />
[SpecSection("5.4.3", "Required Arguments")]
public class RequiredArgumentsConformanceTests
{
    [Fact]
    public void unknown_arguments_are_ignored() =>
        ExpectValid(ProvidedRequiredArguments, """
                                               fragment ignoresUnknownArguments on Dog {
                                                 isHouseTrained(unknownArgument: true)
                                               }
                                               """);

    [Fact]
    public void no_arg_on_optional_arg() =>
        ExpectValid(ProvidedRequiredArguments, """
                                               fragment noArgOnOptionalArg on Dog {
                                                 isHouseTrained
                                               }
                                               """);

    [Fact]
    public void no_arg_on_non_null_field_with_default() =>
        ExpectValid(ProvidedRequiredArguments, """
                                               {
                                                 complicatedArgs {
                                                   nonNullFieldWithDefault
                                                 }
                                               }
                                               """);

    [Fact]
    public void multiple_required_args() =>
        ExpectValid(ProvidedRequiredArguments, """
                                               {
                                                 complicatedArgs {
                                                   multipleReqs(req1: 1, req2: 2)
                                                 }
                                               }
                                               """);

    [Fact]
    public void directive_with_required_arg() =>
        ExpectValid(ProvidedRequiredArguments, """
                                               {
                                                 cat @include(if: true) {
                                                   nickname
                                                 }
                                               }
                                               """);

    [Fact(Skip = "GraphZen does not reject missing required arguments.")]
    public void missing_one_non_nullable_argument()
    {
        _ = """
            {
              complicatedArgs {
                multipleReqs(req2: 2)
              }
            }
            """;
        throw new NotImplementedException(
            "Expected error assertions for ProvidedRequiredArguments rule need to be ported from graphql-js.");
    }

    [Fact(Skip = "GraphZen does not reject missing required arguments.")]
    public void missing_multiple_non_nullable_arguments()
    {
        _ = """
            {
              complicatedArgs {
                multipleReqs
              }
            }
            """;
        throw new NotImplementedException(
            "Expected error assertions for ProvidedRequiredArguments rule need to be ported from graphql-js.");
    }

    [Fact(Skip = "GraphZen does not reject missing required arguments.")]
    public void directive_with_missing_required_arg()
    {
        _ = """
            {
              cat @include {
                nickname @skip
              }
            }
            """;
        throw new NotImplementedException(
            "Expected error assertions for ProvidedRequiredArguments rule need to be ported from graphql-js.");
    }
}
