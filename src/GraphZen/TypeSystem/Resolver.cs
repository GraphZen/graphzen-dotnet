// Copyright (c) GraphZen LLC. All rights reserved.
// Licensed under the GraphZen Community License. See the LICENSE file in the project root for license information.

using GraphZen.Infrastructure;
using JetBrains.Annotations;

namespace GraphZen.TypeSystem
{
    public delegate TResult Resolver<in TSource, out TResult>(TSource source, [NotNull] dynamic args,
        [NotNull] GraphQLContext context,
        [NotNull] ResolveInfo resolveInfo);
}