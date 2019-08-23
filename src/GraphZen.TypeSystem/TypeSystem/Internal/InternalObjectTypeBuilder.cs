// Copyright (c) GraphZen LLC. All rights reserved.
// Licensed under the GraphZen Community License. See the LICENSE file in the project root for license information.

using System;
using System.Linq;
using System.Reflection;
using GraphZen.Infrastructure;
using GraphZen.LanguageModel.Internal;

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
        public InternalObjectTypeBuilder ClrType([NotNull] Type clrType, ConfigurationSource configurationSource)
        {
            if (Definition.SetClrType(clrType, configurationSource))
            {
                ConfigureObjectFromClrType();
            }

            return this;
        }

        public bool ConfigureObjectFromClrType()
        {
            var clrType = Definition.ClrType;

            if (clrType == null)
            {
                return false;
            }

            if (clrType.BaseType != null && clrType.BaseType.IsAbstract)
            {
                SchemaBuilder.Union(clrType.BaseType, ConfigurationSource.Convention);
            }

            ConfigureOutputFields();

            if (clrType.TryGetDescriptionFromDataAnnotation(out var desc))
            {
                Definition.SetDescription(desc, ConfigurationSource.DataAnnotation);
            }

            var interfaces = clrType.GetInterfaces()
                // ReSharper disable once PossibleNullReferenceException
                .Where(_ => !_.IsGenericType)
                .OrderBy(_ => _.MetadataToken);
            foreach (var @interface in interfaces)
            {
                if (@interface.GetCustomAttribute<GraphQLUnionAttribute>() != null)
                {
                    Schema.Builder.Union(@interface, ConfigurationSource.DataAnnotation);
                }
                else
                {
                    Definition.Builder.Interface(@interface, ConfigurationSource.Convention);
                }
            }

            return true;
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