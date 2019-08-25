// Copyright (c) GraphZen LLC. All rights reserved.
// Licensed under the GraphZen Community License. See the LICENSE file in the project root for license information.

using System;
using System.Diagnostics.CodeAnalysis;
using GraphZen.Infrastructure;
using JetBrains.Annotations;

namespace GraphZen.TypeSystem
{
    public interface
        IUnionTypeBuilder<out TUnion, out TContext> : IAnnotableBuilder<IUnionTypeBuilder<TUnion, TContext>>
        where TContext : GraphQLContext
    {
        IUnionTypeBuilder<TUnion, TContext> Description(string description);


        IUnionTypeBuilder<TUnion, TContext> ResolveType(TypeResolver<TUnion, TContext> resolveTypeFn);


        IUnionTypeBuilder<object, TContext> ClrType(Type clrType);


        IUnionTypeBuilder<T, TContext> ClrType<T>();


        IUnionTypeBuilder<TUnion, TContext> OfTypes(params string[] objectTypes);


        IUnionTypeBuilder<TUnion, TContext> OfTypes(params Type[] objectTypes);


        IUnionTypeBuilder<TUnion, TContext> OfTypes<T1>();


        IUnionTypeBuilder<TUnion, TContext> OfTypes<T1, T2>();


        IUnionTypeBuilder<TUnion, TContext> OfTypes<T1, T2, T3>();


        IUnionTypeBuilder<TUnion, TContext> OfTypes<T1, T2, T3, T4>();


        IUnionTypeBuilder<TUnion, TContext> OfTypes<T1, T2, T3, T4, T5>();


        IUnionTypeBuilder<TUnion, TContext> OfTypes<T1, T2, T3, T4, T5, T6>();


        IUnionTypeBuilder<TUnion, TContext> OfTypes<T1, T2, T3, T4, T5, T6, T7>();


        IUnionTypeBuilder<TUnion, TContext> OfTypes<T1, T2, T3, T4, T5, T6, T7, T8, T9>();


        IUnionTypeBuilder<TUnion, TContext> OfTypes<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10>();


        IUnionTypeBuilder<TUnion, TContext> Name(string name);
    }
}