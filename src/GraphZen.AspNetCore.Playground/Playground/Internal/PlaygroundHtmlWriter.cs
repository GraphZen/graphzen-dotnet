using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace GraphZen.Playground.Internal
{
    public class PlaygroundHtmlWriter
    {
        private static readonly Lazy<string> _html = new Lazy<string>(() =>
        {
            var test = typeof(PlaygroundHtmlWriter).Assembly;
            using var stream = test.GetManifestResourceStream("GraphZen.Playground.Internal.Files.playground.html") ?? throw new InvalidOperationException("there was an error getting the playground.html file");
            using var reader = new StreamReader(stream);
            return reader.ReadToEnd();
        });

        private static string Html => _html.Value;

        public static string GetHtml()
        {
            return Html;
        }
    }
}
