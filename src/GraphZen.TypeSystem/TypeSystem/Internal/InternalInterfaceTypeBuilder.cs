// Copyright (c) GraphZen LLC. All rights reserved.
// Licensed under the GraphZen Community License. See the LICENSE file in the project root for license information.

using System;
using System.Diagnostics.CodeAnalysis;
using GraphZen.Infrastructure;
using JetBrains.Annotations;

namespace GraphZen.TypeSystem.Internal
{
    public class
        InternalInterfaceTypeBuilder : InternalFieldsContainerBuilder<InterfaceTypeDefinition,
            InternalInterfaceTypeBuilder>
    {
        public InternalInterfaceTypeBuilder(InterfaceTypeDefinition definition,
            InternalSchemaBuilder schemaBuilder) : base(definition, schemaBuilder)
        {
        }


        public InternalInterfaceTypeBuilder ResolveType(TypeResolver<object, GraphQLContext> resolveType)
        {
            Definition.ResolveType = resolveType;
            return this;
        }


        public InternalInterfaceTypeBuilder Name(string name, ConfigurationSource configurationSource)
        {
            Definition.SetName(name, configurationSource);
            return this;
        }


        public InternalInterfaceTypeBuilder ClrType(Type clrType, ConfigurationSource configurationSource)
        {
            if (Definition.SetClrType(clrType, configurationSource)) ConfigureInterfaceFromClrType();

            return this;
        }

        public bool ConfigureInterfaceFromClrType()
        {
            var clrType = Definition.ClrType;
            if (clrType == null) return false;

            ConfigureOutputFields();

            if (clrType.TryGetDescriptionFromDataAnnotation(out var desc))
                Definition.SetDescription(desc, ConfigurationSource.DataAnnotation);

            return true;
        }
    }
}