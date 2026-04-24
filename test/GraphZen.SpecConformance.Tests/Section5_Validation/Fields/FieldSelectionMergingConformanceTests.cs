// Copyright (c) GraphZen LLC. All rights reserved.
// Licensed under the GraphZen Community License. See the LICENSE file in the project root for license information.

using GraphZen.SpecConformance.Tests.Infrastructure;
using static GraphZen.QueryEngine.Validation.QueryValidationRules;
using static GraphZen.SpecConformance.Tests.Infrastructure.SpecValidation;

namespace GraphZen.SpecConformance.Tests.Section5_Validation.Fields;

/// <seealso href="https://spec.graphql.org/draft/#sec-Field-Selection-Merging" />
[SpecSection("5.3.2", "Field Selection Merging")]
public class FieldSelectionMergingConformanceTests
{
    [Fact]
    public void unique_fields() =>
        ExpectValid(OverlappingFieldsCanBeMerged, """
                                                  fragment uniqueFields on Dog {
                                                    name
                                                    nickname
                                                  }
                                                  """);

    [Fact]
    public void identical_fields() =>
        ExpectValid(OverlappingFieldsCanBeMerged, """
                                                  fragment mergeIdenticalFields on Dog {
                                                    name
                                                    name
                                                  }
                                                  """);

    [Fact]
    public void identical_fields_with_identical_args() =>
        ExpectValid(OverlappingFieldsCanBeMerged, """
                                                  fragment mergeIdenticalFieldsWithIdenticalArgs on Dog {
                                                    doesKnowCommand(dogCommand: SIT)
                                                    doesKnowCommand(dogCommand: SIT)
                                                  }
                                                  """);

    [Fact]
    public void different_args_with_different_aliases() =>
        ExpectValid(OverlappingFieldsCanBeMerged, """
                                                  fragment differentArgsWithDifferentAliases on Dog {
                                                    knowsSit: doesKnowCommand(dogCommand: SIT)
                                                    knowsDown: doesKnowCommand(dogCommand: DOWN)
                                                  }
                                                  """);

    [Fact(Skip = "Error assertions need to be ported from graphql-js.")]
    public void same_aliases_with_different_field_targets()
    {
        _ = """
            fragment sameAliasesWithDifferentFieldTargets on Dog {
              fido: name
              fido: nickname
            }
            """;
        throw new NotImplementedException(
            "Expected error assertions for OverlappingFieldsCanBeMerged rule need to be ported from graphql-js.");
    }

    [Fact(Skip = "Error assertions need to be ported from graphql-js.")]
    public void conflicting_args()
    {
        _ = """
            fragment conflictingArgs on Dog {
              doesKnowCommand(dogCommand: SIT)
              doesKnowCommand(dogCommand: HEEL)
            }
            """;
        throw new NotImplementedException(
            "Expected error assertions for OverlappingFieldsCanBeMerged rule need to be ported from graphql-js.");
    }

    [Fact(Skip = "Error assertions need to be ported from graphql-js.")]
    public void deep_conflict()
    {
        _ = """
            {
              field {
                x: a
              }
              field {
                x: b
              }
            }
            """;
        throw new NotImplementedException(
            "Expected error assertions for OverlappingFieldsCanBeMerged rule need to be ported from graphql-js.");
    }
}
