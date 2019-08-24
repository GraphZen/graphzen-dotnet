#nullable disable
// Copyright (c) GraphZen LLC. All rights reserved.
// Licensed under the GraphZen Community License. See the LICENSE file in the project root for license information.

using System.Diagnostics;
using System.IO;
using GraphZen.Infrastructure;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json.Serialization;

namespace GraphZen.Infrastructure
{
    public static class Json
    {
        [NotNull]
        public static JsonSerializerSettings SerializerSettings { get; } = new JsonSerializerSettings
        {
            Formatting = Formatting.Indented,
            ContractResolver = new CamelCasePropertyNamesContractResolver(),
            NullValueHandling = NullValueHandling.Include
        };

        [NotNull]
        // ReSharper disable once AssignNullToNotNullAttribute
        public static JsonSerializer Serializer { get; } = JsonSerializer.Create(SerializerSettings);

        public static JObject CreateJObject(object value) => JObject.FromObject(value, Serializer);

        [DebuggerStepThrough]
        public static string SerializeObject(object value)
        {
            using (var ms = new MemoryStream())
            using (var writer = new StreamWriter(ms))
            using (var jsonWriter = new JsonTextWriter(writer)
            {
                QuoteChar = '\''
            })
            {
                var ser = Serializer;
                ser.Serialize(jsonWriter, value);
                jsonWriter.Flush();

                ms.Position = 0;
                using (var sr = new StreamReader(ms))
                {
                    var str = sr.ReadToEnd();
                    return str;
                }
            }
        }
    }
}