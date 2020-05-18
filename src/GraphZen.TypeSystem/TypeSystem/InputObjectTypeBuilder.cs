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
    public class InputObjectTypeBuilder<TInputObject> : IInputObjectTypeBuilder<TInputObject>
    {
        public InputObjectTypeBuilder(InternalInputObjectTypeBuilder builder)
        {
            Check.NotNull(builder, nameof(builder));
            Builder = builder;
        }


        private InternalInputObjectTypeBuilder Builder { get; }

        InternalInputObjectTypeBuilder IInfrastructure<InternalInputObjectTypeBuilder>.Instance => Builder;
        InputObjectTypeDefinition IInfrastructure<InputObjectTypeDefinition>.Instance => Builder.Definition;

        public InputObjectTypeBuilder<TInputObject> Description(string description)
        {
            Check.NotNull(description, nameof(description));
            Builder.Description(description, ConfigurationSource.Explicit);
            return this;
        }

        public InputObjectTypeBuilder<TInputObject> RemoveDescription()
        {
            Builder.RemoveDescription(ConfigurationSource.Explicit);
            return this;
        }

        public InputObjectTypeBuilder<object> ClrType(Type clrType)
        {
            Check.NotNull(clrType, nameof(clrType));
            Builder.ClrType(clrType, ConfigurationSource.Explicit);
            return new InputObjectTypeBuilder<object>(Builder);
        }

        public InputObjectTypeBuilder<object> ClrType(Type clrType, string name) =>
            throw new NotImplementedException();

        public InputObjectTypeBuilder<object> RemoveClrType()
        {
            Builder.RemoveClrType(ConfigurationSource.Explicit);


            return new InputObjectTypeBuilder<object>(Builder);
        }

        public InputObjectTypeBuilder<T> ClrType<T>()
        {
            Builder.ClrType(typeof(T), ConfigurationSource.Explicit);
            return new InputObjectTypeBuilder<T>(Builder);
        }

        public InputObjectTypeBuilder<T> ClrType<T>(string name) => throw new NotImplementedException();


        public InputObjectTypeBuilder<TInputObject> Field(string name, string type)
        {
            Check.NotNull(name, nameof(name));
            Check.NotNull(type, nameof(type));
            Builder.Field(name, ConfigurationSource.Explicit)?.Type(type, ConfigurationSource.Explicit);
            return this;
        }

        public InputObjectTypeBuilder<TInputObject> RemoveField(string name)
        {
            Check.NotNull(name, nameof(name));
            Builder.RemoveField(name, ConfigurationSource.Explicit);
            return this;
        }

        public InputObjectTypeBuilder<TInputObject> Field(string name, string type,
            Action<InputValueBuilder<object?>> inputFieldConfigurator)
        {
            Check.NotNull(name, nameof(name));
            Check.NotNull(type, nameof(type));
            Check.NotNull(inputFieldConfigurator, nameof(inputFieldConfigurator));
            var fb = Builder.Field(name, ConfigurationSource.Explicit)?.Type(type, ConfigurationSource.Explicit)!;
            inputFieldConfigurator(new InputValueBuilder<object?>(fb));
            return this;
        }

        public InputValueBuilder<object?> Field(string name)
        {
            Check.NotNull(name, nameof(name));
            var fb = Builder.Field(name, ConfigurationSource.Explicit)!;
            return new InputValueBuilder<object?>(fb);
        }

        public InputObjectTypeBuilder<TInputObject> Field(string name,
            Action<InputValueBuilder<object?>> inputFieldConfigurator)
        {
            Check.NotNull(name, nameof(name));
            Check.NotNull(inputFieldConfigurator, nameof(inputFieldConfigurator));
            var fb = Builder.Field(name, ConfigurationSource.Explicit)!;
            inputFieldConfigurator(new InputValueBuilder<object?>(fb));
            return this;
        }

        public InputObjectTypeBuilder<TInputObject> Field<TField>(string name)
        {
            Check.NotNull(name, nameof(name));
            Builder.Field(name, ConfigurationSource.Explicit)?
                .Type(typeof(TField), ConfigurationSource.Explicit);
            return this;
        }

        public InputObjectTypeBuilder<TInputObject> Field<TField>(string name,
            Action<InputValueBuilder<TField>> inputFieldConfigurator)
        {
            Check.NotNull(name, nameof(name));
            Check.NotNull(inputFieldConfigurator, nameof(inputFieldConfigurator));
            var fb = Builder.Field(name, ConfigurationSource.Explicit)?
                .Type(typeof(TField), ConfigurationSource.Explicit)!;
            inputFieldConfigurator(new InputValueBuilder<TField>(fb));
            return this;
        }

        public InputObjectTypeBuilder<TInputObject>
            Field<TField>(Expression<Func<TInputObject, TField>> fieldSelector)
        {
            var property = fieldSelector.GetPropertyInfoFromExpression();
            Builder.Field(property, ConfigurationSource.Explicit);
            return this;
        }

        public InputObjectTypeBuilder<TInputObject> Field<TField>(Expression<Func<TInputObject, TField>> fieldSelector,
            Action<InputValueBuilder<TField>> fieldBuilder)
        {
            Check.NotNull(fieldSelector, nameof(fieldSelector));
            Check.NotNull(fieldBuilder, nameof(fieldBuilder));
            var property = fieldSelector.GetPropertyInfoFromExpression();
            var fb = Builder.Field(property, ConfigurationSource.Explicit)!;
            fieldBuilder?.Invoke(new InputValueBuilder<TField>(fb));
            return this;
        }

        public InputObjectTypeBuilder<TInputObject> IgnoreField<TField>(
            Expression<Func<TInputObject, TField>> fieldSelector) =>
            throw new NotImplementedException();

        public InputObjectTypeBuilder<TInputObject> IgnoreField(string name)
        {
            Check.NotNull(name, nameof(name));
            Builder.IgnoreField(name, ConfigurationSource.Explicit);
            return this;
        }

        public InputObjectTypeBuilder<TInputObject> UnignoreField(string name)
        {
            Check.NotNull(name, nameof(name));
            Builder.UnignoreField(name, ConfigurationSource.Explicit);
            return this;
        }


        public InputObjectTypeBuilder<TInputObject> Name(string name)
        {
            Check.NotNull(name, nameof(name));
            Builder.SetName(name, ConfigurationSource.Explicit);
            return this;
        }


        public InputObjectTypeBuilder<TInputObject> AddDirectiveAnnotation(string name, object? value = null) =>
            throw new NotImplementedException();

        public InputObjectTypeBuilder<TInputObject> UpdateOrAddDirectiveAnnotation(string name, object? value = null)
        {
            Builder.DirectiveAnnotation(Check.NotNull(name, nameof(name)), value, ConfigurationSource.Explicit);
            return this;
        }

        public InputObjectTypeBuilder<TInputObject> RemoveDirectiveAnnotations(string name) =>
            throw new NotImplementedException();

        public InputObjectTypeBuilder<TInputObject> RemoveDirectiveAnnotations() =>
            throw new NotImplementedException();
    }
}