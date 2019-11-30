// Copyright (c) GraphZen LLC. All rights reserved.
// Licensed under the GraphZen Community License. See the LICENSE file in the project root for license information.

using System.Diagnostics.CodeAnalysis;
using GraphZen.Infrastructure;
using JetBrains.Annotations;

namespace GraphZen
{
    public class GraphQLRequest
    {
        public string? OperationName { get; set; }
        public string? Query { get; set; }
        public object? Variables { get; set; }
    }
}