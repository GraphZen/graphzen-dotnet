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
    public abstract class UsingInlineStructs : VariablesTests
    {
        [UsedImplicitly]
        private class StaticDslTests : UsingInlineStructs
        {
            public override Schema Schema => StaticDslSchema;
        }

        [UsedImplicitly]
        public class SchemaBuilderTest : UsingInlineStructs
        {
            public override Schema Schema => SchemaBuilderSchema;
        }

        [Fact]
        public Task DoesNotUseIncorrectValue()
        {
            return ExecuteAsync(@"
              {
                fieldWithObjectInput(input: [""foo"", ""bar"", ""baz""])
              }
            ").ShouldEqual(new
            {
                data = new
                {
                    fieldWithObjectInput = (object)null
                },
                errors = new object[]
                {
                    new
                    {
                        message = "Argument \"input\" has invalid value [\"foo\", \"bar\", \"baz\"].",
                        locations = Array(new {line = 3, column = 17}),
                        path = Array("fieldWithObjectInput")
                    }
                }
            });
        }

        [Fact]
        public Task ExecutesWithComplextInput()
        {
            return ExecuteAsync(@"
                    {
                        fieldWithObjectInput(input: { a: ""foo"", b: [""bar""], c: ""baz""})
                    }
                    ").ShouldEqual(
                new
                {
                    data = new
                    {
                        fieldWithObjectInput = @"{""a"":""foo"",""b"":[""bar""],""c"":""baz""}"
                    }
                }
            );
        }

        [Fact]
        public Task ProperlyParsesNullValueInList()
        {
            return ExecuteAsync(@"
                    {
                        fieldWithObjectInput(input: {b: [""A"",null,""C""], c: ""C""})
                    }
                    ")
                .ShouldEqual(new
                {
                    data = new
                    {
                        fieldWithObjectInput = @"{""b"":[""A"",null,""C""],""c"":""C""}"
                    }
                });
        }

        [Fact]
        public Task ProperlyParsesNullValueToNull()
        {
            return ExecuteAsync(@"
                    {
                        fieldWithObjectInput(input: {a: null, b: null, c: ""C"", d: null})
                    }
                    ").ShouldEqual(
                new
                {
                    data = new
                    {
                        fieldWithObjectInput = @"{""a"":null,""b"":null,""c"":""C"",""d"":null}"
                    }
                });
        }

        [Fact]
        public Task ProperlyParsesSingleValueToList()
        {
            return ExecuteAsync(@"
                    {
                        fieldWithObjectInput(input: {a: ""foo"", b: ""bar"", c: ""baz""})
                    }
                    ").ShouldEqual(
                new
                {
                    data = new
                    {
                        fieldWithObjectInput = @"{""a"":""foo"",""b"":[""bar""],""c"":""baz""}"
                    }
                }
            );
        }

        [Fact]
        public Task ProperlyRunsParseLiteralOnComplexScalarTypes()
        {
            return ExecuteAsync(@"
                    {
                        fieldWithObjectInput(input: {c: ""foo"", d: ""SerializedValue""}) 
                    }")
                .ShouldEqual(new
                {
                    data = new
                    {
                        fieldWithObjectInput = @"{""c"":""foo"",""d"":""DeserializedValue""}"
                    }
                });
        }
    }
}