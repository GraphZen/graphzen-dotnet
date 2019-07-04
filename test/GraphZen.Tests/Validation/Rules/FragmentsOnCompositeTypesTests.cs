// Copyright (c) GraphZen LLC. All rights reserved.
// Licensed under the GraphZen Community License. See the LICENSE file in the project root for license information.

using GraphZen.Infrastructure;
using GraphZen.QueryEngine.Validation;
using Xunit;
using static GraphZen.Validation.Rules.FragmentsOnCompositeTypes;

namespace GraphZen.Validation.Rules
{
    [NoReorder]
    public class FragmentsOnCompositeTypesTests : ValidationRuleHarness
    {
        public override ValidationRule RuleUnderTest { get; } = QueryValidationRules.FragmentsOnCompositeTypes;

        [Fact]
        public void ObjectIsValidFragmentType() => QueryShouldPass(@"

          fragment validFragment on Dog {
            barks
          }

        ");

        [Fact]
        public void InterfaceIsValidFragmentType() => QueryShouldPass(@"

          fragment validFragment on Pet {
            name
          }

        ");

        [Fact]
        public void ObjectIsValidInlineFragmentType() => QueryShouldPass(@"

          fragment validFragment on Pet {
            ... on Dog {
              barks
            }
          }

        ");


        [Fact]
        public void InlineFragmentWithoutTypeIsValid() => QueryShouldPass(@"

          fragment validFragment on Pet {
            ... {
              name
            }
          }

        ");

        [Fact]
        public void UnionIsValidFragmentType() => QueryShouldPass(@"

          fragment validFragment on CatOrDog {
            __typename
          }

        ");

        private static ExpectedError Error(string fragmentName, string typeName, int line, int column) => Error(
            FragmentOnNonCompositeErrorMessage(fragmentName, typeName), (line, column));


        [Fact]
        public void ScalarIsInvalidFragmentType() => QueryShouldFail(@"

          fragment scalarFragment on Boolean {
            bad
          }

        ", Error("scalarFragment", "Boolean", 3, 38));

        [Fact]
        public void EnumIsInvalidFragmentType() => QueryShouldFail(@"

          fragment scalarFragment on FurColor {
            bad
          }

        ", Error("scalarFragment", "FurColor", 3, 38));

        [Fact]
        public void InputObjectIsInvalidInlineFragmentType() => QueryShouldFail(@"

          fragment inputFragment on ComplexInput {
            stringField
          }

        ", Error("inputFragment", "ComplexInput", 3, 37));

        [Fact]
        public void ScalarIsInvalidInlineFragmentType() => QueryShouldFail(@"

          fragment invalidFragment on Pet {
            ... on String {
              barks
            }
          }

        ", Error(InlineFragmentOnNonCompositeErrorMessage("String"), (4, 20)));
    }
}