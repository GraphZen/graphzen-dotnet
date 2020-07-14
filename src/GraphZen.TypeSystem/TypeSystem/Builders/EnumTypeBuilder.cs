// Copyright (c) GraphZen LLC. All rights reserved.
// Licensed under the GraphZen Community License. See the LICENSE file in the project root for license information.

using System;
using System.Diagnostics.CodeAnalysis;
using GraphZen.Infrastructure;
using GraphZen.TypeSystem.Internal;
using JetBrains.Annotations;

namespace GraphZen.TypeSystem
{
    public class EnumTypeBuilder : IEnumTypeBuilder
    {
        public EnumTypeBuilder(InternalEnumTypeBuilder builder)
        {
            Builder = builder;
        }

        protected InternalEnumTypeBuilder Builder { get; }
        InternalEnumTypeBuilder IInfrastructure<InternalEnumTypeBuilder>.Instance => Builder;
        MutableEnumType IInfrastructure<MutableEnumType>.Instance => Builder.Definition;

        public INameBuilder Name(string name)
        {
            Check.NotNull(name, nameof(name));
            Builder.SetName(name, ConfigurationSource.Explicit);
            return this;
        }

        public IDescriptionBuilder Description(string description)
        {
            Check.NotNull(description, nameof(description));
            Builder.Description(description, ConfigurationSource.Explicit);
            return this;
        }

        public IDescriptionBuilder RemoveDescription()
        {
            Builder.RemoveDescription(ConfigurationSource.Explicit);
            return this;
        }

        public IClrTypeBuilder ClrType(Type clrType, bool inferName = false)
        {
            Check.NotNull(clrType, nameof(clrType));
            Builder.ClrType(clrType, inferName, ConfigurationSource.Explicit);
            return this;
        }

        public IClrTypeBuilder ClrType(Type clrType, string name)
        {
            Check.NotNull(clrType, nameof(clrType));
            Check.NotNull(name, nameof(name));
            Builder.ClrType(clrType, name, ConfigurationSource.Explicit);
            return this;
        }

        public IClrTypeBuilder RemoveClrType()
        {
            Builder.RemoveClrType(ConfigurationSource.Explicit);
            return new EnumTypeBuilder<object>(Builder);
        }

        public IDirectivesBuilder AddDirectiveAnnotation(string name, object value)
        {
            Check.NotNull(name, nameof(name));
            Builder.AddDirectiveAnnotation(name, value, ConfigurationSource.Explicit);
            return this;
        }

        public IDirectivesBuilder AddDirectiveAnnotation(string name)
        {
            Check.NotNull(name, nameof(name));
            Builder.AddDirectiveAnnotation(name, null, ConfigurationSource.Explicit);
            return this;
        }

        public IDirectivesBuilder RemoveDirectiveAnnotations(string name)
        {
            Check.NotNull(name, nameof(name));
            Builder.RemoveDirectiveAnnotations(name, ConfigurationSource.Explicit);
            return this;
        }

        public IDirectivesBuilder ClearDirectiveAnnotations()
        {
            Builder.ClearDirectiveAnnotations(ConfigurationSource.Explicit);
            return this;
        }
    }

    public class EnumTypeBuilder<TEnum> : EnumTypeBuilder, IEnumTypeBuilder<TEnum> where TEnum : notnull
    {
        public EnumTypeBuilder(InternalEnumTypeBuilder builder) : base(builder)
        {
        }

        public new EnumTypeBuilder<TEnum> Description(string description) =>
            (EnumTypeBuilder<TEnum>)base.Description(description);

        public new EnumTypeBuilder<TEnum> RemoveDescription() => (EnumTypeBuilder<TEnum>)base.RemoveDescription();

        public EnumTypeBuilder<TEnum> Value(TEnum value)
        {
            Check.NotNull(value, nameof(value));
            Builder.Value(value, ConfigurationSource.Explicit);
            return this;
        }

        public EnumTypeBuilder<TEnum> RemoveValue(TEnum value)
        {
            Check.NotNull(value, nameof(value));
            Builder.RemoveValue(value, ConfigurationSource.Explicit);
            return this;
        }

        public EnumTypeBuilder<TEnum> Value(TEnum value, Action<EnumValueBuilder> configurator)
        {
            Check.NotNull(value, nameof(value));
            Check.NotNull(configurator, nameof(configurator));
            var vb = new EnumValueBuilder(Builder.Value(value, ConfigurationSource.Explicit)!);
            configurator(vb);
            return this;
        }

        public EnumTypeBuilder<TEnum> IgnoreValue(TEnum value)
        {
            Check.NotNull(value, nameof(value));
            Builder.IgnoreValue(value, ConfigurationSource.Explicit);
            return this;
        }

        public EnumTypeBuilder<TEnum> UnignoreValue(TEnum value)
        {
            Check.NotNull(value, nameof(value));
            Builder.UnignoreValue(value, ConfigurationSource.Explicit);
            return this;
        }


        public new EnumTypeBuilder<TEnum> Name(string name) => (EnumTypeBuilder<TEnum>)base.Name(name);

        public new EnumTypeBuilder<object> ClrType(Type clrType, bool inferName = false)
        {
            base.ClrType(clrType, inferName);
            return new EnumTypeBuilder<object>(Builder);
        }

        public new EnumTypeBuilder<object> ClrType(Type clrType, string name)
        {
            base.ClrType(clrType, name);
            return new EnumTypeBuilder<object>(Builder);
        }

        public EnumTypeBuilder<T> ClrType<T>(bool inferName = false) where T : notnull
        {
            base.ClrType(typeof(T), inferName);
            return new EnumTypeBuilder<T>(Builder);
        }

        public EnumTypeBuilder<T> ClrType<T>(string name) where T : notnull
        {
            base.ClrType(typeof(T), name);
            return new EnumTypeBuilder<T>(Builder);
        }

        public new EnumTypeBuilder<object> RemoveClrType() => (EnumTypeBuilder<object>)base.RemoveClrType();


        public new EnumTypeBuilder<TEnum> AddDirectiveAnnotation(string name, object value) =>
            (EnumTypeBuilder<TEnum>)base.AddDirectiveAnnotation(name, value);


        public new EnumTypeBuilder<TEnum> AddDirectiveAnnotation(string name) =>
            (EnumTypeBuilder<TEnum>)base.AddDirectiveAnnotation(name);


        public new EnumTypeBuilder<TEnum> RemoveDirectiveAnnotations(string name) =>
            (EnumTypeBuilder<TEnum>)base.RemoveDirectiveAnnotations(name);

        public new EnumTypeBuilder<TEnum> ClearDirectiveAnnotations() =>
            (EnumTypeBuilder<TEnum>)base.ClearDirectiveAnnotations();
    }
}