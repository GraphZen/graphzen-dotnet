// Copyright (c) GraphZen LLC. All rights reserved.
// Licensed under the GraphZen Community License. See the LICENSE file in the project root for license information.
using JetBrains.Annotations;
#nullable disable


using System.Collections.Generic;
using System.Linq;
using GraphZen.Infrastructure;
using Newtonsoft.Json;

namespace GraphZen.QueryEngine
{
    public class ExecutionResult
    {
         private readonly IReadOnlyList<GraphQLError> _errors;

        public ExecutionResult(IDictionary<string, object> data, IEnumerable<GraphQLError> errors)
        {
            Data = data;
            _errors = errors?.ToArray() ?? new GraphQLError[] { };
        }

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        
        public IDictionary<string, object> Data { get; }


        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public IReadOnlyList<GraphQLError> Errors => _errors.Any() ? _errors : null;
    }
}