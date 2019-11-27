// Copyright (c) GraphZen LLC. All rights reserved.
// Licensed under the GraphZen Community License. See the LICENSE file in the project root for license information.

using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.Threading.Tasks;
using GraphZen.Infrastructure;
using GraphZen.QueryEngine;
using JetBrains.Annotations;
using Xunit;

namespace GraphZen
{
    public static class QueryEngineTestHelpers
    {
        public static async Task<ExecutionResult> ShouldEqual(this Task<ExecutionResult> result, dynamic expected,
            ResultComparisonOptions? options = null)
        {
            var final = await result;
            TestHelpers.AssertEqualsDynamic(expected, final, options);
            return final;
        }

        public static async Task<ExecutionResult> ShouldEqualJsonFile(this Task<ExecutionResult> result,
            string filePath, ResultComparisonOptions? options = null)
        {
            var final = await result;
            var json = await File.ReadAllTextAsync(filePath);
            var (isEqual, diff) = JsonDiffer.Compare(json, final, options);
            Assert.True(isEqual, diff);
            return final;
        }
    }
}