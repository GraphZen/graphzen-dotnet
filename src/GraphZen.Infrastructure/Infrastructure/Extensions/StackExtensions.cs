// Copyright (c) GraphZen LLC. All rights reserved.
// Licensed under the GraphZen Community License. See the LICENSE file in the project root for license information.

using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using GraphZen.Infrastructure;
using JetBrains.Annotations;

#nullable enable


namespace GraphZen.Infrastructure
{
    internal static class StackExtensions
    {
        public static T PeekOrDefault<T>(this Stack<T> stack) =>
            Check.NotNull(stack, nameof(stack)).Count > 0 ? stack.Peek() : default;
    }
}