// Copyright (c) GraphZen LLC. All rights reserved.
// Licensed under the GraphZen Community License. See the LICENSE file in the project root for license information.

using System;
using System.Diagnostics.CodeAnalysis;
using GraphZen.Infrastructure;
using GraphZen.TypeSystem.Internal;
using JetBrains.Annotations;

namespace GraphZen.TypeSystem
{
    public interface IImplementsInterfacesBuilder<out TBuilder>
    {
        TBuilder ImplementsInterface(string name);

        TBuilder ImplementsInterfaces(string name, params string[] names);
        TBuilder IgnoreInterface<T>();
        TBuilder IgnoreInterface(Type clrType);
        TBuilder IgnoreInterface(string name);
        TBuilder UnignoreInterface(string name);
    }

    public interface IClrTypeBuilder<out TUntypedBuilder>
    {
        TUntypedBuilder ClrType(Type clrType);
        TUntypedBuilder ClrType(Type clrType, string name);
        TUntypedBuilder RemoveClrType();
    }

    internal interface IObjectTypeBuilder<TObject, TContext> :
        IImplementsInterfacesBuilder<ObjectTypeBuilder<TObject, TContext>>,
        IInfrastructure<InternalObjectTypeBuilder>,
        IDescriptionBuilder<ObjectTypeBuilder<TObject, TContext>>,
        IAnnotableBuilder<ObjectTypeBuilder<TObject, TContext>>,
        IClrTypeBuilder<ObjectTypeBuilder<object, TContext>>,
        INameBuilder<ObjectTypeBuilder<TObject, TContext>>,
        IFieldsDefinitionBuilder<ObjectTypeBuilder<TObject, TContext>, TObject, TContext>
        where TContext : GraphQLContext
    {
        ObjectTypeBuilder<T, TContext> ClrType<T>();
        ObjectTypeBuilder<T, TContext> ClrType<T>(string name);
        ObjectTypeBuilder<TObject, TContext> IsTypeOf(Func<TObject, bool> isTypeOfFn);
        ObjectTypeBuilder<TObject, TContext> IsTypeOf(Func<TObject, TContext, bool> isTypeOfFn);
        ObjectTypeBuilder<TObject, TContext> IsTypeOf(Func<TObject, TContext, ResolveInfo, bool> isTypeOfFn);
    }
}