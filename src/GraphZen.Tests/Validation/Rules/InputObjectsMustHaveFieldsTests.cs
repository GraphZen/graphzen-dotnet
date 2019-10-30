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
    public class InputObjectsMustHaveFieldsTests : ValidationRuleHarness
    {
        public override ValidationRule RuleUnderTest { get; } = DocumentValidationRules.InputObjectsMustHaveFields;

        [Fact]
        public void ItAcceptsAnInputObjectTypeWithFields()
        {
            SDLShouldPass(@"
          type Query {
            field(arg: SomeInputObject): String
          }

          input SomeInputObject {
            field: String
          }
        ");
        }

        [Fact]
        public void RejectsAnInputObjectTypeWithMissingFields()
        {
            SDLShouldFail(@"
              type Query {
                field(arg: SomeInputObject): String
              }

              input SomeInputObject

              directive @test on INPUT_OBJECT

              extend input SomeInputObject @test
            ",
                Error("Input Object type SomeInputObject must define one or more fields.", (5, 7), (9, 14)));
        }

        [Fact]
        public void RejectsInputObjectWithIncorrectlyTypedFields()
        {
            SDLShouldFail(@"
              type Query {
                field(arg: SomeInputObject): String
              }

              type SomeObject {
                field: String
              }

              union SomeUnion = SomeObject

              input SomeInputObject {
                badObject: SomeObject
                badUnion: SomeUnion
                goodInputObject: SomeInputObject
              }
            ", Error("The type of SomeInputObject.badObject must be Input Type but got: SomeObject.", (12, 14)),
                Error("The type of SomeInputObject.badUnion must be Input Type but got: SomeUnion.", (13, 13)));
        }
    }
}