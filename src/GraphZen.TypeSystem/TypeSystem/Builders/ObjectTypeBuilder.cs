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
    public class ObjectTypeBuilder : IObjectTypeBuilder
    {
        public ObjectTypeBuilder(InternalObjectTypeBuilder builder)
        {
            Builder = builder;
        }

        protected InternalObjectTypeBuilder Builder { get; }

        public INamedBuilder Name(string name)
        {
            Check.NotNull(name, nameof(name));
            Builder.Name(name, ConfigurationSource.Explicit);
            return this;
        }

        public IDescriptionBuilder Description(string description)
        {
            Check.NotNull(description, nameof(description));
            Builder.Description(description, ConfigurationSource.Explicit);
            return this;
        }

        public IDescriptionBuilder RemoveDescription()
        {
            Builder.RemoveDescription(ConfigurationSource.Explicit);
            return this;
        }

        public IClrTypeBuilder ClrType(Type clrType, bool inferName = false)
        {
            Check.NotNull(clrType, nameof(clrType));
            Builder.ClrType(clrType, inferName, ConfigurationSource.Explicit);
            return this;
        }


        public IClrTypeBuilder ClrType(Type clrType, string name)
        {
            Check.NotNull(clrType, nameof(clrType));
            Check.NotNull(name, nameof(name));
            Builder.ClrType(clrType, name, ConfigurationSource.Explicit);
            return this;
        }


        public IClrTypeBuilder RemoveClrType()
        {
            Builder.RemoveClrType(ConfigurationSource.Explicit);
            return this;
        }


        public IImplementsInterfacesBuilder ImplementsInterface(string name)
        {
            Check.NotNull(name, nameof(name));
            Builder.ImplementsInterface(name, ConfigurationSource.Explicit);
            return this;
        }

        public IImplementsInterfacesBuilder ImplementsInterfaces(string name, params string[] names)
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

        public IImplementsInterfacesBuilder IgnoreInterface<T>()
        {
            Builder.IgnoreInterface(typeof(T).GetGraphQLNameAnnotation(), ConfigurationSource.Explicit);
            return this;
        }

        public IImplementsInterfacesBuilder IgnoreInterface(Type clrType)
        {
            Check.NotNull(clrType, nameof(clrType));
            Builder.IgnoreInterface(clrType.GetGraphQLNameAnnotation(), ConfigurationSource.Explicit);

            return this;
        }

        public IImplementsInterfacesBuilder IgnoreInterface(string name)
        {
            Check.NotNull(name, nameof(name));
            Builder.IgnoreInterface(name, ConfigurationSource.Explicit);
            return this;
        }

        public IImplementsInterfacesBuilder UnignoreInterface(string name)
        {
            Check.NotNull(name, nameof(name));
            Builder.UnignoreInterface(name, ConfigurationSource.Explicit);
            return this;
        }


        InternalObjectTypeBuilder IInfrastructure<InternalObjectTypeBuilder>.Instance => Builder;
        ObjectTypeDefinition IInfrastructure<ObjectTypeDefinition>.Instance => Builder.Definition;

        public IAnnotableBuilder AddDirectiveAnnotation(string name, object value)
        {
            Check.NotNull(name, nameof(name));
            Builder.AddDirectiveAnnotation(name, value, ConfigurationSource.Explicit);
            return this;
        }

        public IAnnotableBuilder AddDirectiveAnnotation(string name)
        {
            Check.NotNull(name, nameof(name));
            Builder.AddDirectiveAnnotation(name, null, ConfigurationSource.Explicit);
            return this;
        }

        public IAnnotableBuilder RemoveDirectiveAnnotations(string name)
        {
            Check.NotNull(name, nameof(name));
            Builder.RemoveDirectiveAnnotations(name, ConfigurationSource.Explicit);
            return this;
        }

        public IAnnotableBuilder ClearDirectiveAnnotations()
        {
            Builder.ClearDirectiveAnnotations(ConfigurationSource.Explicit);
            return this;
        }
    }

    public class ObjectTypeBuilder<TObject, TContext> : ObjectTypeBuilder, IObjectTypeBuilder<TObject, TContext>
        where TObject : notnull
        where TContext : GraphQLContext
    {
        public ObjectTypeBuilder(InternalObjectTypeBuilder builder) : base(builder)
        {
        }

        public new ObjectTypeBuilder<TObject, TContext> Name(string name)
        {
            base.Name(name);
            return this;
        }

        public new ObjectTypeBuilder<object, TContext> ClrType(Type clrType, bool inferName = false)
        {
            base.ClrType(clrType, inferName);
            return new ObjectTypeBuilder<object, TContext>(Builder);
        }

        public new ObjectTypeBuilder<object, TContext> ClrType(Type clrType, string name)
        {
            base.ClrType(clrType, name);

            return new ObjectTypeBuilder<object, TContext>(Builder);
        }

        public new ObjectTypeBuilder<object, TContext> RemoveClrType()
        {
            base.RemoveClrType();
            return new ObjectTypeBuilder<object, TContext>(Builder);
        }

        public ObjectTypeBuilder<T, TContext> ClrType<T>(bool inferName = false) where T : notnull
        {
            base.ClrType(typeof(T), inferName);
            return new ObjectTypeBuilder<T, TContext>(Builder);
        }

        public ObjectTypeBuilder<T, TContext> ClrType<T>(string name) where T : notnull
        {
            base.ClrType(typeof(T), name);
            return new ObjectTypeBuilder<T, TContext>(Builder);
        }

        public new ObjectTypeBuilder<TObject, TContext> Description(string description)
        {
            base.Description(description);
            return this;
        }

        public new ObjectTypeBuilder<TObject, TContext> RemoveDescription()
        {
            base.RemoveDescription();
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

        public new ObjectTypeBuilder<TObject, TContext> ImplementsInterface(string name) =>
            (ObjectTypeBuilder<TObject, TContext>)base.ImplementsInterface(name);

        public new ObjectTypeBuilder<TObject, TContext> ImplementsInterfaces(string name, params string[] names) =>
            (ObjectTypeBuilder<TObject, TContext>)base.ImplementsInterfaces(name, names);


        public new ObjectTypeBuilder<TObject, TContext> IgnoreInterface<T>() =>
            (ObjectTypeBuilder<TObject, TContext>)base.IgnoreInterface<T>();

        public new ObjectTypeBuilder<TObject, TContext> IgnoreInterface(Type clrType) =>
            (ObjectTypeBuilder<TObject, TContext>)base.IgnoreInterface(clrType);

        public new ObjectTypeBuilder<TObject, TContext> IgnoreInterface(string name) =>
            (ObjectTypeBuilder<TObject, TContext>)base.IgnoreInterface(name);

        public new ObjectTypeBuilder<TObject, TContext> UnignoreInterface(string name) =>
            (ObjectTypeBuilder<TObject, TContext>)base.UnignoreInterface(name);

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


        public new ObjectTypeBuilder<TObject, TContext> AddDirectiveAnnotation(string name, object value) =>
            (ObjectTypeBuilder<TObject, TContext>)base.AddDirectiveAnnotation(name, value);

        public new ObjectTypeBuilder<TObject, TContext> AddDirectiveAnnotation(string name) =>
            (ObjectTypeBuilder<TObject, TContext>)base.AddDirectiveAnnotation(name);


        public new ObjectTypeBuilder<TObject, TContext> RemoveDirectiveAnnotations(string name) =>
            (ObjectTypeBuilder<TObject, TContext>)base.RemoveDirectiveAnnotations(name);

        public new ObjectTypeBuilder<TObject, TContext> ClearDirectiveAnnotations() =>
            (ObjectTypeBuilder<TObject, TContext>)base.ClearDirectiveAnnotations();

        public ObjectTypeBuilder<TObject, TContext> IgnoreField(Expression<Func<TObject, object>> fieldSelector) =>
            throw new NotImplementedException();
    }
}