// Copyright (c) GraphZen LLC. All rights reserved.
// Licensed under the GraphZen Community License. See the LICENSE file in the project root for license information.

using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Reflection;
using GraphZen.Infrastructure;
using GraphZen.TypeSystem;
using GraphZen.TypeSystem.Internal;
using JetBrains.Annotations;

namespace GraphZen
{
    public class GraphQLContext
    {
        private GraphQLContextOptions _options;

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
                    var optionsBuilder = new GraphQLContextOptionsBuilder(_options);
                    ((IGraphQLContextOptionsBuilderInfrastructure)optionsBuilder).AddOrUpdateExtension(
                        _options.FindExtension<CoreOptionsExtension>() ?? new CoreOptionsExtension());
                    OnConfiguring(optionsBuilder);

                    foreach (var extension in optionsBuilder.Options.Extensions)
                    {
                        extension.Validate(optionsBuilder.Options);
                    }

                    _options = optionsBuilder.Options;
                    _optionsInitialized = true;
                }

                return _options;
            }
        }


        public Schema Schema
        {
            get
            {
                if (_schema != null)
                {
                    return _schema;
                }

                var contextType = GetType();

                if (!SchemaCache.TryGetValue(contextType, out _schema))
                {
                    var schemaDef = new SchemaDefinition(SpecScalars.All);
                    var schemaBuilder = new SchemaBuilder(schemaDef);
                    var internalBuilder = schemaBuilder.GetInfrastructure<InternalSchemaBuilder>();
                    var options = Options.GetExtension<CoreOptionsExtension>();

                    // Configure query type
                    if (options.QueryClrType != null)
                    {
                        schemaBuilder.QueryType(options.QueryClrType);
                    }
                    else
                    {
                        Type? queryClrType = default;

                        Type? DiscoverQueryType(Assembly? assembly, Func<Assembly, IEnumerable<Type>> getTypes)
                        {
                            if (assembly == null)
                            {
                                return null;
                            }

                            var candidates = getTypes(assembly)
                                .Where(_ => _.IsClass && _.Name == "Query" && !_.IsNested).ToArray();
                            if (candidates.Length > 1)
                            {
                                throw new InvalidOperationException(
                                    $"Unable to determine query type by convention. More than one class named 'Query' in assembly '{assembly}'");
                            }

                            return candidates.SingleOrDefault();
                        }

                        if (contextType != typeof(GraphQLContext))
                        {
                            queryClrType = DiscoverQueryType(contextType.Assembly, a => a.GetExportedTypes());
                        }

                        if (queryClrType == null)
                        {
                            queryClrType = DiscoverQueryType(Assembly.GetEntryAssembly(), a => a.GetTypes());
                        }

                        if (queryClrType != null)
                        {
                            internalBuilder.QueryType(queryClrType, ConfigurationSource.Convention);
                        }
                    }

                    // Configure mutation type
                    if (options.MutationClrType != null)
                    {
                        schemaBuilder.MutationType(options.MutationClrType);
                    }
                    else
                    {
                        Type? mutationClrType = default;
                        if (contextType != typeof(GraphQLContext))
                        {
                            mutationClrType = contextType.Assembly.GetExportedTypes()
                                .SingleOrDefault(_ => _.IsClass && _.Name == "Mutation");
                        }

                        if (mutationClrType == null)
                        {
                            mutationClrType = Assembly.GetEntryAssembly()?.GetTypes()
                                .SingleOrDefault(_ => _.IsClass && _.Name == "Mutation");
                        }

                        if (mutationClrType != null)
                        {
                            internalBuilder.MutationType(mutationClrType, ConfigurationSource.Convention);
                        }
                    }


                    OnSchemaCreating(schemaBuilder);


                    _schema = schemaDef.ToSchema();
                    SchemaCache[contextType] = _schema;
                }

                return _schema;
            }
        }

        protected internal virtual void OnConfiguring(GraphQLContextOptionsBuilder optionsBuilder)
        {
        }

        protected internal virtual void OnSchemaCreating(SchemaBuilder schemaBuilder)
        {
        }
    }
}