// Copyright (c) GraphZen LLC. All rights reserved.
// Licensed under the GraphZen Community License. See the LICENSE file in the project root for license information.

using System.Text;
using System.Text.Json;
using GraphZen.Infrastructure;

namespace GraphZen.Client.Tests;

public class GraphQLRequestToHttpRequestTests
{
    [Fact]
    public void it_can_be_created_with_query()
        => new GraphQLRequest { Query = "{}" }.ToHttpRequest();

    [Fact]
    public void it_can_be_created_with_operation_name()
        => new GraphQLRequest { OperationName = "{}" }.ToHttpRequest();

    [Fact]
    public void it_validates_presence_of_OperationName_or_query()
    {
        Action act = () => new GraphQLRequest().ToHttpRequest();
        var ex = Assert.Throws<ArgumentException>(act);
        Assert.Contains("Cannot convert", ex.Message);
    }

    [Fact]
    public void it_sets_http_method_to_post()
        => Assert.Equal(HttpMethod.Post,
            new GraphQLRequest { OperationName = "test" }
                .ToHttpRequest()
                .Method);

    [Fact]
    public void it_sets_content_to_utf8_application_json_media_type_string()
    {
        var request = new GraphQLRequest { OperationName = "test" };
        var requestJson = JsonSerializer.Serialize(request);
        var requestJsonContent = new StringContent(requestJson, Encoding.UTF8, "application/json");
        Assert.Equivalent(requestJsonContent, request.ToHttpRequest().Content);
    }
}
