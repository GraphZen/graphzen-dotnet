// Copyright (c) GraphZen LLC. All rights reserved.
// Licensed under the GraphZen Community License. See the LICENSE file in the project root for license information.

using GraphZen.Infrastructure;
using JetBrains.Annotations;

namespace GraphZen.TypeSystem.Internal
{
    public class InternalEnumTypeBuilder : AnnotatableMemberDefinitionBuilder<EnumTypeDefinition>
    {
        public InternalEnumTypeBuilder([NotNull] EnumTypeDefinition definition,
            [NotNull] InternalSchemaBuilder schemaBuilder) : base(
            definition, schemaBuilder)
        {
        }

        [NotNull]
        public InternalEnumValueBuilder Value([NotNull] string name,
            ConfigurationSource nameConfigurationSource,
            ConfigurationSource configurationSource) =>
            Definition.GetOrAddValue(name, nameConfigurationSource, configurationSource).Builder;
    }
}