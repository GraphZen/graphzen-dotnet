// Copyright (c) GraphZen LLC. All rights reserved.
// Licensed under the GraphZen Community License. See the LICENSE file in the project root for license information.

using System.Diagnostics.CodeAnalysis;
using GraphZen;
using GraphZen.Infrastructure;
using JetBrains.Annotations;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using SimpleBlog.Models;

namespace SimpleBlog
{
    public class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddGraphQLContext(options =>
            {
                options
                    .UseQueryType<Query>()
                    .RevealInternalServerErrors();
            });
        }

        public void Configure(IApplicationBuilder app)
        {
            // app.UseGraphQLPlayground();

            app.UseRouting();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapGraphQLPlayground();
            });

            //app.UseGraphQL();
        }
    }
}