// Copyright (c) GraphZen LLC. All rights reserved.
// Licensed under the GraphZen Community License. See the LICENSE file in the project root for license information.

using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using GraphZen.Infrastructure;
using GraphZen.TypeSystem;
using JetBrains.Annotations;

namespace GraphZen
{
    public abstract class GraphQLContextOptions : IInfrastructure<GraphQLContextOptionsBuilder>, IGraphQLContextOptions
    {
        private SchemaDefinition? _schemaDefinition;
        protected abstract GraphQLContextOptionsBuilder Builder { get; }

        public Type? QueryClrType { get; set; }
        public Type? MutationClrType { get; set; }

        public IServiceProvider? InternalServiceProvider { get; set; }
        public bool RevealInternalServerErrors { get; set; } = false;


        public SchemaDefinition Schema
        {
            get
            {
                if (_schemaDefinition != null) return _schemaDefinition;
                _schemaDefinition = new SchemaDefinition(SpecScalars.All);
                return _schemaDefinition;
            }
            set => _schemaDefinition = value;
        }


        GraphQLContextOptionsBuilder IInfrastructure<GraphQLContextOptionsBuilder>.Instance => Builder;
        public IEnumerable<IGraphQLContextOptionsExtension> Extensions { get; }
        public TExtension FindExtension<TExtension>() where TExtension : class, IGraphQLContextOptionsExtension => throw new NotImplementedException();
    }


    public class GraphQLContextOptions<TContext> : GraphQLContextOptions where TContext : GraphQLContext
    {
        public GraphQLContextOptions()
        {
            Builder = new GraphQLContextOptionsBuilder<TContext>(this);
        }

        protected override GraphQLContextOptionsBuilder Builder { get; }
    }
}