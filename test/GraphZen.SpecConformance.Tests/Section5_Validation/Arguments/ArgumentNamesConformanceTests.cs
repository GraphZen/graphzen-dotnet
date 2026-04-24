// Copyright (c) GraphZen LLC. All rights reserved.
// Licensed under the GraphZen Community License. See the LICENSE file in the project root for license information.

using GraphZen.SpecConformance.Tests.Infrastructure;
using static GraphZen.QueryEngine.Validation.QueryValidationRules;
using static GraphZen.SpecConformance.Tests.Infrastructure.SpecValidation;

namespace GraphZen.SpecConformance.Tests.Section5_Validation.Arguments;

/// <seealso href="https://spec.graphql.org/draft/#sec-Argument-Names" />
[SpecSection("5.4.1", "Argument Names")]
public class ArgumentNamesConformanceTests
{
    [Fact]
    public void single_arg_is_known() =>
        ExpectValid(KnownArgumentNames, """
                                        fragment argOnRequiredArg on Dog {
                                          doesKnowCommand(dogCommand: SIT)
                                        }
                                        """);

    [Fact]
    public void multiple_args_are_known() =>
        ExpectValid(KnownArgumentNames, """
                                        fragment multipleArgs on ComplicatedArgs {
                                          multipleReqs(req1: 1, req2: 2)
                                        }
                                        """);

    [Fact]
    public void directive_args_are_known() =>
        ExpectValid(KnownArgumentNames, """
                                        {
                                          cat @skip(if: true) {
                                            nickname
                                          }
                                        }
                                        """);

    [Fact(Skip = "GraphZen does not reject unknown argument names on fields and directives.")]
    public void directive_without_args_reports_unknown_arg()
    {
        _ = """
            {
              cat @onField(if: true) {
                nickname
              }
            }
            """;
        throw new NotImplementedException(
            "Expected error assertions for KnownArgumentNames rule need to be ported from graphql-js.");
    }

    [Fact(Skip = "GraphZen does not reject unknown argument names on fields and directives.")]
    public void invalid_field_argument_name()
    {
        _ = """
            fragment invalidArgName on Dog {
              doesKnowCommand(unknown: true)
            }
            """;
        throw new NotImplementedException(
            "Expected error assertions for KnownArgumentNames rule need to be ported from graphql-js.");
    }

    [Fact(Skip = "GraphZen does not reject unknown argument names on fields and directives.")]
    public void unknown_args_amongst_known_args()
    {
        _ = """
            fragment oneGoodArgOneInvalidArg on Dog {
              doesKnowCommand(whoKnows: 1, dogCommand: SIT, unknown: true)
            }
            """;
        throw new NotImplementedException(
            "Expected error assertions for KnownArgumentNames rule need to be ported from graphql-js.");
    }
}
