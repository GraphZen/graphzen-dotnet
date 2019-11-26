// Copyright (c) GraphZen LLC. All rights reserved.
// Licensed under the GraphZen Community License. See the LICENSE file in the project root for license information.

using System.Diagnostics.CodeAnalysis;
using GraphZen.Infrastructure;
using GraphZen.Playground.Internal;
using JetBrains.Annotations;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;

// ReSharper disable once CheckNamespace
namespace Microsoft.AspNetCore.Builder
{
    public static class PlaygroundEndpointBuilderExtensions
    {
        public static void MapGraphQLPlayground(this IEndpointRouteBuilder endpointRouteBuilder, string path = "/")
        {
            var handler = CreatePlaygroundRequestHandler();
            endpointRouteBuilder.MapGet(path, handler);
        }

        private static RequestDelegate CreatePlaygroundRequestHandler()
        {
            var html = PlaygroundHtmlWriter.GetHtml();
            return req => req.Response.WriteAsync(html);
        }
    }
}