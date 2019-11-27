// Copyright (c) GraphZen LLC. All rights reserved.
// Licensed under the GraphZen Community License. See the LICENSE file in the project root for license information.

using System;
using System.Diagnostics.CodeAnalysis;
using System.Net.Http;
using System.Threading.Tasks;
using FluentAssertions;
using GraphZen.Infrastructure;
using GraphZen.Playground.Internal;
using JetBrains.Annotations;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using Xunit;

namespace GraphZen.Playground
{
    [NoReorder]
    public class PlaygroundEndpointBuilderExtensionTests
    {
        private static Action<PlaygroundOptions> ConfigureOptions { get; } = options =>
        {
            options.Endpoint = "customViaAction";
        };

        private static PlaygroundOptions CustomOptions { get; } = new PlaygroundOptions {Endpoint = "custom"};


        [Fact]
        public void playground_should_share_IApplicationBuilder_namespace() =>
            typeof(PlaygroundEndpointBuilderExtensions).Namespace
                .Should().Be(typeof(IApplicationBuilder).Namespace);


        public class DefaultMappingStartup
        {
            public void Configure(IApplicationBuilder app)
            {
                app.UseRouting();
                app.UseEndpoints(endpoints => { endpoints.MapGraphQLPlayground(); });
            }
        }

        [Fact]
        public async Task default_map_to_root_path()
        {
            var client = CreateAppClient<DefaultMappingStartup>();
            var result = await client.GetStringAsync("/");
            var expectedHtml = PlaygroundHtmlWriter.GetHtml();
            result.Should().Be(expectedHtml);
        }

        public class CustomPathStartup
        {
            public void Configure(IApplicationBuilder app)
            {
                app.UseRouting();
                app.UseEndpoints(endpoints => { endpoints.MapGraphQLPlayground("/foo"); });
            }
        }

        [Fact]
        public async Task it_should_map_to_custom_path()
        {
            var client = CreateAppClient<CustomPathStartup>();
            var result = await client.GetStringAsync("/foo");
            var expectedHtml = PlaygroundHtmlWriter.GetHtml();
            result.Should().Be(expectedHtml);
        }


        public class CustomOptionsStartup
        {
            public void Configure(IApplicationBuilder app)
            {
                app.UseRouting();
                app.UseEndpoints(endpoints => { endpoints.MapGraphQLPlayground(CustomOptions); });
            }
        }

        [Fact]
        public async Task it_should_render_custom_options()
        {
            var client = CreateAppClient<CustomOptionsStartup>();
            var result = await client.GetStringAsync("/");
            var expectedHtml = PlaygroundHtmlWriter.GetHtml(CustomOptions);
            result.Should().Be(expectedHtml);
        }

        public class CustomOptionsAndPathStartup
        {
            public void Configure(IApplicationBuilder app)
            {
                app.UseRouting();
                app.UseEndpoints(endpoints => { endpoints.MapGraphQLPlayground("/foo", CustomOptions); });
            }
        }

        [Fact]
        public async Task it_should_render_custom_options_on_custom_path()
        {
            var client = CreateAppClient<CustomOptionsAndPathStartup>();
            var result = await client.GetStringAsync("/foo");
            var expectedHtml = PlaygroundHtmlWriter.GetHtml(CustomOptions);
            result.Should().Be(expectedHtml);
        }

        public class CustomOptionsActionStartup
        {
            public void Configure(IApplicationBuilder app)
            {
                app.UseRouting();
                app.UseEndpoints(endpoints => { endpoints.MapGraphQLPlayground(ConfigureOptions); });
            }
        }

        [Fact]
        public async Task it_should_render_custom_options_configured_via_action_on_default_path()
        {
            var client = CreateAppClient<CustomOptionsActionStartup>();
            var result = await client.GetStringAsync("/");
            var expectedOptions = new PlaygroundOptions();
            ConfigureOptions(expectedOptions);
            var expectedHtml = PlaygroundHtmlWriter.GetHtml(expectedOptions);
            result.Should().Be(expectedHtml);
        }

        public class CustomOptionsActionAndPathStartup
        {
            public void Configure(IApplicationBuilder app)
            {
                app.UseRouting();
                app.UseEndpoints(endpoints => { endpoints.MapGraphQLPlayground("/foo", ConfigureOptions); });
            }
        }

        [Fact]
        public async Task it_should_render_custom_options_configured_via_action_on_custom_path()
        {
            var client = CreateAppClient<CustomOptionsActionAndPathStartup>();
            var result = await client.GetStringAsync("/foo");
            var expectedOptions = new PlaygroundOptions();
            ConfigureOptions(expectedOptions);
            var expectedHtml = PlaygroundHtmlWriter.GetHtml(expectedOptions);
            result.Should().Be(expectedHtml);
        }

        private static HttpClient CreateAppClient<T>() where T : class =>
            new TestServer(WebHost.CreateDefaultBuilder().UseStartup<T>()).CreateClient();
    }
}