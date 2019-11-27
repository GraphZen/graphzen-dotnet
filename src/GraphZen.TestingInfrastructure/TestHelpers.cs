// Copyright (c) GraphZen LLC. All rights reserved.
// Licensed under the GraphZen Community License. See the LICENSE file in the project root for license information.

using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using GraphZen.Infrastructure;
using JetBrains.Annotations;
using Newtonsoft.Json.Linq;
using Xunit;

namespace GraphZen
{
    public static class TestHelpers
    {
        public static IDictionary<string, object> ToDictionary(dynamic value)
        {
            var dict = value != null
                ? JsonExtensions.ToDictionary(JObject.FromObject(value, Json.Serializer))
                : new Dictionary<string, object>();
            return dict;
        }

        
    }
}