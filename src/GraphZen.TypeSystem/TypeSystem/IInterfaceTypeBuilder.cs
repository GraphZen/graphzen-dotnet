// Copyright (c) GraphZen LLC. All rights reserved.
// Licensed under the GraphZen Community License. See the LICENSE file in the project root for license information.

using System;
using System.Diagnostics.CodeAnalysis;
using GraphZen.Infrastructure;
using GraphZen.TypeSystem.Internal;
using JetBrains.Annotations;

namespace GraphZen.TypeSystem
{
    public interface INameBuilder<out TBuilder>
    {
        TBuilder Name(string name);
    }

    public interface
        IInterfaceTypeBuilder<TInterface, TContext> :
            IAnnotableBuilder<IInterfaceTypeBuilder<TInterface, TContext>>,
            IFieldsDefinitionBuilder<IInterfaceTypeBuilder<TInterface, TContext>, TInterface, TContext>,
            INameBuilder<IInterfaceTypeBuilder<TInterface, TContext>>,
            IDescriptionBuilder<IInterfaceTypeBuilder<TInterface, TContext>> where TContext : GraphQLContext
    {
        IInterfaceTypeBuilder<object, TContext> ClrType(Type clrType);
        IInterfaceTypeBuilder<object, TContext> RemoveClrType();
        IInterfaceTypeBuilder<TNewInterfaceType, TContext> ClrType<TNewInterfaceType>();
        IInterfaceTypeBuilder<TInterface, TContext> ResolveType(TypeResolver<TInterface, TContext> resolveTypeFn);
    }
}