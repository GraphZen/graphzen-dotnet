// Copyright (c) GraphZen LLC. All rights reserved.
// Licensed under the GraphZen Community License. See the LICENSE file in the project root for license information.

using System.Diagnostics.CodeAnalysis;
using GraphZen.Infrastructure;
using JetBrains.Annotations;

namespace GraphZen.TypeSystem.Internal
{
    public class InternalEnumValueBuilder : AnnotatableMemberDefinitionBuilder<EnumValueDefinition>
    {
        public InternalEnumValueBuilder(EnumValueDefinition definition,
            InternalSchemaBuilder schemaBuilder)
            : base(definition, schemaBuilder)
        {
        }


        public InternalEnumValueBuilder CustomValue(object value)
        {
            Definition.Value = value;
            return this;
        }


        public InternalEnumValueBuilder Deprecated(bool deprecated)
        {
            Definition.IsDeprecated = deprecated;
            return this;
        }


        public InternalEnumValueBuilder Deprecated(string reason)
        {
            Definition.DeprecationReason = reason;
            return this;
        }
    }
}