// Copyright (c) GraphZen LLC. All rights reserved.
// Licensed under the GraphZen Community License. See the LICENSE file in the project root for license information.

using GraphZen.SpecConformance.Tests.Infrastructure;
using static GraphZen.QueryEngine.Validation.QueryValidationRules;
using static GraphZen.SpecConformance.Tests.Infrastructure.SpecValidation;

namespace GraphZen.SpecConformance.Tests.Section5_Validation.Fragments;

/// <seealso href="https://spec.graphql.org/draft/#sec-Fragment-Spread-Target-Defined" />
[SpecSection("5.5.2.1", "Fragment Spread Target Defined")]
public class FragmentSpreadTargetDefinedConformanceTests
{
    [Fact]
    public void known_fragment_names_are_valid() =>
        ExpectValid(KnownFragmentNames, """
                                        {
                                          human(id: 4) {
                                            ...HumanFields1
                                            ... on Human {
                                              ...HumanFields2
                                            }
                                            ... {
                                              name
                                            }
                                          }
                                        }
                                        fragment HumanFields1 on Human {
                                          name
                                          ...HumanFields3
                                        }
                                        fragment HumanFields2 on Human {
                                          name
                                        }
                                        fragment HumanFields3 on Human {
                                          name
                                        }
                                        """);

    [Fact(Skip = "GraphZen does not reject spreads targeting undefined fragments.")]
    public void unknown_fragment_names()
    {
        _ = """
            {
              human(id: 4) {
                ...UnknownFragment1
                ... on Human {
                  ...UnknownFragment2
                }
              }
            }

            fragment HumanFields on Human {
              name
              ...UnknownFragment3
            }
            """;
        throw new NotImplementedException(
            "Expected error assertions for KnownFragmentNames rule need to be ported from graphql-js.");
    }
}
