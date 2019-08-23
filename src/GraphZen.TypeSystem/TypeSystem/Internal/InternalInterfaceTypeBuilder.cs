// Copyright (c) GraphZen LLC. All rights reserved.
// Licensed under the GraphZen Community License. See the LICENSE file in the project root for license information.

using System;
using GraphZen.Infrastructure;

namespace GraphZen.TypeSystem.Internal
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

        [NotNull]
        public InternalInterfaceTypeBuilder ClrType(Type clrType, ConfigurationSource configurationSource)
        {
            if (Definition.SetClrType(clrType, configurationSource))
            {
                ConfigureInterfaceFromClrType();
            }

            return this;
        }

        public bool ConfigureInterfaceFromClrType()
        {
            var clrType = Definition.ClrType;
            if (clrType == null)
            {
                return false;
            }

            ConfigureOutputFields();

            if (clrType.TryGetDescriptionFromDataAnnotation(out var desc))
            {
                Definition.SetDescription(desc, ConfigurationSource.DataAnnotation);
            }

            return true;

        }
    }
}