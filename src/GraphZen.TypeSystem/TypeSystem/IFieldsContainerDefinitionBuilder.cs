// Copyright (c) GraphZen LLC. All rights reserved.
// Licensed under the GraphZen Community License. See the LICENSE file in the project root for license information.

using System;
using System.Linq.Expressions;
using GraphZen.Infrastructure;

namespace GraphZen.TypeSystem
{
    public interface IFieldsContainerDefinitionBuilder<out TBuilder, TSource, TContext> where TContext : GraphQLContext
    {
        [NotNull]
        TBuilder Field<TField>(string name, Action<IFieldBuilder<TSource, TField, TContext>> fieldConfigurator = null);
        [NotNull]
        TBuilder Field(string name, Action<IFieldBuilder<TSource, object, TContext>> fieldConfigurator = null);


        [NotNull]
        TBuilder Field(string name, string type,
            Action<IFieldBuilder<TSource, object, TContext>> fieldConfigurator = null);


        [NotNull]
        TBuilder Field<TField>(Expression<Func<TSource, TField>> fieldSelector,
            Action<IFieldBuilder<TSource, TField, TContext>> fieldBuilder = null);

        [NotNull]
        TBuilder IgnoreField<TField>(Expression<Func<TSource, TField>> fieldSelector);

        [NotNull]
        TBuilder IgnoreField(string fieldName);

        [NotNull]
        TBuilder UnignoreField(string fieldName);
    }
}