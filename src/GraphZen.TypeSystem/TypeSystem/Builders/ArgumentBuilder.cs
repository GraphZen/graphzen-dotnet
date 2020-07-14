// Copyright (c) GraphZen LLC. All rights reserved.
// Licensed under the GraphZen Community License. See the LICENSE file in the project root for license information.

using System;
using System.Diagnostics.CodeAnalysis;
using GraphZen.Infrastructure;
using GraphZen.TypeSystem.Internal;
using JetBrains.Annotations;

namespace GraphZen.TypeSystem
{

    public class ArgumentBuilder : IArgumentBuilder
    {
        public ArgumentBuilder(InternalArgumentBuilder builder)
        {
            InternalBuilder = builder;
        }

        protected InternalArgumentBuilder InternalBuilder { get; }
        InternalArgumentBuilder IInfrastructure<InternalArgumentBuilder>.Instance => InternalBuilder;
        MutableArgument IInfrastructure<MutableArgument>.Instance => InternalBuilder.Definition;

        public IArgumentBuilder AddDirectiveAnnotation(string name, object value)
        {
            Check.NotNull(name, nameof(name));
            InternalBuilder.AddDirectiveAnnotation(name, value, ConfigurationSource.Explicit);
            return this;
        }

        IDirectivesBuilder IDirectivesBuilder.AddDirectiveAnnotation(string name) => AddDirectiveAnnotation(name);

        IDirectivesBuilder IDirectivesBuilder.RemoveDirectiveAnnotations(string name) => RemoveDirectiveAnnotations(name);

        IDirectivesBuilder IDirectivesBuilder.ClearDirectiveAnnotations() => ClearDirectiveAnnotations();


        IDirectivesBuilder IDirectivesBuilder.AddDirectiveAnnotation(string name, object value) => AddDirectiveAnnotation(name, value);

        public IArgumentBuilder AddDirectiveAnnotation(string name)
        {
            Check.NotNull(name, nameof(name));
            InternalBuilder.AddDirectiveAnnotation(name, null, ConfigurationSource.Explicit);
            return this;
        }


        public IArgumentBuilder RemoveDirectiveAnnotations(string name)
        {
            Check.NotNull(name, nameof(name));
            InternalBuilder.RemoveDirectiveAnnotations(name, ConfigurationSource.Explicit);
            return this;
        }

        public IArgumentBuilder ClearDirectiveAnnotations()
        {
            InternalBuilder.ClearDirectiveAnnotations(ConfigurationSource.Explicit);
            return this;
        }


        public IArgumentBuilder ArgumentType<T>()
        {
            InternalBuilder.ArgumentType(typeof(T), ConfigurationSource.Explicit);
            return this;
        }

        public IArgumentBuilder ArgumentType(Type clrType)
        {
            Check.NotNull(clrType, nameof(clrType));
            InternalBuilder.ArgumentType(clrType, ConfigurationSource.Explicit);
            return this;
        }

        public IArgumentBuilder ArgumentType(string type)
        {
            Check.NotNull(type, nameof(type));
            InternalBuilder.ArgumentType(type, ConfigurationSource.Explicit);
            return this;
        }

        public IArgumentBuilder Name(string name)
        {
            Check.NotNull(name, nameof(name));
            InternalBuilder.SetName(name, ConfigurationSource.Explicit);
            return this;
        }


        public IArgumentBuilder DefaultValue(object? value)
        {
            InternalBuilder.DefaultValue(value, ConfigurationSource.Explicit);
            return this;
        }

        public IArgumentBuilder RemoveDefaultValue() => throw new NotImplementedException();


        public IArgumentBuilder RemoveDescription()
        {
            InternalBuilder.RemoveDescription(ConfigurationSource.Explicit);
            return this;
        }

        public IArgumentBuilder Description(string description)
        {
            Check.NotNull(description, nameof(description));
            InternalBuilder.Description(description, ConfigurationSource.Explicit);
            return this;
        }
    }
}