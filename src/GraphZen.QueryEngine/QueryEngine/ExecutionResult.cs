// Copyright (c) GraphZen LLC. All rights reserved.
// Licensed under the GraphZen Community License. See the LICENSE file in the project root for license information.

using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text.Json.Serialization;
using GraphZen.Infrastructure;
using JetBrains.Annotations;


namespace GraphZen.QueryEngine
{
    public class ExecutionResult
    {
        private readonly IReadOnlyList<GraphQLServerError> _errors;

        public ExecutionResult(IDictionary<string, object?>? data, IEnumerable<GraphQLServerError>? errors)
        {
            Data = data;
            _errors = errors?.ToArray() ?? new GraphQLServerError[] { };
        }

        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]

        public IDictionary<string, object?>? Data { get; }


        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public IReadOnlyList<GraphQLServerError>? Errors => _errors.Any() ? _errors : null;
    }
}
