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
        public static string? GetDiff(object expected, object actual,
            Action<ResultComparisonOptions>? comparisonOptionsAction = null)
        {
            var options = ResultComparisonOptions.FromOptionsAction(comparisonOptionsAction);
            return GetDiff(expected, actual, options);
        }


        public static string? GetDiff(object expected, object actual,
            ResultComparisonOptions? options)
        {
            options = options ?? new ResultComparisonOptions();
            var expectedToken = expected is JToken ejc ? ejc : JToken.FromObject(expected, Json.Serializer);
            var actualToken = actual is JToken ajc ? ajc : JToken.FromObject(actual, Json.Serializer);

            if (options.SortBeforeCompare)
            {
                expectedToken.Sort();
                actualToken.Sort();
            }

            var deepEquals = JToken.DeepEquals(expectedToken, actualToken);
            if (!deepEquals)
            {
                var expectedJson = Json.SerializeObject(expected);
                var actualJson = Json.SerializeObject(actual);
                var diff = StringDiffer.GetDiff(expectedJson, actualJson, options);
                if (diff == null) return "a difference was detected, but there was an error calculating the difference";
                return diff;
            }

            return null;
        }
    }
}