// Copyright (c) GraphZen LLC. All rights reserved.
// Licensed under the GraphZen Community License. See the LICENSE file in the project root for license information.

using System;
using System.Diagnostics.CodeAnalysis;
using System.Text.Json.Nodes;
using GraphZen.Infrastructure;
using JetBrains.Annotations;
using Xunit.Sdk;

namespace GraphZen.Infrastructure;

public static class JsonAssert
{
    public static void EquivalentToJsonFromObject(object? actual, object expected, JsonDiffOptions? options = null)
    {
        if (actual == null) throw new XunitException("Expected object to be equivalent to JSON, but actual was null.");
        var diff = JsonDiffer.GetDiff(actual, expected, options);
        if (diff != null) throw new XunitException(diff);
    }

    public static void EquivalentToJsonFromObject(object? actual, object expected,
        Action<JsonDiffOptions>? optionsAction)
    {
        var options = JsonDiffOptions.FromOptionsAction(optionsAction);
        EquivalentToJsonFromObject(actual, expected, options);
    }

    public static void EquivalentToJson(object? actual, string expected, JsonDiffOptions? options = null)
    {
        if (actual == null) throw new XunitException("Expected object to be equivalent to JSON, but actual was null.");
        var expectedNode = JsonNode.Parse(expected);
        var diff = JsonDiffer.GetDiff(actual, expectedNode!, options);
        if (diff != null) throw new XunitException(diff);
    }

    public static void EquivalentToJson(object? actual, string expected, Action<JsonDiffOptions> optionsAction)
    {
        var options = JsonDiffOptions.FromOptionsAction(optionsAction);
        EquivalentToJson(actual, expected, options);
    }
}