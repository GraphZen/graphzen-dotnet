// Copyright (c) GraphZen LLC. All rights reserved.
// Licensed under the GraphZen Community License. See the LICENSE file in the project root for license information.
using JetBrains.Annotations;
#nullable disable


using GraphZen.Infrastructure;
using GraphZen.TypeSystem;

namespace GraphZen
{
    public static class SchemaBuilderExtensions
    {
        public static SchemaDefinition GetDefinition<T>(this ISchemaBuilder<T> sb) where T : GraphQLContext =>
            // ReSharper disable once AssignNullToNotNullAttribute
            sb.GetInfrastructure<SchemaDefinition>();
    }
}