// Copyright (c) GraphZen LLC. All rights reserved.
// Licensed under the GraphZen Community License. See the LICENSE file in the project root for license information.

using System.Text.Json;
using System.Text.Json.Nodes;

namespace GraphZen.Infrastructure;

public static class JsonNodeExtensions
{
    public static IDictionary<string, object> ToDictionary(this JsonObject jsonObject)
    {
        Check.NotNull(jsonObject, nameof(jsonObject));
        var result = new Dictionary<string, object>();

        foreach (var property in jsonObject)
        {
            result[property.Key] = ConvertNode(property.Value);
        }

        return result;
    }

    public static IDictionary<string, object> ObjectToDictionary(object value)
    {
        var json = JsonSerializer.Serialize(value, Json.SerializerOptions);
        var node = JsonNode.Parse(json);
        if (node is JsonObject jsonObject)
        {
            return jsonObject.ToDictionary();
        }

        throw new Exception("Unable to convert object to Dictionary<string, object>");
    }

    private static object ConvertNode(JsonNode? node)
    {
        switch (node)
        {
            case null:
                return null!;
            case JsonObject obj:
                return obj.ToDictionary();
            case JsonArray arr:
                return arr.Select(ConvertNode).ToArray();
            case JsonValue val:
                return ConvertJsonValue(val);
            default:
                throw new NotImplementedException($"unsupported type {node.GetType()}");
        }
    }

    private static object ConvertJsonValue(JsonValue value)
    {
        var element = value.GetValue<JsonElement>();
        switch (element.ValueKind)
        {
            case JsonValueKind.String:
                return element.GetString()!;
            case JsonValueKind.Number:
                if (element.TryGetInt32(out var intVal))
                {
                    return intVal;
                }

                if (element.TryGetInt64(out var longVal))
                {
                    return longVal;
                }

                return element.GetDouble();
            case JsonValueKind.True:
                return true;
            case JsonValueKind.False:
                return false;
            case JsonValueKind.Null:
                return null!;
            default:
                return element.ToString();
        }
    }
}
