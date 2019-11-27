// Copyright (c) GraphZen LLC. All rights reserved.
// Licensed under the GraphZen Community License. See the LICENSE file in the project root for license information.

using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using GraphZen.Infrastructure;
using JetBrains.Annotations;

namespace GraphZen.Infrastructure
{
    public static class EnumerableTestExtensions
    {
        public static string ToMultiLineString(this IEnumerable<string> values) =>
            string.Join(Environment.NewLine, values);
    }
}