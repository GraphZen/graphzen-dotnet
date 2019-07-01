// Copyright (c) GraphZen LLC. All rights reserved.
// Licensed under the GraphZen Community License. See the LICENSE file in the project root for license information.

using GraphZen.Infrastructure;
using JetBrains.Annotations;

namespace GraphZen.Types
{
    public delegate string TypeResolver<in TSource, in TContext>(TSource value, TContext context,
        ResolveInfo info) where TContext : GraphQLContext;

    public delegate IGraphQLType TypeResolver(IGraphQLTypeReference typeReference);
}