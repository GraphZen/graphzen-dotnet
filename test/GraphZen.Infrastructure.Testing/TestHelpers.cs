// Copyright (c) GraphZen LLC. All rights reserved.
// Licensed under the GraphZen Community License. See the LICENSE file in the project root for license information.

namespace GraphZen.Infrastructure;

public static class TestHelpers
{
    public static IDictionary<string, object> ToDictionary(dynamic value)
    {
        var dict = value != null
            ? JsonNodeExtensions.ObjectToDictionary(value)
            : new Dictionary<string, object>();
        return dict;
    }
}
