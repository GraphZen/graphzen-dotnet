// Copyright (c) GraphZen LLC. All rights reserved.
// Licensed under the GraphZen Community License. See the LICENSE file in the project root for license information.

using GraphZen.SpecConformance.Tests.Infrastructure;
using static GraphZen.QueryEngine.Validation.QueryValidationRules;
using static GraphZen.SpecConformance.Tests.Infrastructure.SpecValidation;

namespace GraphZen.SpecConformance.Tests.Section5_Validation.Fragments;

/// <seealso href="https://spec.graphql.org/draft/#sec-Fragment-Spread-Is-Possible" />
[SpecSection("5.5.2.3", "Fragment Spread Is Possible")]
public class FragmentSpreadIsPossibleConformanceTests
{
    [Fact]
    public void object_into_same_object() =>
        ExpectValid(PossibleFragmentSpreads, """
                                             fragment objectWithinObject on Dog { ...dogFragment }
                                             fragment dogFragment on Dog { barkVolume }
                                             """);

    [Fact]
    public void object_into_implemented_interface() =>
        ExpectValid(PossibleFragmentSpreads, """
                                             fragment objectWithinInterface on Pet { ...dogFragment }
                                             fragment dogFragment on Dog { barkVolume }
                                             """);

    [Fact]
    public void object_into_containing_union() =>
        ExpectValid(PossibleFragmentSpreads, """
                                             fragment objectWithinUnion on CatOrDog { ...dogFragment }
                                             fragment dogFragment on Dog { barkVolume }
                                             """);

    [Fact]
    public void union_into_contained_object() =>
        ExpectValid(PossibleFragmentSpreads, """
                                             fragment unionWithinObject on Dog { ...catOrDogFragment }
                                             fragment catOrDogFragment on CatOrDog { __typename }
                                             """);

    [Fact]
    public void interface_into_implementing_object() =>
        ExpectValid(PossibleFragmentSpreads, """
                                             fragment interfaceWithinObject on Dog { ...petFragment }
                                             fragment petFragment on Pet { name }
                                             """);

    [Fact(Skip = "Error assertions need to be ported from graphql-js.")]
    public void different_object_into_object()
    {
        _ = """
            fragment invalidObjectWithinObject on Cat { ...dogFragment }
            fragment dogFragment on Dog { barkVolume }
            """;
        throw new NotImplementedException(
            "Expected error assertions for PossibleFragmentSpreads rule need to be ported from graphql-js.");
    }

    [Fact(Skip = "Error assertions need to be ported from graphql-js.")]
    public void object_not_implementing_interface()
    {
        _ = """
            fragment invalidObjectWithinInterface on Pet { ...humanFragment }
            fragment humanFragment on Human { pets { name } }
            """;
        throw new NotImplementedException(
            "Expected error assertions for PossibleFragmentSpreads rule need to be ported from graphql-js.");
    }

    [Fact(Skip = "Error assertions need to be ported from graphql-js.")]
    public void object_not_in_union()
    {
        _ = """
            fragment invalidObjectWithinUnion on CatOrDog { ...humanFragment }
            fragment humanFragment on Human { pets { name } }
            """;
        throw new NotImplementedException(
            "Expected error assertions for PossibleFragmentSpreads rule need to be ported from graphql-js.");
    }
}
