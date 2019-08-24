#nullable disable
// Copyright (c) GraphZen LLC. All rights reserved.
// Licensed under the GraphZen Community License. See the LICENSE file in the project root for license information.

using System.Collections.Concurrent;
using System.Collections.Generic;
using GraphZen.Infrastructure;

namespace GraphZen.Infrastructure
{
    internal static class ConcurrentBagExtensions
    {
        public static void AddRange<T>([NotNull] this ConcurrentBag<T> bag, [NotNull] IEnumerable<T> items)
        {
            foreach (var item in items)
            {
                bag.Add(item);
            }
        }
    }
}