// Copyright (c) GraphZen LLC. All rights reserved.
// Licensed under the GraphZen Community License. See the LICENSE file in the project root for license information.

using System;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Reflection;
using GraphZen.Infrastructure;
using GraphZen.LanguageModel.Internal;
using JetBrains.Annotations;

namespace GraphZen.TypeSystem.Internal
{
    public class
        InternalObjectTypeBuilder : InternalFieldsBuilder<MutableObjectType, InternalObjectTypeBuilder>
    {
        public InternalObjectTypeBuilder(MutableObjectType @object) : base(@object)
        {
        }


        public void IsTypeOf(IsTypeOf<object, GraphQLContext> isTypeOfFn)
        {
            Definition.IsTypeOf = isTypeOfFn;
        }


        public bool ImplementsInterface(Type clrType, ConfigurationSource configurationSource)
        {
            if (clrType.IsIgnoredByDataAnnotation())
            {
                IgnoreInterface(clrType.GetGraphQLNameAnnotation(), ConfigurationSource.DataAnnotation);
            }

            if (IsInterfaceIgnored(clrType.GetGraphQLNameAnnotation(), configurationSource))
            {
                return false;
            }

            var existing = Schema.FindOutputType(clrType);

            if (existing is MutableInterfaceType interfaceDef)
            {
                Definition.AddInterface(interfaceDef, configurationSource);
                return true;
            }

            if (existing != null)
            {
                Definition.IgnoreInterface(clrType.GetGraphQLNameAnnotation(), ConfigurationSource.Convention);
                return false;
            }

            var interfaceRef = Schema.InternalBuilder.Interface(clrType, configurationSource)?.Definition;
            if (interfaceRef != null)
            {
                Definition.AddInterface(interfaceRef, configurationSource);
                return true;
            }

            return false;
        }

        public InternalObjectTypeBuilder ClrType(Type clrType, string name, ConfigurationSource configurationSource)
        {
            if (Definition.SetClrType(clrType, name, configurationSource))
            {
                ConfigureObjectFromClrType();
            }

            return this;
        }

        public InternalObjectTypeBuilder ClrType(Type clrType, bool inferName, ConfigurationSource configurationSource)
        {
            if (Definition.SetClrType(clrType, inferName, configurationSource))
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
                Schema.InternalBuilder.Union(clrType.BaseType, ConfigurationSource.Convention);
            }

            ConfigureOutputFields();

            if (clrType.TryGetDescriptionFromDataAnnotation(out var desc))
            {
                Definition.SetDescription(desc, ConfigurationSource.DataAnnotation);
            }

            var interfaces = clrType.GetInterfaces()
                .Where(_ => !_.IsGenericType)
                .OrderBy(_ => _.MetadataToken);

            foreach (var @interface in interfaces)
            {
                if (@interface.GetCustomAttribute<GraphQLUnionAttribute>() != null)
                {
                    Schema.InternalBuilder.Union(@interface, ConfigurationSource.DataAnnotation);
                }
                else
                {
                    Definition.InternalBuilder.ImplementsInterface(@interface, ConfigurationSource.Convention);
                }
            }

            return true;
        }


        public InternalObjectTypeBuilder ImplementsInterface(string interfaceType,
            ConfigurationSource configurationSource)
        {
            var interfaceRef = Schema.InternalBuilder.Interface(interfaceType, configurationSource)?.Definition;
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

        public bool UnignoreInterface(string name, ConfigurationSource configurationSource)
        {
            var ignoredConfigurationSource = Definition.FindIgnoredInterfaceConfigurationSource(name);
            if (!configurationSource.Overrides(ignoredConfigurationSource))
            {
                return false;
            }

            Definition.UnignoreInterface(name);
            return true;
        }

        public bool IsInterfaceIgnored(string interfaceName, ConfigurationSource configurationSource)
        {
            if (configurationSource == ConfigurationSource.Explicit)
            {
                return false;
            }

            var ignoredMemberConfigurationSource = Definition.FindIgnoredInterfaceConfigurationSource(interfaceName);
            return ignoredMemberConfigurationSource.HasValue &&
                   ignoredMemberConfigurationSource.Overrides(configurationSource);
        }

        public InternalObjectTypeBuilder Name(string name, ConfigurationSource configurationSource)
        {
            Definition.SetName(name, configurationSource);
            return this;
        }

        public InternalObjectTypeBuilder Description(string description, ConfigurationSource configurationSource)
        {
            Definition.SetDescription(description, configurationSource);
            return this;
        }

        public InternalObjectTypeBuilder RemoveClrType(ConfigurationSource configurationSource)
        {
            Definition.RemoveClrType(configurationSource);
            return this;
        }
    }
}