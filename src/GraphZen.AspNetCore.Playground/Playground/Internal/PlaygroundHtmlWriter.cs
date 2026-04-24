// Copyright (c) GraphZen LLC. All rights reserved.
// Licensed under the GraphZen Community License. See the LICENSE file in the project root for license information.

using System.Text.Json;
using System.Text.Json.Serialization;

namespace GraphZen.Playground.Internal;

public class PlaygroundHtmlWriter
{
    private static readonly Lazy<string> Html = new(() =>
    {
        var test = typeof(PlaygroundHtmlWriter).Assembly;
        using var stream = test.GetManifestResourceStream("GraphZen.Playground.Internal.Files.playground.html") ??
                           throw new InvalidOperationException(
                               "there was an error getting the playground.html file");
        using var reader = new StreamReader(stream);
        return reader.ReadToEnd();
    });

    private static JsonSerializerOptions SerializerOptions { get; } = new()
    {
        PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
        Converters = { new JsonStringEnumConverter(JsonNamingPolicy.CamelCase) },
        DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull,
        WriteIndented = true
    };


    public static string GetHtml(PlaygroundOptions? options = null)
    {
        if (options == null)
        {
            return Html.Value;
        }

        var html = Html.Value.Replace("var options = {};", $"var options = {GetPlaygroundOptionsJson(options)};");
        return html;
    }

    public static string GetPlaygroundOptionsJson(PlaygroundOptions options) =>
        JsonSerializer.Serialize(options, SerializerOptions);
}
