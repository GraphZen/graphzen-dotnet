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
    public class InputObjectTypeBuilder : IInputObjectTypeBuilder
    {
        public InputObjectTypeBuilder(InternalInputObjectTypeBuilder builder)
        {
            Builder = builder;
        }

        protected InternalInputObjectTypeBuilder Builder { get; }

        InternalInputObjectTypeBuilder IInfrastructure<InternalInputObjectTypeBuilder>.Instance => Builder;
        MutableInputObjectType IInfrastructure<MutableInputObjectType>.Instance => Builder.Definition;

        public INameBuilder Name(string name)
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

        public IDirectivesBuilder AddDirectiveAnnotation(string name, object value)
        {
            Check.NotNull(name, nameof(name));
            Builder.AddDirectiveAnnotation(name, value, ConfigurationSource.Explicit);
            return this;
        }

        public IDirectivesBuilder AddDirectiveAnnotation(string name)
        {
            Check.NotNull(name, nameof(name));
            Builder.AddDirectiveAnnotation(name, null, ConfigurationSource.Explicit);
            return this;
        }

        public IDirectivesBuilder RemoveDirectiveAnnotations(string name)
        {
            Check.NotNull(name, nameof(name));
            Builder.RemoveDirectiveAnnotations(name, ConfigurationSource.Explicit);
            return this;
        }

        public IDirectivesBuilder ClearDirectiveAnnotations()
        {
            Builder.ClearDirectiveAnnotations(ConfigurationSource.Explicit);
            return this;
        }
    }

    public class InputObjectTypeBuilder<TInputObject> : InputObjectTypeBuilder, IInputObjectTypeBuilder<TInputObject>
        where TInputObject : notnull
    {
        public InputObjectTypeBuilder(InternalInputObjectTypeBuilder builder) : base(builder)
        {
        }


        public new InputObjectTypeBuilder<TInputObject> Description(string description) =>
            (InputObjectTypeBuilder<TInputObject>)base.Description(description);

        public new InputObjectTypeBuilder<TInputObject> RemoveDescription() =>
            (InputObjectTypeBuilder<TInputObject>)base.RemoveDescription();

        public new InputObjectTypeBuilder<object> ClrType(Type clrType, bool inferName = false)
        {
            base.ClrType(clrType, inferName);
            return new InputObjectTypeBuilder<object>(Builder);
        }

        public new InputObjectTypeBuilder<object> ClrType(Type clrType, string name)
        {
            base.ClrType(clrType, name);
            return new InputObjectTypeBuilder<object>(Builder);
        }

        public new InputObjectTypeBuilder<object> RemoveClrType()
        {
            base.RemoveClrType();
            return new InputObjectTypeBuilder<object>(Builder);
        }

        public InputObjectTypeBuilder<T> ClrType<T>(bool inferName = false) where T : notnull
        {
            base.ClrType(typeof(T), inferName);
            return new InputObjectTypeBuilder<T>(Builder);
        }

        public InputObjectTypeBuilder<T> ClrType<T>(string name) where T : notnull
        {
            base.ClrType(typeof(T), name);
            return new InputObjectTypeBuilder<T>(Builder);
        }


        public InputObjectTypeBuilder<TInputObject> Field(string name, string type)
        {
            Check.NotNull(name, nameof(name));
            Check.NotNull(type, nameof(type));
            Builder.Field(name, type, ConfigurationSource.Explicit);
            return this;
        }

        public InputObjectTypeBuilder<TInputObject> RemoveField(string name)
        {
            Check.NotNull(name, nameof(name));
            Builder.RemoveField(name, ConfigurationSource.Explicit);
            return this;
        }

        public InputObjectTypeBuilder<TInputObject> Field(string name, string type,
            Action<InputFieldBuilder<object?>> inputFieldConfigurator)
        {
            Check.NotNull(name, nameof(name));
            Check.NotNull(type, nameof(type));
            Check.NotNull(inputFieldConfigurator, nameof(inputFieldConfigurator));
            var fb = Builder.Field(name, type, ConfigurationSource.Explicit)!;
            inputFieldConfigurator(new InputFieldBuilder<object?>(fb));
            return this;
        }

        public InputFieldBuilder<object?> Field(string name) =>
            new InputFieldBuilder<object?>(Builder.Field(Check.NotNull(name, nameof(name))));

        public InputObjectTypeBuilder<TInputObject> Field(string name,
            Action<InputFieldBuilder<object?>> inputFieldConfigurator)
        {
            Check.NotNull(name, nameof(name));
            Check.NotNull(inputFieldConfigurator, nameof(inputFieldConfigurator));
            inputFieldConfigurator(new InputFieldBuilder<object?>(Builder.Field(name)));
            return this;
        }

        public InputObjectTypeBuilder<TInputObject> Field<TField>(string name)
        {
            Check.NotNull(name, nameof(name));
            Builder.Field(name, typeof(TField), ConfigurationSource.Explicit);
            return this;
        }

        public InputObjectTypeBuilder<TInputObject> Field<TField>(string name,
            Action<InputFieldBuilder<TField>> inputFieldConfigurator)
        {
            Check.NotNull(name, nameof(name));
            Check.NotNull(inputFieldConfigurator, nameof(inputFieldConfigurator));
            var fb = Builder.Field(name, typeof(TField), ConfigurationSource.Explicit)!;
            inputFieldConfigurator(new InputFieldBuilder<TField>(fb));
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
            Action<InputFieldBuilder<TField>> fieldBuilder)
        {
            Check.NotNull(fieldSelector, nameof(fieldSelector));
            Check.NotNull(fieldBuilder, nameof(fieldBuilder));
            var property = fieldSelector.GetPropertyInfoFromExpression();
            var fb = Builder.Field(property, ConfigurationSource.Explicit)!;
            fieldBuilder?.Invoke(new InputFieldBuilder<TField>(fb));
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


        public new InputObjectTypeBuilder<TInputObject> Name(string name) =>
            (InputObjectTypeBuilder<TInputObject>)base.Name(name);


        public new InputObjectTypeBuilder<TInputObject> AddDirectiveAnnotation(string name, object value) =>
            (InputObjectTypeBuilder<TInputObject>)base.AddDirectiveAnnotation(name, value);


        public new InputObjectTypeBuilder<TInputObject> AddDirectiveAnnotation(string name) =>
            (InputObjectTypeBuilder<TInputObject>)base.AddDirectiveAnnotation(name);


        public new InputObjectTypeBuilder<TInputObject> RemoveDirectiveAnnotations(string name) =>
            (InputObjectTypeBuilder<TInputObject>)base.RemoveDirectiveAnnotations(name);

        public new InputObjectTypeBuilder<TInputObject> ClearDirectiveAnnotations() =>
            (InputObjectTypeBuilder<TInputObject>)base.ClearDirectiveAnnotations();
    }
}