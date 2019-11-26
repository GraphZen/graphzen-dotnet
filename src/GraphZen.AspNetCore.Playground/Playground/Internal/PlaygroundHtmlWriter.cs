// Copyright (c) GraphZen LLC. All rights reserved.
// Licensed under the GraphZen Community License. See the LICENSE file in the project root for license information.

using System;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using GraphZen.Infrastructure;
using JetBrains.Annotations;

namespace GraphZen.Playground.Internal
{
    public class PlaygroundHtmlWriter
    {
        private static readonly Lazy<string> _html = new Lazy<string>(() =>
        {
            var test = typeof(PlaygroundHtmlWriter).Assembly;
            using var stream = test.GetManifestResourceStream("GraphZen.Playground.Internal.Files.playground.html") ??
                               throw new InvalidOperationException(
                                   "there was an error getting the playground.html file");
            using var reader = new StreamReader(stream);
            return reader.ReadToEnd();
        });

        private static string Html => _html.Value;

        public static string GetHtml() => Html;
    }
}