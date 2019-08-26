// Copyright (c) GraphZen LLC. All rights reserved.
// Licensed under the GraphZen Community License. See the LICENSE file in the project root for license information.

using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using GraphZen.Infrastructure;
using GraphZen.LanguageModel.Validation;
using GraphZen.QueryEngine.Validation;
using GraphZen.QueryEngine.Validation.Rules;
using JetBrains.Annotations;
using Xunit;

#nullable disable


namespace GraphZen.Validation.Rules
{
    [NoReorder]
    public class KnownTypeNamesTests : ValidationRuleHarness
    {
        public override ValidationRule RuleUnderTest { get; } = QueryValidationRules.KnownTypeNames;

        [Fact]
        public void KnownTypeNamesAreValid()
        {
            QueryShouldPass(@"

          query Foo($var: String, $required: [String!]!) {
            user(id: 4) {
              pets { ... on Pet { name }, ...PetFields, ... { name } }
            }
          }

          fragment PetFields on Pet {
            name
          }

        ");
        }

        private static ExpectedError UnknownType(string typeName, IReadOnlyList<string> suggestedTypes, int line,
            int column)
        {
            return Error(KnownTypeNames.UnknownTypeMessage(typeName, suggestedTypes ?? Array.Empty<string>()),
                (line,
                    column));
        }

        [Fact]
        public void UnknownTypeNamesAreInvalid()
        {
            QueryShouldFail(@"

          query Foo($var: JumbledUpLetters) {
            user(id: 4) {
              name
              pets { ... on Badger { name }, ...PetFields }
            }
          }

          fragment PetFields on Peettt {
            name
          }

        ",
                UnknownType("JumbledUpLetters", null, 3, 27),
                UnknownType("Badger", null, 6, 29),
                UnknownType("Peettt", new[] { "Pet" }, 10, 33)
            );
        }

        [Fact]
        public void IgnoresTypeDefinitions()
        {
            QueryShouldFail(@"

          type NotInTheSchema {
            field: FooBar
          }
          interface FooBar {
            field: NotInTheSchema
          }
          union U = A | B
          input Blob {
            field: UnknownType
          }
          query Foo($var: NotInTheSchema) {
            user(id: $var) {
              id
            }
          }

        ", UnknownType("NotInTheSchema", null, 13, 27));
        }
    }
}