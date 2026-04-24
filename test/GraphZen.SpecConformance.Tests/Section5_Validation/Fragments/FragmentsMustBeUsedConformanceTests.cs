// Copyright (c) GraphZen LLC. All rights reserved.
// Licensed under the GraphZen Community License. See the LICENSE file in the project root for license information.

using GraphZen.SpecConformance.Tests.Infrastructure;
using static GraphZen.QueryEngine.Validation.QueryValidationRules;
using static GraphZen.SpecConformance.Tests.Infrastructure.SpecValidation;

namespace GraphZen.SpecConformance.Tests.Section5_Validation.Fragments;

/// <seealso href="https://spec.graphql.org/draft/#sec-Fragments-Must-Be-Used" />
[SpecSection("5.5.1.4", "Fragments Must Be Used")]
public class FragmentsMustBeUsedConformanceTests
{
    [Fact]
    public void all_fragment_names_are_used() =>
        ExpectValid(NoUnusedFragments, """
                                       {
                                         ...FragA
                                       }

                                       fragment FragA on Type {
                                         ...FragB
                                       }

                                       fragment FragB on Type {
                                         field
                                       }
                                       """);

    [Fact]
    public void unknown_fragments_are_ignored() =>
        ExpectValid(NoUnusedFragments, """
                                       {
                                         ...UnknownFragment
                                       }
                                       """);

    [Fact(Skip = "GraphZen does not reject unused fragment definitions.")]
    public void contains_unknown_fragments()
    {
        _ = """
            {
              ...FragA
            }

            fragment FragA on Type {
              field
            }

            fragment FragB on Type {
              field
            }
            """;
        throw new NotImplementedException(
            "Expected error assertions for NoUnusedFragments rule need to be ported from graphql-js.");
    }

    [Fact(Skip = "GraphZen does not reject unused fragment definitions.")]
    public void contains_unknown_and_undefined_fragments()
    {
        _ = """
            {
              ...FragA
            }

            fragment FragA on Type {
              ...FragB
            }

            fragment FragB on Type {
              field
            }

            fragment FragC on Type {
              field
            }
            """;
        throw new NotImplementedException(
            "Expected error assertions for NoUnusedFragments rule need to be ported from graphql-js.");
    }
}
