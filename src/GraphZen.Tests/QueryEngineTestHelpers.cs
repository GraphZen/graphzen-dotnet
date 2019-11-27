// Copyright (c) GraphZen LLC. All rights reserved.
// Licensed under the GraphZen Community License. See the LICENSE file in the project root for license information.

using System;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.Threading.Tasks;
using FluentAssertions;
using GraphZen.Infrastructure;
using GraphZen.QueryEngine;
using JetBrains.Annotations;
using Xunit;

namespace GraphZen
{
    public static class QueryEngineTestHelpers
    {
        public static async Task<ExecutionResult> ShouldEqual(this Task<ExecutionResult> result, object expected,
            ResultComparisonOptions? options = null)
        {

            var final = await result;
            final.Should().BeEquivalentToJson(expected, options);
            return final;
            // TestHelpers.AssertEqualsDynamic(expected, final, options);
            // return final;
        }

        public static async Task<ExecutionResult> ShouldEqualJsonFile(this Task<ExecutionResult> result,
            string filePath, ResultComparisonOptions? options = null)
        {
            var final = await result;
            var json = await File.ReadAllTextAsync(filePath);
            var diff = JsonDiffer.GetDiff(json, final, options);
            Assert.True(diff == null, diff);
            return final;
        }
    }
}