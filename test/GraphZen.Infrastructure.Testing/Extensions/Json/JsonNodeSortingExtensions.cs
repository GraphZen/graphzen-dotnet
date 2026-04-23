// Copyright (c) GraphZen LLC. All rights reserved.
// Licensed under the GraphZen Community License. See the LICENSE file in the project root for license information.

using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text.Json.Nodes;
using GraphZen.Infrastructure;
using JetBrains.Annotations;

namespace GraphZen.Infrastructure
{
    public static class JsonNodeSortingExtensions
    {
        public static void Sort(this JsonNode? jsonNode)
        {
            switch (jsonNode)
            {
                case JsonObject jsonObject:
                    jsonObject.SortProperties();
                    break;
                case JsonArray jsonArray:
                    jsonArray.SortArray();
                    break;
            }
        }

        private static void SortProperties(this JsonObject jsonObj)
        {
            var props = jsonObj.ToList();
            foreach (var prop in props)
            {
                jsonObj.Remove(prop.Key);
            }

            foreach (var prop in props.OrderByDescending(p => p.Key == "name").ThenBy(p => p.Key))
            {
                jsonObj.Add(prop.Key, prop.Value);
                prop.Value?.Sort();
            }
        }

        private static void SortArray(this JsonArray arr)
        {
            foreach (var el in arr)
            {
                if (el is JsonObject elObj)
                    elObj.SortProperties();
            }

            string? GetComparable(JsonNode? jn)
            {
                if (jn is JsonObject jo && jo.ContainsKey("name"))
                {
                    var nameNode = jo["name"];
                    if (nameNode is JsonValue jv)
                        return jv.ToString();
                }

                if (jn is JsonValue jtv)
                    return jtv.ToString();

                return null;
            }

            var elements = arr.ToList();
            // Detach all elements before re-adding
            arr.Clear();

            elements.Sort((x, y) =>
            {
                var xc = GetComparable(x);
                var yc = GetComparable(y);
                if (xc != null && yc != null)
                {
                    return string.Compare(xc, yc, StringComparison.Ordinal);
                }

                return 0;
            });

            foreach (var el in elements)
            {
                arr.Add(el);
            }
        }
    }
}
