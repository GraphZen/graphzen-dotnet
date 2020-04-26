// Copyright (c) GraphZen LLC. All rights reserved.
// Licensed under the GraphZen Community License. See the LICENSE file in the project root for license information.

using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using GraphZen.Infrastructure;
using GraphZen.Internal;
using JetBrains.Annotations;

namespace GraphZen
{
    public class GraphQLResponse
    {
        private readonly Lazy<object?> _data;
        private readonly Dictionary<Type, object?> _typedData = new Dictionary<Type, object?>();
        private readonly Lazy<IReadOnlyList<GraphQLError>> _errors;
        private readonly string _jsonResponse;

        public GraphQLResponse(string jsonResponse)
        {
            _jsonResponse = jsonResponse;
            _data = new Lazy<object?>(() => GraphQLJsonSerializer.ParseData(jsonResponse));
            _errors = new Lazy<IReadOnlyList<GraphQLError>>(() => GraphQLJsonSerializer.ParseErrors(jsonResponse));
        }

        public IReadOnlyList<GraphQLError> Errors => _errors.Value;

        public dynamic? GetData() => _data.Value;

        public T? GetData<T>() where T : class
        {
            if (_typedData.TryGetValue(typeof(T), out var data))
            {
                return (T?)data;
            }

            var parsed = GraphQLJsonSerializer.ParseData<T>(_jsonResponse);
            _typedData[typeof(T)] = parsed;
            return parsed;
        }
    }
}