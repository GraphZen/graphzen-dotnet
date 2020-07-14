// Copyright (c) GraphZen LLC. All rights reserved.
// Licensed under the GraphZen Community License. See the LICENSE file in the project root for license information.

using System;
using System.Diagnostics.CodeAnalysis;
using System.Linq.Expressions;
using GraphZen.Infrastructure;
using JetBrains.Annotations;

namespace GraphZen.TypeSystem
{
    public interface IFieldsBuilder
    {
        IFieldsBuilder Field<TField>(string name);
        IFieldsBuilder RemoveField(string name);
        IFieldsBuilder Field<TField>(string name, Action<IFieldBuilder<object, GraphQLContext, TField>> fieldAction);
        IFieldBuilder Field(string name);
        IFieldsBuilder Field(string name, string type);
        IFieldsBuilder Field(string name, string type, Action<IFieldBuilder> fieldAction);
        IFieldsBuilder IgnoreField(string name);
        IFieldsBuilder UnignoreField(string name);
    }

    public interface IFieldsBuilder<out TBuilder, TSource, TContext> : IFieldsBuilder
    {
        new TBuilder Field<TField>(string name);
        new TBuilder RemoveField(string name);
        TBuilder Field<TField>(string name, Action<IFieldBuilder<TSource, TContext, TField>> fieldAction);
        new TBuilder Field(string name, string type);
        new TBuilder Field(string name, string type, Action<IFieldBuilder> fieldAction);
        new TBuilder IgnoreField(string name);
        new TBuilder UnignoreField(string name);
    }
}