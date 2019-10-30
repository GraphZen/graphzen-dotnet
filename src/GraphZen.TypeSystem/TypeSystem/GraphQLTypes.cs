// Copyright (c) GraphZen LLC. All rights reserved.
// Licensed under the GraphZen Community License. See the LICENSE file in the project root for license information.

using System.Collections.Generic;
using System.Collections.Immutable;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using GraphZen.Infrastructure;
using JetBrains.Annotations;

namespace GraphZen.TypeSystem
{
    public static class GraphQLTypes
    {
        public static IReadOnlyList<NamedType> All = new List<NamedType>
        {
            SpecScalars.String,
            SpecScalars.ID,
            SpecScalars.Boolean,
            SpecScalars.Float,
            SpecScalars.Int
        }.Concat(Introspection.Schema.GetTypes()).ToImmutableList();
    }
}