// Copyright (c) GraphZen LLC. All rights reserved.
// Licensed under the GraphZen Community License. See the LICENSE file in the project root for license information.

using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using GraphZen.Infrastructure;
using JetBrains.Annotations;
using Microsoft.Extensions.DependencyInjection;

namespace GraphZen
{
    [UsedImplicitly]
    public class Program
    {
        [UsedImplicitly]
        public static void Main(string? nullableString)
        {
            Check.NotNull(nullableString, nameof(nullableString));
            string nonNullableString = nullableString;
            Console.WriteLine(nonNullableString);
        }
    }

    public class CoreOptionsExtension
    {
        public CoreOptionsExtension()
        {
            
        }

        protected CoreOptionsExtension(CoreOptionsExtension copyFrom)
        {

        }

        protected CoreOptionsExtension Clone() => new CoreOptionsExtension(this);
    }

    public interface IGraphQLContextOptionsBuilderInfrastructure
    {
        void AddOrUpdateExtension<TExtension>([NotNull] TExtension extension)
            where TExtension : class, IGraphQLContextOptionsExtension;
    }

    public interface IGraphQLContextOptionsExtension
    {
        void ApplyServices([NotNull] IServiceCollection services);
        void Validate([NotNull] IGraphQLContextOptions options);
    }

    public interface IGraphQLContextOptions
    {
        IEnumerable<IGraphQLContextOptionsExtension> Extensions { get; }

        TExtension FindExtension<TExtension>()
            where TExtension : class, IGraphQLContextOptionsExtension;
    }

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

        public virtual GraphQLContextOptions Options { get; }

        public void UseInternalServiceProvider(IServiceProvider serviceProvider)
        {
            Check.NotNull(serviceProvider, nameof(serviceProvider));
            Options.InternalServiceProvider = serviceProvider;
        }

        public void UseQueryType<TQueryType>()
        {
            Options.QueryClrType = typeof(TQueryType);
        }

        void IGraphQLContextOptionsBuilderInfrastructure.AddOrUpdateExtension<TExtension>(TExtension extension)
        {
            throw new NotImplementedException();
        }
    }
}