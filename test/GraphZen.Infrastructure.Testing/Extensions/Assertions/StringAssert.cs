// Copyright (c) GraphZen LLC. All rights reserved.
// Licensed under the GraphZen Community License. See the LICENSE file in the project root for license information.

using GraphZen.Infrastructure;
using Xunit.Sdk;

namespace GraphZen.Infrastructure;

public static class StringAssert
{
    public static void Equal(string? actual, string expected, StringDiffOptions? options)
    {
        if (actual == null) throw new XunitException($"Expected \"{expected}\", but actual was null.");
        var diff = actual.GetDiff(expected, options);
        if (diff != null) throw new XunitException(diff);
    }

    public static void Equal(string? actual, string expected,
        Action<StringDiffOptions>? comparisonOptionsAction)
    {
        var options = StringDiffOptions.FromOptionsAction(comparisonOptionsAction);
        Equal(actual, expected, options);
    }

    public static void Equal(string? actual, string expected, bool showDiff)
    {
        if (showDiff)
            Equal(actual, expected, (Action<StringDiffOptions>?)null);
        else
            Assert.Equal(expected, actual);
    }
}