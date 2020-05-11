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
    public class InterfaceTypeBuilder<TInterface, TContext> : IInterfaceTypeBuilder<TInterface, TContext>,
        IInfrastructure<InternalInterfaceTypeBuilder> where TContext : GraphQLContext
    {
        public InterfaceTypeBuilder(InternalInterfaceTypeBuilder builder)
        {
            Builder = Check.NotNull(builder, nameof(builder));
        }


        private InternalInterfaceTypeBuilder Builder { get; }


        InternalInterfaceTypeBuilder IInfrastructure<InternalInterfaceTypeBuilder>.Instance => Builder;


        public IInterfaceTypeBuilder<TInterface, TContext> Description(string? description)
        {
            Builder.Description(description, ConfigurationSource.Explicit);
            return this;
        }

        public IInterfaceTypeBuilder<object, TContext> ClrType(Type clrType)
        {
            Check.NotNull(clrType, nameof(clrType));
            Builder.ClrType(clrType, ConfigurationSource.Explicit);
            return new InterfaceTypeBuilder<object, TContext>(Builder);
        }

        public IInterfaceTypeBuilder<object, TContext> RemoveClrType() => throw new NotImplementedException();

        public IInterfaceTypeBuilder<TNewInterfaceType, TContext> ClrType<TNewInterfaceType>()
        {
            Builder.ClrType(typeof(TNewInterfaceType), ConfigurationSource.Explicit);
            return new InterfaceTypeBuilder<TNewInterfaceType, TContext>(Builder);
        }


        public IInterfaceTypeBuilder<TInterface, TContext> Field(string name,
            Action<IFieldBuilder<TInterface, object, TContext>> configurator)
        {
            Check.NotNull(name, nameof(name));
            var fb = Builder.Field(name, ConfigurationSource.Explicit, ConfigurationSource.Explicit)!;

            configurator?.Invoke(new FieldBuilder<TInterface, object, TContext>(fb));
            return this;
        }

        public IInterfaceTypeBuilder<TInterface, TContext> Field(string name, string type)
        {
            Check.NotNull(name, nameof(name));
            Check.NotNull(type, nameof(type));
            Builder.Field(name, ConfigurationSource.Explicit, ConfigurationSource.Explicit)?.FieldType(type);
            return this;
        }

        public IFieldBuilder<TInterface, object, TContext> Field(string name)
        {
            Check.NotNull(name, nameof(name));
            return new FieldBuilder<TInterface, object, TContext>(Builder.Field(name, ConfigurationSource.Explicit,
                ConfigurationSource.Explicit)!);
        }

        public IInterfaceTypeBuilder<TInterface, TContext> Field(string name, string type,
            Action<IFieldBuilder<TInterface, object?, TContext>> configurator)

        {
            Check.NotNull(name, nameof(name));
            Check.NotNull(type, nameof(type));
            Check.NotNull(configurator, nameof(configurator));
            var fb = Builder.Field(name, ConfigurationSource.Explicit, ConfigurationSource.Explicit)?.FieldType(type)!;
            configurator(new FieldBuilder<TInterface, object?, TContext>(fb));
            return this;
        }

        public IInterfaceTypeBuilder<TInterface, TContext> Field<TField>(Expression<Func<TInterface, TField>> selector)
        {
            Check.NotNull(selector, nameof(selector));
            var fieldProp = selector.GetPropertyInfoFromExpression();
            Builder.Field(fieldProp, ConfigurationSource.Explicit);
            return this;
        }


        public IInterfaceTypeBuilder<TInterface, TContext> Field<TField>(string name)
        {
            Check.NotNull(name, nameof(name));
            Builder.Field(name, ConfigurationSource.Explicit, ConfigurationSource.Explicit)?
                .FieldType(typeof(TField));
            return this;
        }

        public IInterfaceTypeBuilder<TInterface, TContext> RemoveField(string name)
        {
            Check.NotNull(name, nameof(name));
            Builder.RemoveField(name, ConfigurationSource.Explicit);
            return this;
        }

        public IInterfaceTypeBuilder<TInterface, TContext> Field<TField>(string name,
            Action<IFieldBuilder<TInterface, TField, TContext>> configurator)
        {
            Check.NotNull(name, nameof(name));
            Check.NotNull(configurator, nameof(configurator));
            var fb = Builder.Field(name, ConfigurationSource.Explicit, ConfigurationSource.Explicit)?
                .FieldType(typeof(TField))!;
            configurator(new FieldBuilder<TInterface, TField, TContext>(fb));
            return this;
        }

        public IInterfaceTypeBuilder<TInterface, TContext> Field<TField>(
            Expression<Func<TInterface, TField>> selector,
            Action<IFieldBuilder<TInterface, TField, TContext>> configurator)
        {
            Check.NotNull(selector, nameof(selector));
            Check.NotNull(configurator, nameof(configurator));
            var fieldProp = selector.GetPropertyInfoFromExpression();
            var fb = Builder.Field(fieldProp, ConfigurationSource.Explicit)!;
            configurator(new FieldBuilder<TInterface, TField, TContext>(fb));
            return this;
        }

        public IInterfaceTypeBuilder<TInterface, TContext> IgnoreField<TField>(
            Expression<Func<TInterface, TField>> selector) =>
            throw new NotImplementedException();

        public IInterfaceTypeBuilder<TInterface, TContext> IgnoreField(string name)
        {
            Check.NotNull(name, nameof(name));
            Builder.IgnoreField(name, ConfigurationSource.Explicit);
            return this;
        }

        public IInterfaceTypeBuilder<TInterface, TContext> UnignoreField(string name)
        {
            Check.NotNull(name, nameof(name));
            Builder.UnignoreField(name, ConfigurationSource.Explicit);
            return this;
        }

        public IInterfaceTypeBuilder<TInterface, TContext> ResolveType(
            TypeResolver<TInterface, TContext> resolveTypeFn)
        {
            Check.NotNull(resolveTypeFn, nameof(resolveTypeFn));
            Builder.ResolveType((value, context, info) => resolveTypeFn((TInterface) value, (TContext) context, info));
            return this;
        }

        //public IInterfaceTypeBuilder<object, TContext> ClrType(Type clrType) =>
        //    new InterfaceTypeBuilder<object, TContext>(Builder.ClrType(clrType));

        public IInterfaceTypeBuilder<TInterface, TContext> Name(string name)
        {
            Check.NotNull(name, nameof(name));
            Builder.Name(name, ConfigurationSource.Explicit);
            return this;
        }


        public IInterfaceTypeBuilder<TInterface, TContext> AddDirectiveAnnotation(string name, object? value = null) =>
            throw new NotImplementedException();

        public IInterfaceTypeBuilder<TInterface, TContext> UpdateOrAddDirectiveAnnotation(string name,
            object? value = null)
        {
            Builder.DirectiveAnnotation(Check.NotNull(name, nameof(name)), value, ConfigurationSource.Explicit);
            return this;
        }

        public IInterfaceTypeBuilder<TInterface, TContext> RemoveDirectiveAnnotations(string name) =>
            throw new NotImplementedException();

        public IInterfaceTypeBuilder<TInterface, TContext> RemoveDirectiveAnnotations() =>
            throw new NotImplementedException();
    }
}