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
    public class FieldArgumentsMustHaveInputTypesTests : ValidationRuleHarness
    {
        public override ValidationRule RuleUnderTest { get; } =
            DocumentValidationRules.FieldArgumentsMustHaveInputTypes;


        public static IEnumerable<object[]> GetInputTypeData(string typeName)
        {
            return from fieldsType in OutputFieldsTypes
                   from inputType in InputTypes
                   from fieldType in typeName.WithModifiers()
                   select new object[] { fieldsType, inputType, fieldType };
        }

        [Theory]
        [MemberData(nameof(GetInputTypeData), "SomeInputType")]
        public void AcceptsInputTypeAsFieldArgType(string fieldsType, string inputType, string fieldType)
        {
            SDLShouldPass($@"
              {inputType} SomeInputType
              
              {fieldsType} SomeObjectType {{
                field: {fieldType}
              }}
            ");
        }

        public static IEnumerable<object[]> GetNonInputTypeData(string typeName)
        {
            return from fieldsType in OutputFieldsTypes
                   from inputType in NonInputTypes
                   from fieldType in typeName.WithModifiers()
                   select new object[] { fieldsType, inputType, fieldType };
        }

        [Theory]
        [MemberData(nameof(GetNonInputTypeData), "SomeNonInputType")]
        public void RejectsNonInputTypeAsFieldArgType(string fieldsType, string outputType, string argType)
        {
            SDLShouldFail($@"
              {outputType} SomeNonInputType

              {fieldsType} BadObject {{
                badField(badArg: {argType}): String
              }}
            ", Error($"The type of BadObject.badField(badArg:) must be Input Type but got: {argType}.", (4, 12)));
        }
    }
}