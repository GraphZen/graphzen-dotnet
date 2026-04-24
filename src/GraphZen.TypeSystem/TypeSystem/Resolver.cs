// Copyright (c) GraphZen LLC. All rights reserved.
// Licensed under the GraphZen Community License. See the LICENSE file in the project root for license information.

namespace GraphZen.TypeSystem;

public delegate TResult Resolver<in TSource, out TResult>(TSource source, dynamic args,
    GraphQLContext context,
    ResolveInfo resolveInfo);