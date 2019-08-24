// Copyright (c) GraphZen LLC. All rights reserved.
// Licensed under the GraphZen Community License. See the LICENSE file in the project root for license information.
using JetBrains.Annotations;
#nullable disable


using GraphZen.Infrastructure;
using GraphZen.LanguageModel.Validation;
using Xunit;

namespace GraphZen.Validation.Rules
{
    [NoReorder]
    public class ObjectsMustHaveFieldsTests : ValidationRuleHarness
    {
        public override ValidationRule RuleUnderTest { get; } = DocumentValidationRules.ObjectsMustHaveFields;

        [Fact]
        public void AcceptsAnObjectTypeWithFieldsObject()
        {
            SDLShouldPass(@"
              type Query {
                field: SomeObject
              }

              type SomeObject {
                field: String
              }
            ");
        }

        [Fact]
        public void RejectsAnObjectTypeWithMissingFields()
        {
            SDLShouldFail(@"
              type Query {
                test: IncompleteObject
              }

              type IncompleteObject
            ", Error("Type IncompleteObject must define one or more fields.", (5, 6)));
        }

        [Fact(Skip = "TODO")]
        public void RejectsAnObjectWithIncorrectlyNamedFields()
        {
        }

        [Fact(Skip = "legacy")]
        public void AcceptsAnObjectWithExplicitlyAllowedLegacyNamedFields()
        {
        }

        [Fact(Skip = "legacy")]
        public void ThrowsWithBadValueForExplicitlyAllowedLegacyNames()
        {
        }
    }
}