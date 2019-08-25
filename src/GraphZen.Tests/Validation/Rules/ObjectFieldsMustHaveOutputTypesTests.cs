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
using static GraphZen.Validation.Rules.SDLValidationHelpers;

namespace GraphZen.Validation.Rules
{
    [NoReorder]
    public class ObjectFieldsMustHaveOutputTypesTests : ValidationRuleHarness
    {
        public override ValidationRule RuleUnderTest { get; } = DocumentValidationRules.ObjectFieldsMustHaveOutputTypes;

        public static IEnumerable<object[]> GetValidFieldScenarios()
        {
            return from outputType in OutputTypes
                   from fieldType in "SomeOutputType".WithModifiers()
                   select new[] { outputType, fieldType };
        }

        [Theory]
        [MemberData(nameof(GetValidFieldScenarios))]
        public void AcceptsOutputTypeAsAnObjectFieldType(string outputType, string fieldType)
        {
            SDLShouldPass($@"
               {outputType} SomeOutputType

               type SomeObject {{
                 someField: {fieldType} 
               }}
            ");
        }

        public static IEnumerable<object[]> GetInvalidFieldScenarios()
        {
            return from nonOutputType in NonOutputTypes
                   from fieldType in "SomeInputType".WithModifiers()
                   select new[] { nonOutputType, fieldType };
        }


        [Theory]
        [MemberData(nameof(GetInvalidFieldScenarios))]
        public void RejectsWithRelevantLocationsForNonOutputTypeAsAnObjectFieldType(string nonOutputType,
            string fieldType)
        {
            SDLShouldFail($@"
              {nonOutputType} SomeInputType
              type Query {{
                field:  {fieldType}
              }}
            ",
                Error($"The type of Query.field must be Output Type but got: {fieldType}.", (3, 11))
            );
        }
    }
}