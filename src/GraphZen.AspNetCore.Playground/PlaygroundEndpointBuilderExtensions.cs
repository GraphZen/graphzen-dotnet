// Copyright (c) GraphZen LLC. All rights reserved.
// Licensed under the GraphZen Community License. See the LICENSE file in the project root for license information.

using GraphZen.Playground;
using GraphZen.Playground.Internal;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;

// ReSharper disable once CheckNamespace
namespace Microsoft.AspNetCore.Builder;

public static class PlaygroundEndpointBuilderExtensions
{
    public static IEndpointConventionBuilder MapGraphQLPlayground(this IEndpointRouteBuilder endpointRouteBuilder)
        =>
            endpointRouteBuilder.MapGraphQLPlayground(null, (PlaygroundOptions?)null);


    public static IEndpointConventionBuilder MapGraphQLPlayground(this IEndpointRouteBuilder endpointRouteBuilder,
        string path)
        =>
            endpointRouteBuilder.MapGraphQLPlayground(path, (PlaygroundOptions?)null);


    public static IEndpointConventionBuilder MapGraphQLPlayground(this IEndpointRouteBuilder endpointRouteBuilder,
        Action<PlaygroundOptions>? optionsAction)
        =>
            endpointRouteBuilder.MapGraphQLPlayground(null, optionsAction);


    public static IEndpointConventionBuilder MapGraphQLPlayground(this IEndpointRouteBuilder endpointRouteBuilder,
        string? path, Action<PlaygroundOptions>? optionsAction)
    {
        PlaygroundOptions? options = default;
        if (optionsAction != null)
        {
            options = new PlaygroundOptions();
            optionsAction.Invoke(options);
        }

        return endpointRouteBuilder.MapGraphQLPlayground(path, options);
    }

    public static IEndpointConventionBuilder MapGraphQLPlayground(this IEndpointRouteBuilder endpointRouteBuilder,
        PlaygroundOptions? options) =>
        endpointRouteBuilder.MapGraphQLPlayground(null, options);

    public static IEndpointConventionBuilder MapGraphQLPlayground(this IEndpointRouteBuilder endpointRouteBuilder,
        string? path, PlaygroundOptions? options)
    {
        var html = PlaygroundHtmlWriter.GetHtml(options);
        return endpointRouteBuilder.MapGet(path ?? "/", http => http.Response.WriteAsync(html));
    }
}
