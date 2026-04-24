// Copyright (c) GraphZen LLC. All rights reserved.
// Licensed under the GraphZen Community License. See the LICENSE file in the project root for license information.

using System.Text.Json;
using System.Text.Json.Serialization;

namespace GraphZen.Internal;

public class MaybeJsonConverter : JsonConverter<object>
{
    public override bool CanConvert(Type typeToConvert) => typeof(Maybe<>).IsAssignableFrom(typeToConvert) ||
                                                           (typeToConvert.IsGenericType &&
                                                            typeToConvert.GetGenericTypeDefinition() ==
                                                            typeof(Maybe<>));

    public override object Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options) =>
        throw new NotImplementedException(
            "Unnecessary because CanConvert returns false for non-Maybe types. The type will skip the converter.");

    public override void Write(Utf8JsonWriter writer, object value, JsonSerializerOptions options)
    {
        throw new NotImplementedException();
    }
}
