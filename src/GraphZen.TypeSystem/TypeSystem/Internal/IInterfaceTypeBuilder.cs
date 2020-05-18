// Copyright (c) GraphZen LLC. All rights reserved.
// Licensed under the GraphZen Community License. See the LICENSE file in the project root for license information.

using System;
using System.Diagnostics.CodeAnalysis;
using GraphZen.Infrastructure;
using JetBrains.Annotations;

namespace GraphZen.TypeSystem.Internal
{
    internal interface IInterfaceTypeBuilder<TInterface, TContext> :
        IInfrastructure<InternalInterfaceTypeBuilder>,
        IAnnotableBuilder<InterfaceTypeBuilder<TInterface, TContext>>,
        IFieldsDefinitionBuilder<InterfaceTypeBuilder<TInterface, TContext>, TInterface, TContext>,
        INamedBuilder<InterfaceTypeBuilder<TInterface, TContext>>,
        IDescriptionBuilder<InterfaceTypeBuilder<TInterface, TContext>> where TContext : GraphQLContext
    {
        InterfaceTypeBuilder<object, TContext> ClrType(Type clrType);
        InterfaceTypeBuilder<object, TContext> RemoveClrType();
        InterfaceTypeBuilder<TNewInterfaceType, TContext> ClrType<TNewInterfaceType>();
        InterfaceTypeBuilder<TInterface, TContext> ResolveType(TypeResolver<TInterface, TContext> resolveTypeFn);
    }
}