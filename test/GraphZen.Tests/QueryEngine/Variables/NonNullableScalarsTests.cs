// Copyright (c) GraphZen LLC. All rights reserved.
// Licensed under the GraphZen Community License. See the LICENSE file in the project root for license information.

using System.Diagnostics.CodeAnalysis;
using System.Threading.Tasks;
using GraphZen.Infrastructure;
using GraphZen.TypeSystem;
using JetBrains.Annotations;
using Xunit;

#nullable disable


namespace GraphZen.Tests.QueryEngine.Variables
{
    public abstract class NonNullableScalarsTests : VariablesTests
    {
        [UsedImplicitly]
        private class StaticDslTests : NonNullableScalarsTests
        {
            public override Schema Schema => StaticDslSchema;
        }

        [UsedImplicitly]
        public class SchemaBuilderTests : NonNullableScalarsTests
        {
            public override Schema Schema => SchemaBuilderSchema;
        }

        [Fact]
        public Task AllowsNonNullableInputsToBeOmittedGivenADefault() =>
            ExecuteAsync(@"
              query ($value: String = ""default"") {
                fieldWithNonNullableStringInput(input: $value)
              }
            ").ShouldEqual(new
            {
                data = new { fieldWithNonNullableStringInput = "\"default\"" }
            });

        [Fact]
        public Task AllowsNonNullableInputsToBeSetToAValueDirectly() =>
            ExecuteAsync(@"
              {
                fieldWithNonNullableStringInput(input: ""a"")
              }
            ").ShouldEqual(new
            {
                data = new
                {
                    fieldWithNonNullableStringInput = "\"a\""
                }
            });

        [Fact]
        public Task AllowsNonNullableInputsToBeSetToAValueInAVariable() =>
            ExecuteAsync(@"
              query ($value: String!) {
                fieldWithNonNullableStringInput(input: $value)
              }
            ", new { value = "a" }).ShouldEqual(new
            {
                data = new
                {
                    fieldWithNonNullableStringInput = "\"a\""
                }
            });

        [Fact]
        public Task DoesNotAllowNonNullableInputsToBeOmittedInAVariable() =>
            ExecuteAsync(@"
              query ($value: String!) {
                fieldWithNonNullableStringInput(input: $value)
              }
            ").ShouldEqual(new
            {
                errors = Array(new
                {
                    message = "Variable \"$value\" of required type \"String!\" was not provided.",
                    locations = Array(new
                    {
                        line = 2,
                        column = 22
                    })
                })
            });

        [Fact]
        public Task DoesNotAllowNonNullableInputsToBeSetToNullInAVairable() =>
            ExecuteAsync(@"
              query ($value: String!) {
                fieldWithNonNullableStringInput(input: $value)
              }
            ", new { value = (string)null }).ShouldEqual(new
            {
                errors = Array(new
                {
                    message = "Variable \"$value\" of non-null type \"String!\" must not be null.",
                    locations = Array(new
                    {
                        line = 2,
                        column = 22
                    })
                })
            });

        [Fact]
        public async Task ReportsErrorForArrayPassedIntoStringInput()
        {
            var result = await ExecuteAsync(@"
              query ($value: String!) {
                fieldWithNonNullableStringInput(input: $value)
              }
            ", new { value = Array(1, 2, 3) }).ShouldEqual(new
            {
                errors = Array(new
                {
                    message =
                        "Variable \"$value\" got invalid value `[1, 2, 3]`; Expected type String; String cannot represent a non string value: [1, 2, 3]",
                    locations = Array(new { line = 2, column = 22 })
                })
            });

            Assert.NotNull(result.Errors[0].InnerException);
        }

        [Fact]
        public Task ReportsErrorForMissingNonNullabeInputs() =>
            ExecuteAsync("{ fieldWithNonNullableStringInput }").ShouldEqual(new
            {
                data = new
                {
                    fieldWithNonNullableStringInput = (object)null
                },
                errors = Array(new
                {
                    message = "Argument \"input\" of required type \"String!\" was not provided.",
                    locations = Array(new
                    {
                        line = 1,
                        column = 3
                    }),
                    path = Array("fieldWithNonNullableStringInput")
                })
            });

        [Fact]
        public Task ReportsErrorForNonProvidedVariablesForNonNullableInputs() =>
            ExecuteAsync(@"
              {
                fieldWithNonNullableStringInput(input: $foo)
              }
            ").ShouldEqual(new
            {
                data = new
                {
                    fieldWithNonNullableStringInput = (object)null
                },
                errors = Array(new
                {
                    message =
                        "Argument \"input\" of required type \"String!\" was provided the variable \"$foo\" which was not provided a runtime value.",
                    locations = Array(new
                    {
                        line = 3,
                        column = 56
                    }),
                    path = Array("fieldWithNonNullableStringInput")
                })
            });
    }
}