// Copyright (c) GraphZen LLC. All rights reserved.
// Licensed under the GraphZen Community License. See the LICENSE file in the project root for license information.

using System;
using System.Diagnostics.CodeAnalysis;
using GraphZen.Infrastructure;
using GraphZen.TypeSystem.Internal;
using JetBrains.Annotations;

namespace GraphZen.TypeSystem
{
    public class InputValueBuilder<TInputValue> : IInfrastructure<InternalInputValueBuilder>,
        IAnnotableBuilder<InputValueBuilder<TInputValue>>
    {
        public InputValueBuilder(InternalInputValueBuilder builder)
        {
            Check.NotNull(builder, nameof(builder));
            Builder = builder;
        }


        protected InternalInputValueBuilder Builder { get; }

        public InputValueBuilder<TInputValue> AddDirectiveAnnotation(string name, object? value = null) =>
            throw new NotImplementedException();

        public InputValueBuilder<TInputValue> UpdateOrAddDirectiveAnnotation(string name, object? value = null)
        {
            Builder.DirectiveAnnotation(Check.NotNull(name, nameof(name)), value, ConfigurationSource.Explicit);
            return this;
        }

        public InputValueBuilder<TInputValue> RemoveDirectiveAnnotations(string name) =>
            throw new NotImplementedException();

        public InputValueBuilder<TInputValue> RemoveDirectiveAnnotations() => throw new NotImplementedException();


        InternalInputValueBuilder IInfrastructure<InternalInputValueBuilder>.Instance => Builder;

        public InputValueBuilder<TInputValue> Name(string name)
        {
            Check.NotNull(name, nameof(name));
            Builder.SetName(name, ConfigurationSource.Explicit);
            return this;
        }


        public InputValueBuilder<TInputValue> DefaultValue(TInputValue value)
        {
            Builder.DefaultValue(value, ConfigurationSource.Explicit);
            return this;
        }


        public InputValueBuilder<TInputValue> RemoveDescription()
        {
            Builder.RemoveDescription(ConfigurationSource.Explicit);
            return this;
        }

        public InputValueBuilder<TInputValue> Description(string description)
        {
            Check.NotNull(description, nameof(description));
            Builder.Description(description, ConfigurationSource.Explicit);
            return this;
        }
    }
}