// Copyright (c) GraphZen LLC. All rights reserved.
// Licensed under the GraphZen Community License. See the LICENSE file in the project root for license information.

using System;
using System.Diagnostics.CodeAnalysis;
using GraphZen.Infrastructure;
using GraphZen.TypeSystem.Internal;
using JetBrains.Annotations;

namespace GraphZen.TypeSystem
{
    // ReSharper disable once PossibleInterfaceMemberAmbiguity
    public interface IUnionTypeBuilder : IInfrastructure<MutableUnionType>,
        IInfrastructure<InternalUnionTypeBuilder>, INamedTypeDefinitionBuilder<IUnionTypeBuilder, IUnionTypeBuilder>

    {
        IUnionTypeBuilder ResolveType<TUnion>(TypeResolver<TUnion, GraphQLContext> resolveTypeFn);

        IUnionTypeBuilder OfTypes(params string[] objectTypes);

        IUnionTypeBuilder OfTypes(params Type[] objectTypes);

        IUnionTypeBuilder OfTypes<T1>();


        IUnionTypeBuilder OfTypes<T1, T2>();


        IUnionTypeBuilder OfTypes<T1, T2, T3>();


        IUnionTypeBuilder OfTypes<T1, T2, T3, T4>();


        IUnionTypeBuilder OfTypes<T1, T2, T3, T4, T5>();


        IUnionTypeBuilder OfTypes<T1, T2, T3, T4, T5, T6>();


        IUnionTypeBuilder OfTypes<T1, T2, T3, T4, T5, T6, T7>();


        IUnionTypeBuilder OfTypes<T1, T2, T3, T4, T5, T6, T7, T8, T9>();


        IUnionTypeBuilder OfTypes<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10>();

    }
}