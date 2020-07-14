// Copyright (c) GraphZen LLC. All rights reserved.
// Licensed under the GraphZen Community License. See the LICENSE file in the project root for license information.

using System;
using System.Collections;
using System.Diagnostics.CodeAnalysis;
using GraphZen.Infrastructure;
using GraphZen.TypeSystem.Internal;
using JetBrains.Annotations;

namespace GraphZen.TypeSystem
{

    // ReSharper disable once PossibleInterfaceMemberAmbiguity
    public interface IFieldBuilder : IDirectivesBuilder<IFieldBuilder>, IArgumentsBuilder<IFieldBuilder>, INameBuilder<IFieldBuilder>, IDescriptionBuilder<IFieldBuilder>, IMaybeDeprecatedBuilder<IFieldBuilder>, IInfrastructure<InternalFieldBuilder>, IInfrastructure<MutableField>
    {
        IFieldBuilder FieldType(string type);
        IFieldBuilder FieldType(Type clrType, bool canBeNull = false, bool itemCanBeNull = false);
        IFieldBuilder<object, GraphQLContext, TField> FieldType<TField>();
        IFieldBuilder Resolve(Resolver<object, dynamic, GraphQLContext, object?> resolver);
        IFieldBuilder RemoveResolver();
    }

    // ReSharper disable once PossibleInterfaceMemberAmbiguity
    public interface IFieldBuilder<TSource, TContext, in TField> : IFieldBuilder,
        IDirectivesBuilder<IFieldBuilder<TSource, TContext, TField>>,
        IArgumentsBuilder<IFieldBuilder<TSource, TContext, TField>>,
        INameBuilder<IFieldBuilder<TSource, TContext, TField>>,
        IDescriptionBuilder<IFieldBuilder<TSource, TContext, TField>>,
        IMaybeDeprecatedBuilder<IFieldBuilder<TSource, TContext, TField>>
    {
        new IFieldBuilder<TSource, TContext, TField> FieldType(string type);
        new IFieldBuilder<TSource, TContext, TField> FieldType(Type clrType, bool canBeNull = false, bool itemCanBeNull = false);
        new IFieldBuilder<TSource, TContext, TFieldNew> FieldType<TFieldNew>();
        IFieldBuilder<TSource, TContext, TField> Resolve(Resolver<object, dynamic, GraphQLContext, TField> resolver);
        new IFieldBuilder<TSource, TContext, TField> RemoveResolver();
    }



}