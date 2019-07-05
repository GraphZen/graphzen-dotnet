﻿// Copyright (c) GraphZen LLC. All rights reserved.
// Licensed under the GraphZen Community License. See the LICENSE file in the project root for license information.

using GraphZen.Infrastructure;
using GraphZen.LanguageModel;
using GraphZen.LanguageModel.Internal;
using GraphZen.TypeSystem.Internal;

namespace GraphZen.TypeSystem
{
    public static class SchemaBuilderExtensions
    {
        [NotNull]
        public static SchemaBuilder Build(this SchemaBuilder schemaBuilder,
            DocumentSyntax schemaDocument)
        {
            Check.NotNull(schemaBuilder, nameof(schemaBuilder));
            Check.NotNull(schemaDocument, nameof(schemaDocument));
            var sdlConfig = new SDLSchemaConfigurator(schemaDocument);
            sdlConfig.Configure(schemaBuilder);
            return schemaBuilder;
        }

        [NotNull]
        public static SchemaBuilder Build(this SchemaBuilder schemaBuilder, string schemaDocument)
        {
            Check.NotNull(schemaBuilder, nameof(schemaBuilder));
            Check.NotNull(schemaDocument, nameof(schemaDocument));
            var ast = Parser.ParseDocument(schemaDocument);
            return schemaBuilder.Build(ast);
        }
    }
}