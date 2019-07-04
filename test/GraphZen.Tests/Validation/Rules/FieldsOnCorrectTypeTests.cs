// Copyright (c) GraphZen LLC. All rights reserved.
// Licensed under the GraphZen Community License. See the LICENSE file in the project root for license information.

using FluentAssertions;
using GraphZen.Infrastructure;
using GraphZen.QueryEngine.Validation;
using Xunit;
using static GraphZen.Validation.Rules.FieldsOnCorrectType;

namespace GraphZen.Validation.Rules
{
    [NoReorder]
    public class FieldsOnCorrectTypeTests : ValidationRuleHarness
    {
        public override ValidationRule RuleUnderTest { get; } = QueryValidationRules.FieldsOnCorrectType;

        [Fact]
        public void ObjectFieldSelection() => QueryShouldPass(@"

          fragment objectFieldSelection on Dog {
            __typename
            name
          }

        ");

        [Fact]
        public void AliasedObjectFieldSelection() => QueryShouldPass(@"

          fragment aliasedObjectFieldSelection on Dog {
            tn : __typename
            otherName : name
          }

        ");

        [Fact]
        public void InterfaceFieldSelection() => QueryShouldPass(@"

          fragment interfaceFieldSelection on Pet {
            __typename
            name
          }
        
        ");

        [Fact]
        public void AliasedInterfaceFieldSelection() => QueryShouldPass(@"
          fragment interfaceFieldSelection on Pet {
            otherName : name
          }
        ");

        [Fact]
        public void LyingAliasSelection() => QueryShouldPass(@"

          fragment lyingAliasSelection on Dog {
            name : nickname
          }

        ");

        [Fact]
        public void IgnoresFieldsOnUnknownType() => QueryShouldPass(@"

          fragment unknownSelection on UnknownType {
            unknownField
          }

        ");


        private static ExpectedError UndefinedField(string fieldName, string type, string[] suggestedTypeNames,
            string[] suggestedFieldNames, int line, int column) => Error(
            UndefinedFieldMessage(fieldName, type, suggestedTypeNames ?? new string[] { },
                suggestedFieldNames ?? new string[] { }), (line, column));

        [Fact]
        public void ReportsErrorsWhenTypeIsKnownAgain() => QueryShouldFail(@"

          fragment typeKnownAgain on Pet {
            unknown_pet_field {
              ... on Cat {
                unknown_cat_field
              }
            }
          } 

        ",
            UndefinedField("unknown_pet_field", "Pet", null, null, 4, 13),
            UndefinedField("unknown_cat_field", "Cat", null, null, 6, 17)
        );

        [Fact]
        public void IgnoresDeeplyUnknownField() => QueryShouldFail(@"

          fragment deepFieldNotDefined on Dog {
            unknown_field {
              deeper_unknown_field
            }
          }

        ", UndefinedField("unknown_field", "Dog", null, null, 4, 13));

        [Fact]
        public void SubFieldNotDefined() => QueryShouldFail(@"

          fragment subFieldNotDefined on Human {
            pets {
              unknown_field
            }
          }

        ",
            UndefinedField("unknown_field", "Pet", null, null, 5, 15));

        [Fact]
        public void FieldNotDefinedOnInlineFragment() => QueryShouldFail(@"

          fragment fieldNotDefined on Pet {
            ... on Dog {
              meowVolume
            }
          }

        ",
            UndefinedField("meowVolume", "Dog", null, new[] {"barkVolume"}, 5, 15));

        [Fact]
        public void AliasedFieldTargetNotDefined() => QueryShouldFail(@"

          fragment aliasedFieldTargetNotDefined on Dog {
            volume : mooVolume
          }

        ", UndefinedField("mooVolume", "Dog", null, new[] {"barkVolume"}, 4, 13));


        [Fact]
        public void NotDefinedOnInterface() => QueryShouldFail(@"

          fragment notDefinedOnInterface on Pet {
            tailLength
          }

        ", UndefinedField("tailLength", "Pet", null, null, 4, 13));


        [Fact]
        public void DefinedOnImplementorsButNotOnInterface() => QueryShouldFail(@"

          fragment definedOnImplementorsButNotInterface on Pet {
            nickname
          }

        ", UndefinedField("nickname", "Pet", new[] {"Cat", "Dog"}, new[] {"name"}, 4, 13));

        [Fact]
        public void MetaFieldSelectionOnUnion() => QueryShouldPass(@"

          fragment directFieldSelectionOnUnion on CatOrDog {
            __typename
          }

        ");

        [Fact]
        public void DirectFieldSelectionOnUnion() => QueryShouldFail(@"

          fragment directFieldSelectionOnUnion on CatOrDog {
            directField
          }

        ", UndefinedField("directField", "CatOrDog", null, null, 4, 13));

        [Fact]
        public void DefinedOnImplementorsQueriedOnUnion() => QueryShouldFail(@"

          fragment definedOnImplementorsQueriedOnUnion on CatOrDog {
            name
          }

        ", UndefinedField("name", "CatOrDog", new[] {"Being", "Pet", "Canine", "Cat", "Dog"}, null, 4, 13));

        [Fact]
        public void ValidFieldInInlineFragment() => QueryShouldPass(@"
    
          fragment objectFieldSelection on Pet {
            ... on Dog {
              name
            }
            ... {
              name
            }
          }

        ");

        [NoReorder]
        public class FieldsOnCorrectTypeErrorMessage
        {
            [Fact]
            public void WorksWithNoSuggestions() =>
                UndefinedFieldMessage("f", "T", new string[] { }, new string[] { }).Should()
                    .Be("Cannot query field \"f\" on type \"T\".");

            [Fact]
            public void SmallNumberOfTypeSuggestions() =>
                UndefinedFieldMessage("f", "T", new[] {"A", "B"}, new string[] { }).Should()
                    .Be(
                        "Cannot query field \"f\" on type \"T\". Did you mean to use an inline fragment on \"A\" or \"B\"?");

            [Fact]
            public void SmallNumberOfFieldSuggestions() =>
                UndefinedFieldMessage("f", "T", new string[] { }, new[] {"z", "y"}).Should()
                    .Be("Cannot query field \"f\" on type \"T\". Did you mean \"z\" or \"y\"?");

            [Fact]
            public void OnlyShowsOneSetOfSuggestionsAtATimePreferringTypes() =>
                UndefinedFieldMessage("f", "T", new[] {"A", "B"}, new[] {"z", "y"}).Should()
                    .Be(
                        "Cannot query field \"f\" on type \"T\". Did you mean to use an inline fragment on \"A\" or \"B\"?");

            [Fact]
            public void LimitsLotsOfTypeSuggestions() =>
                UndefinedFieldMessage("f", "T", new[] {"A", "B", "C", "D", "E", "F"}, new string[] { }).Should()
                    .Be(
                        "Cannot query field \"f\" on type \"T\". Did you mean to use an inline fragment on \"A\", \"B\", \"C\", \"D\", or \"E\"?");

            [Fact]
            public void LimitsLotsOfFieldSuggestions() =>
                UndefinedFieldMessage("f", "T", new string[] { }, new[] {"z", "y", "x", "w", "v", "u"}).Should()
                    .Be("Cannot query field \"f\" on type \"T\". Did you mean \"z\", \"y\", \"x\", \"w\", or \"v\"?");
        }
    }
}