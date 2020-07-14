// Copyright (c) GraphZen LLC. All rights reserved.
// Licensed under the GraphZen Community License. See the LICENSE file in the project root for license information.

using System.Diagnostics.CodeAnalysis;
using GraphZen.Infrastructure;
using GraphZen.TypeSystem;
using JetBrains.Annotations;

namespace GraphZen.Infrastructure
{
    internal static class SchemaBuilderInfrastructureExtensions
    {
        public static MutableSchema GetDefinition<T>(this SchemaBuilder<T> schemaBuilder) where T : GraphQLContext =>
            schemaBuilder.GetInfrastructure<MutableSchema>();
    }
}