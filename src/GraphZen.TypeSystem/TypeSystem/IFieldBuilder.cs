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
    public interface IFieldBuilder<TDeclaringType, TField, TContext> :
        IAnnotableBuilder<FieldBuilder<TDeclaringType, TField, TContext>>,
        IArgumentsDefinitionBuilder<FieldBuilder<TDeclaringType, TField, TContext>>,
        INameBuilder<FieldBuilder<TDeclaringType, TField, TContext>>,
        IDescriptionBuilder<FieldBuilder<TDeclaringType, TField, TContext>>,
        IInfrastructure<IFieldDefinition>,
        IInfrastructure<InternalFieldBuilder>
        where TContext : GraphQLContext
    {
        FieldBuilder<TDeclaringType, object, TContext> FieldType(string type);


        FieldBuilder<TDeclaringType, TFieldNew, TContext> FieldType<TFieldNew>(bool canBeNull = false,
            bool itemCanBeNull = false) where TFieldNew : IEnumerable;


        FieldBuilder<TDeclaringType, TField, TContext> Resolve(Func<TField> resolver);


        FieldBuilder<TDeclaringType, TField, TContext> Resolve(Func<TDeclaringType, TField> resolver);


        FieldBuilder<TSource, TField, TContext> Resolve<TSource>(Func<TSource, TField> resolver);


        FieldBuilder<TDeclaringType, TField, TContext> Resolve(Func<TDeclaringType, dynamic, TField> resolver);


        FieldBuilder<TDeclaringType, TField, TContext> Resolve(
            Func<TDeclaringType, dynamic, GraphQLContext, TField> resolver);

        FieldBuilder<TDeclaringType, TField, TContext> Resolve(
            Func<TDeclaringType, dynamic, GraphQLContext, ResolveInfo, TField> resolver);
    }
}