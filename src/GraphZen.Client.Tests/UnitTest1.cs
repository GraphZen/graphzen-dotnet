using System.Diagnostics.CodeAnalysis;
using System.Net.Http;
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



    public class UnitTest1
    {
        public class Query
        {
            public string Message => "Hello world";
        }

        public class Startup
        {
            public void ConfigureServices(IServiceCollection services)
            {
                services.AddGraphQLContext(optionsAction: options =>
                {
                    options.UseQueryType<Query>().RevealInternalServerErrors();
                });
            }

            public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
            {
                app.UseRouting();
                app.UseEndpoints(endpoints =>
                {
                    endpoints.MapGraphQL();
                });
            }


        }

        private readonly GraphQLClient _graphQL;

        public UnitTest1()
        {
            var _httpClient = new TestServer(WebHost.CreateDefaultBuilder().UseStartup<Startup>()).CreateClient();
            _graphQL = new GraphQLClient(_httpClient);
        }


        [Fact(Skip = "wip")]
        public async Task Test1()
        {
            GraphQLResponse result = await _graphQL.SendAsync(new GraphQLRequest()
            {
                Query = @"{ message }"
            });
        }

    }
}
