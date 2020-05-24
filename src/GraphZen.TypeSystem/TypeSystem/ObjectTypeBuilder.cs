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
        where TObject : notnull
        where TContext : GraphQLContext
    {
        public ObjectTypeBuilder(InternalObjectTypeBuilder builder)
        {
            Check.NotNull(builder, nameof(builder));
            Builder = builder;
        }


        private InternalObjectTypeBuilder Builder { get; }

        InternalObjectTypeBuilder IInfrastructure<InternalObjectTypeBuilder>.Instance => Builder;

        public ObjectTypeBuilder<TObject, TContext> Name(string name)
        {
            Check.NotNull(name, nameof(name));
            Builder.Name(name, ConfigurationSource.Explicit);
            return this;
        }

        public ObjectTypeBuilder<object, TContext> ClrType(Type clrType, bool inferName = false)
        {
            Check.NotNull(clrType, nameof(clrType));
            Builder.ClrType(clrType, inferName, ConfigurationSource.Explicit);
            return new ObjectTypeBuilder<object, TContext>(Builder);
        }


        public ObjectTypeBuilder<object, TContext> ClrType(Type clrType, string name)
        {
            Check.NotNull(clrType, nameof(clrType));
            Check.NotNull(name, nameof(name));
            Builder.ClrType(clrType, name, ConfigurationSource.Explicit);
            return new ObjectTypeBuilder<object, TContext>(Builder);
        }

        public ObjectTypeBuilder<object, TContext> RemoveClrType()
        {
            Builder.RemoveClrType(ConfigurationSource.Explicit);

            return new ObjectTypeBuilder<object, TContext>(Builder);
        }

        public ObjectTypeBuilder<T, TContext> ClrType<T>(bool inferName = false) where T : notnull
        {
            Builder.ClrType(typeof(T), inferName, ConfigurationSource.Explicit);
            return new ObjectTypeBuilder<T, TContext>(Builder);
        }

        public ObjectTypeBuilder<T, TContext> ClrType<T>(string name) where T : notnull
        {
            Check.NotNull(name, nameof(name));
            Builder.ClrType(typeof(T), name, ConfigurationSource.Explicit);
            return new ObjectTypeBuilder<T, TContext>(Builder);
        }

        public ObjectTypeBuilder<TObject, TContext> Description(string description)
        {
            Check.NotNull(description, nameof(description));
            Builder.Description(description, ConfigurationSource.Explicit);
            return this;
        }

        public ObjectTypeBuilder<TObject, TContext> RemoveDescription()
        {
            Builder.RemoveDescription(ConfigurationSource.Explicit);
            return this;
        }


        public ObjectTypeBuilder<TObject, TContext> Field(string name,
            Action<FieldBuilder<TObject, object, TContext>> configurator)
        {
            Check.NotNull(name, nameof(name));
            var ib = Builder.Field(name);

            configurator?.Invoke(new FieldBuilder<TObject, object, TContext>(ib));
            return this;
        }

        public ObjectTypeBuilder<TObject, TContext> Field(string name, string type)
        {
            Check.NotNull(name, nameof(name));
            Check.NotNull(type, nameof(type));
            Builder.Field(name, type, ConfigurationSource.Explicit);
            return this;
        }

        public FieldBuilder<TObject, object, TContext> Field(string name)
        {
            Check.NotNull(name, nameof(name));
            var ib = Builder.Field(name);
            return new FieldBuilder<TObject, object, TContext>(ib);
        }

        public ObjectTypeBuilder<TObject, TContext> Field(string name, string type,
            Action<FieldBuilder<TObject, object?, TContext>> configurator)
        {
            Check.NotNull(name, nameof(name));
            Check.NotNull(type, nameof(type));
            Check.NotNull(configurator, nameof(configurator));
            var ib = Builder.Field(name, type, ConfigurationSource.Explicit)!;
            configurator?.Invoke(new FieldBuilder<TObject, object?, TContext>(ib));
            return this;
        }

        public ObjectTypeBuilder<TObject, TContext> Field<TField>(Expression<Func<TObject, TField>> selector)
        {
            Check.NotNull(selector, nameof(selector));
            var property = selector.GetPropertyInfoFromExpression();
            Builder.Field(property, ConfigurationSource.Explicit);
            return this;
        }

        public ObjectTypeBuilder<TObject, TContext> Field<TField>(string name)
        {
            Check.NotNull(name, nameof(name));
            Builder.Field(name, typeof(TField), ConfigurationSource.Explicit);
            return this;
        }

        public ObjectTypeBuilder<TObject, TContext> RemoveField(string name)
        {
            Check.NotNull(name, nameof(name));
            Builder.RemoveField(name, ConfigurationSource.Explicit);
            return this;
        }

        public ObjectTypeBuilder<TObject, TContext> Field<TField>(string name,
            Action<FieldBuilder<TObject, TField, TContext>> configurator)
        {
            Check.NotNull(name, nameof(name));
            Check.NotNull(configurator, nameof(configurator));
            var ib = Builder.Field(name, typeof(TField), ConfigurationSource.Explicit)!;
            configurator(new FieldBuilder<TObject, TField, TContext>(ib));
            return this;
        }

        public ObjectTypeBuilder<TObject, TContext> Field<TField>(Expression<Func<TObject, TField>> selector,
            Action<FieldBuilder<TObject, TField, TContext>> configurator)
        {
            Check.NotNull(selector, nameof(selector));
            Check.NotNull(configurator, nameof(configurator));
            var property = selector.GetPropertyInfoFromExpression();
            var fb = Builder.Field(property, ConfigurationSource.Explicit)!;
            configurator(new FieldBuilder<TObject, TField, TContext>(fb));
            return this;
        }

        public ObjectTypeBuilder<TObject, TContext> IsTypeOf(Func<TObject, bool> isTypeOfFn)
        {
            Check.NotNull(isTypeOfFn, nameof(isTypeOfFn));
            Builder.IsTypeOf((value, context, info) => isTypeOfFn((TObject)value));
            return this;
        }

        public ObjectTypeBuilder<TObject, TContext> IsTypeOf(Func<TObject, TContext, bool> isTypeOfFn)
        {
            Check.NotNull(isTypeOfFn, nameof(isTypeOfFn));
            Builder.IsTypeOf((value, context, info) => isTypeOfFn((TObject)value, (TContext)context));
            return this;
        }

        public ObjectTypeBuilder<TObject, TContext> IsTypeOf(Func<TObject, TContext, ResolveInfo, bool> isTypeOfFn)
        {
            Check.NotNull(isTypeOfFn, nameof(isTypeOfFn));
            Builder.IsTypeOf((value, context, info) => isTypeOfFn((TObject)value, (TContext)context, info));
            return this;
        }

        public ObjectTypeBuilder<TObject, TContext> ImplementsInterface(string name)
        {
            Check.NotNull(name, nameof(name));
            Builder.ImplementsInterface(name, ConfigurationSource.Explicit);
            return this;
        }

        public ObjectTypeBuilder<TObject, TContext> ImplementsInterfaces(string name, params string[] names)
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


        public ObjectTypeBuilder<TObject, TContext> IgnoreInterface<T>()
        {
            Builder.IgnoreInterface(typeof(T).GetGraphQLName(), ConfigurationSource.Explicit);

            return this;
        }

        public ObjectTypeBuilder<TObject, TContext> IgnoreInterface(Type clrType)
        {
            Check.NotNull(clrType, nameof(clrType));
            Builder.IgnoreInterface(clrType.GetGraphQLName(), ConfigurationSource.Explicit);
            return this;
        }

        public ObjectTypeBuilder<TObject, TContext> IgnoreInterface(string name)
        {
            Check.NotNull(name, nameof(name));
            Builder.IgnoreInterface(name, ConfigurationSource.Explicit);
            return this;
        }

        public ObjectTypeBuilder<TObject, TContext> UnignoreInterface(string name)
        {
            Check.NotNull(name, nameof(name));
            Builder.UnignoreInterface(name, ConfigurationSource.Explicit);
            return this;
        }

        public ObjectTypeBuilder<TObject, TContext> IgnoreField<TField>(
            Expression<Func<TObject, TField>> selector) =>
            throw new NotImplementedException();

        public ObjectTypeBuilder<TObject, TContext> IgnoreField(string name)
        {
            Check.NotNull(name, nameof(name));
            Builder.IgnoreField(name, ConfigurationSource.Explicit);
            return this;
        }

        public ObjectTypeBuilder<TObject, TContext> UnignoreField(string name)
        {
            Check.NotNull(name, nameof(name));
            Builder.UnignoreField(name, ConfigurationSource.Explicit);
            return this;
        }

        public ObjectTypeBuilder<TObject, TContext> AddDirectiveAnnotation(string name, object? value = null) =>
            throw new NotImplementedException();

        public ObjectTypeBuilder<TObject, TContext> UpdateOrAddDirectiveAnnotation(string name, object? value = null)
        {
            Builder.DirectiveAnnotation(Check.NotNull(name, nameof(name)), value, ConfigurationSource.Explicit);
            return this;
        }

        public ObjectTypeBuilder<TObject, TContext> RemoveDirectiveAnnotations(string name) =>
            throw new NotImplementedException();

        public ObjectTypeBuilder<TObject, TContext> RemoveDirectiveAnnotations() =>
            throw new NotImplementedException();

        public ObjectTypeBuilder<TObject, TContext> IgnoreField(Expression<Func<TObject, object>> fieldSelector) =>
            throw new NotImplementedException();
    }
}