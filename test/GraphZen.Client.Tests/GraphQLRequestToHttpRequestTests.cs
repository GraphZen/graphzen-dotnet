// Copyright (c) GraphZen LLC. All rights reserved.
// Licensed under the GraphZen Community License. See the LICENSE file in the project root for license information.

using System;
using System.Diagnostics.CodeAnalysis;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using FluentAssertions;
using GraphZen.Infrastructure;
using JetBrains.Annotations;
using Xunit;

namespace GraphZen
{
    public class GraphQLRequestToHttpRequestTests
    {
        [Fact]
        public void it_can_be_created_with_query()
            => new GraphQLRequest {Query = "{}"}.ToHttpRequest();

        [Fact]
        public void it_can_be_created_with_operation_name()
            => new GraphQLRequest {OperationName = "{}"}.ToHttpRequest();

        [Fact]
        public void it_validates_presence_of_OperationName_or_query()
        {
            Action act = () => new GraphQLRequest().ToHttpRequest();
            act.Should().Throw<ArgumentException>().WithMessage("Cannot convert*");
        }

        [Fact]
        public void it_sets_http_method_to_post()
            => new GraphQLRequest {OperationName = "test"}
                .ToHttpRequest()
                .Method.Should().Be(HttpMethod.Post);

        [Fact]
        public void it_sets_content_to_utf8_application_json_media_type_string()
        {
            var request = new GraphQLRequest {OperationName = "test"};
            var requestJson = JsonSerializer.Serialize(request);
            var requestJsonContent = new StringContent(requestJson, Encoding.UTF8, "application/json");
            request.ToHttpRequest().Content.Should().BeEquivalentTo(requestJsonContent);
        }
    }
}