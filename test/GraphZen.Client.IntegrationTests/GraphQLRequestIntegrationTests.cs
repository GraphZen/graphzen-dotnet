// Copyright (c) GraphZen LLC. All rights reserved.
// Licensed under the GraphZen Community License. See the LICENSE file in the project root for license information.

using GraphZen.Infrastructure;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.DependencyInjection;

namespace GraphZen.Client.IntegrationTests;

public class GraphQLRequestIntegrationTests
{
    private readonly IGraphQLClient _gql;

    public GraphQLRequestIntegrationTests()
    {
#pragma warning disable ASPDEPR008
        var httpClient = new TestServer(WebHost.CreateDefaultBuilder().UseStartup<Startup>()).CreateClient();
#pragma warning restore ASPDEPR008
        _gql = new GraphQLClient(httpClient);
    }


    [Fact]
    public async Task it_can_be_used_to_execute_graphql_request()
    {
        var graphqlRequest = new GraphQLRequest
        {
            Query = @"{ message }"
        };

        var expected = new { message = "Hello world" };
        var response = await _gql.SendAsync(graphqlRequest);
        JsonAssert.EquivalentToJsonFromObject(response.GetData() as object, expected);
        JsonAssert.EquivalentToJsonFromObject(response.GetData<TypedQueryResult>(), expected);
    }

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

    public class TypedQueryResult
    {
        public string? Message { get; set; }
    }
}