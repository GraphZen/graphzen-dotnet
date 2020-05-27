// Copyright (c) GraphZen LLC. All rights reserved.
// Licensed under the GraphZen Community License. See the LICENSE file in the project root for license information.

using System;
using System.Diagnostics.CodeAnalysis;
using GraphZen.Infrastructure;
using GraphZen.TypeSystem.Internal;
using JetBrains.Annotations;

namespace GraphZen.TypeSystem
{
    public class InputFieldBuilder<TInputField> : IInfrastructure<InternalInputFieldBuilder>,
        IAnnotableBuilder<InputFieldBuilder<TInputField>>
    {
        public InputFieldBuilder(InternalInputFieldBuilder builder)
        {
            Check.NotNull(builder, nameof(builder));
            Builder = builder;
        }


        protected InternalInputFieldBuilder Builder { get; }

        public InputFieldBuilder<TInputField> AddDirectiveAnnotation(string name, object? value = null) =>
            throw new NotImplementedException();

        public InputFieldBuilder<TInputField> UpdateOrAddDirectiveAnnotation(string name, object? value = null)
        {
            Builder.DirectiveAnnotation(Check.NotNull(name, nameof(name)), value, ConfigurationSource.Explicit);
            return this;
        }

        public InputFieldBuilder<TInputField> RemoveDirectiveAnnotations(string name) =>
            throw new NotImplementedException();

        public InputFieldBuilder<TInputField> RemoveDirectiveAnnotations() => throw new NotImplementedException();


        InternalInputFieldBuilder IInfrastructure<InternalInputFieldBuilder>.Instance => Builder;

        public InputFieldBuilder<TInputField> Name(string name)
        {
            Check.NotNull(name, nameof(name));
            Builder.SetName(name, ConfigurationSource.Explicit);
            return this;
        }


        public InputFieldBuilder<TInputField> DefaultValue(TInputField value)
        {
            Builder.DefaultValue(value, ConfigurationSource.Explicit);
            return this;
        }


        public InputFieldBuilder<TInputField> RemoveDescription()
        {
            Builder.RemoveDescription(ConfigurationSource.Explicit);
            return this;
        }

        public InputFieldBuilder<TInputField> Description(string description)
        {
            Check.NotNull(description, nameof(description));
            Builder.Description(description, ConfigurationSource.Explicit);
            return this;
        }
    }
}