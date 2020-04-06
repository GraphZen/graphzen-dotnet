// Copyright (c) GraphZen LLC. All rights reserved.
// Licensed under the GraphZen Community License. See the LICENSE file in the project root for license information.

using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using GraphZen.Infrastructure;
using JetBrains.Annotations;
using Newtonsoft.Json.Linq;

namespace GraphZen.Infrastructure
{
    public static class JObjectExtensions
    {
        public static IDictionary<string, object> ToDictionary(this JObject jObject)
        {
            Check.NotNull(jObject, nameof(jObject));
            var result = jObject.ToObject<Dictionary<string, object>>() ??
                         throw new Exception("Unable to convert JObject to Dictionary<string, object>");

            var objectEntries = (from r in result
                where r.Value?.GetType() == typeof(JObject)
                let objectKey = r.Key
                let objectValue = (JObject) r.Value
                select (objectKey, objectValue)).ToList();

            var arrayEntries = (from r in result
                where r.Value?.GetType() == typeof(JArray)
                let arrayKey = r.Key
                let arrayValue = (JArray) r.Value
                select (arrayKey, arrayValue)).ToList();


            foreach (var (arrayKey, arrayValue) in arrayEntries)
            {
                result[arrayKey] = arrayValue.Children().Select(v =>
                {
                    switch (v)
                    {
                        case JValue jv:
                            return jv.Value;
                        case JObject jo:
                            return jo.ToDictionary();
                    }

                    throw new NotImplementedException($"unsupported type {v?.GetType()}");
                }).ToArray();
            }

            foreach (var (objectKey, objectValue) in objectEntries)
            {
                result[objectKey] = objectValue.ToDictionary();
            }

            return result;
        }
    }
}