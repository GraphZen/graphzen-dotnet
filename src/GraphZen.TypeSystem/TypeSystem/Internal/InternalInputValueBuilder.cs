// Copyright (c) GraphZen LLC. All rights reserved.
// Licensed under the GraphZen Community License. See the LICENSE file in the project root for license information.
using JetBrains.Annotations;



using System;
using System.ComponentModel;
using System.Reflection;
using GraphZen.Infrastructure;

namespace GraphZen.TypeSystem.Internal
{
    public class InternalInputValueBuilder : AnnotatableMemberDefinitionBuilder<InputValueDefinition>
    {
        public InternalInputValueBuilder( InputValueDefinition definition,
             InternalSchemaBuilder schemaBuilder) : base(
            definition, schemaBuilder)
        {
        }

        
        public InternalInputValueBuilder Type( string type)
        {
            Definition.InputType = Schema.GetOrAddTypeReference(type, Definition);
            return this;
        }

        
        public InternalInputValueBuilder Type( Type clrType)
        {
            Definition.InputType = Schema.GetOrAddTypeReference(clrType, false, false, Definition);
            return this;
        }

        
        public InternalInputValueBuilder FieldType( PropertyInfo property)
        {
            Definition.InputType = Schema.GetOrAddTypeReference(property, Definition);
            return this;
        }

        
        public InternalInputValueBuilder DefaultValue( ParameterInfo parameter,
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

        
        public InternalInputValueBuilder DefaultValue( PropertyInfo property,
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


        
        public InternalInputValueBuilder DefaultValue( object value, ConfigurationSource configurationSource)
        {
            Definition.SetDefaultValue(value, configurationSource);
            return this;
        }

        
        public InternalInputValueBuilder RemoveDefaultValue(ConfigurationSource configurationSource)
        {
            Definition.RemoveDefaultValue(configurationSource);
            return this;
        }
    }
}