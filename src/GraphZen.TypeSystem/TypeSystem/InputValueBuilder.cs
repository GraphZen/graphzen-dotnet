// Copyright (c) GraphZen LLC. All rights reserved.
// Licensed under the GraphZen Community License. See the LICENSE file in the project root for license information.

using System.Diagnostics.CodeAnalysis;
using GraphZen.Infrastructure;
using GraphZen.TypeSystem.Internal;
using JetBrains.Annotations;

namespace GraphZen.TypeSystem
{
    public class InputValueBuilder : IInfrastructure<InternalInputValueBuilder>, IAnnotableBuilder<InputValueBuilder>
    {
        public InputValueBuilder(InternalInputValueBuilder builder)
        {
            Check.NotNull(builder, nameof(builder));
            Builder = builder;
        }


        protected InternalInputValueBuilder Builder { get; }

        public InputValueBuilder DirectiveAnnotation(string name) => DirectiveAnnotation(name, null);
        public InputValueBuilder DirectiveAnnotation(object directive) => throw new System.NotImplementedException();

        public InputValueBuilder DirectiveAnnotation(string name, object? value)
        {
            Builder.DirectiveAnnotation(Check.NotNull(name, nameof(name)), value);
            return this;
        }

        public InputValueBuilder RemoveDirectiveAnnotation(string name)
        {
            Builder.RemoveDirectiveAnnotation(Check.NotNull(name, nameof(name)));
            return this;
        }

        InternalInputValueBuilder IInfrastructure<InternalInputValueBuilder>.Instance => Builder;

        public InputValueBuilder Name(string name)
        {
            Builder.Name(name, ConfigurationSource.Explicit);
            return this;
        }


        public InputValueBuilder DefaultValue(object value)
        {
            Builder.DefaultValue(value, ConfigurationSource.Explicit);
            return this;
        }


        public InputValueBuilder Description(string? description)
        {
            Builder.Description(description, ConfigurationSource.Explicit);
            return this;
        }
    }
}