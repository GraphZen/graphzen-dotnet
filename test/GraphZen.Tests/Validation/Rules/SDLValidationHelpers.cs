// Copyright (c) GraphZen LLC. All rights reserved.
// Licensed under the GraphZen Community License. See the LICENSE file in the project root for license information.

using System.Collections.Generic;
using GraphZen.Infrastructure;


namespace GraphZen.Validation.Rules
{
    public static class SDLValidationHelpers
    {
        public static readonly IReadOnlyList<string> OutputTypes = new[]
            {"scalar", "enum", "union", "interface", "type"};

        public static readonly IReadOnlyList<string> InputTypes = new[] {"scalar", "enum", "input"};
        public static readonly IReadOnlyList<string> NonInputTypes = new[] {"type", "union", "interface"};
        public static readonly IReadOnlyList<string> NonOutputTypes = new[] {"input"};
        public static readonly IReadOnlyList<string> OutputFieldsTypes = new[] {"interface", "type"};
        public static readonly IReadOnlyList<string> InputFieldsTypes = new[] {"input"};

        public static IEnumerable<string> WithModifiers(this string name)
        {
            yield return name;
            yield return $"{name}!";
            yield return $"[{name}]";
            yield return $"[{name}]!";
            yield return $"[{name}!]!";
        }
    }
}