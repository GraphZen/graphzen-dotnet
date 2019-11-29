// Copyright (c) GraphZen LLC. All rights reserved.
// Licensed under the GraphZen Community License. See the LICENSE file in the project root for license information.

using System.Diagnostics.CodeAnalysis;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using GraphZen.Infrastructure;
using JetBrains.Annotations;

namespace GraphZen
{
    public class GraphQLClient : IGraphQLClient
    {
        public GraphQLClient(HttpClient httpClient)
        {
            HttpClient = httpClient;
        }

        public HttpClient HttpClient { get; }

        public async Task<GraphQLResponse> SendAsync(GraphQLRequest request, CancellationToken cancellationToken)
        {
            var httpRequest = request.ToHttpRequest();
            var httpResponse = await HttpClient.SendAsync(httpRequest, cancellationToken);
            var responseContent = await httpResponse.Content.ReadAsStringAsync();
            return new GraphQLResponse(responseContent);
        }

        public Task<GraphQLResponse> SendAsync(GraphQLRequest request) => SendAsync(request, default);
    }
}