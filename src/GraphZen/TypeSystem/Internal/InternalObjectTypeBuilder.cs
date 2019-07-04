// Copyright (c) GraphZen LLC. All rights reserved.
// Licensed under the GraphZen Community License. See the LICENSE file in the project root for license information.

using System;
using GraphZen.Infrastructure;
using GraphZen.LanguageModel.Internal;
using JetBrains.Annotations;

namespace GraphZen.TypeSystem.Internal
{
    public class
        InternalObjectTypeBuilder : InternalFieldsContainerBuilder<ObjectTypeDefinition, InternalObjectTypeBuilder>
    {
        public InternalObjectTypeBuilder([NotNull] ObjectTypeDefinition definition,
            [NotNull] InternalSchemaBuilder schemaBuilder) : base(
            definition, schemaBuilder)
        {
        }

        public void IsTypeOf([NotNull] IsTypeOf<object, GraphQLContext> isTypeOfFn) => Definition.IsTypeOf = isTypeOfFn;


        public bool Interface([NotNull] Type clrType, ConfigurationSource configurationSource)
        {
            if (clrType.IsIgnoredByDataAnnotation())
            {
                IgnoreInterface(clrType.GetGraphQLName(), ConfigurationSource.DataAnnotation);
            }

            if (IsInterfaceIgnored(clrType.GetGraphQLName(), configurationSource))
            {
                return false;
            }

            var existing = Schema.FindOutputType(clrType);

            if (existing is InterfaceTypeDefinition interfaceDef)
            {
                Definition.AddInterface(interfaceDef.GetTypeReference(), configurationSource);
                return true;
            }

            if (existing != null)
            {
                Definition.IgnoreInterface(clrType.GetGraphQLName(), ConfigurationSource.Convention);
                return false;
            }

            var interfaceRef = Schema.NamedTypeReference(clrType, TypeKind.Interface);
            if (interfaceRef != null)
            {
                Definition.AddInterface(interfaceRef, configurationSource);
                return true;
            }

            return false;
        }


        [NotNull]
        public InternalObjectTypeBuilder Interface([NotNull] string interfaceType,
            ConfigurationSource configurationSource)
        {
            var interfaceRef = Schema.GetOrAddTypeReference(interfaceType, Definition);
            if (interfaceRef != null)
            {
                Definition.AddInterface(interfaceRef, configurationSource);
            }

            return this;
        }


        public InternalObjectTypeBuilder IgnoreInterface(string interfaceName, ConfigurationSource configurationSource)
        {
            Definition.IgnoreInterface(interfaceName, configurationSource);
            return this;
        }

        public bool IsInterfaceIgnored([NotNull] string interfaceName, ConfigurationSource configurationSource)
        {
            if (configurationSource == ConfigurationSource.Explicit)
            {
                return false;
            }

            var ignoredMemberConfigurationSource = Definition.FindIgnoredInterfaceConfigurationSource(interfaceName);
            return ignoredMemberConfigurationSource.HasValue &&
                   ignoredMemberConfigurationSource.Overrides(configurationSource);
        }
    }
}