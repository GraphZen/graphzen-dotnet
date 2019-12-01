﻿// Copyright (c) GraphZen LLC. All rights reserved.
// Licensed under the GraphZen Community License. See the LICENSE file in the project root for license information.

using System.Collections.Generic;
using System.Collections.Immutable;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text.Json;
using System.Text.Json.Serialization;
using GraphZen.Infrastructure;
using JetBrains.Annotations;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using JsonSerializer = System.Text.Json.JsonSerializer;

namespace GraphZen.Internal
{
    public static class GraphQLJsonSerializer
    {
        private static JsonSerializerOptions SerializerOptions { get; } = new JsonSerializerOptions
        {
            Converters = { new JsonStringEnumConverter() },
            IgnoreNullValues = false,
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase
        };

        private static JsonSerializerSettings NewtonsoftSerializerSettings { get; } = new JsonSerializerSettings
        {
            ContractResolver = new CamelCasePropertyNamesContractResolver()
        };

        private static ImmutableList<GraphQLError> EmptyErrors { get; } = ImmutableList.Create<GraphQLError>();

        public static T? ParseData<T>(string json) where T : class
        {
            Check.NotNull(json, nameof(json));
            var result = JsonConvert.DeserializeObject<GraphQLDataJson<T>>(json, NewtonsoftSerializerSettings);
            return result?.Data;
        }

        public static dynamic? ParseData(string json)
        {
            Check.NotNull(json, nameof(json));
            var expando = JsonConvert.DeserializeObject<DynamicDictionary>(json);
            if (expando != null)
                return ((IDictionary<string, object>)expando).TryGetValue("data", out var data) ? data : null;

            return null;
        }

        public static IReadOnlyList<GraphQLError> ParseErrors(string json)
        {
            Check.NotNull(json, nameof(json));
            var result = JsonSerializer.Deserialize<GraphQLErrorsJson?>(json, SerializerOptions);
            return result?.GetErrors() ?? EmptyErrors;
        }


        private class GraphQLErrorsJson
        {
            // ReSharper disable once CollectionNeverUpdated.Local
            // ReSharper disable once UnusedAutoPropertyAccessor.Local
            public List<GraphQLErrorJson?>? Errors { get; set; }

            public ImmutableList<GraphQLError> GetErrors() =>
                Errors?.Select(_ => _?.ToGraphQLError()).Where(_ => _ != null).Select(_ => _!).ToImmutableList()
                ?? EmptyErrors;
        }

        private class GraphQLErrorJson
        {
            // ReSharper disable once UnusedAutoPropertyAccessor.Local
            public string? Message { get; set; }

            public GraphQLError? ToGraphQLError() => Message != null ? new GraphQLError(Message) : null;
        }


        private class GraphQLDataJson<T> where T : class
        {
            // ReSharper disable once UnusedAutoPropertyAccessor.Local
            public T? Data { get; set; }
        }
    }
}