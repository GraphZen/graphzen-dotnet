// Copyright (c) GraphZen LLC. All rights reserved.
// Licensed under the GraphZen Community License. See the LICENSE file in the project root for license information.

using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.Text.Json;
using System.Text.Json.Nodes;
using System.Text.Json.Serialization;
using GraphZen.Infrastructure;
using JetBrains.Annotations;

namespace GraphZen.Infrastructure
{
    public static class Json
    {
        public static JsonSerializerOptions SerializerOptions { get; } = new JsonSerializerOptions
        {
            WriteIndented = true,
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
            DictionaryKeyPolicy = JsonNamingPolicy.CamelCase,
            PropertyNameCaseInsensitive = true,
            DefaultIgnoreCondition = JsonIgnoreCondition.Never
        };

        public static JsonNode? CreateJsonNode(object value)
        {
            var json = JsonSerializer.Serialize(value, SerializerOptions);
            return JsonNode.Parse(json);
        }

        [DebuggerStepThrough]
        public static string SerializeObject(object value)
        {
            var options = new JsonSerializerOptions(SerializerOptions)
            {
                WriteIndented = true
            };
            return JsonSerializer.Serialize(value, options);
        }
    }
}
