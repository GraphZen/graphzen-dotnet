// Copyright (c) GraphZen LLC. All rights reserved.
// Licensed under the GraphZen Community License. See the LICENSE file in the project root for license information.

using System.Diagnostics.CodeAnalysis;
using GraphZen.Infrastructure;
using JetBrains.Annotations;

namespace GraphZen.TypeSystem.Internal
{
    public class InternalEnumValueBuilder : AnnotatableMemberDefinitionBuilder<EnumValueDefinition>
    {
        public InternalEnumValueBuilder(EnumValueDefinition value) : base(value)
        {
        }


        public InternalEnumValueBuilder CustomValue(object value)
        {
            Definition.Value = value;
            return this;
        }


        public InternalEnumValueBuilder Name(string name, ConfigurationSource configurationSource)
        {
            var prevName = Definition.Name;
            if (Definition.SetName(name, configurationSource))
            {
                Definition.DeclaringType.InternalValues.Remove(prevName);
                Definition.DeclaringType.InternalValues[name] = Definition;
            }

            return this;
        }

        public InternalEnumValueBuilder Description(string description, ConfigurationSource configurationSource)
        {
            Definition.SetDescription(description, configurationSource);
            return this;
        }
    }
}