// Copyright (c) GraphZen LLC. All rights reserved.
// Licensed under the GraphZen Community License. See the LICENSE file in the project root for license information.

using System;
using System.Diagnostics.CodeAnalysis;
using GraphZen.Infrastructure;
using JetBrains.Annotations;
using Newtonsoft.Json.Linq;

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

            JToken Coerce(object val) =>
                val is string str ? JToken.Parse(str)
                : val is JToken jt ? jt
                : JToken.FromObject(val, Json.Serializer);

            var expectedToken = Coerce(expected);
            var actualToken = Coerce(actual);

            if (options.SortBeforeCompare)
            {
                expectedToken.Sort();
                actualToken.Sort();
            }

            var deepEquals = JToken.DeepEquals(expectedToken, actualToken);
            if (!deepEquals)
            {
                var expectedJson = Json.SerializeObject(expectedToken);
                var actualJson = Json.SerializeObject(actualToken);
                var diff = actualJson.GetDiff(expectedJson, options.StringDiffOptions);
                if (diff == null)
                {
                    return "a difference was detected, but there was an error calculating the difference";
                }

                return diff;
            }

            return null;
        }
    }
}