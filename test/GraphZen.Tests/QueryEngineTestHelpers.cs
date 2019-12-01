// Copyright (c) GraphZen LLC. All rights reserved.
// Licensed under the GraphZen Community License. See the LICENSE file in the project root for license information.

using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.Threading.Tasks;
using FluentAssertions;
using GraphZen.Infrastructure;
using GraphZen.QueryEngine;
using JetBrains.Annotations;

namespace GraphZen.Tests
{
    public static class QueryEngineTestHelpers
    {
        public static async Task<ExecutionResult> ShouldEqual(this Task<ExecutionResult> result, object expected,
            JsonDiffOptions? options = null)
        {
            var final = await result;
            final.Should().BeEquivalentToJsonFromObject(expected, options);
            return final;
            // TestHelpers.AssertEqualsDynamic(expected, final, options);
            // return final;
        }

        public static async Task<ExecutionResult> ShouldEqualJsonFile(this Task<ExecutionResult> result,
            string filePath, JsonDiffOptions? options = null)
        {
            var actual = await result;
            var expected = await File.ReadAllTextAsync(filePath);
            actual.Should().BeEquivalentToJson(expected, options);
            return actual;
        }
    }
}