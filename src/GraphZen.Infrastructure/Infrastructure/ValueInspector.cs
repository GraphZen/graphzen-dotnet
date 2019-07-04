// Copyright (c) GraphZen LLC. All rights reserved.
// Licensed under the GraphZen Community License. See the LICENSE file in the project root for license information.

using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using GraphZen.Infrastructure;
using GraphZen.Infrastructure.Extensions;
using Newtonsoft.Json.Linq;

namespace GraphZen.Infrastructure
{
    internal static class ValueInspector
    {
        public static T Dump<T, TR>(this T value, [NotNull] Func<T, TR> selector, string prefix = null)
        {
            selector(value).Dump(prefix);
            return value;
        }

        public static T Dump<T>(this T value, string label = "_", bool expanded = false)
        {
            if (expanded)
            {
                Console.WriteLine($"= {label} =\n{value.Inspect(true)}");
            }
            else
            {
                Console.WriteLine($"\t\t{label} \t\t-> {value.Inspect()}");
            }

            return value;
        }


        internal static string Inspect(this object value, bool expanded = false)
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
                    return value.ToString();
            }
        }
    }
}