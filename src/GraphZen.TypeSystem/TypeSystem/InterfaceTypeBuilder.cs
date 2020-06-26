// Copyright (c) GraphZen LLC. All rights reserved.
// Licensed under the GraphZen Community License. See the LICENSE file in the project root for license information.

using System;
using System.Diagnostics.CodeAnalysis;
using System.Linq.Expressions;
using GraphZen.Infrastructure;
using GraphZen.TypeSystem.Internal;
using JetBrains.Annotations;

namespace GraphZen.TypeSystem
{
    public class InterfaceTypeBuilder<TInterface, TContext> : IInterfaceTypeBuilder<TInterface, TContext>
        where TContext : GraphQLContext
        where TInterface : notnull
    {
        public InterfaceTypeBuilder(InternalInterfaceTypeBuilder builder)
        {
            Builder = Check.NotNull(builder, nameof(builder));
        }


        private InternalInterfaceTypeBuilder Builder { get; }


        InternalInterfaceTypeBuilder IInfrastructure<InternalInterfaceTypeBuilder>.Instance => Builder;


        public InterfaceTypeBuilder<TInterface, TContext> Description(string description)
        {
            Check.NotNull(description, nameof(description));
            Builder.Description(description, ConfigurationSource.Explicit);
            return this;
        }

        public InterfaceTypeBuilder<TInterface, TContext> RemoveDescription()
        {
            Builder.RemoveDescription(ConfigurationSource.Explicit);
            return this;
        }

        public InterfaceTypeBuilder<object, TContext> ClrType(Type clrType, bool inferName = false)
        {
            Check.NotNull(clrType, nameof(clrType));
            Builder.ClrType(clrType, inferName, ConfigurationSource.Explicit);
            return new InterfaceTypeBuilder<object, TContext>(Builder);
        }

        public InterfaceTypeBuilder<object, TContext> ClrType(Type clrType, string name)
        {
            Check.NotNull(clrType, nameof(clrType));
            Check.NotNull(name, nameof(name));
            Builder.ClrType(clrType, name, ConfigurationSource.Explicit);
            return new InterfaceTypeBuilder<object, TContext>(Builder);
        }

        public InterfaceTypeBuilder<object, TContext> RemoveClrType()
        {
            Builder.RemoveClrType(ConfigurationSource.Explicit);
            return new InterfaceTypeBuilder<object, TContext>(Builder);
        }

        public InterfaceTypeBuilder<T, TContext> ClrType<T>(string name) where T : notnull
        {
            Check.NotNull(name, nameof(name));
            Builder.ClrType(typeof(T), name, ConfigurationSource.Explicit);
            return new InterfaceTypeBuilder<T, TContext>(Builder);
        }

        public InterfaceTypeBuilder<T, TContext> ClrType<T>(bool inferName = false) where T : notnull
        {
            Builder.ClrType(typeof(T), inferName, ConfigurationSource.Explicit);
            return new InterfaceTypeBuilder<T, TContext>(Builder);
        }


        public InterfaceTypeBuilder<TInterface, TContext> Field(string name,
            Action<FieldBuilder<TInterface, object, TContext>> configurator)
        {
            Check.NotNull(name, nameof(name));
            var fb = Builder.Field(name)!;

            configurator?.Invoke(new FieldBuilder<TInterface, object, TContext>(fb));
            return this;
        }

        public InterfaceTypeBuilder<TInterface, TContext> Field(string name, string type)
        {
            Check.NotNull(name, nameof(name));
            Check.NotNull(type, nameof(type));
            Builder.Field(name, type, ConfigurationSource.Explicit);
            return this;
        }

        public FieldBuilder<TInterface, object, TContext> Field(string name)
        {
            Check.NotNull(name, nameof(name));
            return new FieldBuilder<TInterface, object, TContext>(Builder.Field(name));
        }

        public InterfaceTypeBuilder<TInterface, TContext> Field(string name, string type,
            Action<FieldBuilder<TInterface, object?, TContext>> configurator)

        {
            Check.NotNull(name, nameof(name));
            Check.NotNull(type, nameof(type));
            Check.NotNull(configurator, nameof(configurator));
            var fb = Builder.Field(name, type, ConfigurationSource.Explicit)!;
            configurator(new FieldBuilder<TInterface, object?, TContext>(fb));
            return this;
        }

        public InterfaceTypeBuilder<TInterface, TContext> Field<TField>(Expression<Func<TInterface, TField>> selector)
        {
            Check.NotNull(selector, nameof(selector));
            var fieldProp = selector.GetPropertyInfoFromExpression();
            Builder.Field(fieldProp, ConfigurationSource.Explicit);
            return this;
        }


        public InterfaceTypeBuilder<TInterface, TContext> Field<TField>(string name)
        {
            Check.NotNull(name, nameof(name));
            Builder.Field(name, typeof(TField), ConfigurationSource.Explicit);
            return this;
        }

        public InterfaceTypeBuilder<TInterface, TContext> RemoveField(string name)
        {
            Check.NotNull(name, nameof(name));
            Builder.RemoveField(name, ConfigurationSource.Explicit);
            return this;
        }

        public InterfaceTypeBuilder<TInterface, TContext> Field<TField>(string name,
            Action<FieldBuilder<TInterface, TField, TContext>> configurator)
        {
            Check.NotNull(name, nameof(name));
            Check.NotNull(configurator, nameof(configurator));
            var fb = Builder.Field(name, typeof(TField), ConfigurationSource.Explicit)!;
            configurator(new FieldBuilder<TInterface, TField, TContext>(fb));
            return this;
        }

        public InterfaceTypeBuilder<TInterface, TContext> Field<TField>(
            Expression<Func<TInterface, TField>> selector,
            Action<FieldBuilder<TInterface, TField, TContext>> configurator)
        {
            Check.NotNull(selector, nameof(selector));
            Check.NotNull(configurator, nameof(configurator));
            var fieldProp = selector.GetPropertyInfoFromExpression();
            var fb = Builder.Field(fieldProp, ConfigurationSource.Explicit)!;
            configurator(new FieldBuilder<TInterface, TField, TContext>(fb));
            return this;
        }

        public InterfaceTypeBuilder<TInterface, TContext> IgnoreField<TField>(
            Expression<Func<TInterface, TField>> selector) =>
            throw new NotImplementedException();

        public InterfaceTypeBuilder<TInterface, TContext> IgnoreField(string name)
        {
            Check.NotNull(name, nameof(name));
            Builder.IgnoreField(name, ConfigurationSource.Explicit);
            return this;
        }

        public InterfaceTypeBuilder<TInterface, TContext> UnignoreField(string name)
        {
            Check.NotNull(name, nameof(name));
            Builder.UnignoreField(name, ConfigurationSource.Explicit);
            return this;
        }

        public InterfaceTypeBuilder<TInterface, TContext> ResolveType(
            TypeResolver<TInterface, TContext> resolveTypeFn)
        {
            Check.NotNull(resolveTypeFn, nameof(resolveTypeFn));
            Builder.ResolveType((value, context, info) => resolveTypeFn((TInterface)value, (TContext)context, info));
            return this;
        }

        //public InterfaceTypeBuilder<object, TContext> ClrType(Type clrType) =>
        //    new InterfaceTypeBuilder<object, TContext>(Builder.ClrType(clrType));

        public InterfaceTypeBuilder<TInterface, TContext> Name(string name)
        {
            Check.NotNull(name, nameof(name));
            Builder.Name(name, ConfigurationSource.Explicit);
            return this;
        }


        public InterfaceTypeBuilder<TInterface, TContext> AddDirectiveAnnotation(string name, object value)
        {
            Builder.AddDirectiveAnnotation(name, value, ConfigurationSource.Explicit);
            return this;

        }

        public InterfaceTypeBuilder<TInterface, TContext> AddDirectiveAnnotation(string name)
        {
            Builder.AddDirectiveAnnotation(name, null, ConfigurationSource.Explicit);
            return this;
        }


        public InterfaceTypeBuilder<TInterface, TContext> RemoveDirectiveAnnotations(string name) =>
            throw new NotImplementedException();

        public InterfaceTypeBuilder<TInterface, TContext> RemoveDirectiveAnnotations() =>
            throw new NotImplementedException();
    }
}