// Copyright (c) GraphZen LLC. All rights reserved.
// Licensed under the GraphZen Community License. See the LICENSE file in the project root for license information.

using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text.Json;
using System.Text.Json.Serialization;
using GraphZen.Infrastructure;
using JetBrains.Annotations;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json.Serialization;
using Check = GraphZen.Infrastructure.Check;
using JsonSerializer = System.Text.Json.JsonSerializer;

namespace GraphZen.Internal
{

    public static class GraphQLJsonSerializer
    {
        private static ImmutableList<GraphQLError> EmptyErrors { get; } = ImmutableList.Create<GraphQLError>();

        private static JsonSerializerOptions SerializerOptions { get; } = new JsonSerializerOptions()
        {
            Converters = { new JsonStringEnumConverter() },
            IgnoreNullValues = false,
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase
        };



        private class GraphQLErrorsJson
        {
            public List<GraphQLErrorJson?>? Errors { get; set; }

            public ImmutableList<GraphQLError> GetErrors() =>
                Errors?.Select(_ => _?.ToGraphQLError()).Where(_ => _ != null).Select(_ => _!).ToImmutableList()
                ?? EmptyErrors;
        }

        private class GraphQLErrorJson
        {
            public string? Message { get; set; }

            public GraphQLError? ToGraphQLError() => Message != null ? new GraphQLError(this.Message) : null;
        }




        public static IReadOnlyList<GraphQLError> ParseErrors(string json)
        {
            Check.NotNull(json, nameof(json));
            var result = JsonSerializer.Deserialize<GraphQLErrorsJson?>(json, SerializerOptions);
            return result?.GetErrors() ?? EmptyErrors;
        }

        private class GraphQLDataJson<T> where T : class
        {
            public T? Data { get; set; }
        }

        private static JsonSerializerSettings NewtonsoftSerializerSettings { get; } = new JsonSerializerSettings
        {
            ContractResolver = new CamelCasePropertyNamesContractResolver(),
        };

        public static T? ParseData<T>(string json) where T : class
        {
            Check.NotNull(json, nameof(json));
            var result = JsonConvert.DeserializeObject<GraphQLDataJson<T>>(json, NewtonsoftSerializerSettings);
            return result?.Data;
        }

        public static dynamic? ParseData(string json) => ParseData<JObject>(json);
    }
}