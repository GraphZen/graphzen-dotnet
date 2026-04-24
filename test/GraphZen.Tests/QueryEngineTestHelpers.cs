// Copyright (c) GraphZen LLC. All rights reserved.
// Licensed under the GraphZen Community License. See the LICENSE file in the project root for license information.

using GraphZen.Infrastructure;
using GraphZen.QueryEngine;

namespace GraphZen.Tests;

public static class QueryEngineTestHelpers
{
    public static async Task<ExecutionResult> ShouldEqual(this Task<ExecutionResult> result, object expected,
        JsonDiffOptions? options = null)
    {
        var final = await result;
        JsonAssert.EquivalentToJsonFromObject(final, expected, options);
        return final;
    }

    public static async Task<ExecutionResult> ShouldEqualJsonFile(this Task<ExecutionResult> result,
        string filePath, JsonDiffOptions? options = null)
    {
        var actual = await result;
        var expected = await File.ReadAllTextAsync(filePath);
        JsonAssert.EquivalentToJson(actual, expected, options);
        return actual;
    }
}
