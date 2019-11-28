// Copyright (c) GraphZen LLC. All rights reserved.
// Licensed under the GraphZen Community License. See the LICENSE file in the project root for license information.

using System;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using GraphZen.Infrastructure;
using JetBrains.Annotations;
using Newtonsoft.Json.Linq;

namespace GraphZen.Infrastructure
{
    public static class JTokenSortingExtensions
    {
        public static void Sort(this JToken jToken)
        {
            if (jToken is JObject jObject) jObject.Sort();
            if (jToken is JArray jArr) jArr.Sort();
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
                prop.Value.Sort();
            }
        }

        private static void Sort(this JArray arr)
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

            arr.Clear();
            elList.ForEach(arr.Add);
        }


    }
}