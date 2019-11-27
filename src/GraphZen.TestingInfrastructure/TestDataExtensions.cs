// Copyright (c) GraphZen LLC. All rights reserved.
// Licensed under the GraphZen Community License. See the LICENSE file in the project root for license information.

using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using GraphZen.Infrastructure;
using JetBrains.Annotations;

namespace GraphZen
{
    public static class TestDataExtensions
    {
        /// <summary>
        ///     Wraps source items in a single member object array for use in XUnit test fixture parameters.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="source"></param>
        /// <returns></returns>
        public static IEnumerable<object[]> ToTestData<T>(this IEnumerable<T> source) =>
            source.Select(_ => new object[] {_});
    }
}