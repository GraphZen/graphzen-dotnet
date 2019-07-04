// Copyright (c) GraphZen LLC. All rights reserved.
// Licensed under the GraphZen Community License. See the LICENSE file in the project root for license information.

using System.Diagnostics;
using GraphZen.Infrastructure;

namespace GraphZen.Infrastructure
{

    public static class AccessorExtensions
    {
        [DebuggerStepThrough]
        [NotNull]
        public static T GetInfrastructure<T>([NotNull] this IInfrastructure<T> accessor)
            => Check.NotNull(accessor, nameof(accessor)).Instance;
    }
}