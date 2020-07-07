// Copyright (c) GraphZen LLC. All rights reserved.
// Licensed under the GraphZen Community License. See the LICENSE file in the project root for license information.

using System;
using System.Diagnostics.CodeAnalysis;
using GraphZen.Infrastructure;
using GraphZen.TypeSystem.Internal;
using JetBrains.Annotations;

namespace GraphZen.TypeSystem
{
    public class ArgumentBuilder<T> : IArgumentBuilder<T>
    {
        public ArgumentBuilder(InternalArgumentBuilder builder)
        {
            Check.NotNull(builder, nameof(builder));
            Builder = builder;
        }


        protected InternalArgumentBuilder Builder { get; }


        public ArgumentBuilder<T> AddDirectiveAnnotation(string name, object value)
        {
            Check.NotNull(name, nameof(name));
            Builder.AddDirectiveAnnotation(name, value, ConfigurationSource.Explicit);
            return this;
        }

        public ArgumentBuilder<T> AddDirectiveAnnotation(string name)
        {
            Check.NotNull(name, nameof(name));
            Builder.AddDirectiveAnnotation(name, null, ConfigurationSource.Explicit);
            return this;
        }


        public ArgumentBuilder<T> RemoveDirectiveAnnotations(string name)
        {
            Check.NotNull(name, nameof(name));
            Builder.RemoveDirectiveAnnotations(name, ConfigurationSource.Explicit);
            return this;
        }

        public ArgumentBuilder<T> ClearDirectiveAnnotations()
        {
            Builder.ClearDirectiveAnnotations(ConfigurationSource.Explicit);
            return this;
        }


        InternalArgumentBuilder IInfrastructure<InternalArgumentBuilder>.Instance => Builder;
        ArgumentDefinition IInfrastructure<ArgumentDefinition>.Instance => Builder.Definition;

        public ArgumentBuilder<TNew> ArgumentType<TNew>()
        {
            Builder.ArgumentType(typeof(TNew), ConfigurationSource.Explicit);
            return new ArgumentBuilder<TNew>(Builder);
        }

        public ArgumentBuilder<object?> ArgumentType(Type clrType)
        {
            Check.NotNull(clrType, nameof(clrType));
            Builder.ArgumentType(clrType, ConfigurationSource.Explicit);
            return new ArgumentBuilder<object?>(Builder);
        }

        public ArgumentBuilder<object?> ArgumentType(string type)
        {
            Check.NotNull(type, nameof(type));
            Builder.ArgumentType(type, ConfigurationSource.Explicit);
            return new ArgumentBuilder<object?>(Builder);
        }

        public ArgumentBuilder<T> Name(string name)
        {
            Check.NotNull(name, nameof(name));
            Builder.SetName(name, ConfigurationSource.Explicit);
            return this;
        }


        public ArgumentBuilder<T> DefaultValue(T value)
        {
            Builder.DefaultValue(value, ConfigurationSource.Explicit);
            return this;
        }


        public ArgumentBuilder<T> RemoveDescription()
        {
            Builder.RemoveDescription(ConfigurationSource.Explicit);
            return this;
        }

        public ArgumentBuilder<T> Description(string description)
        {
            Check.NotNull(description, nameof(description));
            Builder.Description(description, ConfigurationSource.Explicit);
            return this;
        }
    }
}