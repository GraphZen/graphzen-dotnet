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
    public abstract class ListsAndNullabilityTests : VariablesTests
    {
        [UsedImplicitly]
        private class StaticDslTests : ListsAndNullabilityTests
        {
            public override Schema Schema => StaticDslSchema;
        }

        [UsedImplicitly]
        public class SchemaBuilderTests : ListsAndNullabilityTests
        {
            public override Schema Schema => SchemaBuilderSchema;
        }


        [Fact]
        public Task AllowsListsOfNonNullsToBeNull()
        {
            return ExecuteAsync(@"
              query ($input: [String!]) {
                listNN(input: $input)
              }
            ", new { input = (object)null }).ShouldEqual(
                new
                {
                    data = new { listNN = "null" }
                });
        }

        [Fact]
        public Task AllowsListsOfNonNullsToContainValues()
        {
            return ExecuteAsync(@"
              query ($input: [String!]) {
                listNN(input: $input)
              }
            ", new { input = Array("A") }).ShouldEqual(
                new
                {
                    data = new { listNN = "[\"A\"]" }
                });
        }

        [Fact]
        public Task AllowsListsToBeNull()
        {
            return ExecuteAsync(@"
              query ($input: [String]) {
                list(input: $input)
              }
            ", new { input = (object)null }).ShouldEqual(new
            {
                data = new
                {
                    list = "null"
                }
            });
        }

        [Fact]
        public Task AllowsListsToContainNull()
        {
            return ExecuteAsync(@"
              query ($input: [String]) {
                list(input: $input)
              }
            ", new { input = Array("A", null, "B") }).ShouldEqual(new
            {
                data = new
                {
                    list = "[\"A\",null,\"B\"]"
                }
            });
        }

        [Fact]
        public Task AllowsListsToContainValues()
        {
            return ExecuteAsync(@"
              query ($input: [String]) {
                list(input: $input)
              }
            ", new { input = Array("A") }).ShouldEqual(new
            {
                data = new
                {
                    list = "[\"A\"]"
                }
            });
        }

        [Fact]
        public Task AllowsNonNullListsOfNonNullsToContainValues()
        {
            return ExecuteAsync(@"
              query ($input: [String!]!) {
                nnListNN(input: $input)
              }
            ", new { input = Array("A") }).ShouldEqual(
                new
                {
                    data = new
                    {
                        nnListNN = "[\"A\"]"
                    }
                });
        }

        [Fact]
        public Task AllowsNonNullListsToContainValues()
        {
            return ExecuteAsync(@"
              query ($input: [String]!) {
                nnList(input: $input)
              }
            ", new { input = Array("A") }).ShouldEqual(new
            {
                data = new
                {
                    nnList = "[\"A\"]"
                }
            });
        }

        [Fact]
        public Task DoesNotAllowInvalidTypesToBeUsedAsValues()
        {
            return ExecuteAsync(@"
              query ($input: TestType!) {
                fieldWithObjectInput(input: $input)
              }
            ", new { input = new { list = Array("A", "B") } }).ShouldEqual(new
            {
                errors = Array(new
                {
                    message =
                        "Variable \"$input\" expected value of type \"TestType!\" which cannot be used as an input type.",
                    locations = Array(new
                    {
                        line = 2,
                        column = 30
                    })
                })
            });
        }

        [Fact]
        public Task DoesNotAllowListsOfNonNullsToContainNull()
        {
            return ExecuteAsync(@"
              query ($input: [String!]) {
                listNN(input: $input)
              }
            ", new { input = Array("A", null, "B") }).ShouldEqual(
                new
                {
                    errors = Array(new
                    {
                        message =
                            "Variable \"$input\" got invalid value `[\"A\", null, \"B\"]`; Expected non-nullable type String! not to be null at value[1].",
                        locations = Array(new
                        {
                            line = 2,
                            column = 22
                        })
                    })
                });
        }

        [Fact]
        public Task DoesNotAllowNonNullListsOfNonNullsToBeNull()
        {
            return ExecuteAsync(@"
              query ($input: [String!]!) {
                nnListNN(input: $input)
              }
            ", new { input = (object)null }).ShouldEqual(
                new
                {
                    errors = Array(new
                    {
                        message = "Variable \"$input\" of non-null type \"[String!]!\" must not be null.",
                        locations = Array(new
                        {
                            line = 2,
                            column = 22
                        })
                    })
                });
        }

        [Fact]
        public Task DoesNotAllowNonNullListsToBeNull()
        {
            return ExecuteAsync(@"
              query ($input: [String]!) {
                nnList(input: $input)
              }
            ", new { input = (object)null }).ShouldEqual(new
            {
                errors = Array(new
                {
                    message = "Variable \"$input\" of non-null type \"[String]!\" must not be null.",
                    locations = Array(new
                    {
                        line = 2,
                        column = 22
                    })
                })
            });
        }

        [Fact]
        public Task DoesNotAllowsNonNullListsOfNonNullsToContainNull()
        {
            return ExecuteAsync(@"
              query ($input: [String!]!) {
                nnListNN(input: $input)
              }
            ", new { input = Array("A", null, "B") }).ShouldEqual(
                new
                {
                    errors = Array(new
                    {
                        message =
                            "Variable \"$input\" got invalid value `[\"A\", null, \"B\"]`; Expected non-nullable type String! not to be null at value[1].",
                        locations = Array(new
                        {
                            line = 2,
                            column = 22
                        })
                    })
                });
        }

        [Fact]
        public Task DoesNotAllowUnknownTypesToBeUsedAsValues()
        {
            return ExecuteAsync(@"
              query ($input: UnknownType!) {
                fieldWithObjectInput(input: $input)
              }
            ", new { input = "whoknows" }).ShouldEqual(new
            {
                errors = Array(new
                {
                    message =
                        "Variable \"$input\" expected value of type \"UnknownType!\" which cannot be used as an input type.",
                    locations = Array(new
                    {
                        line = 2,
                        column = 30
                    })
                })
            });
        }

        [Fact]
        public Task ItAllowsNonNullListsToContainNull()
        {
            return ExecuteAsync(@"
              query ($input: [String]!) {
                nnList(input: $input)
              }
            ", new { input = Array("A", null, "B") }).ShouldEqual(new
            {
                data = new
                {
                    nnList = "[\"A\",null,\"B\"]"
                }
            });
        }
    }
}