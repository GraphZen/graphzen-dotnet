// Copyright (c) GraphZen LLC. All rights reserved.
// Licensed under the GraphZen Community License. See the LICENSE file in the project root for license information.

using System.Diagnostics.CodeAnalysis;
using GraphZen.Infrastructure;
using GraphZen.TypeSystem.Internal;
using JetBrains.Annotations;

namespace GraphZen.TypeSystem
{
    // ReSharper disable once PossibleInterfaceMemberAmbiguity
    public interface IInterfaceTypeBuilder : INamedTypeDefinitionBuilder<IInterfaceTypeBuilder, IInterfaceTypeBuilder>,
        IFieldsBuilder<IInterfaceTypeBuilder, object, GraphQLContext>,
        IImplementsInterfacesBuilder<IInterfaceTypeBuilder>,
        IInfrastructure<MutableInterfaceType>,
        IInfrastructure<InternalInterfaceTypeBuilder>
    {
    }

    // ReSharper disable once PossibleInterfaceMemberAmbiguity
    public interface IInterfaceTypeBuilder<TInterface, TContext> :
        INamedTypeDefinitionBuilder<IInterfaceTypeBuilder<TInterface, TContext>, IInterfaceTypeBuilder<object, TContext>>,
        IFieldsBuilder<IInterfaceTypeBuilder<TInterface, TContext>, TInterface, TContext>
        where TContext : GraphQLContext
        where TInterface : notnull
    {
        IInterfaceTypeBuilder<T, TContext> ClrType<T>(string name) where T : notnull;
        IInterfaceTypeBuilder<T, TContext> ClrType<T>(bool inferName = false) where T : notnull;
        IInterfaceTypeBuilder<TInterface, TContext> ResolveType(TypeResolver<TInterface, TContext> resolveTypeFn);
    }
}