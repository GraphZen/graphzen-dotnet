// Copyright (c) GraphZen LLC. All rights reserved.
// Licensed under the GraphZen Community License. See the LICENSE file in the project root for license information.

using System;
using System.Diagnostics.CodeAnalysis;
using GraphZen.Infrastructure;
using JetBrains.Annotations;

namespace GraphZen.TypeSystem
{
    public interface
        IInterfaceTypeBuilder<TInterface, TContext> : IAnnotableBuilder<IInterfaceTypeBuilder<TInterface, TContext>>,
            IFieldsDefinitionBuilder<IInterfaceTypeBuilder<TInterface, TContext>, TInterface, TContext>
        where TContext : GraphQLContext
    {
        IInterfaceTypeBuilder<TInterface, TContext> Description(string? description);


        IInterfaceTypeBuilder<object, TContext> ClrType(Type clrType);


        IInterfaceTypeBuilder<TNewInterfaceType, TContext> ClrType<TNewInterfaceType>();


        IInterfaceTypeBuilder<TInterface, TContext>
            ResolveType(TypeResolver<TInterface, TContext> resolveTypeFn);


        IInterfaceTypeBuilder<TInterface, TContext> Name(string newName);
    }
}