// Copyright (c) GraphZen LLC. All rights reserved.
// Licensed under the GraphZen Community License. See the LICENSE file in the project root for license information.

using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using FluentAssertions;
using GraphZen.Infrastructure;
using JetBrains.Annotations;
using Xunit;

namespace GraphZen.Internal
{
    [NoReorder]
    public class GraphQLSerializerTests
    {
        [Theory]
        [InlineData("{}")]
        [InlineData("null")]
        [InlineData("{\"data\":null}")]
        [InlineData("{\"data\":{\"value\": 1}}")]
        [InlineData("{\"errors\": []}")]
        [InlineData("{\"errors\": [{}]}")]
        [InlineData("{\"errors\": [null]}")]
        [InlineData("{\"errors\": null}")]
        public void parse_errors_should_return_empty(string json)
            => GraphQLJsonSerializer.ParseErrors(json).Should().BeEmpty();

        [Fact]
        public void parse_errors_should_return_error()
        {
            var result = GraphQLJsonSerializer.ParseErrors(
                "{\"errors\": [{\"message\": \"hello\"}]}");

            result.Should().BeEquivalentTo(
                new List<GraphQLError> {new GraphQLError("hello")});
        }

        [Theory]
        [InlineData("null")]
        [InlineData("{}")]
        [InlineData("{\"data\":null}")]
        [InlineData("{\"errors\": []}")]
        [InlineData("{\"errors\": [{}]}")]
        [InlineData("{\"errors\": [null]}")]
        [InlineData("{\"errors\": null}")]
        [InlineData("{\"errors\": [{\"message\": \"hello\"}]}")]
        public void parse_data_should_return_null(string json)
            => (GraphQLJsonSerializer.ParseData(json) as object).Should().BeNull();

        [Fact]
        public void parse_data_should_return_dynamic()
        {
            var result = GraphQLJsonSerializer.ParseData("{\"data\":{\"value\": 1}}");
            (result as object).Should().BeEquivalentToJsonFromObject(new {value = 1});
        }

        public class TypedQueryResult
        {
            public string? Message { get; set; }
            public int? Number { get; set; }
        }

        [Fact]
        public void parse_typed_data_should_return_typed_data()
        {
            var result =
                GraphQLJsonSerializer.ParseData<TypedQueryResult>("{\"data\":{\"number\": 1, \"message\":\"hello\"}}");
            result.Should().BeEquivalentToJsonFromObject(new TypedQueryResult
            {
                Message = "hello",
                Number = 1
            });
        }
    }
}