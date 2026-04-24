// Copyright (c) GraphZen LLC. All rights reserved.
// Licensed under the GraphZen Community License. See the LICENSE file in the project root for license information.

using System.Diagnostics;

namespace GraphZen.Infrastructure;

public static class AccessorExtensions
{
    [DebuggerStepThrough]
    public static T GetInfrastructure<T>(this IInfrastructure<T> accessor) =>
        Check.NotNull(accessor, nameof(accessor)).Instance;
}
