// Copyright (c) GraphZen LLC. All rights reserved.
// Licensed under the GraphZen Community License. See the LICENSE file in the project root for license information.

using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Net.Http;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using GraphZen.Infrastructure;
using JetBrains.Annotations;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace GraphZen
{
    public class GraphQLClient : IGraphQLClient
    {
        private class GraphQLResponseJson
        {
            public object? Data { get; set; }
            public List<GraphQLError>? Errors { get; set; }
        }

        private class GraphQLResponseJson<T> : GraphQLResponseJson where T : class
        {
            public new T? Data { get; set; }
        }

        public GraphQLClient(HttpClient httpClient)
        {
            HttpClient = httpClient;
        }

        public HttpClient HttpClient { get; }

        private static JsonSerializerOptions SerializationOptions { get; } = new JsonSerializerOptions
        {
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
            Converters = {new JsonStringEnumConverter(JsonNamingPolicy.CamelCase)},
            IgnoreNullValues = true
        };

        public async Task<GraphQLResponse> SendAsync(GraphQLRequest request)
        {
            var httpRequest = request.ToHttpRequest();
            var result = await HttpClient.SendAsync(httpRequest);
            var responseJson = await result.Content.ReadAsStringAsync();
            var raw = JsonConvert.DeserializeObject<GraphQLResponseJson>(responseJson, DeserializationSettings);
            Console.WriteLine("test" + responseJson);

            throw new NotImplementedException();
        }

        private static JsonSerializerSettings DeserializationSettings { get; } = new JsonSerializerSettings
        {
            ContractResolver = new DefaultContractResolver
            {
                NamingStrategy = new CamelCaseNamingStrategy()
            }
        };
    }
}