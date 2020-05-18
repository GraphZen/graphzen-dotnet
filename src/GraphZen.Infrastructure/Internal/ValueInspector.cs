// Copyright (c) GraphZen LLC. All rights reserved.
// Licensed under the GraphZen Community License. See the LICENSE file in the project root for license information.

using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using GraphZen.Infrastructure;
using JetBrains.Annotations;
using Newtonsoft.Json.Linq;

namespace GraphZen.Internal
{
    internal static class ValueInspector
    {
        internal static string Inspect(this object? value, bool expanded = false)
        {
            switch (value)
            {
                case null:
                    return "null";
                case bool boolean:
                    return boolean ? "true" : "false";
                case Type type:
                    return type.Name;
                case IInspectable inspectable:
                    return inspectable.GetDisplayValue();
                case string str:
                    return $"\"{str}\"";
                case IDictionary<string, object> dict:
                    return "{" +
                           string.Join(", ", dict.Select(kv => $"{kv.Key}: {Inspect(kv.Value, expanded)}")) + "}";
                case IDictionary dict:
                    return "{\n" +
                           string.Join(",\n",
                               dict.GetEntries().Select(kv => $"  {{{kv.Key}: {Inspect(kv.Value, expanded)}}}")) +
                           "\n}";
                case JObject jObject:
                    return jObject.ToDictionary().Inspect(expanded);
                case IEnumerable enumerable:
                    var inspected = enumerable.Cast<object>().Select(_ => Inspect(_, expanded));
                    return expanded ? $"[\n{string.Join(",\n", inspected)}\n]" : $"[{string.Join(", ", inspected)}]";
                default:
                    return value.ToString() ?? "null";
            }
        }
    }
}