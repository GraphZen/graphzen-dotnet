// Copyright (c) GraphZen LLC. All rights reserved.
// Licensed under the GraphZen Community License. See the LICENSE file in the project root for license information.

using System.Collections.Generic;
using System.Collections.Immutable;
using System.Diagnostics.CodeAnalysis;
using GraphZen.Infrastructure;
using JetBrains.Annotations;

#nullable enable


namespace GraphZen.Infrastructure
{
    internal static class SpecReservedNames
    {
        public static IReadOnlyList<string> ScalarTypeNames { get; } =
            ImmutableArray.Create("String", "Int", "Float", "Boolean", "ID");


        public static IReadOnlyList<string> DirectiveNames { get; } =
            ImmutableArray.Create("deprecated", "include", "skip");


        public static IReadOnlyList<string> IntrospectionTypeNames { get; } =
            ImmutableArray.Create("__Type", "__Field", "__Schema", "__Directive", "__InputValue", "__EnumValue",
                "__DirectiveLocation",
                "__TypeKind");
    }
}