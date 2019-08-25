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
        IFieldsContainerDefinitionBuilder<
            IObjectTypeBuilder<TObject, TContext>, TObject, TContext> where TContext : GraphQLContext
    {
        IObjectTypeBuilder<TObject, TContext> Name(string name);


        IObjectTypeBuilder<object, TContext> ClrType(Type clrType);


        IObjectTypeBuilder<T, TContext> ClrType<T>();


        IObjectTypeBuilder<TObject, TContext> Description(string description);


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