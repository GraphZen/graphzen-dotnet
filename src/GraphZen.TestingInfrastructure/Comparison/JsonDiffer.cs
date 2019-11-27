// Copyright (c) GraphZen LLC. All rights reserved.
// Licensed under the GraphZen Community License. See the LICENSE file in the project root for license information.

using System;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using GraphZen.Infrastructure;
using JetBrains.Annotations;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace GraphZen
{
    public static class JsonDiffer
    {
        public static (bool isEqual, string? objectDiff) Compare(string expectedJson, object actual,
            ResultComparisonOptions? options = null)
        {
            var expected = JsonConvert.DeserializeObject<JObject>(expectedJson, Json.SerializerSettings);
            if (expected == null) throw new Exception("Could not deserialize expected JSON for comparison");
            return Compare(expected, actual, options);
        }

        public static (bool isEqual, string? objectDiff) Compare(object expected, object actual,
            ResultComparisonOptions? options = null)
        {
            options = options ?? new ResultComparisonOptions();
            var expectedJObject = expected is JObject ejo ? ejo : JObject.FromObject(expected, Json.Serializer);
            var actualJObject = actual is JObject ajo ? ajo : JObject.FromObject(expected, Json.Serializer);

            if (options.SortBeforeCompare)
            {
                Sort(expectedJObject);
                Sort(actualJObject);
            }

            var deepEquals = JToken.DeepEquals(expectedJObject, actualJObject);
            if (!deepEquals)
            {
                var objectDiff = GetDiff(expectedJObject, actualJObject, options);
                return (false, objectDiff);
            }

            return (true, null);
        }

        private static void Sort(JObject jObj)
        {
            var props = jObj.Properties().ToList();
            foreach (var prop in props)
            {
                prop.Remove();
            }

            foreach (var prop in props.OrderByDescending(p => p.Name == "name").ThenBy(p => p.Name))
            {
                jObj.Add(prop);
                if (prop.Value is JObject objValue) Sort(objValue);

                if (prop.Value is JArray arr)
                {
                    foreach (var el in arr)
                    {
                        if (el is JObject elObj)
                            Sort(elObj);
                    }


                    IComparable? GetComparable(JToken jt)
                    {
                        if (jt is JObject jo)
                        {
                            var nameProp = jo.Properties().FirstOrDefault(_ => _.Name == "name");
                            if (nameProp?.Value is JValue jv) return jv;
                        }

                        if (jt is JValue jtv) return jtv;

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

        public static string? GetDiff(object expected, object actual, ResultComparisonOptions? options = null)
        {
            var expectedJson = Json.SerializeObject(expected);
            var actualJson = Json.SerializeObject(actual);
            return StringDiffer.GetDiff(expectedJson, actualJson, options);
        }
    }
}