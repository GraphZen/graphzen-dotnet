#nullable disable
// Copyright (c) GraphZen LLC. All rights reserved.
// Licensed under the GraphZen Community License. See the LICENSE file in the project root for license information.

using GraphZen.Infrastructure;
using GraphZen.TypeSystem.Internal;
using GraphZen.TypeSystem.Taxonomy;

namespace GraphZen.TypeSystem
{
    public delegate string TypeResolver<in TSource, in TContext>(TSource value, TContext context,
        ResolveInfo info) where TContext : GraphQLContext;

    public delegate IGraphQLType TypeResolver(IGraphQLTypeReference typeReference);
}