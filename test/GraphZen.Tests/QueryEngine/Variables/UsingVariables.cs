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
    public abstract class UsingVariables : VariablesTests
    {
        [UsedImplicitly]
        private class StaticDslTests : UsingVariables
        {
            public override Schema Schema => StaticDslSchema;
        }

        [UsedImplicitly]
        public class SchemaBuilderTests : UsingVariables
        {
            public override Schema Schema => SchemaBuilderSchema;
        }

        private const string Doc = @"
                query q($input: TestInputObject) {
                    fieldWithObjectInput(input: $input)
                }";


        [Fact]
        public Task ErrorsOnAdditionOfUnkownInputField() =>
            ExecuteAsync(Doc, new { input = new { a = "foo", b = "bar", c = "baz", extra = "dog" } })
                .ShouldEqual(new
                {
                    errors = Array(new
                    {
                        message =
                            "Variable \"$input\" got invalid value `{a: \"foo\", b: \"bar\", c: \"baz\", extra: \"dog\"}`; Field \"extra\" is not defined by type TestInputObject; Did you mean to select another field?",
                        locations = Array(new
                        {
                            line = 2,
                            column = 25
                        })
                    })
                });

        [Fact]
        public Task ErrorsOnDeepNestedErrorsAndWithmanyErrors() =>
            ExecuteAsync(@"
            query ($input: TestNestedInputObject) {
              fieldWithNestedObjectInput(input: $input)
            }", new
            {
                input = new
                {
                    na = new { a = "foo" }
                }
            })
                .ShouldEqual(new
                {
                    errors = Array(new
                    {
                        message =
                                "Variable \"$input\" got invalid value `{na: {a: \"foo\"}}`; Field value.nb of required type String! was not provided.",
                        locations = Array(new
                        {
                            line = 2,
                            column = 20
                        })
                    },
                        new
                        {
                            message =
                                "Variable \"$input\" got invalid value `{na: {a: \"foo\"}}`; Field value.na.c of required type String! was not provided.",
                            locations = Array(new
                            {
                                line = 2,
                                column = 20
                            })
                        }
                    )
                });

        [Fact]
        public Task ErrorsOnIncorrectType() =>
            ExecuteAsync(Doc, new
            {
                input = "foo bar"
            }).ShouldEqual(new
            {
                errors = Array(new
                {
                    message =
                        "Variable \"$input\" got invalid value `\"foo bar\"`; Expected TestInputObject to be an object.",
                    locations = Array(new
                    {
                        line = 2,
                        column = 25
                    })
                })
            });

        [Fact]
        public Task ErrorsOnNullForNestedNonNull() =>
            ExecuteAsync(Doc, new
            {
                input = new
                {
                    a = "foo",
                    b = "bar",
                    c = (string)null
                }
            }).ShouldEqual(new
            {
                errors = Array(
                    new
                    {
                        message =
                            "Variable \"$input\" got invalid value `{a: \"foo\", b: \"bar\", c: null}`; Field value.c of required type String! was not provided.",
                        locations = Array(new { line = 2, column = 25 })
                    })
            });

        [Fact]
        public Task ErrorsOnOmissionOfNestedNonNull() =>
            ExecuteAsync(Doc, new { input = new { a = "foo", b = "bar" } })
                .ShouldEqual(new
                {
                    errors = Array(new
                    {
                        message =
                            "Variable \"$input\" got invalid value `{a: \"foo\", b: \"bar\"}`; Field value.c of required type String! was not provided.",
                        locations = Array(new
                        {
                            line = 2,
                            column = 25
                        })
                    })
                });

        [Fact]
        public Task ItDoesNotUseDefaultValueWhenProvided() =>
            ExecuteAsync(@" 
                        query q($input: String = ""Default value"") {
                          fieldWithNullableStringInput(input: $input)
                        }
                    ", new
            {
                input = "Variable value"
            })
                .ShouldEqual(new
                {
                    data = new
                    {
                        fieldWithNullableStringInput = "\"Variable value\""
                    }
                });

        [Fact]
        public Task ItExecutesWithComplexInput() =>
            ExecuteAsync(Doc,
                new
                {
                    input = new
                    {
                        a = "foo",
                        b = Array("bar"),
                        c = "baz"
                    }
                }
            ).ShouldEqual(new
            {
                data = new
                {
                    fieldWithObjectInput = @"{""a"":""foo"",""b"":[""bar""],""c"":""baz""}"
                }
            });

        [Fact]
        public Task ItExecutesWithComplexScalarInput() =>
            ExecuteAsync(Doc, new { input = new { c = "foo", d = "SerializedValue" } }).ShouldEqual(new
            {
                data = new
                {
                    fieldWithObjectInput = "{\"c\":\"foo\",\"d\":\"DeserializedValue\"}"
                }
            });

        [Fact]
        public Task ItUsesDefaultValueWhenNotProvided() =>
            ExecuteAsync(@" 
                        query ($input: TestInputObject = {a: ""foo"", b: [""bar""], c: ""baz""}) {
                            fieldWithObjectInput(input: $input)
                        }")
                .ShouldEqual(new
                {
                    data = new
                    {
                        fieldWithObjectInput = "{\"a\":\"foo\",\"b\":[\"bar\"],\"c\":\"baz\"}"
                    }
                });


        [Fact]
        public Task ItUsesExplicitNullValueInsteadOfDefaultValue() =>
            ExecuteAsync(@" 
                        query q($input: String = ""Default value"") {
                          fieldWithNullableStringInput(input: $input)
                        }
                    ", new
            {
                input = (string)null
            })
                .ShouldEqual(new
                {
                    data = new
                    {
                        fieldWithNullableStringInput = "null"
                    }
                });

        [Fact]
        public Task ItUsesNullDefaultValueWhenNotProvided() =>
            ExecuteAsync(@" 
                        query q($input: String = null) {
                          fieldWithNullableStringInput(input: $input)
                        }
                    ")
                .ShouldEqual(new
                {
                    data = new
                    {
                        fieldWithNullableStringInput = "null"
                    }
                });

        [Fact]
        public Task ItUsesNullWhenVariableProvidedExplicitNullValue() =>
            ExecuteAsync(@" 
                        query q($input: String) {
                            fieldWithNullableStringInput(input: $input)
                       }", new { input = (string)null })
                .ShouldEqual(new
                {
                    data = new
                    {
                        fieldWithNullableStringInput = "null"
                    }
                });

        [Fact]
        public Task ItUsesUndefinedWhenVariableNotProvided() =>
            ExecuteAsync(@" 
                        query q($input: String) {
                            fieldWithNullableStringInput(input: $input)
                       }", new { })
                .ShouldEqual(new
                {
                    data = new
                    {
                        fieldWithNullableStringInput = (string)null
                    }
                });

        [Fact]
        public Task ProperlyParsesSingleValueToList() =>
            ExecuteAsync(Doc, new { input = new { a = "foo", b = "bar", c = "baz" } })
                .ShouldEqual(new
                {
                    data = new
                    {
                        fieldWithObjectInput = "{\"a\":\"foo\",\"b\":[\"bar\"],\"c\":\"baz\"}"
                    }
                });
    }
}