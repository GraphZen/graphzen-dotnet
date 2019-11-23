// Copyright (c) GraphZen LLC. All rights reserved.
// Licensed under the GraphZen Community License. See the LICENSE file in the project root for license information.

using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using GraphZen.Infrastructure;
using GraphZen.TypeSystem;
using JetBrains.Annotations;

namespace GraphZen
{
    public abstract class GraphQLContextOptions : IInfrastructure<GraphQLContextOptionsBuilder>, IGraphQLContextOptions
    {
        private SchemaDefinition? _schemaDefinition;

        private readonly IReadOnlyDictionary<Type, IGraphQLContextOptionsExtension> _extensions;

        protected GraphQLContextOptions(IReadOnlyDictionary<Type, IGraphQLContextOptionsExtension> extensions)
        {
            _extensions = extensions;
        }

        protected abstract GraphQLContextOptionsBuilder Builder { get; }


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
        public IEnumerable<IGraphQLContextOptionsExtension> Extensions => _extensions.Values;

        public TExtension? FindExtension<TExtension>() where TExtension : class, IGraphQLContextOptionsExtension =>
            _extensions.TryGetValue(typeof(TExtension), out var ext) ? (TExtension)ext : null;


        public virtual TExtension GetExtension<TExtension>()
                    where TExtension : class, IGraphQLContextOptionsExtension =>
                    FindExtension<TExtension>() ?? throw new InvalidOperationException($"{typeof(TExtension).Name} extension not found.");

        /// <summary>
        ///     Adds the given extension to the underlying options and creates a new
        ///     <see cref="DbContextOptions"/> with the extension added.
        /// </summary>
        /// <typeparam name="TExtension"> The type of extension to be added. </typeparam>
        /// <param name="extension"> The extension to be added. </param>
        /// <returns> The new options instance with the given extension added. </returns>
        public abstract GraphQLContextOptions WithExtension<TExtension>([NotNull] TExtension extension)
            where TExtension : class, IGraphQLContextOptionsExtension;

    }


    public class GraphQLContextOptions<TContext> : GraphQLContextOptions where TContext : GraphQLContext
    {
        public GraphQLContextOptions() : this(new Dictionary<Type, IGraphQLContextOptionsExtension>())
        {
        }

        public GraphQLContextOptions(IReadOnlyDictionary<Type, IGraphQLContextOptionsExtension> extensions) : base(extensions)
        {
            Builder = new GraphQLContextOptionsBuilder<TContext>(this);
        }


        protected override GraphQLContextOptionsBuilder Builder { get; }
        public override GraphQLContextOptions WithExtension<TExtension>(TExtension extension)
        {
            var extensions = Extensions.ToDictionary(p => p.GetType(), p => p);
            extensions[typeof(TExtension)] = extension;
            return new GraphQLContextOptions<TContext>(extensions);
        }
    }
}