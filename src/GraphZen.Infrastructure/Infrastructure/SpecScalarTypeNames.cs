// Copyright (c) GraphZen LLC. All rights reserved.
// Licensed under the GraphZen Community License. See the LICENSE file in the project root for license information.

using System.Collections.Immutable;
using GraphZen.Infrastructure;

namespace GraphZen.Infrastructure
{
    internal static class SpecReservedNames
    {
        [ItemNotNull]
        public static ImmutableArray<string> ScalarTypeNames { get; } =
            ImmutableArray.Create("String", "Int", "Float", "Boolean", "ID");

        [ItemNotNull]
        public static ImmutableArray<string> DirectiveNames { get; } =
            ImmutableArray.Create("deprecated", "include", "skip");

        [ItemNotNull]
        public static ImmutableArray<string> IntrospectionTypeNames { get; } =
            ImmutableArray.Create("__Type", "__Field", "__Schema", "__Directive", "__InputValue", "__EnumValue",
                "__DirectiveLocation",
                "__TypeKind");
    }
}