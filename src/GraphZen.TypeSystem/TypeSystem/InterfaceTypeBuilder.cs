// Copyright (c) GraphZen LLC. All rights reserved.
// Licensed under the GraphZen Community License. See the LICENSE file in the project root for license information.

using System;
using System.Linq.Expressions;
using GraphZen.Infrastructure;
using GraphZen.TypeSystem.Internal;

namespace GraphZen.TypeSystem
{
    public class InterfaceTypeBuilder<TInterface, TContext> : IInterfaceTypeBuilder<TInterface, TContext>,
        IInfrastructure<InternalInterfaceTypeBuilder> where TContext : GraphQLContext
    {
        public InterfaceTypeBuilder(InternalInterfaceTypeBuilder builder)
        {
            Builder = Check.NotNull(builder, nameof(builder));
        }

        [NotNull]
        private InternalInterfaceTypeBuilder Builder { get; }


        InternalInterfaceTypeBuilder IInfrastructure<InternalInterfaceTypeBuilder>.Instance => Builder;


        public IInterfaceTypeBuilder<TInterface, TContext> Description(string description)
        {
            Builder.Description(description, ConfigurationSource.Explicit);
            return this;
        }

        public IInterfaceTypeBuilder<TInterface, TContext> Field(string name,
            Action<IFieldBuilder<TInterface, object, TContext>> fieldConfigurator = null)
        {
            Check.NotNull(name, nameof(name));
            var fb = Builder.Field(name, ConfigurationSource.Explicit, ConfigurationSource.Explicit);
            // ReSharper disable once AssignNullToNotNullAttribute
            fieldConfigurator?.Invoke(new FieldBuilder<TInterface, object, TContext>(fb));
            return this;
        }

        public IInterfaceTypeBuilder<TInterface, TContext> Field(string name, string type,
            Action<IFieldBuilder<TInterface, object, TContext>> fieldConfigurator = null)
        {
            Check.NotNull(name, nameof(name));
            Check.NotNull(type, nameof(type));
            // ReSharper disable once PossibleNullReferenceException -- because this is explicitly configured, should always return a value
            var fb = Builder.Field(name, ConfigurationSource.Explicit, ConfigurationSource.Explicit).FieldType(type);
            fieldConfigurator?.Invoke(new FieldBuilder<TInterface, object, TContext>(fb));
            return this;
        }


        public IInterfaceTypeBuilder<TInterface, TContext> Field<TField>(string name,
            Action<IFieldBuilder<TInterface, TField, TContext>> fieldConfigurator = null)
        {
            Check.NotNull(name, nameof(name));
            // ReSharper disable once PossibleNullReferenceException -- because this is explicitly configured, should always return a value
            var fb = Builder.Field(name, ConfigurationSource.Explicit, ConfigurationSource.Explicit)
                .FieldType(typeof(TField));
            fieldConfigurator?.Invoke(new FieldBuilder<TInterface, TField, TContext>(fb));
            return this;
        }

        public IInterfaceTypeBuilder<TInterface, TContext> Field<TField>(
            Expression<Func<TInterface, TField>> fieldSelector,
            Action<IFieldBuilder<TInterface, TField, TContext>> fieldBuilder = null)
        {
            Check.NotNull(fieldSelector, nameof(fieldSelector));
            Check.NotNull(fieldBuilder, nameof(fieldBuilder));
            var fieldProp = fieldSelector.GetPropertyInfoFromExpression();
            var fb = Builder.Field(fieldProp, ConfigurationSource.Explicit);
            // ReSharper disable once AssignNullToNotNullAttribute
            fieldBuilder(new FieldBuilder<TInterface, TField, TContext>(fb));
            return this;
        }

        public IInterfaceTypeBuilder<TInterface, TContext> ResolveType(
            TypeResolver<TInterface, TContext> resolveTypeFn)
        {
            Check.NotNull(resolveTypeFn, nameof(resolveTypeFn));
            Builder.ResolveType((value, context, info) => resolveTypeFn((TInterface)value, (TContext)context, info));
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


        public IInterfaceTypeBuilder<TInterface, TContext> DirectiveAnnotation(string name, object value)
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