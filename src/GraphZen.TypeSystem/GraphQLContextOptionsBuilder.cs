// Copyright (c) GraphZen LLC. All rights reserved.
// Licensed under the GraphZen Community License. See the LICENSE file in the project root for license information.

using System;
using System.Diagnostics.CodeAnalysis;
using GraphZen.Infrastructure;
using GraphZen.TypeSystem.Taxonomy;
using JetBrains.Annotations;

namespace GraphZen
{
    public class GraphQLContextOptionsBuilder<TContext> : GraphQLContextOptionsBuilder where
        TContext : GraphQLContext
    {
        public GraphQLContextOptionsBuilder() : this(new GraphQLContextOptions<TContext>())
        {
        }

        public GraphQLContextOptionsBuilder(GraphQLContextOptions<TContext> options) : base(options)
        {
        }

        public new virtual GraphQLContextOptions<TContext> Options => (GraphQLContextOptions<TContext>) base.Options;
    }

    public class GraphQLContextOptionsBuilder : IGraphQLContextOptionsBuilderInfrastructure
    {
        public GraphQLContextOptionsBuilder() : this(new GraphQLContextOptions<GraphQLContext>())
        {
        }

        public GraphQLContextOptionsBuilder(GraphQLContextOptions options)
        {
            Options = Check.NotNull(options, nameof(options));
        }

        public virtual GraphQLContextOptions Options { get; private set; }

        public GraphQLContextOptionsBuilder UseInternalServiceProvider(IServiceProvider serviceProvider)
            => WithOption(o => o.WithInternalServiceProvider(serviceProvider));

        public GraphQLContextOptionsBuilder UseApplicationServiceProvider(IServiceProvider serviceProvider)
            => WithOption(o => o.WithApplicationServiceProvider(serviceProvider));

        public GraphQLContextOptionsBuilder RevealInternalServerErrors(bool enabled = true)
            => WithOption(o => o.WithRevealInternalServerErrors(enabled));

        public virtual GraphQLContextOptionsBuilder UseSchema(ISchema schema) =>
            WithOption(o => o.WithSchema(schema));

        public GraphQLContextOptionsBuilder UseQueryType<TQueryType>() =>
            WithOption(o => o.WithQueryClrType(typeof(TQueryType)));

        void IGraphQLContextOptionsBuilderInfrastructure.AddOrUpdateExtension<TExtension>(TExtension extension) =>
            Options = Options.WithExtension(extension);

        private GraphQLContextOptionsBuilder WithOption(Func<CoreOptionsExtension, CoreOptionsExtension> withFunc)
        {
            ((IGraphQLContextOptionsBuilderInfrastructure) this).AddOrUpdateExtension(
                withFunc(Options.FindExtension<CoreOptionsExtension>() ?? new CoreOptionsExtension()));

            return this;
        }
    }
}