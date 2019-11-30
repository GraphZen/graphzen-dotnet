// Copyright (c) GraphZen LLC. All rights reserved.
// Licensed under the GraphZen Community License. See the LICENSE file in the project root for license information.

using System.Diagnostics.CodeAnalysis;
using GraphZen.Infrastructure;
using JetBrains.Annotations;
using Microsoft.AspNetCore.StaticFiles.Infrastructure;
using Microsoft.Extensions.FileProviders;

#nullable disable


// ReSharper disable once CheckNamespace
namespace Microsoft.AspNetCore.Builder
{
    public static class PlaygroundApplicationBuilderExtensions
    {
        [UsedImplicitly]
        public static void UseGraphQLPlayground(this IApplicationBuilder app)
        {
            var shared = new SharedOptions
            {
                RequestPath = ""
            };
            var defaultFileOptions = new DefaultFilesOptions(shared);
            // ReSharper disable once PossibleNullReferenceException
            defaultFileOptions.DefaultFileNames.Clear();
            defaultFileOptions.DefaultFileNames.Add("playground.html");
            app.UseDefaultFiles(defaultFileOptions);
            var staticFileOptions = new StaticFileOptions(shared)
            {
                FileProvider = new ManifestEmbeddedFileProvider(typeof(PlaygroundApplicationBuilderExtensions).Assembly,
                    "StaticFiles")
            };
            app.UseStaticFiles(staticFileOptions);
        }
    }
}