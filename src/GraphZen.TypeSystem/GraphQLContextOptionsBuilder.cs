// Copyright (c) GraphZen LLC. All rights reserved.
// Licensed under the GraphZen Community License. See the LICENSE file in the project root for license information.
using JetBrains.Annotations;


using System;
using GraphZen.Infrastructure;

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
    public class GraphQLContextOptionsBuilder<TContext> : GraphQLContextOptionsBuilder where
        TContext : GraphQLContext
    {
        public GraphQLContextOptionsBuilder(GraphQLContextOptions<TContext> options) : base(options)
        {
        }


        public new virtual GraphQLContextOptions<TContext> Options => (GraphQLContextOptions<TContext>)base.Options;
    }

    public class GraphQLContextOptionsBuilder
    {
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
    }
}