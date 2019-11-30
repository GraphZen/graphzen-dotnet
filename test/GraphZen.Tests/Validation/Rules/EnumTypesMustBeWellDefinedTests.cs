// Copyright (c) GraphZen LLC. All rights reserved.
// Licensed under the GraphZen Community License. See the LICENSE file in the project root for license information.

using System.Diagnostics.CodeAnalysis;
using GraphZen.Infrastructure;
using GraphZen.LanguageModel.Validation;
using JetBrains.Annotations;
using Xunit;

#nullable disable


namespace GraphZen.Tests.Validation.Rules
{
    [NoReorder]
    public class EnumTypesMustBeWellDefinedTests : ValidationRuleHarness
    {
        public override ValidationRule RuleUnderTest { get; } = DocumentValidationRules.EnumTypesMustBeWellDefined;

        [Fact]
        public void ItRejectsAnEnumTypeWithoutValues()
        {
            SDLShouldFail(@"
              type Query {
                field: SomeEnum
              }

              enum SomeEnum

              directive @test on ENUM

              extend enum SomeEnum @test
            ", Error("Enum type SomeEnum must define one or more values.", (5, 6), (9, 13)));
        }

        [Fact]
        public void RejectsAnEnumTypeWithDuplicateValues()
        {
            SDLShouldFail(@"
              type Query {
                field: SomeEnum
              }

              enum SomeEnum {
                SOME_VALUE
                SOME_VALUE
              }
            ", Error("Enum type SomeEnum can include value SOME_VALUE only once.", (6, 3), (7, 3)));
        }

        [Fact(Skip = "TODO")]
        public void RejectsEnumWithIncorrectlyTypedEnums()
        {
        }
    }
}