// Copyright (c) GraphZen LLC. All rights reserved.
// Licensed under the GraphZen Community License. See the LICENSE file in the project root for license information.

using System;
using System.ComponentModel;
using System.Diagnostics.CodeAnalysis;
using System.Reflection;
using GraphZen.Infrastructure;
using JetBrains.Annotations;

namespace GraphZen.TypeSystem.Internal
{
    public class InternalInputValueBuilder : AnnotatableMemberDefinitionBuilder<InputValueDefinition>
    {
        public InternalInputValueBuilder(InputValueDefinition definition,
            InternalSchemaBuilder schemaBuilder) : base(
            definition, schemaBuilder)
        {
        }


        public InternalInputValueBuilder InputType(string type, ConfigurationSource configurationSource)
        {
            var typeRef = Schema.GetOrAddTypeReference(type, Definition);
            Definition.SetTypeReference(typeRef, configurationSource);
            return this;
        }


        public InternalInputValueBuilder Type(Type clrType, ConfigurationSource configurationSource)
        {
            var typeRef = Schema.GetOrAddTypeReference(clrType, false, false, Definition, configurationSource);
            Definition.SetTypeReference(typeRef, configurationSource);
            return this;
        }


        public InternalInputValueBuilder InputFieldType(PropertyInfo property)
        {
            var typeRef = Schema.GetOrAddTypeReference(property, Definition);
            Definition.SetTypeReference(typeRef, ConfigurationSource.Convention);
            return this;
        }


        public InternalInputValueBuilder DefaultValue(ParameterInfo parameter,
            ConfigurationSource configurationSource)
        {
            var defaultValueAttribute = parameter.GetCustomAttribute<DefaultValueAttribute>();
            if (defaultValueAttribute != null && defaultValueAttribute.Value != null)
            {
                Definition.SetDefaultValue(defaultValueAttribute.Value, ConfigurationSource.DataAnnotation);
            }
            else if (parameter.HasDefaultValue && parameter.RawDefaultValue != null)
            {
                Definition.SetDefaultValue(parameter.RawDefaultValue, configurationSource);
            }
            else
            {
                RemoveDefaultValue(configurationSource);
            }

            return this;
        }


        public InternalInputValueBuilder DefaultValue(PropertyInfo property,
            ConfigurationSource configurationSource)
        {
            var defaultValueAttribute = property.GetCustomAttribute<DefaultValueAttribute>();
            if (defaultValueAttribute?.Value != null)
            {
                Definition.SetDefaultValue(defaultValueAttribute.Value, ConfigurationSource.DataAnnotation);
            }
            else
            {
                RemoveDefaultValue(configurationSource);
            }

            return this;
        }


        public InternalInputValueBuilder DefaultValue(object? value, ConfigurationSource configurationSource)
        {
            Definition.SetDefaultValue(value, configurationSource);
            return this;
        }


        public InternalInputValueBuilder RemoveDefaultValue(ConfigurationSource configurationSource)
        {
            Definition.RemoveDefaultValue(configurationSource);
            return this;
        }

        public InternalInputValueBuilder Description(string description, ConfigurationSource configurationSource)
        {
            Definition.SetDescription(description, configurationSource);
            return this;
        }

        public InternalInputValueBuilder SetName(string name, ConfigurationSource configurationSource)
        {
            Definition.SetName(name, configurationSource);
            return this;
        }
    }
}