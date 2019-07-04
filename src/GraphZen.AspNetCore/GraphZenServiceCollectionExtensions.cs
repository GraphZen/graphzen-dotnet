// Copyright (c) GraphZen LLC. All rights reserved.
// Licensed under the GraphZen Community License. See the LICENSE file in the project root for license information.

using System;
using System.Linq;
using System.Reflection;
using GraphZen.Infrastructure;
using GraphZen.Logging;
using GraphZen.Validation;

using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

// ReSharper disable once CheckNamespace
namespace GraphZen
{
    public static class GraphZenServiceCollectionExtensions
    {
        private static ILog Logger { get; } = LogProvider.GetCurrentClassLogger();

        public static void AddGraphQLContext(
            [NotNull] this IServiceCollection serviceCollection,
            Action<GraphQLContextOptionsBuilder> optionsAction = null)
        {
            AddGraphQLContext<GraphQLContext>(serviceCollection, optionsAction);
        }


        public static void AddGraphQLContext<TContext>(
            [NotNull] this IServiceCollection serviceCollection,
            [CanBeNull] Action<GraphQLContextOptionsBuilder> optionsAction = null)
            where TContext : GraphQLContext
        {
            Check.NotNull(serviceCollection, nameof(serviceCollection));

            var optionsActionImpl =
                optionsAction != null
                    ? (p, b) => optionsAction(b)
                    : (Action<IServiceProvider, GraphQLContextOptionsBuilder>) null;

            var contextType = typeof(TContext);
            Logger.Debug($"Adding GraphQL context {contextType}");
            if (optionsAction != null)
            {
                // ReSharper disable once AssignNullToNotNullAttribute
                // ReSharper disable once PossibleNullReferenceException
                var declaredConstructors = contextType.GetTypeInfo().DeclaredConstructors.ToList();
                // ReSharper disable once PossibleNullReferenceException
                if (declaredConstructors.Count == 1 && declaredConstructors[0].GetParameters().Length == 0)
                {
                    throw new ArgumentException(
                        $"{nameof(AddGraphQLContext)} was called with configuration, but the context type '{contextType}' only declares a parameterless constructor. This means that the configuration passed to {nameof(AddGraphQLContext)} will never be used. If configuration is passed to {nameof(AddGraphQLContext)}, then '{contextType}' should declare a constructor that accepts a {nameof(GraphQLContextOptions)}<{contextType.Name}> and must pass it to the base constructor for {nameof(GraphQLContext)}.");
                }
            }

            Logger.Debug($"Registering {nameof(GraphQLContextOptions<TContext>)}");
            serviceCollection.TryAdd(new ServiceDescriptor(typeof(GraphQLContextOptions<TContext>),
                // ReSharper disable once AssignNullToNotNullAttribute
                p => GraphQLContextOptionsFactory<TContext>(p, optionsActionImpl),
                ServiceLifetime.Scoped));

            Logger.Debug($"Registering {nameof(GraphQLContextOptions)}");
            serviceCollection.Add(new ServiceDescriptor(typeof(GraphQLContextOptions),
                p => p.GetRequiredService<GraphQLContextOptions<TContext>>(), ServiceLifetime.Scoped));


            Logger.Debug($"Registering {typeof(TContext)}");
            serviceCollection.TryAdd(new ServiceDescriptor(typeof(TContext),
                typeof(TContext), ServiceLifetime.Scoped));

            Logger.Debug($"Registering {nameof(GraphQLContext)}");
            serviceCollection.TryAdd(new ServiceDescriptor(typeof(GraphQLContext),
                p => p.GetRequiredService<TContext>(),
                ServiceLifetime.Scoped));

            Logger.Debug($"Registering {nameof(IQueryValidator)}");
            serviceCollection.TryAddScoped<IQueryValidator>(_ => new QueryValidator());


            Logger.Debug("Building temporary service provider");
            var tempServiceProvider = serviceCollection.BuildServiceProvider();

            Logger.Debug("Building temporary service provider");
            var context = tempServiceProvider.GetRequiredService(contextType) as TContext;
            // ReSharper disable once PossibleNullReferenceException
            // ReSharper disable once ConstantConditionalAccessQualifier
            var queryClrType = context.Schema.QueryType?.ClrType;
            if (queryClrType != null)
            {
                serviceCollection.TryAddScoped(queryClrType);
            }

            var mutationClrType = context.Schema.MutationType?.ClrType;
            if (mutationClrType != null)
            {
                serviceCollection.TryAddScoped(mutationClrType);
            }
        }

        private static GraphQLContextOptions<TContext> GraphQLContextOptionsFactory<TContext>(
            [NotNull] IServiceProvider serviceProvider,
            Action<IServiceProvider, GraphQLContextOptionsBuilder> optionsAction)
            where TContext : GraphQLContext
        {
            var builder = new GraphQLContextOptionsBuilder<TContext>(new GraphQLContextOptions<TContext>());
            builder.UseInternalServiceProvider(serviceProvider);
            optionsAction?.Invoke(serviceProvider, builder);
            return builder.Options;
        }
    }
}