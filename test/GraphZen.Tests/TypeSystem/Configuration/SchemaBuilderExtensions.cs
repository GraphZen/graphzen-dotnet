// Copyright (c) GraphZen LLC. All rights reserved.
// Licensed under the GraphZen Community License. See the LICENSE file in the project root for license information.

using GraphZen.Infrastructure;

namespace GraphZen.TypeSystem
{
    public static class SchemaBuilderExtensions
    {
        public static SchemaDefinition GetDefinition<T>(this ISchemaBuilder<T> sb) where T : GraphQLContext =>
            sb.GetInfrastructure<SchemaDefinition>();
    }
}