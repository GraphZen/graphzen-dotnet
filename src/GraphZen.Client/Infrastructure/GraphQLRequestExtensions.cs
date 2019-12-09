// Copyright (c) GraphZen LLC. All rights reserved.
// Licensed under the GraphZen Community License. See the LICENSE file in the project root for license information.

using System;
using System.Diagnostics.CodeAnalysis;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using GraphZen.Infrastructure;
using JetBrains.Annotations;

namespace GraphZen.Infrastructure
{
    public static class GraphQLRequestExtensions
    {
        public static HttpRequestMessage ToHttpRequest(this GraphQLRequest request)
        {
            Check.NotNull(request, nameof(request));
            if (request.OperationName == null && request.Query == null)
                throw new ArgumentException(
                    $"Cannot convert {nameof(GraphQLRequest)} to {nameof(HttpRequestMessage)}: query or operation name required.");
            var requestJson = JsonSerializer.Serialize(request);
            var requestJsonContent = new StringContent(requestJson, Encoding.UTF8, "application/json");
            var message = new HttpRequestMessage
            {
                Method = HttpMethod.Post,
                Content = requestJsonContent
            };
            return message;
        }
    }
}