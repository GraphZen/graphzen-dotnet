// Copyright (c) GraphZen LLC. All rights reserved.
// Licensed under the GraphZen Community License. See the LICENSE file in the project root for license information.

using GraphZen.SpecConformance.Tests.Infrastructure;
using static GraphZen.QueryEngine.Validation.QueryValidationRules;
using static GraphZen.SpecConformance.Tests.Infrastructure.SpecValidation;

namespace GraphZen.SpecConformance.Tests.Section5_Validation.Documents;

/// <seealso href="https://spec.graphql.org/draft/#sec-Executable-Definitions" />
[SpecSection("5.1.1", "Executable Definitions")]
public class ExecutableDefinitionsConformanceTests
{
    [Fact]
    public void with_only_operation() =>
        ExpectValid(ExecutableDefinitions, """
                                           query Foo {
                                             dog {
                                               name
                                             }
                                           }
                                           """);

    [Fact]
    public void with_operation_and_fragment() =>
        ExpectValid(ExecutableDefinitions, """
                                           query Foo {
                                             dog {
                                               name
                                               ...Frag
                                             }
                                           }

                                           fragment Frag on Dog {
                                             name
                                           }
                                           """);

    [Fact(Skip = "GraphZen does not reject non-executable definitions in query documents.")]
    public void with_type_definition() =>
        ExpectErrors(ExecutableDefinitions, """
                                            query Foo {
                                              dog {
                                                name
                                              }
                                            }

                                            type Cow {
                                              name: String
                                            }

                                            extend type Dog {
                                              color: String
                                            }
                                            """).ToDeepEqual(
            new ExpectedError("The \"Cow\" definition is not executable.", 7, 1),
            new ExpectedError("The \"Dog\" definition is not executable.", 11, 1));

    [Fact(Skip = "GraphZen does not reject non-executable definitions in query documents.")]
    public void with_schema_definition() =>
        ExpectErrors(ExecutableDefinitions, """
                                            schema {
                                              query: Query
                                            }

                                            type Query {
                                              test: String
                                            }

                                            extend schema @directive
                                            """).ToDeepEqual(
            new ExpectedError("The schema definition is not executable.", 1, 1),
            new ExpectedError("The \"Query\" definition is not executable.", 5, 1),
            new ExpectedError("The schema definition is not executable.", 9, 1));
}
