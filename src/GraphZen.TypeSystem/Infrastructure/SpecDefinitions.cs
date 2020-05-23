// Copyright (c) GraphZen LLC. All rights reserved.
// Licensed under the GraphZen Community License. See the LICENSE file in the project root for license information.

using System.Diagnostics.CodeAnalysis;
using GraphZen.Infrastructure;
using GraphZen.TypeSystem;
using JetBrains.Annotations;

namespace GraphZen.Infrastructure
{
    internal static class SpecDefinitions
    {
        public static SchemaBuilder<TContext> AddSpecMembers<TContext>(this SchemaBuilder<TContext> schemaBuilder)
            where TContext : GraphQLContext =>
            schemaBuilder.AddSpecScalars().AddSpecDirectives().AddIntrospectionTypes();
    }
}