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
    public class InputObjectTypeBuilder<TInputObject> : IInputObjectTypeBuilder<TInputObject>,
        IInfrastructure<InternalInputObjectTypeBuilder>
    {
        public InputObjectTypeBuilder(InternalInputObjectTypeBuilder builder)
        {
            Check.NotNull(builder, nameof(builder));
            Builder = builder;
        }


        private InternalInputObjectTypeBuilder Builder { get; }

        InternalInputObjectTypeBuilder IInfrastructure<InternalInputObjectTypeBuilder>.Instance => Builder;

        public IInputObjectTypeBuilder<TInputObject> Description(string? description)
        {
            Builder.Description(description, ConfigurationSource.Explicit);
            return this;
        }

        public IInputObjectTypeBuilder<object> ClrType(Type clrType)
        {
            Check.NotNull(clrType, nameof(clrType));
            Builder.ClrType(clrType, ConfigurationSource.Explicit);
            return new InputObjectTypeBuilder<object>(Builder);
        }

        public IInputObjectTypeBuilder<T> ClrType<T>()
        {
            Builder.ClrType(typeof(T), ConfigurationSource.Explicit);
            return new InputObjectTypeBuilder<T>(Builder);
        }

        public IInputObjectTypeBuilder<TInputObject> Field(string name, string type,
            Action<InputValueBuilder>? inputFieldConfigurator = null)
        {
            Check.NotNull(name, nameof(name));
            Check.NotNull(type, nameof(type));
            InternalInputValueBuilder fb = Builder.Field(name, ConfigurationSource.Explicit)?.Type(type)!;
            inputFieldConfigurator?.Invoke(new InputValueBuilder(fb));
            return this;
        }

        public IInputObjectTypeBuilder<TInputObject> Field(string name,
            Action<InputValueBuilder>? inputFieldConfigurator = null)
        {
            Check.NotNull(name, nameof(name));
            var fb = Builder.Field(name, ConfigurationSource.Explicit)!;
            inputFieldConfigurator?.Invoke(new InputValueBuilder(fb));
            return this;
        }

        public IInputObjectTypeBuilder<TInputObject> Field<TField>(string name,
            Action<InputValueBuilder>? inputFieldConfigurator = null)
        {
            Check.NotNull(name, nameof(name));
            var fb = Builder.Field(name, ConfigurationSource.Explicit)?
                .Type(typeof(TField))!;
            inputFieldConfigurator?.Invoke(new InputValueBuilder(fb));
            return this;
        }

        public IInputObjectTypeBuilder<TInputObject> Field<TField>(Expression<Func<TInputObject, TField>> fieldSelector,
            Action<InputValueBuilder>? fieldBuilder = null)
        {
            Check.NotNull(fieldSelector, nameof(fieldSelector));
            var property = fieldSelector.GetPropertyInfoFromExpression();
            var fb = Builder.Field(property, ConfigurationSource.Explicit)!;
            fieldBuilder?.Invoke(new InputValueBuilder(fb));
            return this;
        }

        public IInputObjectTypeBuilder<TInputObject> IgnoreField<TField>(
            Expression<Func<TInputObject, TField>> fieldSelector) =>
            throw new NotImplementedException();

        public IInputObjectTypeBuilder<TInputObject> IgnoreField(string name)
        {
            Check.NotNull(name, nameof(name));
            Builder.IgnoreField(name, ConfigurationSource.Explicit);
            return this;
        }

        public IInputObjectTypeBuilder<TInputObject> UnignoreField(string name)
        {
            Check.NotNull(name, nameof(name));
            Builder.UnignoreField(name, ConfigurationSource.Explicit);
            return this;
        }


        public IInputObjectTypeBuilder<TInputObject> Name(string name)
        {
            Check.NotNull(name, nameof(name));
            Builder.Name(name, ConfigurationSource.Explicit);
            return this;
        }


        public IInputObjectTypeBuilder<TInputObject> DirectiveAnnotation(string name, object? value = null)
        {
            Builder.DirectiveAnnotation(Check.NotNull(name, nameof(name)), value, ConfigurationSource.Explicit);
            return this;
        }

        public IInputObjectTypeBuilder<TInputObject> IgnoreDirectiveAnnotation(string name) =>
            throw new NotImplementedException();
    }
}