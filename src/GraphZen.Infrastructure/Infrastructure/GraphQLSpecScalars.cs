// Copyright (c) GraphZen LLC. All rights reserved.
// Licensed under the GraphZen Community License. See the LICENSE file in the project root for license information.

using System.Collections.Immutable;
using System.Diagnostics.CodeAnalysis;
using GraphZen.Infrastructure;
using JetBrains.Annotations;

namespace GraphZen.Infrastructure
{
    public static class GraphQLSpecScalars
    {
        public const string String = nameof(String);
        public const string Int = nameof(Int);
        public const string Float = nameof(Float);
        public const string Boolean = nameof(Boolean);
        public const string ID = nameof(ID);

        private static ImmutableHashSet<string> All { get; } =
            ImmutableHashSet.Create(String, Int, Float, Boolean, ID);

        public static bool IsSpecScalar(this string value) => All.Contains(value);
    }
}