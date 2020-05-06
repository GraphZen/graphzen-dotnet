// Copyright (c) GraphZen LLC. All rights reserved.
// Licensed under the GraphZen Community License. See the LICENSE file in the project root for license information.

using System;
using System.Diagnostics.CodeAnalysis;
using GraphZen.Infrastructure;
using GraphZen.TypeSystem.Internal;
using JetBrains.Annotations;

namespace GraphZen.TypeSystem
{
    public interface IObjectTypeBuilder<TObject, TContext> :
        IInfrastructure<InternalObjectTypeBuilder>,
        IAnnotableBuilder<IObjectTypeBuilder<TObject, TContext>>,
        IFieldsDefinitionBuilder<
            IObjectTypeBuilder<TObject, TContext>, TObject, TContext> where TContext : GraphQLContext
    {
        IObjectTypeBuilder<TObject, TContext> SetName(string name);

        IObjectTypeBuilder<object, TContext> SetClrType(Type clrType);
        IObjectTypeBuilder<object, TContext> RemoveClrType();

        IObjectTypeBuilder<T, TContext> SetClrType<T>();

        IObjectTypeBuilder<TObject, TContext> SetDescription(string description);
        IObjectTypeBuilder<TObject, TContext> RemoveDescription();


        IObjectTypeBuilder<TObject, TContext> IsTypeOf(Func<TObject, bool> isTypeOfFn);


        IObjectTypeBuilder<TObject, TContext> IsTypeOf(Func<TObject, TContext, bool> isTypeOfFn);


        IObjectTypeBuilder<TObject, TContext> IsTypeOf(Func<TObject, TContext, ResolveInfo, bool> isTypeOfFn);


        IObjectTypeBuilder<TObject, TContext> ImplementsInterface(string name);

        IObjectTypeBuilder<TObject, TContext> ImplementsInterfaces(string name, params string[] names);


        IObjectTypeBuilder<TObject, TContext> IgnoreInterface<T>();


        IObjectTypeBuilder<TObject, TContext> IgnoreInterface(Type clrType);


        IObjectTypeBuilder<TObject, TContext> IgnoreInterface(string name);


        IObjectTypeBuilder<TObject, TContext> UnignoreInterface(string name);
    }
}