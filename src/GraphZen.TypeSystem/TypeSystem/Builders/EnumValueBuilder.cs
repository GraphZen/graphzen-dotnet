// Copyright (c) GraphZen LLC. All rights reserved.
// Licensed under the GraphZen Community License. See the LICENSE file in the project root for license information.

using System;
using System.Diagnostics.CodeAnalysis;
using GraphZen.Infrastructure;
using GraphZen.TypeSystem.Internal;
using JetBrains.Annotations;

namespace GraphZen.TypeSystem
{
    public class EnumValueBuilder : IEnumValueBuilder
    {
        public EnumValueBuilder(InternalEnumValueBuilder builder)
        {
            Check.NotNull(builder, nameof(builder));
            Builder = builder;
        }


        private InternalEnumValueBuilder Builder { get; }

        IDescriptionBuilder IDescriptionBuilder.Description(string description) => Description(description);

        IDescriptionBuilder IDescriptionBuilder.RemoveDescription() => RemoveDescription();

        public EnumValueBuilder RemoveDescription()
        {
            Builder.RemoveDescription(ConfigurationSource.Explicit);
            return this;
        }

        public EnumValueBuilder CustomValue(object value)
        {
            Builder.CustomValue(value);
            return this;
        }

        public EnumValueBuilder RemoveCustomValue() => throw new NotImplementedException();

        public EnumValueBuilder Name(string name)
        {
            Check.NotNull(name, nameof(name));
            Builder.Name(name, ConfigurationSource.Explicit);
            return this;
        }

        public EnumValueBuilder Description(string description)
        {
            Check.NotNull(description, nameof(description));
            Builder.Description(description, ConfigurationSource.Explicit);
            return this;
        }


        public EnumValueBuilder AddDirectiveAnnotation(string name, object value)
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

        public EnumValueBuilder AddDirectiveAnnotation(string name)
        {
            Check.NotNull(name, nameof(name));
            Builder.AddDirectiveAnnotation(name, null, ConfigurationSource.Explicit);
            return this;
        }


        public EnumValueBuilder RemoveDirectiveAnnotations(string name)
        {
            Check.NotNull(name, nameof(name));
            Builder.RemoveDirectiveAnnotations(name, ConfigurationSource.Explicit);
            return this;
        }

        public EnumValueBuilder ClearDirectiveAnnotations()
        {
            Builder.ClearDirectiveAnnotations(ConfigurationSource.Explicit);
            return this;
        }


        InternalEnumValueBuilder IInfrastructure<InternalEnumValueBuilder>.Instance => Builder;
        MutableEnumValue IInfrastructure<MutableEnumValue>.Instance => Builder.Definition;
        INameBuilder INameBuilder.Name(string name) => Name(name);
    }
}