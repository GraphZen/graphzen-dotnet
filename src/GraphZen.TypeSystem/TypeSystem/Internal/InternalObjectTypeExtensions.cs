// Copyright (c) GraphZen LLC. All rights reserved.
// Licensed under the GraphZen Community License. See the LICENSE file in the project root for license information.

using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Threading.Tasks;
using GraphZen.Infrastructure;
using JetBrains.Annotations;

namespace GraphZen.TypeSystem.Internal
{
    public static class InternalObjectTypeExtensions
    {
        public static async Task<bool> IsTypeOfAsync(this ObjectType objectType, object source,
            GraphQLContext context, ResolveInfo info)
        {
            object? src = source;
            if (source is Task awaitable)
            {
                await awaitable;
                src = awaitable.GetResult();
            }

            Debug.Assert(objectType.IsTypeOf != null, "objectType.IsTypeOf != null");
            return objectType.IsTypeOf(src, context, info);
        }
    }
}