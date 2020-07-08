// Copyright (c) GraphZen LLC. All rights reserved.
// Licensed under the GraphZen Community License. See the LICENSE file in the project root for license information.

using System.Diagnostics.CodeAnalysis;
using GraphZen.Infrastructure;
using GraphZen.TypeSystem.Internal;
using JetBrains.Annotations;

namespace GraphZen.TypeSystem
{
    // ReSharper disable once PossibleInterfaceMemberAmbiguity
    public interface IInterfaceTypeBuilder : INamedTypeBuilder,
        IInfrastructure<InterfaceTypeDefinition>,
        IInfrastructure<InternalInterfaceTypeBuilder>
    {

    }

    // ReSharper disable once PossibleInterfaceMemberAmbiguity
    public interface IInterfaceTypeBuilder<TInterface, TContext> :
        INamedTypeBuilder<InterfaceTypeBuilder<TInterface, TContext>, InterfaceTypeBuilder<object, TContext>>,
        IFieldsDefinitionBuilder<InterfaceTypeBuilder<TInterface, TContext>, TInterface, TContext>
        where TContext : GraphQLContext
        where TInterface : notnull
    {
        InterfaceTypeBuilder<T, TContext> ClrType<T>(string name) where T : notnull;
        InterfaceTypeBuilder<T, TContext> ClrType<T>(bool inferName = false) where T : notnull;
        InterfaceTypeBuilder<TInterface, TContext> ResolveType(TypeResolver<TInterface, TContext> resolveTypeFn);
    }
}