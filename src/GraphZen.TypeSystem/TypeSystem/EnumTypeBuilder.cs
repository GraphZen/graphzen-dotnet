// Copyright (c) GraphZen LLC. All rights reserved.
// Licensed under the GraphZen Community License. See the LICENSE file in the project root for license information.

using System;
using System.Diagnostics.CodeAnalysis;
using GraphZen.Infrastructure;
using GraphZen.TypeSystem.Internal;
using JetBrains.Annotations;

namespace GraphZen.TypeSystem
{
    public class EnumTypeBuilder<TEnum> : IEnumTypeBuilder<TEnum>
        where TEnum : notnull
    {
        public EnumTypeBuilder(InternalEnumTypeBuilder builder)
        {
            Check.NotNull(builder, nameof(builder));
            Builder = builder;
        }


        private InternalEnumTypeBuilder Builder { get; }

        public EnumTypeBuilder<TEnum> Description(string description)
        {
            Check.NotNull(description, nameof(description));
            Builder.Description(description, ConfigurationSource.Explicit);
            return this;
        }

        public EnumTypeBuilder<TEnum> RemoveDescription()
        {
            Builder.RemoveDescription(ConfigurationSource.Explicit);
            return this;
        }

        public EnumTypeBuilder<TEnum> Value(TEnum value)
        {
            Check.NotNull(value, nameof(value));
            Builder.Value(value, ConfigurationSource.Explicit);
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


        public EnumTypeBuilder<TEnum> Name(string name)
        {
            Check.NotNull(name, nameof(name));
            Builder.SetName(name, ConfigurationSource.Explicit);
            return this;
        }

        public EnumTypeBuilder<object> ClrType(Type clrType, bool inferName = false)
        {
            Check.NotNull(clrType, nameof(clrType));
            Builder.ClrType(clrType, inferName, ConfigurationSource.Explicit);
            return new EnumTypeBuilder<object>(Builder);
        }

        public EnumTypeBuilder<object> ClrType(Type clrType, string name)
        {
            Check.NotNull(clrType, nameof(clrType));
            Check.NotNull(name, nameof(name));
            Builder.ClrType(clrType, name, ConfigurationSource.Explicit);
            return new EnumTypeBuilder<object>(Builder);
        }

        public EnumTypeBuilder<T> ClrType<T>(bool inferName = false) where T : notnull
        {
            Builder.ClrType(typeof(T), inferName, ConfigurationSource.Explicit);
            return new EnumTypeBuilder<T>(Builder);
        }

        public EnumTypeBuilder<T> ClrType<T>(string name) where T : notnull
        {
            Check.NotNull(name, nameof(name));
            Builder.ClrType(typeof(T), name, ConfigurationSource.Explicit);
            return new EnumTypeBuilder<T>(Builder);
        }

        public EnumTypeBuilder<object> RemoveClrType()
        {
            Builder.RemoveClrType(ConfigurationSource.Explicit);
            return new EnumTypeBuilder<object>(Builder);
        }


        public EnumTypeBuilder<TEnum> AddDirectiveAnnotation(string name, object? value = null) =>
            throw new NotImplementedException();

        public EnumTypeBuilder<TEnum> UpdateOrAddDirectiveAnnotation(string name, object? value = null)
        {
            Builder.DirectiveAnnotation(Check.NotNull(name, nameof(name)), value, ConfigurationSource.Explicit);
            return this;
        }

        public EnumTypeBuilder<TEnum> RemoveDirectiveAnnotations(string name) => throw new NotImplementedException();
        public EnumTypeBuilder<TEnum> RemoveDirectiveAnnotations() => throw new NotImplementedException();


        InternalEnumTypeBuilder IInfrastructure<InternalEnumTypeBuilder>.Instance => Builder;
    }
}