// Copyright (c) GraphZen LLC. All rights reserved.
// Licensed under the GraphZen Community License. See the LICENSE file in the project root for license information.

using System.Diagnostics.CodeAnalysis;
using System.Threading.Tasks;
using GraphZen.Infrastructure;
using JetBrains.Annotations;

namespace GraphZen
{
    public interface IGraphQLClient
    {
        Task<GraphQLResponse> SendAsync(GraphQLRequest request);
    }
}