#nullable disable
// Copyright (c) GraphZen LLC. All rights reserved.
// Licensed under the GraphZen Community License. See the LICENSE file in the project root for license information.

using System;
using GraphZen.Infrastructure;
using GraphZen.TypeSystem.Internal;

namespace GraphZen.TypeSystem
{
    public interface IObjectTypeBuilder<TObject, TContext> :
        IInfrastructure<InternalObjectTypeBuilder>,
        IAnnotableBuilder<IObjectTypeBuilder<TObject, TContext>>,
        IFieldsContainerDefinitionBuilder<
            IObjectTypeBuilder<TObject, TContext>, TObject, TContext> where TContext : GraphQLContext
    {
        [NotNull]
        IObjectTypeBuilder<TObject, TContext> Name(string name);

        [NotNull]
        IObjectTypeBuilder<object, TContext> ClrType(Type clrType);

        [NotNull]
        IObjectTypeBuilder<T, TContext> ClrType<T>();

        [NotNull]
        IObjectTypeBuilder<TObject, TContext> Description([CanBeNull] string description);

        [NotNull]
        IObjectTypeBuilder<TObject, TContext> IsTypeOf(Func<TObject, bool> isTypeOfFn);

        [NotNull]
        IObjectTypeBuilder<TObject, TContext> IsTypeOf(Func<TObject, TContext, bool> isTypeOfFn);

        [NotNull]
        IObjectTypeBuilder<TObject, TContext> IsTypeOf(Func<TObject, TContext, ResolveInfo, bool> isTypeOfFn);

        [NotNull]
        IObjectTypeBuilder<TObject, TContext> ImplementsInterface(string name);
        [NotNull]
        IObjectTypeBuilder<TObject, TContext> ImplementsInterfaces(string name, params string[] names);


        [NotNull]
        IObjectTypeBuilder<TObject, TContext> IgnoreInterface<T>();

        [NotNull]
        IObjectTypeBuilder<TObject, TContext> IgnoreInterface(Type clrType);

        [NotNull]
        IObjectTypeBuilder<TObject, TContext> IgnoreInterface(string name);

        [NotNull]
        IObjectTypeBuilder<TObject, TContext> UnignoreInterface(string name);

    }
}