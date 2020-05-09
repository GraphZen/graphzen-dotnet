// Copyright (c) GraphZen LLC. All rights reserved.
// Licensed under the GraphZen Community License. See the LICENSE file in the project root for license information.

using System.Diagnostics.CodeAnalysis;
using GraphZen.Infrastructure;
using GraphZen.LanguageModel;
using GraphZen.LanguageModel.Internal;
using GraphZen.TypeSystem.Internal;
using GraphZen.TypeSystem.Taxonomy;
using JetBrains.Annotations;

namespace GraphZen.TypeSystem
{
    public static class SchemaBuilderExtensions
    {
        public static ISchemaBuilder<T> FromSchema<T>(this ISchemaBuilder<T> schemaBuilder, DocumentSyntax schema) where T : GraphQLContext
        {
            Check.NotNull(schemaBuilder, nameof(schemaBuilder));
            Check.NotNull(schema, nameof(schema));
            var sdlConfig = new SDLSchemaConfigurator<T>(schema);
            sdlConfig.Configure(schemaBuilder);
            return schemaBuilder;
        }


        public static ISchemaBuilder<T> FromSchema<T>(this ISchemaBuilder<T> schemaBuilder, string schema) where T : GraphQLContext
        {
            Check.NotNull(schemaBuilder, nameof(schemaBuilder));
            Check.NotNull(schema, nameof(schema));
            var ast = Parser.ParseDocument(schema);
            return schemaBuilder.FromSchema(ast);
        }


        internal static SchemaDefinition GetDefinition(this ISchemaBuilder<GraphQLContext> schemaBuilder)   =>
            Check.NotNull(schemaBuilder, nameof(schemaBuilder)).GetInfrastructure<SchemaDefinition>();

        internal static ISchemaDefinition GetDefinition<T>(this ISchemaBuilder<T> schemaBuilder)
            where T : GraphQLContext =>
            Check.NotNull(schemaBuilder, nameof(schemaBuilder)).GetInfrastructure<ISchemaDefinition>();
    }
}