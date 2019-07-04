// Copyright (c) GraphZen LLC. All rights reserved.
// Licensed under the GraphZen Community License. See the LICENSE file in the project root for license information.

using System;
using System.Collections;
using GraphZen.Infrastructure;
using JetBrains.Annotations;

namespace GraphZen.Types
{
    public interface
        IFieldBuilder<out TDeclaringType, in TField, TContext> : IAnnotableBuilder<
            IFieldBuilder<TDeclaringType, TField, TContext>> where TContext : GraphQLContext
    {
        [NotNull]
        IFieldBuilder<TDeclaringType, object, TContext> FieldType(string type);

        [NotNull]
        IFieldBuilder<TDeclaringType, TField, TContext> Name(string name);


        [NotNull]
        IFieldBuilder<TDeclaringType, TFieldNew, TContext> FieldType<TFieldNew>(bool canBeNull = false,
            bool itemCanBeNull = false) where TFieldNew : IEnumerable;

        [NotNull]
        IFieldBuilder<TDeclaringType, TField, TContext> Description([CanBeNull] string description);

        [NotNull]
        IFieldBuilder<TDeclaringType, TField, TContext> Resolve(Func<TField> resolver);

        [NotNull]
        IFieldBuilder<TDeclaringType, TField, TContext> Resolve(Func<TDeclaringType, TField> resolver);

        [NotNull]
        IFieldBuilder<TSource, TField, TContext> Resolve<TSource>(Func<TSource, TField> resolver);


        [NotNull]
        IFieldBuilder<TDeclaringType, TField, TContext> Resolve(Func<TDeclaringType, dynamic, TField> resolver);

        [NotNull]
        IFieldBuilder<TDeclaringType, TField, TContext> Resolve(
            Func<TDeclaringType, dynamic, GraphQLContext, TField> resolver);

        [NotNull]
        IFieldBuilder<TDeclaringType, TField, TContext> Resolve(
            Func<TDeclaringType, dynamic, GraphQLContext, ResolveInfo, TField> resolver);

        [NotNull]
        IFieldBuilder<TDeclaringType, TField, TContext> Argument(string name, string type,
            Action<InputValueBuilder> argumentBuilder = null);

        [NotNull]
        IFieldBuilder<TDeclaringType, TField, TContext> Argument<TArg>(string name,
            Action<InputValueBuilder> argumentBuilder = null);

        [NotNull]
        IFieldBuilder<TDeclaringType, TField, TContext> Deprecated(string reason);

        [NotNull]
        IFieldBuilder<TDeclaringType, TField, TContext> Deprecated(bool deprecated = true);
    }
}