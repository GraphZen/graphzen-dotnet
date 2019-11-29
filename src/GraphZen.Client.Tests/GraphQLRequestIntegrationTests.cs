// Copyright (c) GraphZen LLC. All rights reserved.
// Licensed under the GraphZen Community License. See the LICENSE file in the project root for license information.

using System.Diagnostics.CodeAnalysis;
using System.Threading.Tasks;
using FluentAssertions;
using GraphZen.Infrastructure;
using JetBrains.Annotations;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.DependencyInjection;
using Xunit;

namespace GraphZen
{
    public class GraphQLRequestIntegrationTests
    {
        public class Query
        {
            public string Message => "Hello world";
        }

        public class Startup
        {
            public void ConfigureServices(IServiceCollection services)
            {
                services.AddGraphQLContext(options => { options.UseQueryType<Query>().RevealInternalServerErrors(); });
            }

            public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
            {
                app.UseRouting();
                app.UseEndpoints(endpoints => { endpoints.MapGraphQL(); });
            }
        }

        private readonly IGraphQLClient _client;

        public GraphQLRequestIntegrationTests()
        {
            var httpClient = new TestServer(WebHost.CreateDefaultBuilder().UseStartup<Startup>()).CreateClient();
            _client = new GraphQLClient(httpClient);
        }

        public class TypedQueryResult
        {
            public string? Message { get; set; }
        }


        [Fact]
        public async Task it_can_be_used_to_execute_graphql_request()
        {
            var graphqlRequest = new GraphQLRequest
            {
                Query = @"{ message }"
            };

            var expected = new {message = "Hello world"};
            var response = await _client.SendAsync(graphqlRequest);
            (response.GetData() as object).Should().BeEquivalentToJsonFromObject(expected);
            response.GetData<TypedQueryResult>().Should().BeEquivalentToJsonFromObject(expected);
        }
    }
}