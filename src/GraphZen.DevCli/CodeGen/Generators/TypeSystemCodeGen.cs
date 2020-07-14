// Copyright (c) GraphZen LLC. All rights reserved.
// Licensed under the GraphZen Community License. See the LICENSE file in the project root for license information.

using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using GraphZen.Infrastructure;
using GraphZen.TypeSystem;
using JetBrains.Annotations;

namespace GraphZen.CodeGen.Generators
{
    public static class TypeSystemCodeGen
    {
        public static IReadOnlyList<(string kind, string type)> NamedTypes { get; } = typeof(NamedTypeDefinition).Assembly
            .GetTypes()
            .Where(typeof(NamedTypeDefinition).IsAssignableFrom)
            .Where(_ => !_.IsAbstract)
            .OrderBy(_ => _.Name)
            .Select(_ => (_.Name.Replace("Type", ""), _.Name))
            .ToList();
    }
}