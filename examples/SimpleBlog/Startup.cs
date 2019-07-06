// Copyright (c) GraphZen LLC. All rights reserved.
// Licensed under the GraphZen Community License. See the LICENSE file in the project root for license information.

using GraphZen;
using GraphZen.Infrastructure;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;

namespace SimpleBlog
{
    public class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddGraphQLContext();
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            app.UseGraphQLPlayground();
            app.UseGraphQL();
        }
    }
}