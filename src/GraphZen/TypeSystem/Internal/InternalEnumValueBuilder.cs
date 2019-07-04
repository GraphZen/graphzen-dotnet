// Copyright (c) GraphZen LLC. All rights reserved.
// Licensed under the GraphZen Community License. See the LICENSE file in the project root for license information.

using GraphZen.Infrastructure;


namespace GraphZen.TypeSystem.Internal
{
    public class InternalEnumValueBuilder : AnnotatableMemberDefinitionBuilder<EnumValueDefinition>
    {
        public InternalEnumValueBuilder([NotNull] EnumValueDefinition definition,
            [NotNull] InternalSchemaBuilder schemaBuilder)
            : base(definition, schemaBuilder)
        {
        }

        [NotNull]
        public InternalEnumValueBuilder CustomValue([CanBeNull] object value)
        {
            Definition.Value = value;
            return this;
        }

        [NotNull]
        public InternalEnumValueBuilder Deprecated(bool deprecated)
        {
            Definition.IsDeprecated = deprecated;
            return this;
        }

        [NotNull]
        public InternalEnumValueBuilder Deprecated(string reason)
        {
            Definition.DeprecationReason = reason;
            return this;
        }
    }
}