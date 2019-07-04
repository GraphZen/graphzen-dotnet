// Copyright (c) GraphZen LLC. All rights reserved.
// Licensed under the GraphZen Community License. See the LICENSE file in the project root for license information.

using System.Threading.Tasks;
using GraphZen.Infrastructure;
using GraphZen.Types;
using GraphZen.TypeSystem;
using JetBrains.Annotations;
using Xunit;

namespace GraphZen.Execution.Variables
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
        public Task NotWhenArugmentCannotBeCoerced() => ExecuteAsync(@"
              {
                fieldWithDefaultArgumentValue(input: WRONG_TYPE)
              }
            ").ShouldEqual(new
        {
            data = new
            {
                fieldWithDefaultArgumentValue = (object) null
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

        [Fact]
        public Task WhenNoArgumentProvided() => ExecuteAsync("{fieldWithDefaultArgumentValue}")
            .ShouldEqual(new
            {
                data = new
                {
                    fieldWithDefaultArgumentValue = "\"Hello World\""
                }
            });

        [Fact]
        public Task WhenNoRuntimeValueIsProvidedToANonNullArgument() => ExecuteAsync(@"
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

        [Fact]
        public Task WhenOmittedVariableProvided() => ExecuteAsync(@"
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