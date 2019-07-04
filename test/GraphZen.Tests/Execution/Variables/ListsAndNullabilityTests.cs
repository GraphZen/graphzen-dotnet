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
        public Task AllowsListsOfNonNullsToBeNull() => ExecuteAsync(@"
              query ($input: [String!]) {
                listNN(input: $input)
              }
            ", new {input = (object) null}).ShouldEqual(
            new
            {
                data = new {listNN = "null"}
            });

        [Fact]
        public Task AllowsListsOfNonNullsToContainValues() => ExecuteAsync(@"
              query ($input: [String!]) {
                listNN(input: $input)
              }
            ", new {input = Array("A")}).ShouldEqual(
            new
            {
                data = new {listNN = "[\"A\"]"}
            });

        [Fact]
        public Task AllowsListsToBeNull() => ExecuteAsync(@"
              query ($input: [String]) {
                list(input: $input)
              }
            ", new {input = (object) null}).ShouldEqual(new
        {
            data = new
            {
                list = "null"
            }
        });

        [Fact]
        public Task AllowsListsToContainNull() => ExecuteAsync(@"
              query ($input: [String]) {
                list(input: $input)
              }
            ", new {input = Array("A", null, "B")}).ShouldEqual(new
        {
            data = new
            {
                list = "[\"A\",null,\"B\"]"
            }
        });

        [Fact]
        public Task AllowsListsToContainValues() => ExecuteAsync(@"
              query ($input: [String]) {
                list(input: $input)
              }
            ", new {input = Array("A")}).ShouldEqual(new
        {
            data = new
            {
                list = "[\"A\"]"
            }
        });

        [Fact]
        public Task AllowsNonNullListsOfNonNullsToContainValues() => ExecuteAsync(@"
              query ($input: [String!]!) {
                nnListNN(input: $input)
              }
            ", new {input = Array("A")}).ShouldEqual(
            new
            {
                data = new
                {
                    nnListNN = "[\"A\"]"
                }
            });

        [Fact]
        public Task AllowsNonNullListsToContainValues() => ExecuteAsync(@"
              query ($input: [String]!) {
                nnList(input: $input)
              }
            ", new {input = Array("A")}).ShouldEqual(new
        {
            data = new
            {
                nnList = "[\"A\"]"
            }
        });

        [Fact]
        public Task DoesNotAllowInvalidTypesToBeUsedAsValues() => ExecuteAsync(@"
              query ($input: TestType!) {
                fieldWithObjectInput(input: $input)
              }
            ", new {input = new {list = Array("A", "B")}}).ShouldEqual(new
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

        [Fact]
        public Task DoesNotAllowListsOfNonNullsToContainNull() => ExecuteAsync(@"
              query ($input: [String!]) {
                listNN(input: $input)
              }
            ", new {input = Array("A", null, "B")}).ShouldEqual(
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

        [Fact]
        public Task DoesNotAllowNonNullListsOfNonNullsToBeNull() => ExecuteAsync(@"
              query ($input: [String!]!) {
                nnListNN(input: $input)
              }
            ", new {input = (object) null}).ShouldEqual(
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

        [Fact]
        public Task DoesNotAllowNonNullListsToBeNull() => ExecuteAsync(@"
              query ($input: [String]!) {
                nnList(input: $input)
              }
            ", new {input = (object) null}).ShouldEqual(new
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

        [Fact]
        public Task DoesNotAllowsNonNullListsOfNonNullsToContainNull() => ExecuteAsync(@"
              query ($input: [String!]!) {
                nnListNN(input: $input)
              }
            ", new {input = Array("A", null, "B")}).ShouldEqual(
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

        [Fact]
        public Task DoesNotAllowUnknownTypesToBeUsedAsValues() => ExecuteAsync(@"
              query ($input: UnknownType!) {
                fieldWithObjectInput(input: $input)
              }
            ", new {input = "whoknows"}).ShouldEqual(new
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

        [Fact]
        public Task ItAllowsNonNullListsToContainNull() => ExecuteAsync(@"
              query ($input: [String]!) {
                nnList(input: $input)
              }
            ", new {input = Array("A", null, "B")}).ShouldEqual(new
        {
            data = new
            {
                nnList = "[\"A\",null,\"B\"]"
            }
        });
    }
}