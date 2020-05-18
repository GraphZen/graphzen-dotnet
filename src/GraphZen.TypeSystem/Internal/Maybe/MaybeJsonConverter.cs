// Copyright (c) GraphZen LLC. All rights reserved.
// Licensed under the GraphZen Community License. See the LICENSE file in the project root for license information.

using System;
using System.Diagnostics.CodeAnalysis;
using GraphZen.Infrastructure;
using JetBrains.Annotations;
using Newtonsoft.Json;


namespace GraphZen.Internal
{
    public class MaybeJsonConverter : JsonConverter
    {
        public override bool CanRead => false;

        public override void WriteJson(JsonWriter writer, object? value, JsonSerializer serializer)
        {
            throw new NotImplementedException();
        }

        public override object ReadJson(JsonReader reader, Type objectType, object? existingValue,
            JsonSerializer serializer) =>
            throw new NotImplementedException(
                "Unnecessary because CanRead is false. The type will skip the converter.");

        public override bool CanConvert(Type objectType) => true;
    }
}