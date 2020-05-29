// Copyright (c) GraphZen LLC. All rights reserved.
// Licensed under the GraphZen Community License. See the LICENSE file in the project root for license information.

using System;
using System.Diagnostics.CodeAnalysis;
using GraphZen.Infrastructure;
using GraphZen.TypeSystem.Internal;
using JetBrains.Annotations;

namespace GraphZen.TypeSystem
{
    public class InputFieldBuilder<T> : IInputFieldBuilder<T>
    {
        public InputFieldBuilder(InternalInputFieldBuilder builder)
        {
            Check.NotNull(builder, nameof(builder));
            Builder = builder;
        }

        protected InternalInputFieldBuilder Builder { get; }

        public InputFieldBuilder<T> AddDirectiveAnnotation(string name, object? value = null) =>
            throw new NotImplementedException();

        public InputFieldBuilder<T> UpdateOrAddDirectiveAnnotation(string name, object? value = null)
        {
            Builder.DirectiveAnnotation(Check.NotNull(name, nameof(name)), value, ConfigurationSource.Explicit);
            return this;
        }

        public InputFieldBuilder<object?> FieldType(string type)
        {
            Check.NotNull(type, nameof(type));
            Builder.FieldType(type, ConfigurationSource.Explicit);
            return new InputFieldBuilder<object?>(Builder);
        }

        public InputFieldBuilder<object?> FieldType(Type clrType)
        {
            Check.NotNull(clrType, nameof(clrType));
            Builder.FieldType(clrType, ConfigurationSource.Explicit);
            return new InputFieldBuilder<object?>(Builder);
        }

        public InputFieldBuilder<TNew> FieldType<TNew>()
        {
            Builder.FieldType(typeof(TNew), ConfigurationSource.Explicit);
            return new InputFieldBuilder<TNew>(Builder);
        }

        public InputFieldBuilder<T> RemoveDirectiveAnnotations(string name) =>
            throw new NotImplementedException();

        public InputFieldBuilder<T> RemoveDirectiveAnnotations() => throw new NotImplementedException();


        InternalInputFieldBuilder IInfrastructure<InternalInputFieldBuilder>.Instance => Builder;

        public InputFieldBuilder<T> Name(string name)
        {
            Check.NotNull(name, nameof(name));
            Builder.SetName(name, ConfigurationSource.Explicit);
            return this;
        }


        public InputFieldBuilder<T> DefaultValue(T value)
        {
            Builder.DefaultValue(value, ConfigurationSource.Explicit);
            return this;
        }


        public InputFieldBuilder<T> RemoveDescription()
        {
            Builder.RemoveDescription(ConfigurationSource.Explicit);
            return this;
        }

        public InputFieldBuilder<T> Description(string description)
        {
            Check.NotNull(description, nameof(description));
            Builder.Description(description, ConfigurationSource.Explicit);
            return this;
        }
    }
}