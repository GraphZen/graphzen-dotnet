// Copyright (c) GraphZen LLC. All rights reserved.
// Licensed under the GraphZen Community License. See the LICENSE file in the project root for license information.

using System;
using System.Diagnostics.CodeAnalysis;
using GraphZen.Infrastructure;
using GraphZen.TypeSystem.Internal;
using JetBrains.Annotations;

namespace GraphZen.TypeSystem
{
    public class ArgumentBuilder<TArgument> : IInfrastructure<InternalArgumentBuilder>,
        IAnnotableBuilder<ArgumentBuilder<TArgument>>
    {
        public ArgumentBuilder(InternalArgumentBuilder builder)
        {
            Check.NotNull(builder, nameof(builder));
            Builder = builder;
        }


        protected InternalArgumentBuilder Builder { get; }

        public ArgumentBuilder<TArgument> AddDirectiveAnnotation(string name, object? value = null) =>
            throw new NotImplementedException();

        public ArgumentBuilder<TArgument> UpdateOrAddDirectiveAnnotation(string name, object? value = null)
        {
            Builder.DirectiveAnnotation(Check.NotNull(name, nameof(name)), value, ConfigurationSource.Explicit);
            return this;
        }

        public ArgumentBuilder<TArgument> RemoveDirectiveAnnotations(string name) =>
            throw new NotImplementedException();

        public ArgumentBuilder<TArgument> RemoveDirectiveAnnotations() => throw new NotImplementedException();


        InternalArgumentBuilder IInfrastructure<InternalArgumentBuilder>.Instance => Builder;

        public ArgumentBuilder<TArgument> Name(string name)
        {
            Check.NotNull(name, nameof(name));
            Builder.SetName(name, ConfigurationSource.Explicit);
            return this;
        }


        public ArgumentBuilder<TArgument> DefaultValue(TArgument value)
        {
            Builder.DefaultValue(value, ConfigurationSource.Explicit);
            return this;
        }


        public ArgumentBuilder<TArgument> RemoveDescription()
        {
            Builder.RemoveDescription(ConfigurationSource.Explicit);
            return this;
        }

        public ArgumentBuilder<TArgument> Description(string description)
        {
            Check.NotNull(description, nameof(description));
            Builder.Description(description, ConfigurationSource.Explicit);
            return this;
        }
    }
}