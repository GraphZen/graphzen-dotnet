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

        private InternalInputFieldBuilder Builder { get; }


        public InputFieldBuilder<T> AddDirectiveAnnotation(string name, object value)
        {
            Check.NotNull(name, nameof(name));
            Builder.AddDirectiveAnnotation(name, value, ConfigurationSource.Explicit);
            return this;
        }

        IDirectivesBuilder IDirectivesBuilder.AddDirectiveAnnotation(string name) => AddDirectiveAnnotation(name);

        IDirectivesBuilder IDirectivesBuilder.RemoveDirectiveAnnotations(string name) => RemoveDirectiveAnnotations(name);

        IDirectivesBuilder IDirectivesBuilder.ClearDirectiveAnnotations() => ClearDirectiveAnnotations();

        IDirectivesBuilder IDirectivesBuilder.AddDirectiveAnnotation(string name, object value) =>
            AddDirectiveAnnotation(name, value);

        public InputFieldBuilder<T> AddDirectiveAnnotation(string name)
        {
            Check.NotNull(name, nameof(name));
            Builder.AddDirectiveAnnotation(name, null, ConfigurationSource.Explicit);
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

        public InputFieldBuilder<T> RemoveDirectiveAnnotations(string name)
        {
            Check.NotNull(name, nameof(name));
            Builder.RemoveDirectiveAnnotations(name, ConfigurationSource.Explicit);
            return this;
        }

        public InputFieldBuilder<T> ClearDirectiveAnnotations()
        {
            Builder.ClearDirectiveAnnotations(ConfigurationSource.Explicit);
            return this;
        }


        InternalInputFieldBuilder IInfrastructure<InternalInputFieldBuilder>.Instance => Builder;
        MutableInputField IInfrastructure<MutableInputField>.Instance => Builder.Definition;

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