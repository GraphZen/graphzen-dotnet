// Copyright (c) GraphZen LLC. All rights reserved.
// Licensed under the GraphZen Community License. See the LICENSE file in the project root for license information.

#nullable disable
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using GraphZen.Infrastructure;
using GraphZen.LanguageModel.Validation;
using JetBrains.Annotations;
using Xunit;
using static GraphZen.Tests.Validation.Rules.SDLValidationHelpers;

namespace GraphZen.Tests.Validation.Rules
{
    [NoReorder]
    public class InterfaceFieldsMustHaveOutputTypesTests : ValidationRuleHarness
    {
        public override ValidationRule RuleUnderTest { get; } =
            DocumentValidationRules.InterfaceFieldsMustHaveOutputTypes;

        public static IEnumerable<object[]> GetValidInterfaceFieldTypeScenarios()
        {
            return from outputType in OutputTypes
                   from fieldType in "SomeOutputType".WithModifiers()
                   select new object[] { outputType, fieldType };
        }

        [Theory]
        [MemberData(nameof(GetValidInterfaceFieldTypeScenarios))]
        public void AcceptsAnOutputTypeAsAnInterfaceFieldType(string outputType, string fieldType)
        {
            SDLShouldPass($@"
              {outputType} SomeOutputType

              interface SomeInterface {{
                 someField: {fieldType}
              }}
            ");
        }

        public static IEnumerable<object[]> GetInvalidInterfaceFieldTypeScenarios()
        {
            return from nonOutputType in NonOutputTypes
                   from fieldType in "SomeInputType".WithModifiers()
                   select new object[] { nonOutputType, fieldType };
        }

        [Theory]
        [MemberData(nameof(GetInvalidInterfaceFieldTypeScenarios))]
        public void RejectsNonOutputTypeAsAnInterfaceFieldType(string nonOutputType, string badFieldType)
        {
            SDLShouldFail($@"
              {nonOutputType} SomeInputType 
            
              interface BadInterface {{
                 badField: {badFieldType} 
              }}

              type BadImplementing {{
                badField: {badFieldType}
              }}
            ", Error($"The type of BadInterface.badField must be Output Type but got: {badFieldType}.", (4, 14)),
                Error($"The type of BadImplementing.badField must be Output Type but got: {badFieldType}.", (8, 13)));
        }


        [Fact]
        public void RejectsNonOutputTypeAsAnInterfaceFieldTypeWithLocations()
        {
            SDLShouldFail(@"
              type Query {
                test: SomeInterface
              }

              interface SomeInterface {
                field: SomeInputObject
              }

              input SomeInputObject {
                foo: String
              }

              type SomeObject implements SomeInterface {
                field: SomeInputObject
              }
            ", Error("The type of SomeInterface.field must be Output Type but got: SomeInputObject.", (6, 10)),
                Error("The type of SomeObject.field must be Output Type but got: SomeInputObject.", (14, 10)));
        }

        [Fact]
        public void AcceptsAnInterfaceNotImplementedByAtLeastOneObject()
        {
            SDLShouldPass(@"
              type Query {
                test: SomeInterface
              }

              interface SomeInterface {
                foo: String
              }
            ");
        }
    }
}