// Copyright (c) GraphZen LLC. All rights reserved.
// Licensed under the GraphZen Community License. See the LICENSE file in the project root for license information.

using System.Diagnostics.CodeAnalysis;
using GraphZen.Infrastructure;
using GraphZen.LanguageModel;
using GraphZen.LanguageModel.Internal;
using GraphZen.TypeSystem.Internal;
using JetBrains.Annotations;

namespace GraphZen.TypeSystem
{
    public static class SchemaBuilderExtensions
    {
        public static SchemaBuilder ConfigureFromSchema(this SchemaBuilder schemaBuilder, DocumentSyntax schema)
        {
            Check.NotNull(schemaBuilder, nameof(schemaBuilder));
            Check.NotNull(schema, nameof(schema));
            var sdlConfig = new SDLSchemaConfigurator(schema);
            sdlConfig.Configure(schemaBuilder);
            return schemaBuilder;
        }


        public static SchemaBuilder ConfigureFromSchema(this SchemaBuilder schemaBuilder, string schema)
        {
            Check.NotNull(schemaBuilder, nameof(schemaBuilder));
            Check.NotNull(schema, nameof(schema));
            var ast = Parser.ParseDocument(schema);
            return schemaBuilder.ConfigureFromSchema(ast);
        }


        internal static SchemaDefinition GetDefinition(this SchemaBuilder schemaBuilder) =>
            Check.NotNull(schemaBuilder, nameof(schemaBuilder)).GetInfrastructure<SchemaDefinition>();
    }
}