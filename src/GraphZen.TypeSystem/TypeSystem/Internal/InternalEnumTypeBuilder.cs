// Copyright (c) GraphZen LLC. All rights reserved.
// Licensed under the GraphZen Community License. See the LICENSE file in the project root for license information.

using System;
using System.Diagnostics.CodeAnalysis;
using System.Reflection;
using GraphZen.Infrastructure;
using JetBrains.Annotations;

namespace GraphZen.TypeSystem.Internal
{
    public class InternalEnumTypeBuilder : AnnotatableMemberDefinitionBuilder<EnumTypeDefinition>
    {
        public InternalEnumTypeBuilder(EnumTypeDefinition definition, InternalSchemaBuilder schemaBuilder)
            : base(definition, schemaBuilder)
        {
        }


        public InternalEnumValueBuilder? Value(object value, ConfigurationSource configurationSource)
        {
            var nameConfigurationSource = ConfigurationSource.Explicit;
            string? name;
            MemberInfo? enumMember = default;
            if (value is string strValue)
            {
                name = strValue;
            }
            else if (!value.GetType().IsEnum)
            {
                throw new InvalidOperationException(
                    $"Enum values can only be configured with string values or CLR enum values. The provided type was '{value.GetType()}'");
            }
            else
            {
                enumMember = GetMemberInfo(value.GetType(), value.ToString()!);
                (name, nameConfigurationSource) = enumMember.GetGraphQLNameForEnumValue();
                if (enumMember.IsIgnoredByDataAnnotation())
                {
                    IgnoreValue(name, ConfigurationSource.DataAnnotation);
                }
            }

            if (IsValueIgnored(name, configurationSource))
            {
                return null;
            }

            var enumValue = Definition.FindValue(name);
            if (enumValue is null)
            {
                enumValue = Definition.AddValue(name, configurationSource, nameConfigurationSource);
            }
            else
            {
                enumValue.UpdateConfigurationSource(configurationSource);
                enumValue.SetName(name, nameConfigurationSource);
            }

            var builder = new InternalEnumValueBuilder(enumValue, SchemaBuilder);
            if (enumMember != null)
            {
                builder.CustomValue(value);
                if (enumMember.TryGetDescriptionFromDataAnnotation(out var desc))
                {
                    builder.Description(desc, ConfigurationSource.DataAnnotation);
                }
            }

            return builder;
        }

        public InternalEnumTypeBuilder ClrType(Type clrType, string name, ConfigurationSource configurationSource)
        {
            if (Definition.SetClrType(clrType, name, configurationSource))
            {
                ConfigureEnumFromClrType();
            }

            return this;
        }

        public InternalEnumTypeBuilder ClrType(Type clrType, bool inferName, ConfigurationSource configurationSource)
        {
            if (Definition.SetClrType(clrType, inferName, configurationSource))
            {
                ConfigureEnumFromClrType();
            }

            return this;
        }

        public bool IsValueIgnored(string name, ConfigurationSource configurationSource) =>
            !configurationSource.Overrides(Definition.FindIgnoredValueConfigurationSource(name));

        public bool ConfigureEnumFromClrType()
        {
            var clrType = Definition.ClrType;
            if (clrType == null)
            {
                return false;
            }

            if (clrType.TryGetDescriptionFromDataAnnotation(out var desc))
            {
                Definition.SetDescription(desc, ConfigurationSource.DataAnnotation);
            }

            foreach (var value in Enum.GetValues(clrType))
            {
                Value(value!, ConfigurationSource.Convention);
            }

            return true;
        }

        private static MemberInfo GetMemberInfo(Type clrEnumType, string memberName) =>
            clrEnumType.GetMember(memberName)[0];

        private static string GetName(object value)
        {
            if (value is string strValue)
            {
                return strValue;
            }

            var enumMember = GetMemberInfo(value.GetType(), value.ToString()!);
            var (name, _) = enumMember.GetGraphQLNameForEnumValue();
            return name;
        }


        public InternalEnumTypeBuilder IgnoreValue(object value, ConfigurationSource configurationSource)
        {
            var name = GetName(value);
            Definition.IgnoreValue(name, configurationSource);
            return this;
        }

        public InternalEnumTypeBuilder UnignoreValue(object value, ConfigurationSource configurationSource)
        {
            var name = GetName(value);
            Definition.UnignoreValue(name, configurationSource);
            return this;
        }

        public InternalEnumTypeBuilder Description(string description, ConfigurationSource configurationSource)
        {
            Definition.SetDescription(description, configurationSource);
            return this;
        }

        public InternalEnumTypeBuilder SetName(string name, ConfigurationSource configurationSource)
        {
            Definition.SetName(name, configurationSource);
            return this;
        }

        public InternalEnumTypeBuilder RemoveClrType(ConfigurationSource configurationSource)
        {
            Definition.RemoveClrType(configurationSource);
            return this;
        }
    }
}