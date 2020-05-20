// Copyright (c) GraphZen LLC. All rights reserved.
// Licensed under the GraphZen Community License. See the LICENSE file in the project root for license information.

using System;
using System.Diagnostics.CodeAnalysis;
using GraphZen.Infrastructure;
using JetBrains.Annotations;

namespace GraphZen.TypeSystem.Internal
{
    internal interface IObjectTypeBuilder<TObject, TContext> :
        IInfrastructure<InternalObjectTypeBuilder>,
        IImplementsInterfacesBuilder<ObjectTypeBuilder<TObject, TContext>>,
        IDescriptionBuilder<ObjectTypeBuilder<TObject, TContext>>,
        IAnnotableBuilder<ObjectTypeBuilder<TObject, TContext>>,
        IClrTypeBuilder<ObjectTypeBuilder<object, TContext>>,
        INamedBuilder<ObjectTypeBuilder<TObject, TContext>>,
        IFieldsDefinitionBuilder<ObjectTypeBuilder<TObject, TContext>, TObject, TContext>
        where TObject : notnull
        where TContext : GraphQLContext
    {
        ObjectTypeBuilder<T, TContext> ClrType<T>(bool inferName = false) where T : notnull;
        ObjectTypeBuilder<T, TContext> ClrType<T>(string name) where T : notnull;
        ObjectTypeBuilder<TObject, TContext> IsTypeOf(Func<TObject, bool> isTypeOfFn);
        ObjectTypeBuilder<TObject, TContext> IsTypeOf(Func<TObject, TContext, bool> isTypeOfFn);
        ObjectTypeBuilder<TObject, TContext> IsTypeOf(Func<TObject, TContext, ResolveInfo, bool> isTypeOfFn);
    }
}