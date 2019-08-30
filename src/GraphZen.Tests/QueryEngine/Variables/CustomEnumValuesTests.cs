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
    public abstract class CustomEnumValuesTests : VariablesTests
    {
        [UsedImplicitly]
        private class StaticDslTests : CustomEnumValuesTests
        {
            public override Schema Schema => StaticDslSchema;
        }

        [UsedImplicitly]
        public class SchemaBuilderTests : CustomEnumValuesTests
        {
            public override Schema Schema => SchemaBuilderSchema;
        }


        [Fact]
        public Task AllowsCustomEnumValuesAsInputs() =>
            ExecuteAsync(@"
            {
              null: fieldWithEnumInput(input: NULL)
              negative: fieldWithEnumInput(input: NEGATIVE)
              boolean: fieldWithEnumInput(input: BOOLEAN)
              customValue: fieldWithEnumInput(input: CUSTOM)
              defaultValue: fieldWithEnumInput(input: DEFAULT_VALUE)
            }").ShouldEqual(new
            {
                data = new
                {
                    @null = "null",
                    negative = "-1",
                    boolean = "false",
                    customValue = "\"custom value\"",
                    defaultValue = "{}"
                }
            });

        [Fact]
        public Task AllowsNonNullableInputsToHaveNullAsCustomEnumValue() =>
            ExecuteAsync(@"
            {
              fieldWithNonNullableEnumInput(input: NULL)
            }").ShouldEqual(new
            {
                data = new
                {
                    fieldWithNonNullableEnumInput = "null"
                }
            });
    }
}