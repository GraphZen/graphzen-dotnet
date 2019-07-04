// Copyright (c) GraphZen LLC. All rights reserved.
// Licensed under the GraphZen Community License. See the LICENSE file in the project root for license information.

using System.Collections.Generic;
using System.Linq;
using GraphZen.Infrastructure;

using Xunit;
using static GraphZen.Validation.Rules.SDLValidationHelpers;

namespace GraphZen.Validation.Rules
{
    [NoReorder]
    public class ObjectFieldsMustHaveOutputTypesTests : ValidationRuleHarness
    {
        public override ValidationRule RuleUnderTest { get; } = ValidationRules.ObjectFieldsMustHaveOutputTypes;

        public static IEnumerable<object[]> GetValidFieldScenarios() =>
            from outputType in OutputTypes
            from fieldType in "SomeOutputType".WithModifiers()
            select new[] {outputType, fieldType};

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

        public static IEnumerable<object[]> GetInvalidFieldScenarios() =>
            from nonOutputType in NonOutputTypes
            from fieldType in "SomeInputType".WithModifiers()
            select new[] {nonOutputType, fieldType};


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