// Copyright (c) GraphZen LLC. All rights reserved.
// Licensed under the GraphZen Community License. See the LICENSE file in the project root for license information.

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Reflection;
using GraphZen.Infrastructure;
using GraphZen.Logging;
using GraphZen.TypeSystem;
using GraphZen.TypeSystem.Internal;
using JetBrains.Annotations;

namespace GraphZen
{
    public class GraphQLContext
    {
        private static readonly ILog Logger = LogProvider.For<GraphQLContext>();
        private readonly GraphQLContextOptions _options;

        private bool _optionsInitialized;
        private Schema? _schema;

        public GraphQLContext() : this(new GraphQLContextOptions<GraphQLContext>())
        {
        }

        public GraphQLContext(GraphQLContextOptions options)
        {
            Check.NotNull(options, nameof(options));
            _options = options;
        }

        protected internal GraphQLContext(Schema schema) : this()
        {
            Check.NotNull(schema, nameof(schema));
            _schema = schema;
        }


        internal static Dictionary<Type, Schema> SchemaCache { get; } = new Dictionary<Type, Schema>();


        public GraphQLContextOptions Options
        {
            get
            {
                if (!_optionsInitialized)
                {
                    OnConfiguring(new GraphQLContextOptionsBuilder(_options));
                    _optionsInitialized = true;
                    Logger.Debug("howdy from GraphZen");
                }

                return _options;
            }
        }


        public Schema Schema
        {
            get
            {
                if (_schema != null) return _schema;

                var contextType = GetType();

                if (!SchemaCache.TryGetValue(contextType, out _schema))
                {
                    var schemaDef = new SchemaDefinition(SpecScalars.All);
                    var schemaBuilder = new SchemaBuilder(schemaDef);
                    var internalBuilder = schemaBuilder.GetInfrastructure<InternalSchemaBuilder>();

                    // Configure query type
                    if (Options.QueryClrType != null)
                    {
                        schemaBuilder.QueryType(Options.QueryClrType);
                    }
                    else
                    {
                        Type? queryClrType = default;
                        if (contextType != typeof(GraphQLContext))
                            queryClrType = contextType.Assembly.GetExportedTypes()
                                .SingleOrDefault(_ => _.IsClass && _.Name == "Query");

                        queryClrType = queryClrType ?? Assembly.GetEntryAssembly()?.GetTypes()
                                           .SingleOrDefault(_ => _.IsClass && _.Name == "Query");

                        if (queryClrType != null)
                            internalBuilder.QueryType(queryClrType, ConfigurationSource.Convention);
                    }

                    // Configure mutation type
                    if (Options.MutationClrType != null)
                    {
                        schemaBuilder.MutationType(Options.MutationClrType);
                    }
                    else
                    {
                        Type? mutationClrType = default;
                        if (contextType != typeof(GraphQLContext))
                            mutationClrType = contextType.Assembly.GetExportedTypes()
                                .SingleOrDefault(_ => _.IsClass && _.Name == "Mutation");

                        mutationClrType = mutationClrType ?? Assembly.GetEntryAssembly()?.GetTypes()
                                              .SingleOrDefault(_ => _.IsClass && _.Name == "Mutation");

                        if (mutationClrType != null)
                            internalBuilder.MutationType(mutationClrType, ConfigurationSource.Convention);
                    }


                    OnSchemaCreating(schemaBuilder);


                    _schema = schemaDef.ToSchema();
                    SchemaCache[contextType] = _schema;
                }

                Debug.Assert(_schema != null, nameof(_schema) + " != null");
                return _schema;
            }
        }

        protected internal virtual void OnConfiguring(GraphQLContextOptionsBuilder optionsBuilder)
        {
        }


        protected virtual SchemaBuilder CreateSchemaBuilder() => new SchemaBuilder(Options.Schema);

        protected internal virtual void OnSchemaCreating(SchemaBuilder schemaBuilder)
        {
        }
    }
}