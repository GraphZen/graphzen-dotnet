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
    public abstract class NullableScalarsTests : VariablesTests
    {
        [UsedImplicitly]
        private class StaticDslTests : NullableScalarsTests
        {
            public override Schema Schema => StaticDslSchema;
        }

        [UsedImplicitly]
        public class SchemaBuilderTests : NullableScalarsTests
        {
            public override Schema Schema => SchemaBuilderSchema;
        }

        [Fact]
        public Task AllowsNullableInputsToBeOmitted() =>
            ExecuteAsync(@"
              {
                fieldWithNullableStringInput
              }
            ").ShouldEqual(new
            {
                data = new
                {
                    fieldWithNullableStringInput = (string)null
                }
            });

        [Fact]
        public Task AllowsNullableInputsToBeOmittedInAnUnlistedVariable() =>
            ExecuteAsync(@"
              query {
                fieldWithNullableStringInput(input: $value)
              }
            ").ShouldEqual(new
            {
                data = new
                {
                    fieldWithNullableStringInput = (string)null
                }
            });

        [Fact]
        public Task AllowsNullableInputsToBeOmittedInAVariable() =>
            ExecuteAsync(@"
              query ($value: String) {
                fieldWithNullableStringInput(input: $value)
              }
            ").ShouldEqual(new
            {
                data = new
                {
                    fieldWithNullableStringInput = (string)null
                }
            });

        [Fact]
        public Task AllowsNullableInputsToBeSetInAVariable() =>
            ExecuteAsync(@"
              query ($value: String) {
                fieldWithNullableStringInput(input: $value)
              }
            ", new { value = "a" }).ShouldEqual(new
            {
                data = new
                {
                    fieldWithNullableStringInput = "\"a\""
                }
            });

        [Fact]
        public Task AllowsNullableInputsToBeSetToNullInAVariable() =>
            ExecuteAsync(@"
              query ($value: String) {
                fieldWithNullableStringInput(input: $value)
              }
            ", new { value = (string)null }).ShouldEqual(new
            {
                data = new
                {
                    fieldWithNullableStringInput = "null"
                }
            });

        [Fact]
        public Task AllowsNullableInputsToBeSetToValueDirectly() =>
            ExecuteAsync(@"
               {
                fieldWithNullableStringInput(input: ""a"")
              }
            ", new { value = "a" }).ShouldEqual(new
            {
                data = new
                {
                    fieldWithNullableStringInput = "\"a\""
                }
            });
    }
}