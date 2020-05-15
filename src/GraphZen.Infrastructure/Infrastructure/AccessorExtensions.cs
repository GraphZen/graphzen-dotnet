    // Copyright (c) GraphZen LLC. All rights reserved.
// Licensed under the GraphZen Community License. See the LICENSE file in the project root for license information.

using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using GraphZen.Infrastructure;
using JetBrains.Annotations;

#nullable enable


namespace GraphZen.Infrastructure
{
    public static class AccessorExtensions
    {
        [DebuggerStepThrough]
        public static T GetInfrastructure<T>(this IInfrastructure<T> accessor) =>
            Check.NotNull(accessor, nameof(accessor)).Instance;
    }
}