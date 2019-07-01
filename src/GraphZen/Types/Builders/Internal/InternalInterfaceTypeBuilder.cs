// Copyright (c) GraphZen LLC. All rights reserved.
// Licensed under the GraphZen Community License. See the LICENSE file in the project root for license information.

using GraphZen.Infrastructure;
using GraphZen.Types.Internal;
using JetBrains.Annotations;

namespace GraphZen.Types.Builders.Internal
{
    public class
        InternalInterfaceTypeBuilder : InternalFieldsContainerBuilder<InterfaceTypeDefinition,
            InternalInterfaceTypeBuilder>
    {
        public InternalInterfaceTypeBuilder([NotNull] InterfaceTypeDefinition definition,
            [NotNull] InternalSchemaBuilder schemaBuilder) : base(definition, schemaBuilder)
        {
        }


        [NotNull]
        public InternalInterfaceTypeBuilder ResolveType(TypeResolver<object, GraphQLContext> resolveType)
        {
            Definition.ResolveType = resolveType;
            return this;
        }

        [NotNull]
        public InternalInterfaceTypeBuilder Name(string name, ConfigurationSource configurationSource)
        {
            Definition.SetName(name, configurationSource);
            return this;
        }
    }
}