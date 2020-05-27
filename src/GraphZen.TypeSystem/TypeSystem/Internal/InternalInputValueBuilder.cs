// Copyright (c) GraphZen LLC. All rights reserved.
// Licensed under the GraphZen Community License. See the LICENSE file in the project root for license information.

using System.ComponentModel;
using System.Diagnostics.CodeAnalysis;
using System.Reflection;
using GraphZen.Infrastructure;
using JetBrains.Annotations;

namespace GraphZen.TypeSystem.Internal
{
    public class InternalInputFieldBuilder : InternalInputValueBuilder<InputFieldDefinition, InternalInputFieldBuilder>
    {
        public InternalInputFieldBuilder(InputFieldDefinition definition) : base(definition)
        {
        }
    }

    public class InternalArgumentBuilder : InternalInputValueBuilder<ArgumentDefinition, InternalArgumentBuilder>
    {
        public InternalArgumentBuilder(ArgumentDefinition definition) : base(definition)
        {
        }
    }

    public abstract class InternalInputValueBuilder<T, TBuilder> : AnnotatableMemberDefinitionBuilder<T>
        where T : InputValueDefinition
        where TBuilder : InternalInputValueBuilder<T, TBuilder>
    {
        public InternalInputValueBuilder(T definition) : base(
            definition)
        {
        }


        public TBuilder DefaultValue(ParameterInfo parameter,
            ConfigurationSource configurationSource)
        {
            var defaultValueAttribute = parameter.GetCustomAttribute<DefaultValueAttribute>();
            if (defaultValueAttribute?.Value != null)
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

            return (TBuilder)this;
        }


        public TBuilder DefaultValue(PropertyInfo property,
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

            return (TBuilder)this;
        }


        public TBuilder DefaultValue(object? value, ConfigurationSource configurationSource)
        {
            Definition.SetDefaultValue(value, configurationSource);
            return (TBuilder)this;
        }


        public TBuilder RemoveDefaultValue(ConfigurationSource configurationSource)
        {
            Definition.RemoveDefaultValue(configurationSource);
            return (TBuilder)this;
        }

        public TBuilder Description(string description, ConfigurationSource configurationSource)
        {
            Definition.SetDescription(description, configurationSource);
            return (TBuilder)this;
        }

        public TBuilder SetName(string name, ConfigurationSource configurationSource)
        {
            Definition.SetName(name, configurationSource);
            return (TBuilder)this;
        }
    }
}