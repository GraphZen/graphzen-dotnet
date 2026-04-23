// Copyright (c) GraphZen LLC. All rights reserved.
// Licensed under the GraphZen Community License. See the LICENSE file in the project root for license information.

using System;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text.Json;
using System.Text.Json.Nodes;
using GraphZen.Infrastructure;
using JetBrains.Annotations;

namespace GraphZen.Infrastructure
{
    public static class JsonDiffer
    {
        public static string? GetDiff(object actual, object expected,
            Action<JsonDiffOptions>? optionsAction = null)
        {
            var options = JsonDiffOptions.FromOptionsAction(optionsAction);
            return GetDiff(actual, expected, options);
        }

        public static string? GetDiff(object actual, object expected,
            JsonDiffOptions? options)
        {
            options = options ?? new JsonDiffOptions();

            JsonNode? Coerce(object val) =>
                val is string str ? JsonNode.Parse(str)
                : val is JsonNode jn ? jn
                : JsonNode.Parse(JsonSerializer.Serialize(val, Json.SerializerOptions));

            var expectedNode = Coerce(expected);
            var actualNode = Coerce(actual);

            if (options.SortBeforeCompare)
            {
                expectedNode?.Sort();
                actualNode?.Sort();
            }

            if (!JsonNodesEqual(expectedNode, actualNode))
            {
                var expectedFormatted = Json.SerializeObject(expected);
                var actualFormatted = Json.SerializeObject(actual);
                var diff = actualFormatted.GetDiff(expectedFormatted, options.StringDiffOptions);
                if (diff == null) return "a difference was detected, but there was an error calculating the difference";
                return diff;
            }

            return null;
        }

        private static bool JsonNodesEqual(JsonNode? a, JsonNode? b)
        {
            if (a is null && b is null) return true;
            if (a is null || b is null) return false;

            if (a is JsonObject objA && b is JsonObject objB)
            {
                if (objA.Count != objB.Count) return false;
                foreach (var prop in objA)
                {
                    if (!objB.TryGetPropertyValue(prop.Key, out var bVal))
                        return false;
                    if (!JsonNodesEqual(prop.Value, bVal))
                        return false;
                }
                return true;
            }

            if (a is JsonArray arrA && b is JsonArray arrB)
            {
                if (arrA.Count != arrB.Count) return false;
                return arrA.Zip(arrB).All(pair => JsonNodesEqual(pair.First, pair.Second));
            }

            if (a is JsonValue valA && b is JsonValue valB)
            {
                return valA.ToJsonString() == valB.ToJsonString();
            }

            return false;
        }
    }
}
