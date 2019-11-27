// Copyright (c) GraphZen LLC. All rights reserved.
// Licensed under the GraphZen Community License. See the LICENSE file in the project root for license information.

using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using GraphZen.Infrastructure;
using JetBrains.Annotations;

namespace GraphZen
{
    public abstract class GraphQLResponse
    {
        public abstract dynamic? Data { get; }
        public abstract T? GetData<T>() where T : class;
        public bool HasErrors => Errors.Any();
        public abstract IReadOnlyList<GraphQLError> Errors { get; }
    }
}