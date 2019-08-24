// Copyright (c) GraphZen LLC. All rights reserved.
// Licensed under the GraphZen Community License. See the LICENSE file in the project root for license information.
using JetBrains.Annotations;
#nullable disable


using System;
using GraphZen.Infrastructure;

namespace GraphZen.TypeSystem.Internal
{
    public class InternalEnumTypeBuilder : AnnotatableMemberDefinitionBuilder<EnumTypeDefinition>
    {
        public InternalEnumTypeBuilder( EnumTypeDefinition definition,
             InternalSchemaBuilder schemaBuilder) : base(
            definition, schemaBuilder)
        {
        }

        
        public InternalEnumValueBuilder Value( string name,
            ConfigurationSource nameConfigurationSource,
            ConfigurationSource configurationSource) =>
            Definition.GetOrAddValue(name, nameConfigurationSource, configurationSource).Builder;

        public InternalEnumTypeBuilder ClrType(Type clrType, ConfigurationSource configurationSource)
        {
            if (Definition.SetClrType(clrType, configurationSource))
            {
                ConfigureEnumFromClrType();
            }

            return this;
        }

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
                var member = clrType.GetMember(value.ToString());
                if (member.Length > 0)
                {
                    var memberInfo = clrType.GetMember(value.ToString())[0] ??
                                     // ReSharper disable once ConstantNullCoalescingCondition
                                     throw new InvalidOperationException(
                                         $"Unable to get MemberInfo for enum value of type {Definition}");
                    var (name, nameConfigurationSource) = memberInfo.GetGraphQLNameForEnumValue();
                    var valueBuilder = Value(name, nameConfigurationSource, ConfigurationSource.Convention)
                        .CustomValue(value);
                    if (memberInfo.TryGetDescriptionFromDataAnnotation(out var description))
                    {
                        valueBuilder.Description(description, ConfigurationSource.DataAnnotation);
                    }
                }
            }

            return true;
        }
    }
}