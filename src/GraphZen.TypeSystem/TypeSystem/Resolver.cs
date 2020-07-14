// Copyright (c) GraphZen LLC. All rights reserved.
// Licensed under the GraphZen Community License. See the LICENSE file in the project root for license information.

using System.Diagnostics.CodeAnalysis;
using GraphZen.Infrastructure;
using GraphZen.TypeSystem.Internal;
using JetBrains.Annotations;

namespace GraphZen.TypeSystem
{
    public delegate TResult Resolver<in TSource, out TResult>(TSource source, dynamic args, GraphQLContext context, ResolveInfo resolveInfo);
    public delegate TResult Resolver<in TSource, in TArgs, in TContext, out TResult>(TSource source, TArgs args, TContext context, ResolveInfo resolveInfo) where TContext : GraphQLContext;
}