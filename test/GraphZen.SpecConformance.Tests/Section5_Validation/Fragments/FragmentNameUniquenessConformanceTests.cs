// Copyright (c) GraphZen LLC. All rights reserved.
// Licensed under the GraphZen Community License. See the LICENSE file in the project root for license information.

using GraphZen.SpecConformance.Tests.Infrastructure;
using static GraphZen.QueryEngine.Validation.QueryValidationRules;
using static GraphZen.SpecConformance.Tests.Infrastructure.SpecValidation;

namespace GraphZen.SpecConformance.Tests.Section5_Validation.Fragments;

/// <seealso href="https://spec.graphql.org/draft/#sec-Fragment-Name-Uniqueness" />
[SpecSection("5.5.1.1", "Fragment Name Uniqueness")]
public class FragmentNameUniquenessConformanceTests
{
    [Fact]
    public void many_fragments() =>
        ExpectValid(UniqueFragmentNames, """
                                         {
                                           dogOrHuman {
                                             __typename
                                           }
                                         }

                                         fragment one on Dog {
                                           name
                                         }

                                         fragment two on Cat {
                                           name
                                         }
                                         """);

    [Fact]
    public void fragment_and_operation_named_the_same() =>
        ExpectValid(UniqueFragmentNames, """
                                         query dog {
                                           cat {
                                             name
                                           }
                                         }

                                         fragment dog on Dog {
                                           name
                                         }
                                         """);

    [Fact(Skip = "GraphZen does not reject duplicate fragment names.")]
    public void fragments_named_the_same()
    {
        _ = """
            fragment fragmentOne on Dog {
              name
            }

            fragment fragmentOne on Cat {
              name
            }
            """;
        throw new NotImplementedException(
            "Expected error assertions for UniqueFragmentNames rule need to be ported from graphql-js.");
    }

    [Fact(Skip = "GraphZen does not reject duplicate fragment names.")]
    public void duplicate_fragment_name_without_reference()
    {
        _ = """
            fragment fragmentOne on Dog {
              name
            }

            fragment fragmentOne on Cat {
              name
            }
            """;
        throw new NotImplementedException(
            "Expected error assertions for UniqueFragmentNames rule need to be ported from graphql-js.");
    }
}
