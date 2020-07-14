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
    public interface IObjectTypeBuilder : INamedTypeDefinitionBuilder<IObjectTypeBuilder, IObjectTypeBuilder>,
        IImplementsInterfacesBuilder<IObjectTypeBuilder>,
        IFieldsBuilder<IObjectTypeBuilder, object, GraphQLContext>,
        IInfrastructure<InternalObjectTypeBuilder>,
        IInfrastructure<MutableObjectType>
    {
    }

    public interface IObjectTypeBuilder<TObject, TContext> : IObjectTypeBuilder,
        INamedTypeDefinitionBuilder<IObjectTypeBuilder<TObject, TContext>, IObjectTypeBuilder<object, TContext>>,
        IImplementsInterfacesBuilder<IObjectTypeBuilder<TObject, TContext>>,
        IFieldsBuilder<IObjectTypeBuilder<TObject, TContext>, TObject, TContext>
        where TObject : notnull
        where TContext : GraphQLContext
    {
        IObjectTypeBuilder<T, TContext> ClrType<T>(bool inferName = false) where T : notnull;
        IObjectTypeBuilder<T, TContext> ClrType<T>(string name) where T : notnull;
        IObjectTypeBuilder<TObject, TContext> IsTypeOf(Func<TObject, bool> isTypeOfFn);
        IObjectTypeBuilder<TObject, TContext> IsTypeOf(Func<TObject, TContext, bool> isTypeOfFn);
        IObjectTypeBuilder<TObject, TContext> IsTypeOf(Func<TObject, TContext, ResolveInfo, bool> isTypeOfFn);
    }
}