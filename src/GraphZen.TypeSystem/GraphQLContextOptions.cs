// Copyright (c) GraphZen LLC. All rights reserved.
// Licensed under the GraphZen Community License. See the LICENSE file in the project root for license information.

using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using GraphZen.Infrastructure;
using JetBrains.Annotations;

namespace GraphZen
{
    public abstract class GraphQLContextOptions :
        IGraphQLContextOptions
    {
        private readonly IReadOnlyDictionary<Type, IGraphQLContextOptionsExtension> _extensions;

        protected GraphQLContextOptions(IReadOnlyDictionary<Type, IGraphQLContextOptionsExtension> extensions)
        {
            _extensions = extensions;
        }

        public IEnumerable<IGraphQLContextOptionsExtension> Extensions => _extensions.Values;

        public TExtension? FindExtension<TExtension>() where TExtension : class, IGraphQLContextOptionsExtension =>
            _extensions.TryGetValue(typeof(TExtension), out var ext) ? (TExtension)ext : null;


        public virtual TExtension GetExtension<TExtension>()
            where TExtension : class, IGraphQLContextOptionsExtension =>
            FindExtension<TExtension>() ??
            throw new InvalidOperationException($"{typeof(TExtension).Name} extension not found.");

        public abstract GraphQLContextOptions WithExtension<TExtension>([NotNull] TExtension extension)
            where TExtension : class, IGraphQLContextOptionsExtension;
    }


    public class GraphQLContextOptions<TContext> : GraphQLContextOptions where TContext : GraphQLContext
    {
        public GraphQLContextOptions() : this(new Dictionary<Type, IGraphQLContextOptionsExtension>())
        {
        }

        public GraphQLContextOptions(IReadOnlyDictionary<Type, IGraphQLContextOptionsExtension> extensions) :
            base(extensions)
        {
        }


        public override GraphQLContextOptions WithExtension<TExtension>(TExtension extension)
        {
            var extensions = Extensions.ToDictionary(p => p.GetType(), p => p);
            extensions[typeof(TExtension)] = extension;
            return new GraphQLContextOptions<TContext>(extensions);
        }
    }
}