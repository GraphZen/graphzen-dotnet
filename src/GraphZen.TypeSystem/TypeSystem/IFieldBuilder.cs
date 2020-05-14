// Copyright (c) GraphZen LLC. All rights reserved.
// Licensed under the GraphZen Community License. See the LICENSE file in the project root for license information.

using System;
using System.Collections;
using System.Diagnostics.CodeAnalysis;
using GraphZen.Infrastructure;
using GraphZen.TypeSystem.Internal;
using GraphZen.TypeSystem.Taxonomy;
using JetBrains.Annotations;

namespace GraphZen.TypeSystem
{
    // ReSharper disable once PossibleInterfaceMemberAmbiguity
    public interface IFieldBuilder<out TDeclaringType, in TField, TContext> :
        IAnnotableBuilder<IFieldBuilder<TDeclaringType, TField, TContext>>,
        IArgumentsDefinitionBuilder<IFieldBuilder<TDeclaringType, TField, TContext>>,
        IInfrastructure<IFieldDefinition>,
        IInfrastructure<InternalFieldBuilder>
        where TContext : GraphQLContext
    {
        IFieldBuilder<TDeclaringType, object, TContext> FieldType(string type);


        IFieldBuilder<TDeclaringType, TField, TContext> Name(string name);


        IFieldBuilder<TDeclaringType, TFieldNew, TContext> FieldType<TFieldNew>(bool canBeNull = false,
            bool itemCanBeNull = false) where TFieldNew : IEnumerable;


        IFieldBuilder<TDeclaringType, TField, TContext> Description(string description);
        IFieldBuilder<TDeclaringType, TField, TContext> RemoveDescription();


        IFieldBuilder<TDeclaringType, TField, TContext> Resolve(Func<TField> resolver);


        IFieldBuilder<TDeclaringType, TField, TContext> Resolve(Func<TDeclaringType, TField> resolver);


        IFieldBuilder<TSource, TField, TContext> Resolve<TSource>(Func<TSource, TField> resolver);


        IFieldBuilder<TDeclaringType, TField, TContext> Resolve(Func<TDeclaringType, dynamic, TField> resolver);


        IFieldBuilder<TDeclaringType, TField, TContext> Resolve(
            Func<TDeclaringType, dynamic, GraphQLContext, TField> resolver);

        IFieldBuilder<TDeclaringType, TField, TContext> Resolve(
            Func<TDeclaringType, dynamic, GraphQLContext, ResolveInfo, TField> resolver);


        IFieldBuilder<TDeclaringType, TField, TContext> Deprecated(string reason);


        IFieldBuilder<TDeclaringType, TField, TContext> Deprecated(bool deprecated = true);
    }
}