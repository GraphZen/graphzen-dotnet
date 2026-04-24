// Copyright (c) GraphZen LLC. All rights reserved.
// Licensed under the GraphZen Community License. See the LICENSE file in the project root for license information.

using GraphZen.SpecConformance.Tests.Infrastructure;
using static GraphZen.QueryEngine.Validation.QueryValidationRules;
using static GraphZen.SpecConformance.Tests.Infrastructure.SpecValidation;

namespace GraphZen.SpecConformance.Tests.Section5_Validation.Fragments;

/// <seealso href="https://spec.graphql.org/draft/#sec-Fragment-Spreads-Must-Not-Form-Cycles" />
[SpecSection("5.5.2.2", "Fragment Spreads Must Not Form Cycles")]
public class FragmentSpreadsMustNotFormCyclesConformanceTests
{
    [Fact]
    public void single_reference_is_valid() =>
        ExpectValid(NoFragmentCycles, """
                                      fragment fragA on Type {
                                        ...fragB
                                      }

                                      fragment fragB on Type {
                                        field
                                      }
                                      """);

    [Fact]
    public void spreading_twice_is_not_circular() =>
        ExpectValid(NoFragmentCycles, """
                                      fragment fragA on Type {
                                        ...fragB
                                        ...fragB
                                      }

                                      fragment fragB on Type {
                                        field
                                      }
                                      """);

    [Fact(Skip = "GraphZen does not reject cyclic fragment spreads.")]
    public void no_spreading_itself_directly()
    {
        _ = """
            fragment fragA on Type {
              ...fragA
            }
            """;
        throw new NotImplementedException(
            "Expected error assertions for NoFragmentCycles rule need to be ported from graphql-js.");
    }

    [Fact(Skip = "GraphZen does not reject cyclic fragment spreads.")]
    public void no_spreading_itself_indirectly()
    {
        _ = """
            fragment fragA on Type {
              ...fragB
            }

            fragment fragB on Type {
              ...fragA
            }
            """;
        throw new NotImplementedException(
            "Expected error assertions for NoFragmentCycles rule need to be ported from graphql-js.");
    }

    [Fact(Skip = "GraphZen does not reject cyclic fragment spreads.")]
    public void no_spreading_itself_deeply()
    {
        _ = """
            fragment fragA on Type {
              ...fragB
            }

            fragment fragB on Type {
              ...fragC
            }

            fragment fragC on Type {
              ...fragA
            }
            """;
        throw new NotImplementedException(
            "Expected error assertions for NoFragmentCycles rule need to be ported from graphql-js.");
    }
}
