// Copyright (c) GraphZen LLC. All rights reserved.
// Licensed under the GraphZen Community License. See the LICENSE file in the project root for license information.

using System;
using System.Diagnostics.CodeAnalysis;
using GraphZen.Infrastructure;
using JetBrains.Annotations;

namespace GraphZen.TypeSystem.Internal
{
    internal interface IUnionTypeBuilder<TUnion, TContext> :
        IDescriptionBuilder<UnionTypeBuilder<TUnion, TContext>>,
        IAnnotableBuilder<UnionTypeBuilder<TUnion, TContext>>,
        INameBuilder<UnionTypeBuilder<TUnion, TContext>>,
        IClrTypeBuilder<UnionTypeBuilder<object, TContext>> where TContext : GraphQLContext
    {
        UnionTypeBuilder<TUnion, TContext> ResolveType(TypeResolver<TUnion, TContext> resolveTypeFn);


        UnionTypeBuilder<T, TContext> ClrType<T>();
        UnionTypeBuilder<T, TContext> ClrType<T>(string name);


        UnionTypeBuilder<TUnion, TContext> OfTypes(params string[] objectTypes);


        UnionTypeBuilder<TUnion, TContext> OfTypes(params Type[] objectTypes);


        UnionTypeBuilder<TUnion, TContext> OfTypes<T1>();


        UnionTypeBuilder<TUnion, TContext> OfTypes<T1, T2>();


        UnionTypeBuilder<TUnion, TContext> OfTypes<T1, T2, T3>();


        UnionTypeBuilder<TUnion, TContext> OfTypes<T1, T2, T3, T4>();


        UnionTypeBuilder<TUnion, TContext> OfTypes<T1, T2, T3, T4, T5>();


        UnionTypeBuilder<TUnion, TContext> OfTypes<T1, T2, T3, T4, T5, T6>();


        UnionTypeBuilder<TUnion, TContext> OfTypes<T1, T2, T3, T4, T5, T6, T7>();


        UnionTypeBuilder<TUnion, TContext> OfTypes<T1, T2, T3, T4, T5, T6, T7, T8, T9>();


        UnionTypeBuilder<TUnion, TContext> OfTypes<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10>();
    }
}