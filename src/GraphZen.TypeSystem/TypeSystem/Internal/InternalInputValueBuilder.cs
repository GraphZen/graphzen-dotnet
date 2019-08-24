#nullable disable
// Copyright (c) GraphZen LLC. All rights reserved.
// Licensed under the GraphZen Community License. See the LICENSE file in the project root for license information.

using System;
using System.ComponentModel;
using System.Reflection;
using GraphZen.Infrastructure;

namespace GraphZen.TypeSystem.Internal
{
    public class InternalInputValueBuilder : AnnotatableMemberDefinitionBuilder<InputValueDefinition>
    {
        public InternalInputValueBuilder([NotNull] InputValueDefinition definition,
            [NotNull] InternalSchemaBuilder schemaBuilder) : base(
            definition, schemaBuilder)
        {
        }

        [NotNull]
        public InternalInputValueBuilder Type([NotNull] string type)
        {
            Definition.InputType = Schema.GetOrAddTypeReference(type, Definition);
            return this;
        }

        [NotNull]
        public InternalInputValueBuilder Type([NotNull] Type clrType)
        {
            Definition.InputType = Schema.GetOrAddTypeReference(clrType, false, false, Definition);
            return this;
        }

        [NotNull]
        public InternalInputValueBuilder FieldType([NotNull] PropertyInfo property)
        {
            Definition.InputType = Schema.GetOrAddTypeReference(property, Definition);
            return this;
        }

        [NotNull]
        public InternalInputValueBuilder DefaultValue([NotNull] ParameterInfo parameter,
            ConfigurationSource configurationSource)
        {
            var defaultValueAttribute = parameter.GetCustomAttribute<DefaultValueAttribute>();
            if (defaultValueAttribute != null)
            {
                Definition.SetDefaultValue(defaultValueAttribute.Value, ConfigurationSource.DataAnnotation);
            }
            else if (parameter.HasDefaultValue)
            {
                Definition.SetDefaultValue(parameter.RawDefaultValue, configurationSource);
            }
            else
            {
                RemoveDefaultValue(configurationSource);
            }

            return this;
        }

        [NotNull]
        public InternalInputValueBuilder DefaultValue([NotNull] PropertyInfo property,
            ConfigurationSource configurationSource)
        {
            var defaultValueAttribute = property.GetCustomAttribute<DefaultValueAttribute>();
            if (defaultValueAttribute != null)
            {
                Definition.SetDefaultValue(defaultValueAttribute.Value, ConfigurationSource.DataAnnotation);
            }
            else
            {
                RemoveDefaultValue(configurationSource);
            }

            return this;
        }


        [NotNull]
        public InternalInputValueBuilder DefaultValue([CanBeNull] object value, ConfigurationSource configurationSource)
        {
            Definition.SetDefaultValue(value, configurationSource);
            return this;
        }

        [NotNull]
        public InternalInputValueBuilder RemoveDefaultValue(ConfigurationSource configurationSource)
        {
            Definition.RemoveDefaultValue(configurationSource);
            return this;
        }
    }
}