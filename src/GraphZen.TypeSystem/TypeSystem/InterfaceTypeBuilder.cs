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

        public IFieldBuilder<TInterface, object, TContext> Field(string name)
        {
            Check.NotNull(name, nameof(name));
            return new FieldBuilder<TInterface, object, TContext>(Builder.Field(name, ConfigurationSource.Explicit,
                ConfigurationSource.Explicit)!);
        }

        public IInterfaceTypeBuilder<TInterface, TContext> Field(string name, string type,
            Action<IFieldBuilder<TInterface, object?, TContext>>? configurator = null)

        {
            Check.NotNull(name, nameof(name));
            Check.NotNull(type, nameof(type));
            // ReSharper disable once PossibleNullReferenceException -- because this is explicitly configured, should always return a value
            var fb = Builder.Field(name, ConfigurationSource.Explicit, ConfigurationSource.Explicit)?.FieldType(type)!;
            configurator?.Invoke(new FieldBuilder<TInterface, object?, TContext>(fb));
            return this;
        }


        public IInterfaceTypeBuilder<TInterface, TContext> Field<TField>(string name,
            Action<IFieldBuilder<TInterface, TField, TContext>>? configurator = null)
        {
            Check.NotNull(name, nameof(name));
            // ReSharper disable once PossibleNullReferenceException -- because this is explicitly configured, should always return a value
            var fb = Builder.Field(name, ConfigurationSource.Explicit, ConfigurationSource.Explicit)?
                .FieldType(typeof(TField))!;
            configurator?.Invoke(new FieldBuilder<TInterface, TField, TContext>(fb));
            return this;
        }

        public IInterfaceTypeBuilder<TInterface, TContext> Field<TField>(
            Expression<Func<TInterface, TField>> selector,
            Action<IFieldBuilder<TInterface, TField, TContext>>? configurator = null)
        {
            Check.NotNull(selector, nameof(selector));
            var fieldProp = selector.GetPropertyInfoFromExpression();
            var fb = Builder.Field(fieldProp, ConfigurationSource.Explicit)!;
            configurator?.Invoke(new FieldBuilder<TInterface, TField, TContext>(fb));
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

        public IInterfaceTypeBuilder<TInterface, TContext> Name(string newName)
        {
            Builder.Name(newName, ConfigurationSource.Explicit);
            return this;
        }

        public IInterfaceTypeBuilder<TInterface, TContext> DirectiveAnnotation(string name) =>
            DirectiveAnnotation(name, null);


        public IInterfaceTypeBuilder<TInterface, TContext> DirectiveAnnotation(string name, object? value)
        {
            Builder.AddOrUpdateDirectiveAnnotation(Check.NotNull(name, nameof(name)), value);
            return this;
        }

        public IInterfaceTypeBuilder<TInterface, TContext> RemoveDirectiveAnnotation(string name)
        {
            Builder.RemoveDirectiveAnnotation(Check.NotNull(name, nameof(name)));
            return this;
        }
    }
}