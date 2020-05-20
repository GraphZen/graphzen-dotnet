// Copyright (c) GraphZen LLC. All rights reserved.
// Licensed under the GraphZen Community License. See the LICENSE file in the project root for license information.

using System.Diagnostics.CodeAnalysis;
using GraphZen.Infrastructure;
using JetBrains.Annotations;

namespace GraphZen.TypeSystem.Internal
{
    internal interface IInterfaceTypeBuilder<TInterface, TContext> :
        IInfrastructure<InternalInterfaceTypeBuilder>,
        IClrTypeBuilder<InterfaceTypeBuilder<object, TContext>>,
        IAnnotableBuilder<InterfaceTypeBuilder<TInterface, TContext>>,
        IFieldsDefinitionBuilder<InterfaceTypeBuilder<TInterface, TContext>, TInterface, TContext>,
        INamedBuilder<InterfaceTypeBuilder<TInterface, TContext>>,
        IDescriptionBuilder<InterfaceTypeBuilder<TInterface, TContext>>
        where TContext : GraphQLContext
        where TInterface : notnull
    {
        InterfaceTypeBuilder<T, TContext> ClrType<T>(string name) where T : notnull;
        InterfaceTypeBuilder<T, TContext> ClrType<T>(bool inferName = false) where T : notnull;
        InterfaceTypeBuilder<TInterface, TContext> ResolveType(TypeResolver<TInterface, TContext> resolveTypeFn);
    }
}