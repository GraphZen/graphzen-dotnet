// Copyright (c) GraphZen LLC. All rights reserved.
// Licensed under the GraphZen Community License. See the LICENSE file in the project root for license information.

using System.Diagnostics.CodeAnalysis;
using System.Threading.Tasks;
using GraphZen.Infrastructure;
using GraphZen.TypeSystem;
using JetBrains.Annotations;
using Xunit;
#nullable disable


namespace GraphZen.QueryEngine.Variables
{
    public abstract class ArgumentDefaultValuesTests : VariablesTests
    {
        [UsedImplicitly]
        private class StaticDslTests : ArgumentDefaultValuesTests
        {
            public override Schema Schema => StaticDslSchema;
        }

        [UsedImplicitly]
        public class SchemaBuilderTests : ArgumentDefaultValuesTests
        {
            public override Schema Schema => SchemaBuilderSchema;
        }


        [Fact]
        public Task NotWhenArugmentCannotBeCoerced()
        {
            return ExecuteAsync(@"
              {
                fieldWithDefaultArgumentValue(input: WRONG_TYPE)
              }
            ").ShouldEqual(new
            {
                data = new
                {
                    fieldWithDefaultArgumentValue = (object)null
                },
                errors = Array(new
                {
                    message = "Argument \"input\" has invalid value \"WRONG_TYPE\".",
                    locations = Array(new
                    {
                        column = 17,
                        line = 3
                    }),
                    path = Array("fieldWithDefaultArgumentValue")
                })
            });
        }

        [Fact]
        public Task WhenNoArgumentProvided()
        {
            return ExecuteAsync("{fieldWithDefaultArgumentValue}")
                .ShouldEqual(new
                {
                    data = new
                    {
                        fieldWithDefaultArgumentValue = "\"Hello World\""
                    }
                });
        }

        [Fact]
        public Task WhenNoRuntimeValueIsProvidedToANonNullArgument()
        {
            return ExecuteAsync(@"
              query optionalVariable($optional: String) {
                fieldWithNonNullableStringInputAndDefaultArgumentValue(input: $optional)
              }
            ").ShouldEqual(new
            {
                data = new
                {
                    fieldWithNonNullableStringInputAndDefaultArgumentValue = "\"Hello World\""
                }
            });
        }

        [Fact]
        public Task WhenOmittedVariableProvided()
        {
            return ExecuteAsync(@"
              query ($optional: String) {
                fieldWithDefaultArgumentValue(input: $optional)
              }
            ").ShouldEqual(new
            {
                data = new
                {
                    fieldWithDefaultArgumentValue = "\"Hello World\""
                }
            });
        }
    }
}