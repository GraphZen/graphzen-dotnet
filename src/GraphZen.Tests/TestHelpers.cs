// Copyright (c) GraphZen LLC. All rights reserved.
// Licensed under the GraphZen Community License. See the LICENSE file in the project root for license information.
using JetBrains.Annotations;
#nullable disable


using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DiffPlex;
using DiffPlex.DiffBuilder;
using DiffPlex.DiffBuilder.Model;
using GraphZen.Infrastructure;
using GraphZen.QueryEngine;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Superpower.Model;
using Xunit;

namespace GraphZen
{
    public static class TestHelpers
    {
        public static IDictionary<string, object> ToDictionary(dynamic value)
        {
            var dict = value != null
                ? JsonExtensions.ToDictionary(JObject.FromObject(value, Json.Serializer))
                : new Dictionary<string, object>();
            return dict;
        }

        [DebuggerStepThrough]
        private static void AssertEqualsJson(string expectedJson, object actual, ResultComparisonOptions options)
        {
            var expected = JsonConvert.DeserializeObject<JObject>(expectedJson, Json.SerializerSettings);
            AssertEquals(expected, JObject.FromObject(actual, Json.Serializer), options);
        }

        public static string GetObjectDiff(object expected, object actual, ResultComparisonOptions options)
        {
            var expectedJson = Json.SerializeObject(expected);
            var actualJson = Json.SerializeObject(actual);
            return GetDiff(expectedJson, actualJson, options);
        }

        public static void AssertEquals(string expected, string actual, ResultComparisonOptions options = null)
        {
            options = options ?? new ResultComparisonOptions();
            if (!expected.Equals(actual))
            {
                if (TryGetDiff(expected, actual, out var diff, options))
                {
                    throw new Exception(diff);
                }

                expected = Json.SerializeObject(expected);
                actual = Json.SerializeObject(actual);

                if (TryGetDiff(expected, actual, out diff, options))
                {
                    throw new Exception(diff);
                }
            }
        }

        public static bool TryGetDiff(string expected, string actual, out string differences,
            ResultComparisonOptions options)
        {
            var diffStringBuilder = new StringBuilder();
            var diffBuilder = new InlineDiffBuilder(new Differ());
            var diff = diffBuilder.BuildDiffModel(actual, expected);

            foreach (var line in diff.Lines)
            {
                switch (line.Type)
                {
                    case ChangeType.Inserted:
                        diffStringBuilder.Append("+ ");
                        break;
                    case ChangeType.Deleted:
                        diffStringBuilder.Append("- ");
                        break;
                    default:
                        diffStringBuilder.Append(" ");
                        break;
                }

                diffStringBuilder.AppendLine(line.Text);
            }

            var hasDiff = diff.Lines.Any(_ => _.Type != ChangeType.Unchanged);

            var diffString = diffStringBuilder.ToString();
            if (!hasDiff)
            {
                var expectedDetail = Json.SerializeObject(expected);
                var actualDetail = Json.SerializeObject(actual);
                hasDiff = expectedDetail != actualDetail;
                diffString = $@"
                Expected: {expectedDetail}
                Actual:   {actualDetail}
                ".Dedent();
            }


            var errorMessage = hasDiff ? "Differences found " : "No differences found";
            errorMessage += "\n";
            if (options.ShowActual)
            {
                errorMessage += $"=== Actual ===\n{actual}\n";
            }

            if (options.ShowExpected)
            {
                errorMessage += $"=== Expected ===\n{expected}\n";
            }

            if (options.ShowDiffs)
            {
                errorMessage += $"=== Differences ===\n{diffString}\n";
            }

            differences = errorMessage;
            return hasDiff;
        }

        public static string GetDiff(string expected, string actual, ResultComparisonOptions options) =>
            TryGetDiff(expected, actual, out var differences, options) ? differences : null;

        private static void AssertEquals(JObject expected, JObject actual, ResultComparisonOptions options)
        {
            options = options ?? new ResultComparisonOptions();
            if (options.SortBeforeCompare)
            {
                Sort(expected);
                Sort(actual);
            }

            var deepEquals = JToken.DeepEquals(expected, actual);
            if (!deepEquals)
            {
                var errorMessage = GetObjectDiff(expected, actual, options);

                Assert.True(deepEquals, errorMessage);
            }
        }


        public static void AssertEqualsDynamic(dynamic expected, object actual,
            ResultComparisonOptions options = null)
        {
            AssertEquals(JObject.FromObject(expected, Json.Serializer), JObject.FromObject(actual, Json.Serializer),
                options);
        }


        public static async Task<ExecutionResult> ShouldEqual(this Task<ExecutionResult> result, dynamic expected,
            ResultComparisonOptions options = null)
        {
            var final = await result;
            AssertEqualsDynamic(expected, final, options);
            return final;
        }

        public static async Task<ExecutionResult> ShouldEqualJsonFile(this Task<ExecutionResult> result,
            string filePath, ResultComparisonOptions options = null)
        {
            var final = await result;
            var json = await File.ReadAllTextAsync(filePath);
            AssertEqualsJson(json, final, options);
            return final;
        }


        private static void Sort(this JObject jObj)
        {
            var props = jObj.Properties().ToList();
            foreach (var prop in props)
            {
                prop.Remove();
            }


            foreach (var prop in props.OrderByDescending(p => p.Name == "name").ThenBy(p => p.Name))
            {
                jObj.Add(prop);
                if (prop.Value is JObject objValue)
                {
                    Sort(objValue);
                }

                if (prop.Value is JArray arr)
                {
                    foreach (var el in arr)
                    {
                        if (el is JObject elObj)
                        {
                            Sort(elObj);
                        }
                    }


                    IComparable GetComparable(JToken jt)
                    {
                        if (jt is JObject jo)
                        {
                            var nameProp = jo.Properties().FirstOrDefault(_ => _.Name == "name");
                            if (nameProp?.Value is JValue jv)
                            {
                                return jv;
                            }
                        }

                        if (jt is JValue jtv)
                        {
                            return jtv;
                        }

                        return null;
                    }

                    var elList = arr.ToList();
                    elList.Sort((x, y) =>
                    {
                        var xc = GetComparable(x);
                        var yc = GetComparable(y);
                        if (xc != null && yc != null)
                        {
                            var result = xc.CompareTo(yc);

                            return result;
                        }


                        return 0;
                    });

                    //elList.Sort((x, y) =>
                    //{
                    //    var xc = GetFirstComparable(x);
                    //    var yc = GetFirstComparable(y);


                    //    if (xc != null && yc != null)
                    //    {
                    //        return xc.CompareTo(yc);
                    //    }


                    //    return 0;
                    //});


                    prop.Value = new JArray(elList);

                    // prop.Value = sortedArray;
                }
            }
        }


        public static void ThrowOnParserError<TKind, T>(this TokenListParserResult<TKind, T> result)
        {
            if (!result.HasValue)
            {
                throw new Exception(result.ToString());
            }
        }

        public static string ToMultiLineString(this IEnumerable<string> values) =>
            string.Join(Environment.NewLine, values);
    }
}