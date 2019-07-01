﻿// Copyright (c) GraphZen LLC. All rights reserved.
// Licensed under the GraphZen Community License. See the LICENSE file in the project root for license information.

using System.Collections.Generic;
using System.Linq;
using GraphZen.Infrastructure;
using JetBrains.Annotations;
using Xunit;
using static GraphZen.Validation.Rules.SDLValidationHelpers;

namespace GraphZen.Validation.Rules
{
    [NoReorder]
    public class InputObjectFieldsMustHaveInputTypesTests : ValidationRuleHarness
    {
        public override ValidationRule RuleUnderTest { get; } = ValidationRules.InputObjectFieldsMustHaveInputTypes;

        public static IEnumerable<object[]> GetValidInputFieldScenarios() =>
            from inputType in InputTypes
            from fieldsType in InputFieldsTypes
            from fieldType in "SomeInputType".WithModifiers()
            select new object[] {inputType, fieldsType, fieldType};

        [Theory]
        [MemberData(nameof(GetValidInputFieldScenarios))]
        public void AcceptsAnInputTypeAsAnInputFieldType(string inputType, string inputFieldsType, string fieldType)
        {
            SDLShouldPass($@"
              {inputType} SomeInputType

              {inputFieldsType} SomeObject {{
                field: {fieldType}
              }}
           ");
        }

        public static IEnumerable<object[]> GetInvalidInputFieldScenarios() =>
            from nonInputType in NonInputTypes
            from inputFieldsType in InputFieldsTypes
            from fieldType in "SomeOutputType".WithModifiers()
            select new object[] {nonInputType, inputFieldsType, fieldType};

        [Theory]
        [MemberData(nameof(GetInvalidInputFieldScenarios))]
        public void RejectsNonInputTypeAsAnInputObjectField(string nonInputType, string inputFieldsType,
            string fieldType)
        {
            SDLShouldFail($@"
              {nonInputType} SomeOutputType
              
              {inputFieldsType} SomeInputObject {{
                foo: {fieldType}
              }}
           ", Error($"The type of SomeInputObject.foo must be Input Type but got: {fieldType}.", (4, 8)));
        }
    }
}