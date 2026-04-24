// Copyright (c) GraphZen LLC. All rights reserved.
// Licensed under the GraphZen Community License. See the LICENSE file in the project root for license information.

using GraphZen.SpecConformance.Tests.Infrastructure;
using static GraphZen.QueryEngine.Validation.QueryValidationRules;
using static GraphZen.SpecConformance.Tests.Infrastructure.SpecValidation;

namespace GraphZen.SpecConformance.Tests.Section5_Validation.Fields;

/// <seealso href="https://spec.graphql.org/draft/#sec-Leaf-Field-Selections" />
[SpecSection("5.3.3", "Leaf Field Selections")]
public class LeafFieldSelectionsConformanceTests
{
    [Fact]
    public void valid_scalar_selection() =>
        ExpectValid(ScalarLeafs, """
                                 fragment scalarSelection on Dog {
                                   barks
                                 }
                                 """);

    [Fact]
    public void valid_scalar_selection_with_args() =>
        ExpectValid(ScalarLeafs, """
                                 fragment scalarSelectionWithArgs on Dog {
                                   doesKnowCommand(dogCommand: SIT)
                                 }
                                 """);

    [Fact(Skip =
        "GraphZen does not reject sub-selections on scalar/enum types or missing selections on composite types.")]
    public void object_type_missing_selection()
    {
        _ = """
            query directQueryOnObjectWithoutSubFields {
              human
            }
            """;
        throw new NotImplementedException(
            "Expected error assertions for ScalarLeafs rule need to be ported from graphql-js.");
    }

    [Fact(Skip =
        "GraphZen does not reject sub-selections on scalar/enum types or missing selections on composite types.")]
    public void interface_type_missing_selection()
    {
        _ = """
            {
              human {
                pets
              }
            }
            """;
        throw new NotImplementedException(
            "Expected error assertions for ScalarLeafs rule need to be ported from graphql-js.");
    }

    [Fact(Skip =
        "GraphZen does not reject sub-selections on scalar/enum types or missing selections on composite types.")]
    public void scalar_selection_not_allowed_on_boolean()
    {
        _ = """
            fragment scalarSelectionsNotAllowedOnBoolean on Dog {
              barks {
                sinceWhen
              }
            }
            """;
        throw new NotImplementedException(
            "Expected error assertions for ScalarLeafs rule need to be ported from graphql-js.");
    }

    [Fact(Skip =
        "GraphZen does not reject sub-selections on scalar/enum types or missing selections on composite types.")]
    public void scalar_selection_not_allowed_on_enum()
    {
        _ = """
            fragment scalarSelectionsNotAllowedOnEnum on Cat {
              furColor {
                inHexDec
              }
            }
            """;
        throw new NotImplementedException(
            "Expected error assertions for ScalarLeafs rule need to be ported from graphql-js.");
    }

    [Fact(Skip =
        "GraphZen does not reject sub-selections on scalar/enum types or missing selections on composite types.")]
    public void scalar_selection_not_allowed_with_args()
    {
        _ = """
            fragment scalarSelectionsNotAllowedWithArgs on Dog {
              doesKnowCommand(dogCommand: SIT) {
                sinceWhen
              }
            }
            """;
        throw new NotImplementedException(
            "Expected error assertions for ScalarLeafs rule need to be ported from graphql-js.");
    }
}
