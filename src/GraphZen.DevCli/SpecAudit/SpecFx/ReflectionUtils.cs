// Copyright (c) GraphZen LLC. All rights reserved.
// Licensed under the GraphZen Community License. See the LICENSE file in the project root for license information.

using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Reflection;
using GraphZen.Infrastructure;
using JetBrains.Annotations;

namespace GraphZen.SpecAudit.SpecFx
{
    public static class ReflectionUtils
    {
        public static IEnumerable<FieldInfo> GetConstFields(Type type) =>
            type.GetFields(BindingFlags.Public | BindingFlags.Static)
                .Where(_ => _.IsLiteral && !_.IsInitOnly);
    }
}