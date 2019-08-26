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
            Builder.Name(name, ConfigurationSource.Explicit);
            return this;
        }

        public IObjectTypeBuilder<object, TContext> ClrType(Type clrType)
        {
            Check.NotNull(clrType, nameof(ClrType));
            Builder.ClrType(clrType, ConfigurationSource.Explicit);
            return new ObjectTypeBuilder<object, TContext>(Builder);
        }

        public IObjectTypeBuilder<T, TContext> ClrType<T>()
        {
            Builder.ClrType(typeof(T), ConfigurationSource.Explicit);
            return new ObjectTypeBuilder<T, TContext>(Builder);
        }

        public IObjectTypeBuilder<TObject, TContext> Description(string? description)
        {
            Builder.Description(description, ConfigurationSource.Explicit);
            return this;
        }


        public IObjectTypeBuilder<TObject, TContext> Field(string name,
            Action<IFieldBuilder<TObject, object, TContext>>? fieldConfigurator = null)
        {
            Check.NotNull(name, nameof(name));
            var ib = Builder.Field(name, ConfigurationSource.Explicit, ConfigurationSource.Explicit)!;

            fieldConfigurator?.Invoke(new FieldBuilder<TObject, object, TContext>(ib));
            return this;
        }

        public IObjectTypeBuilder<TObject, TContext> Field(string name, string type,
            Action<IFieldBuilder<TObject, object, TContext>>? fieldConfigurator = null)
        {
            Check.NotNull(name, nameof(name));
            Check.NotNull(type, nameof(type));
            // ReSharper disable once PossibleNullReferenceException -- because this is explicitly configured, should always return a value
            var ib = Builder.Field(name, ConfigurationSource.Explicit, ConfigurationSource.Explicit)?.FieldType(type)!;
            fieldConfigurator?.Invoke(new FieldBuilder<TObject, object, TContext>(ib));
            return this;
        }

        public IObjectTypeBuilder<TObject, TContext> Field<TField>(string name,
            Action<IFieldBuilder<TObject, TField, TContext>>? fieldConfigurator = null)
        {
            Check.NotNull(name, nameof(name));
            // ReSharper disable once PossibleNullReferenceException -- because this is explicitly configured, should always return a value
            var ib = Builder.Field(name, ConfigurationSource.Explicit, ConfigurationSource.Explicit)
                ?.FieldType(typeof(TField))!;
            fieldConfigurator?.Invoke(new FieldBuilder<TObject, TField, TContext>(ib));
            return this;
        }

        public IObjectTypeBuilder<TObject, TContext> Field<TField>(Expression<Func<TObject, TField>> fieldSelector,
            Action<IFieldBuilder<TObject, TField, TContext>>? fieldConfigurator = null)
        {
            Check.NotNull(fieldSelector, nameof(fieldSelector));
            var property = fieldSelector.GetPropertyInfoFromExpression();
            var fb = Builder.Field(property, ConfigurationSource.Explicit)!;
            fieldConfigurator?.Invoke(new FieldBuilder<TObject, TField, TContext>(fb));
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
                foreach (var n in names)
                {
                    ImplementsInterface(n);
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
            Expression<Func<TObject, TField>> fieldSelector)
        {
            throw new NotImplementedException();
        }

        public IObjectTypeBuilder<TObject, TContext> IgnoreField(string fieldName)
        {
            Check.NotNull(fieldName, nameof(fieldName));
            Builder.IgnoreField(fieldName, ConfigurationSource.Explicit);
            return this;
        }

        public IObjectTypeBuilder<TObject, TContext> UnignoreField(string fieldName)
        {
            Check.NotNull(fieldName, nameof(fieldName));
            Builder.UnignoreField(fieldName, ConfigurationSource.Explicit);
            return this;
        }

        public IObjectTypeBuilder<TObject, TContext> DirectiveAnnotation(string name)
        {
            return DirectiveAnnotation(name, null);
        }

        public IObjectTypeBuilder<TObject, TContext> DirectiveAnnotation(string name, object? value)
        {
            Builder.AddOrUpdateDirectiveAnnotation(Check.NotNull(name, nameof(name)), value);
            return this;
        }

        public IObjectTypeBuilder<TObject, TContext> RemoveDirectiveAnnotation(string name)
        {
            Builder.RemoveDirectiveAnnotation(Check.NotNull(name, nameof(name)));
            return this;
        }

        public IObjectTypeBuilder<TObject, TContext> IgnoreField(Expression<Func<TObject, object>> fieldSelector)
        {
            throw new NotImplementedException();
        }
    }
}