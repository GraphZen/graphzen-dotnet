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
    public class ObjectsMustAdhereToInterfaceTheyImplementTests : ValidationRuleHarness
    {
        public override ValidationRule RuleUnderTest { get; } =
            DocumentValidationRules.ObjectsMustAdhereToInterfaceTheyImplement;

        [Fact]
        public void AcceptsAnObjectWhichImplementsAnInterface()
        {
            SDLShouldPass(@"
              type Query {
                test: AnotherObject
              }

              interface AnotherInterface {
                field(input: String): String
              }

              type AnotherObject implements AnotherInterface {
                field(input: String): String
              }
            ");
        }

        [Fact]
        public void AcceptsAnObjectWhichImplmentsAnInterfacceAlongWithMoreFields()
        {
            SDLShouldPass(@"
              type Query {
                test: AnotherObject
              }

              interface AnotherInterface {
                field(input: String): String
              }

              type AnotherObject implements AnotherInterface {
                field(input: String): String
                anotherField: String
              }
            ");
        }

        [Fact]
        public void AcceptsAnObjectWhichImplementsAnInterfaceAlongWithAdditionalOptionalArguments()
        {
            SDLShouldPass(@"
              type Query {
                test: AnotherObject
              }

              interface AnotherInterface {
                field(input: String): String
              }

              type AnotherObject implements AnotherInterface {
                field(input: String, anotherInput: String): String
              }
            ");
        }

        [Fact]
        public void ItRejectsAnObjectMissingAnInterfaceField()
        {
            SDLShouldFail(@"
              type Query {
                test: AnotherObject
              }

              interface AnotherInterface {
                field(input: String): String
              }

              type AnotherObject implements AnotherInterface {
                anotherField: String
              }
            ",
                Error("Interface field AnotherInterface.field expected but AnotherObject does not provide it.", (6, 3),
                    (9, 6)));
        }

        [Fact]
        public void RejectsAnObjectWithAnIncorrectlyTypedInterfaceField()
        {
            SDLShouldFail(@"
              type Query {
                test: AnotherObject
              }

              interface AnotherInterface {
                field(input: String): String
              }

              type AnotherObject implements AnotherInterface {
                field(input: String): Int
              }
            ",
                Error("Interface field AnotherInterface.field expects type String but AnotherObject.field is type Int.",
                    (10, 25), (6, 25)));
        }

        [Fact]
        public void RejectsAnObjectWithADifferentlyTypeInterfaceField()
        {
            SDLShouldFail(@"
              type Query {
                test: AnotherObject
              }

              type A { foo: String }
              type B { foo: String }

              interface AnotherInterface {
                field: A
              }

              type AnotherObject implements AnotherInterface {
                field: B
              }
            ",
                Error("Interface field AnotherInterface.field expects type A but AnotherObject.field is type B.",
                    (13, 10),
                    (9, 10)));
        }

        [Fact]
        public void AcceptsAnObjectWithSubTypedInterfaceField_Interface()
        {
            SDLShouldPass(@"
              type Query {
                test: AnotherObject
              }

              interface AnotherInterface {
                field: AnotherInterface
              }

              type AnotherObject implements AnotherInterface {
                field: AnotherObject
              }
            ");
        }

        [Fact]
        public void AcceptsAnObjectWithSubTypedInterfaceField_Union()
        {
            SDLShouldPass(@"
              type Query {
                test: AnotherObject
              }

              type SomeObject {
                field: String
              }

              union SomeUnionType = SomeObject

              interface AnotherInterface {
                field: SomeUnionType
              }

              type AnotherObject implements AnotherInterface {
                field: SomeObject
              }
            ");
        }

        [Fact]
        public void RejectsAnObjectMissingAnInterfaceArgument()
        {
            SDLShouldFail(@"
              type Query {
                test: AnotherObject
              }

              interface AnotherInterface {
                field(input: String): String
              }

              type AnotherObject implements AnotherInterface {
                field: String
              }
            ",
                Error(
                    "Interface field argument AnotherInterface.field(input:) expected but AnotherObject.field does not provide it.",
                    (6, 9), (10, 3)));
        }

        [Fact]
        public void RejectsAnObjectWithAnIncorrectlyTypedInterfaceArgument()
        {
            SDLShouldFail(@"
              type Query {
                test: AnotherObject
              }

              interface AnotherInterface {
                field(input: String): String
              }

              type AnotherObject implements AnotherInterface {
                field(input: Int): String
              }
            ",
                Error(
                    "Interface field argument AnotherInterface.field(input:) expects type String but AnotherObject.field(input:) is type Int.",
                    (6, 16), (10, 16)));
        }

        [Fact]
        public void RejectsAnObjectWithBothAnIncorrectlyTypedFieldAndArgument()
        {
            SDLShouldFail(@"
              type Query {
                test: AnotherObject
              }

              interface AnotherInterface {
                field(input: String): String
              }

              type AnotherObject implements AnotherInterface {
                field(input: Int): Int
              }
            ", Error("Interface field AnotherInterface.field expects type String but AnotherObject.field is type Int.",
                    (10, 22), (6, 25))
                , Error("Interface field argument AnotherInterface.field(input:) expects type String but AnotherObject.field(input:) is type Int.", (6, 16), (10, 16)));
        }

        [Fact]
        public void RejectsAnObjectWhichIMplementsAnInterfaceFieldAlongWithAdditionalRequiredArguments()
        {
            SDLShouldFail(@"
              type Query {
                test: AnotherObject
              }

              interface AnotherInterface {
                field(baseArg: String): String
              }

              type AnotherObject implements AnotherInterface {
                field(
                  baseArg: String,
                  requiredArg: String!
                  optionalArg1: String,
                  optionalArg2: String = """",
                ): String
              }
            ",
                Error(
                    "Object field AnotherObject.field includes required argument requiredArg that is missing from the Interface field AnotherInterface.field.",
                    (12, 5), (6, 3)));
        }

        [Fact]
        public void AcceptsAnObjectWithAnEquivalentlyWrappedInterfaceFieldType()
        {
            SDLShouldPass(@"
              type Query {
                test: AnotherObject
              }

              interface AnotherInterface {
                field: [String]!
              }

              type AnotherObject implements AnotherInterface {
                field: [String]!
              }
           ");
        }

        [Fact]
        public void RejectsAnObjectWithNonListInterfaceFieldListType()
        {
            SDLShouldFail(@"
              type Query {
                test: AnotherObject
              }

              interface AnotherInterface {
                field: [String]
              }

              type AnotherObject implements AnotherInterface {
                field: String
              }",
                Error(
                    "Interface field AnotherInterface.field expects type [String] but AnotherObject.field is type String.",
                    (10, 10), (6, 10)));
        }

        [Fact]
        public void RejectsAnObjectWithAListInterfaceFieldNonListType()
        {
            SDLShouldFail(@"
              type Query {
                test: AnotherObject
              }

              interface AnotherInterface {
                field: String
              }

              type AnotherObject implements AnotherInterface {
                field: [String]
              }",
                Error(
                    "Interface field AnotherInterface.field expects type String but AnotherObject.field is type [String].",
                    (10, 10), (6, 10)));
        }

        [Fact]
        public void AcceptsAnObjectWithASubsetNonNullInterfaceFieldType()
        {
            SDLShouldPass(@"
              type Query {
                test: AnotherObject
              }

              interface AnotherInterface {
                field: String
              }

              type AnotherObject implements AnotherInterface {
                field: String!
              }
            ");
        }

        [Fact]
        public void RejectsAnObjectWithASupersetNullableInterfaceFieldType()
        {
            SDLShouldFail(@"
              type Query {
                test: AnotherObject
              }

              interface AnotherInterface {
                field: String!
              }

              type AnotherObject implements AnotherInterface {
                field: String
              }
            ",
                Error(
                    "Interface field AnotherInterface.field expects type String! but AnotherObject.field is type String.",
                    (10, 10), (6, 10)));
        }
    }
}