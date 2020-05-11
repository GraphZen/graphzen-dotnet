// Copyright (c) GraphZen LLC. All rights reserved.
// Licensed under the GraphZen Community License. See the LICENSE file in the project root for license information.

using System;
using System.Diagnostics.CodeAnalysis;
using System.Linq.Expressions;
using GraphZen.Infrastructure;
using JetBrains.Annotations;

namespace GraphZen.TypeSystem
{
    public interface IFieldsDefinitionBuilder<out TBuilder, TSource, TContext> where TContext : GraphQLContext
    {
        TBuilder Field<TField>(string name);
        TBuilder Field<TField>(string name, Action<IFieldBuilder<TSource, TField, TContext>> configurator);

        TBuilder Field(string name, Action<IFieldBuilder<TSource, object, TContext>> configurator);

        IFieldBuilder<TSource, object, TContext> Field(string name);

        TBuilder Field(string name, string type,
            Action<IFieldBuilder<TSource, object?, TContext>>? configurator = null);

        TBuilder Field<TField>(Expression<Func<TSource, TField>> selector,
            Action<IFieldBuilder<TSource, TField, TContext>>? configurator = null);

        TBuilder IgnoreField<TField>(Expression<Func<TSource, TField>> selector);

        TBuilder IgnoreField(string name);

        TBuilder UnignoreField(string name);
    }
}