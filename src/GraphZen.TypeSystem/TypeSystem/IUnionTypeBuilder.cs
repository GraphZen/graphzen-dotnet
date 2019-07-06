// Copyright (c) GraphZen LLC. All rights reserved.
// Licensed under the GraphZen Community License. See the LICENSE file in the project root for license information.

using System;
using GraphZen.Infrastructure;

namespace GraphZen.TypeSystem
{
    public interface
        IUnionTypeBuilder<out TUnion, out TContext> : IAnnotableBuilder<IUnionTypeBuilder<TUnion, TContext>>
        where TContext : GraphQLContext
    {
        [NotNull]
        IUnionTypeBuilder<TUnion, TContext> Description([CanBeNull] string description);

        [NotNull]
        IUnionTypeBuilder<TUnion, TContext> ResolveType(TypeResolver<TUnion, TContext> resolveTypeFn);

        [NotNull]
        IUnionTypeBuilder<TUnion, TContext> OfTypes(params string[] objectTypes);

        [NotNull]
        IUnionTypeBuilder<TUnion, TContext> OfTypes(params Type[] objectTypes);


        [NotNull]
        IUnionTypeBuilder<TUnion, TContext> OfTypes<T1>();


        [NotNull]
        IUnionTypeBuilder<TUnion, TContext> OfTypes<T1, T2>();


        [NotNull]
        IUnionTypeBuilder<TUnion, TContext> OfTypes<T1, T2, T3>();


        [NotNull]
        IUnionTypeBuilder<TUnion, TContext> OfTypes<T1, T2, T3, T4>();


        [NotNull]
        IUnionTypeBuilder<TUnion, TContext> OfTypes<T1, T2, T3, T4, T5>();


        [NotNull]
        IUnionTypeBuilder<TUnion, TContext> OfTypes<T1, T2, T3, T4, T5, T6>();


        [NotNull]
        IUnionTypeBuilder<TUnion, TContext> OfTypes<T1, T2, T3, T4, T5, T6, T7>();


        [NotNull]
        IUnionTypeBuilder<TUnion, TContext> OfTypes<T1, T2, T3, T4, T5, T6, T7, T8, T9>();


        [NotNull]
        IUnionTypeBuilder<TUnion, TContext> OfTypes<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10>();


        [NotNull]
        IUnionTypeBuilder<TUnion, TContext> Name(string name);
    }
}