// Copyright (c) GraphZen LLC. All rights reserved.
// Licensed under the GraphZen Community License. See the LICENSE file in the project root for license information.

using System;
using System.Diagnostics.CodeAnalysis;
using GraphZen.Infrastructure;
using GraphZen.TypeSystem.Internal;
using JetBrains.Annotations;

namespace GraphZen.TypeSystem
{
    public class EnumValueBuilder : IInfrastructure<InternalEnumValueBuilder>, IEnumValueBuilder
    {
        public EnumValueBuilder(InternalEnumValueBuilder builder)
        {
            Check.NotNull(builder, nameof(builder));
            Builder = builder;
        }


        private InternalEnumValueBuilder Builder { get; }

        public IEnumValueBuilder CustomValue(object value)
        {
            Builder.CustomValue(value);
            return this;
        }

        public IEnumValueBuilder Deprecated(bool deprecated = true) => throw new NotImplementedException();

        public IEnumValueBuilder Deprecated(string? reason) => throw new NotImplementedException();

        public IEnumValueBuilder Name(string name)
        {
            Check.NotNull(name, nameof(name));
            Builder.Name(name, ConfigurationSource.Explicit);
            return this;
        }

        public IEnumValueBuilder Description(string? description)
        {
            Builder.Description(description, ConfigurationSource.Explicit);
            return this;
        }

        public IEnumValueBuilder DirectiveAnnotation(string name) => DirectiveAnnotation(name, null);
        public IEnumValueBuilder DirectiveAnnotation(object directive) => throw new NotImplementedException();

        public IEnumValueBuilder DirectiveAnnotation(string name, object? value)
        {
            Builder.DirectiveAnnotation(Check.NotNull(name, nameof(name)), value);
            return this;
        }

        public IEnumValueBuilder RemoveDirectiveAnnotation(string name)
        {
            Builder.RemoveDirectiveAnnotation(Check.NotNull(name, nameof(name)));
            return this;
        }

        InternalEnumValueBuilder IInfrastructure<InternalEnumValueBuilder>.Instance => Builder;
    }
}