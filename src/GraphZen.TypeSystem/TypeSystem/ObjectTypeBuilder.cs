// Copyright (c) GraphZen LLC. All rights reserved.
// Licensed under the GraphZen Community License. See the LICENSE file in the project root for license information.

using System;
using System.Diagnostics.CodeAnalysis;
using System.Linq.Expressions;
using GraphZen.Infrastructure;
using GraphZen.LanguageModel.Internal;
using GraphZen.TypeSystem.Internal;
using JetBrains.Annotations;

namespace GraphZen.TypeSystem
{
    public class ObjectTypeBuilder<TObject, TContext> : IObjectTypeBuilder<TObject, TContext>
        where TContext : GraphQLContext
    {
        public ObjectTypeBuilder(InternalObjectTypeBuilder builder)
        {
            Check.NotNull(builder, nameof(builder));
            Builder = builder;
        }


        private InternalObjectTypeBuilder Builder { get; }

        InternalObjectTypeBuilder IInfrastructure<InternalObjectTypeBuilder>.Instance => Builder;

        public IObjectTypeBuilder<TObject, TContext> Name(string name)
        {
            Check.NotNull(name, nameof(name));
            Builder.SetName(name, ConfigurationSource.Explicit);
            return this;
        }

        public IObjectTypeBuilder<TObject, TContext> RemoveName() => throw new NotImplementedException();

        public IObjectTypeBuilder<object, TContext> ClrType(Type clrType)
        {
            Check.NotNull(clrType, nameof(clrType));
            Builder.SetClrType(clrType, ConfigurationSource.Explicit);
            return new ObjectTypeBuilder<object, TContext>(Builder);
        }

        public IObjectTypeBuilder<object, TContext> RemoveClrType() => throw new NotImplementedException();

        public IObjectTypeBuilder<T, TContext> ClrType<T>()
        {
            Builder.SetClrType(typeof(T), ConfigurationSource.Explicit);
            return new ObjectTypeBuilder<T, TContext>(Builder);
        }

        public IObjectTypeBuilder<TObject, TContext> SetDescription(string? description)
        {
            Builder.Description(description, ConfigurationSource.Explicit);
            return this;
        }

        public IObjectTypeBuilder<TObject, TContext> RemoveDescription() => throw new NotImplementedException();


        public IObjectTypeBuilder<TObject, TContext> Field(string name,
            Action<IFieldBuilder<TObject, object, TContext>> configurator)
        {
            Check.NotNull(name, nameof(name));
            var ib = Builder.Field(name, ConfigurationSource.Explicit, ConfigurationSource.Explicit)!;

            configurator?.Invoke(new FieldBuilder<TObject, object, TContext>(ib));
            return this;
        }

        public IObjectTypeBuilder<TObject, TContext> Field(string name, string type)
        {
            Check.NotNull(name, nameof(name));
            Check.NotNull(type, nameof(type));
            Builder.Field(name, ConfigurationSource.Explicit, ConfigurationSource.Explicit)?.FieldType(type);
            return this;
        }

        public IFieldBuilder<TObject, object, TContext> Field(string name)
        {
            Check.NotNull(name, nameof(name));
            var ib = Builder.Field(name, ConfigurationSource.Explicit, ConfigurationSource.Explicit)!;
            return new FieldBuilder<TObject, object, TContext>(ib);
        }

        public IObjectTypeBuilder<TObject, TContext> Field(string name, string type,
            Action<IFieldBuilder<TObject, object?, TContext>> configurator)
        {
            Check.NotNull(name, nameof(name));
            Check.NotNull(type, nameof(type));
            Check.NotNull(configurator, nameof(configurator));
            var ib = Builder.Field(name, ConfigurationSource.Explicit, ConfigurationSource.Explicit)?.FieldType(type)!;
            configurator?.Invoke(new FieldBuilder<TObject, object?, TContext>(ib));
            return this;
        }

        public IObjectTypeBuilder<TObject, TContext> Field<TField>(Expression<Func<TObject, TField>> selector)
        {
            Check.NotNull(selector, nameof(selector));
            var property = selector.GetPropertyInfoFromExpression();
            Builder.Field(property, ConfigurationSource.Explicit);
            return this;
        }

        public IObjectTypeBuilder<TObject, TContext> Field<TField>(string name)
        {
            Check.NotNull(name, nameof(name));
            Builder.Field(name, ConfigurationSource.Explicit, ConfigurationSource.Explicit)
                ?.FieldType(typeof(TField));
            return this;
        }

        public IObjectTypeBuilder<TObject, TContext> RemoveField(string name)
        {
            Check.NotNull(name, nameof(name));
            Builder.RemoveField(name, ConfigurationSource.Explicit);
            return this;
        }

        public IObjectTypeBuilder<TObject, TContext> Field<TField>(string name,
            Action<IFieldBuilder<TObject, TField, TContext>> configurator)
        {
            Check.NotNull(name, nameof(name));
            Check.NotNull(configurator, nameof(configurator));
            var ib = Builder.Field(name, ConfigurationSource.Explicit, ConfigurationSource.Explicit)
                ?.FieldType(typeof(TField))!;
            configurator(new FieldBuilder<TObject, TField, TContext>(ib));
            return this;
        }

        public IObjectTypeBuilder<TObject, TContext> Field<TField>(Expression<Func<TObject, TField>> selector,
            Action<IFieldBuilder<TObject, TField, TContext>> configurator)
        {
            Check.NotNull(selector, nameof(selector));
            Check.NotNull(configurator, nameof(configurator));
            var property = selector.GetPropertyInfoFromExpression();
            var fb = Builder.Field(property, ConfigurationSource.Explicit)!;
            configurator(new FieldBuilder<TObject, TField, TContext>(fb));
            return this;
        }

        public IObjectTypeBuilder<TObject, TContext> IsTypeOf(Func<TObject, bool> isTypeOfFn)
        {
            Check.NotNull(isTypeOfFn, nameof(isTypeOfFn));
            Builder.IsTypeOf((value, context, info) => isTypeOfFn((TObject)value));
            return this;
        }

        public IObjectTypeBuilder<TObject, TContext> IsTypeOf(Func<TObject, TContext, bool> isTypeOfFn)
        {
            Check.NotNull(isTypeOfFn, nameof(isTypeOfFn));
            Builder.IsTypeOf((value, context, info) => isTypeOfFn((TObject)value, (TContext)context));
            return this;
        }

        public IObjectTypeBuilder<TObject, TContext> IsTypeOf(Func<TObject, TContext, ResolveInfo, bool> isTypeOfFn)
        {
            Check.NotNull(isTypeOfFn, nameof(isTypeOfFn));
            Builder.IsTypeOf((value, context, info) => isTypeOfFn((TObject)value, (TContext)context, info));
            return this;
        }

        public IObjectTypeBuilder<TObject, TContext> ImplementsInterface(string name)
        {
            Check.NotNull(name, nameof(name));
            Builder.ImplementsInterface(name, ConfigurationSource.Explicit);
            return this;
        }

        public IObjectTypeBuilder<TObject, TContext> ImplementsInterfaces(string name, params string[] names)
        {
            ImplementsInterface(name);
            if (names != null)
            {
                foreach (var n in names)
                {
                    ImplementsInterface(n);
                }
            }

            return this;
        }


        public IObjectTypeBuilder<TObject, TContext> IgnoreInterface<T>()
        {
            Builder.IgnoreInterface(typeof(T).GetGraphQLName(), ConfigurationSource.Explicit);

            return this;
        }

        public IObjectTypeBuilder<TObject, TContext> IgnoreInterface(Type clrType)
        {
            Check.NotNull(clrType, nameof(clrType));
            Builder.IgnoreInterface(clrType.GetGraphQLName(), ConfigurationSource.Explicit);
            return this;
        }

        public IObjectTypeBuilder<TObject, TContext> IgnoreInterface(string name)
        {
            Check.NotNull(name, nameof(name));
            Builder.IgnoreInterface(name, ConfigurationSource.Explicit);
            return this;
        }

        public IObjectTypeBuilder<TObject, TContext> UnignoreInterface(string name)
        {
            Check.NotNull(name, nameof(name));
            Builder.UnignoreInterface(name, ConfigurationSource.Explicit);
            return this;
        }

        public IObjectTypeBuilder<TObject, TContext> IgnoreField<TField>(
            Expression<Func<TObject, TField>> selector) =>
            throw new NotImplementedException();

        public IObjectTypeBuilder<TObject, TContext> IgnoreField(string name)
        {
            Check.NotNull(name, nameof(name));
            Builder.IgnoreField(name, ConfigurationSource.Explicit);
            return this;
        }

        public IObjectTypeBuilder<TObject, TContext> UnignoreField(string name)
        {
            Check.NotNull(name, nameof(name));
            Builder.UnignoreField(name, ConfigurationSource.Explicit);
            return this;
        }

        public IObjectTypeBuilder<TObject, TContext> AddDirectiveAnnotation(string name, object? value = null) =>
            throw new NotImplementedException();

        public IObjectTypeBuilder<TObject, TContext> UpdateOrAddDirectiveAnnotation(string name, object? value = null)
        {
            Builder.DirectiveAnnotation(Check.NotNull(name, nameof(name)), value, ConfigurationSource.Explicit);
            return this;
        }

        public IObjectTypeBuilder<TObject, TContext> RemoveDirectiveAnnotations(string name) =>
            throw new NotImplementedException();

        public IObjectTypeBuilder<TObject, TContext> RemoveDirectiveAnnotations() =>
            throw new NotImplementedException();

        public IObjectTypeBuilder<TObject, TContext> IgnoreField(Expression<Func<TObject, object>> fieldSelector) =>
            throw new NotImplementedException();
    }
}