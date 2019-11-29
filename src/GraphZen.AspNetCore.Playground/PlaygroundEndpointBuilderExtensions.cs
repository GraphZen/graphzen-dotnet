// Copyright (c) GraphZen LLC. All rights reserved.
// Licensed under the GraphZen Community License. See the LICENSE file in the project root for license information.

using System;
using System.Diagnostics.CodeAnalysis;
using GraphZen.Infrastructure;
using GraphZen.Playground;
using GraphZen.Playground.Internal;
using JetBrains.Annotations;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;

// ReSharper disable once CheckNamespace
namespace Microsoft.AspNetCore.Builder
{
    public static class PlaygroundEndpointBuilderExtensions
    {
        public static IEndpointConventionBuilder MapGraphQLPlayground(this IEndpointRouteBuilder endpointRouteBuilder)
            => MapGraphQLPlayground(endpointRouteBuilder, null, (PlaygroundOptions?) null);


        public static IEndpointConventionBuilder MapGraphQLPlayground(this IEndpointRouteBuilder endpointRouteBuilder,
            string path)
            => MapGraphQLPlayground(endpointRouteBuilder, path, (PlaygroundOptions?) null);


        public static IEndpointConventionBuilder MapGraphQLPlayground(this IEndpointRouteBuilder endpointRouteBuilder,
            Action<PlaygroundOptions>? optionsAction)
            => MapGraphQLPlayground(endpointRouteBuilder, null, optionsAction);


        public static IEndpointConventionBuilder MapGraphQLPlayground(this IEndpointRouteBuilder endpointRouteBuilder,
            string? path, Action<PlaygroundOptions>? optionsAction)
        {
            PlaygroundOptions? options = default;
            if (optionsAction != null)
            {
                options = new PlaygroundOptions();
                optionsAction.Invoke(options);
            }

            return MapGraphQLPlayground(endpointRouteBuilder, path, options);
        }

        public static IEndpointConventionBuilder MapGraphQLPlayground(this IEndpointRouteBuilder endpointRouteBuilder,
            PlaygroundOptions? options) => MapGraphQLPlayground(endpointRouteBuilder, null, options);

        public static IEndpointConventionBuilder MapGraphQLPlayground(this IEndpointRouteBuilder endpointRouteBuilder,
            string? path, PlaygroundOptions? options)
        {
            var html = PlaygroundHtmlWriter.GetHtml(options);
            return endpointRouteBuilder.MapGet(path ?? "/", http => http.Response.WriteAsync(html));
        }
    }
}