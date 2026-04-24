// Copyright (c) GraphZen LLC. All rights reserved.
// Licensed under the GraphZen Community License. See the LICENSE file in the project root for license information.

using GraphZen.Infrastructure;
using GraphZen.LanguageModel.Validation;

namespace GraphZen.Tests.Validation.Rules;

[NoReorder]
public class ObjectsCanOnlyImplementUniqueInterfacesTests : ValidationRuleHarness
{
    public override ValidationRule RuleUnderTest { get; } =
        DocumentValidationRules.ObjectsCanOnlyImplementUniqueInterfaces;

    [Fact]
    public void RejectsAnObjectImplementingANonInterfaceType()
    {
        SdlShouldFail(@"
              type Query {
                test: BadObject
              }

              input SomeInputObject {
                field: String
              }

              type BadObject implements SomeInputObject {
                field: String
              }
            ",
            Error("Type BadObject must only implement Interface types, it cannot implement SomeInputObject.",
                (9, 27)));
    }

    [Fact]
    public void RejectsObjectImplementingSameInterfaceTwice()
    {
        SdlShouldFail(@"
              type Query {
                test: AnotherObject
              }

              interface AnotherInterface {
                field: String
              }

              type AnotherObject implements AnotherInterface & AnotherInterface {
                field: String
              }
            ", Error("Type AnotherObject can only implement AnotherInterface once.", (9, 31), (9, 50)));
    }

    [Fact]
    public void RejectsAnObjectImplementingTheSameInterfaceTwiceDueToAnExtension()
    {
        SdlShouldFail(@"
              type Query {
                test: AnotherObject
              }

              interface AnotherInterface {
                field: String
              }

              type AnotherObject implements AnotherInterface {
                field: String
              }

              extend type AnotherObject implements AnotherInterface
            ", Error("Type AnotherObject can only implement AnotherInterface once.", (9, 31), (13, 38)));
    }
}