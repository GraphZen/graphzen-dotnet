// Copyright (c) GraphZen LLC. All rights reserved.
// Licensed under the GraphZen Community License. See the LICENSE file in the project root for license information.

using System.Collections.Immutable;
using System.Text.Json;
using System.Text.Json.Serialization;
using GraphZen.Infrastructure;

namespace GraphZen.Internal;

public static class GraphQLJsonSerializer
{
    private static JsonSerializerOptions SerializerOptions { get; } = new()
    {
        Converters = { new JsonStringEnumConverter() },
        DefaultIgnoreCondition = JsonIgnoreCondition.Never,
        PropertyNamingPolicy = JsonNamingPolicy.CamelCase
    };

    private static ImmutableList<GraphQLError> EmptyErrors { get; } = ImmutableList.Create<GraphQLError>();

    public static T? ParseData<T>(string json) where T : class
    {
        Check.NotNull(json, nameof(json));
        var result = JsonSerializer.Deserialize<GraphQLDataJson<T>>(json, SerializerOptions);
        return result?.Data;
    }

    public static dynamic? ParseData(string json)
    {
        Check.NotNull(json, nameof(json));
        using var doc = JsonDocument.Parse(json);
        if (doc.RootElement.ValueKind == JsonValueKind.Object &&
            doc.RootElement.TryGetProperty("data", out var dataElement))
        {
            if (dataElement.ValueKind == JsonValueKind.Null)
                return null;
            return ConvertJsonElement(dataElement);
        }

        return null;
    }

    private static object? ConvertJsonElement(JsonElement element)
    {
        switch (element.ValueKind)
        {
            case JsonValueKind.Object:
                var dict = new DynamicDictionary();
                foreach (var prop in element.EnumerateObject())
                {
                    dict[prop.Name] = ConvertJsonElement(prop.Value)!;
                }

                return dict;
            case JsonValueKind.Array:
                var list = new List<object?>();
                foreach (var item in element.EnumerateArray())
                {
                    list.Add(ConvertJsonElement(item));
                }

                return list;
            case JsonValueKind.String:
                return element.GetString();
            case JsonValueKind.Number:
                if (element.TryGetInt64(out var longVal))
                    return longVal;
                return element.GetDouble();
            case JsonValueKind.True:
                return true;
            case JsonValueKind.False:
                return false;
            case JsonValueKind.Null:
                return null;
            default:
                return element.ToString();
        }
    }

    public static IReadOnlyList<GraphQLError> ParseErrors(string json)
    {
        Check.NotNull(json, nameof(json));
        var result = JsonSerializer.Deserialize<GraphQLErrorsJson?>(json, SerializerOptions);
        return result?.GetErrors() ?? EmptyErrors;
    }


    private class GraphQLErrorsJson
    {
        // ReSharper disable once CollectionNeverUpdated.Local
        // ReSharper disable once UnusedAutoPropertyAccessor.Local
        public List<GraphQLErrorJson?>? Errors { get; set; }

        public ImmutableList<GraphQLError> GetErrors() =>
            Errors?.Select(_ => _?.ToGraphQLError()).Where(_ => _ != null).Select(_ => _!).ToImmutableList()
            ?? EmptyErrors;
    }

    private class GraphQLErrorJson
    {
        // ReSharper disable once UnusedAutoPropertyAccessor.Local
        public string? Message { get; set; }

        public GraphQLError? ToGraphQLError() => Message != null ? new GraphQLError(Message) : null;
    }


    private class GraphQLDataJson<T> where T : class
    {
        // ReSharper disable once UnusedAutoPropertyAccessor.Local
        public T? Data { get; set; }
    }
}