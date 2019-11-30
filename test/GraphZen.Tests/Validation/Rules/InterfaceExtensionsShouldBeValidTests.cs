// Copyright (c) GraphZen LLC. All rights reserved.
// Licensed under the GraphZen Community License. See the LICENSE file in the project root for license information.

using System.Diagnostics.CodeAnalysis;
using GraphZen.Infrastructure;
using GraphZen.LanguageModel.Validation;
using JetBrains.Annotations;
using Xunit;

#nullable disable


namespace GraphZen.Validation.Rules
{
    [NoReorder]
    public class InterfaceExtensionsShouldBeValidTests : ValidationRuleHarness
    {
        public override ValidationRule RuleUnderTest { get; } =
            DocumentValidationRules.InterfaceExtensionsShouldBeValid;

        [Fact]
        public void RejectsAnObjectImplementingTheExtendedInterfaceDueToMissingField()
        {
            SDLShouldFail(@"
              type Query {
                test: AnotherObject
              }

              interface AnotherInterface {
                field: String
              }

              type AnotherObject implements AnotherInterface {
                field: String
              }
              extend interface AnotherInterface {
                newField: String
              }

              extend type AnotherObject {
                differentNewField: String
              }
            ",
                Error("Interface field AnotherInterface.newField expected but AnotherObject does not provide it.",
                    (9, 6),
                    (13, 3), (16, 13)));
        }

        [Fact]
        public void RejectsAnObjectImplementedTheExtendedInterfaceDueToMissingFieldArgs()
        {
            SDLShouldFail(@"
              type Query {
                test: AnotherObject
              }

              interface AnotherInterface {
                field: String
              }

              type AnotherObject implements AnotherInterface {
                field: String
              }
                 
              extend interface AnotherInterface {
                newField(test: Boolean): String
              }

              extend type AnotherObject {
                newField: String
              }
            ",
                Error(
                    "Interface field argument AnotherInterface.newField(test:) expected but AnotherObject.newField does not provide it.",
                    (14, 12), (18, 3)));
        }

        [Fact]
        public void RejectsObjectsImplementingTheExtendedInterfaceDueToMismatchingInterfaceType()
        {
            SDLShouldFail(@"
              type Query {
                test: AnotherObject
              }

              interface AnotherInterface {
                field: String
              }

              type AnotherObject implements AnotherInterface {
                field: String
              }

              extend interface AnotherInterface {
                newInterfaceField: NewInterface
              }

              interface NewInterface {
                newField: String
              }

              interface MismatchingInterface {
                newField: String
              }

              extend type AnotherObject {
                newInterfaceField: MismatchingInterface
              }

              # Required to prevent unused interface errors
              type DummyObject implements NewInterface & MismatchingInterface {
                newField: String
              }
            ",
                Error(
                    "Interface field AnotherInterface.newInterfaceField expects type NewInterface but AnotherObject.newInterfaceField is type MismatchingInterface.",
                    (14, 22), (26, 22)));
        }
    }
}