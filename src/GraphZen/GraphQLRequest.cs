#nullable disable
// Copyright (c) GraphZen LLC. All rights reserved.
// Licensed under the GraphZen Community License. See the LICENSE file in the project root for license information.

using System.Collections.Generic;
using GraphZen.Infrastructure;

namespace GraphZen
{
    internal class GraphQLRequest
    {
        public string OperationName { get; set; }
        public string Query { get; set; }
        public IDictionary<string, object> Variables { get; set; }
    }
}