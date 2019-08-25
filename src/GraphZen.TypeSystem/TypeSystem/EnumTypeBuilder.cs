// Copyright (c) GraphZen LLC. All rights reserved.
// Licensed under the GraphZen Community License. See the LICENSE file in the project root for license information.
using JetBrains.Annotations;



using System;
using GraphZen.Infrastructure;
using GraphZen.TypeSystem.Internal;

namespace GraphZen.TypeSystem
{
    public class EnumTypeBuilder<TEnum> : IEnumTypeBuilder<TEnum>, IInfrastructure<InternalEnumTypeBuilder>
    {
        public EnumTypeBuilder(InternalEnumTypeBuilder builder)
        {
            Check.NotNull(builder, nameof(builder));
            Builder = builder;
        }

        
        private InternalEnumTypeBuilder Builder { get; }

        public IEnumTypeBuilder<TEnum> Description(string description)
        {
            Builder.Description(description, ConfigurationSource.Explicit);
            return this;
        }

        public IEnumTypeBuilder<TEnum> Value(TEnum value, Action<IEnumValueBuilder>? valueConfigurator = null)
        {
            Check.NotNull(value, nameof(value));
            var enumType = typeof(TEnum);
            if (enumType != typeof(string) && !enumType.IsEnum)
            {
                throw new ArgumentException("Enum types can only be bound to strings or CLR enum types", nameof(value));
            }

            var vb = Builder.Value(value.ToString()!, ConfigurationSource.Convention, ConfigurationSource.Explicit);
            valueConfigurator?.Invoke(new EnumValueBuilder(vb));
            return this;
        }


        public IEnumTypeBuilder<TEnum> Name(string name)
        {
            Check.NotNull(name, nameof(name));
            Builder.Name(name, ConfigurationSource.Explicit);
            return this;
        }

        public IEnumTypeBuilder<object> ClrType(Type clrType)
        {
            Check.NotNull(clrType, nameof(clrType));
            Builder.ClrType(clrType, ConfigurationSource.Explicit);
            return new EnumTypeBuilder<object>(Builder);
        }

        public IEnumTypeBuilder<T> ClrType<T>()
        {
            Builder.ClrType(typeof(T), ConfigurationSource.Explicit);
            return new EnumTypeBuilder<T>(Builder);
        }

        public IEnumTypeBuilder<TEnum> DirectiveAnnotation(string name) => DirectiveAnnotation(name, null);

        public IEnumTypeBuilder<TEnum> DirectiveAnnotation(string name, object? value)
        {
            Builder.AddOrUpdateDirectiveAnnotation(Check.NotNull(name, nameof(name)), value);
            return this;
        }

        public IEnumTypeBuilder<TEnum> RemoveDirectiveAnnotation(string name)
        {
            Builder.RemoveDirectiveAnnotation(Check.NotNull(name, nameof(name)));
            return this;
        }

        InternalEnumTypeBuilder IInfrastructure<InternalEnumTypeBuilder>.Instance => Builder;
    }
}