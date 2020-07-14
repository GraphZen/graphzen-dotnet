// Copyright (c) GraphZen LLC. All rights reserved.
// Licensed under the GraphZen Community License. See the LICENSE file in the project root for license information.

using System;
using System.Diagnostics.CodeAnalysis;
using GraphZen.Infrastructure;
using JetBrains.Annotations;

namespace GraphZen.TypeSystem.Internal
{
    public class
        InternalInterfaceTypeBuilder : InternalFieldsBuilder<MutableInterfaceType,
            InternalInterfaceTypeBuilder>
    {
        public InternalInterfaceTypeBuilder(MutableInterfaceType @interface) : base(@interface)
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

        public InternalInterfaceTypeBuilder ClrType(Type clrType, string name, ConfigurationSource configurationSource)
        {
            if (Definition.SetClrType(clrType, name, configurationSource))
            {
                ConfigureInterfaceFromClrType();
            }

            return this;
        }

        public InternalInterfaceTypeBuilder ClrType(Type clrType, bool inferName,
            ConfigurationSource configurationSource)
        {
            if (Definition.SetClrType(clrType, inferName, configurationSource))
            {
                ConfigureInterfaceFromClrType();
            }

            return this;
        }

        public InternalInterfaceTypeBuilder RemoveClrType(ConfigurationSource configurationSource)
        {
            Definition.RemoveClrType(configurationSource);

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

        public InternalInterfaceTypeBuilder Description(string description, ConfigurationSource configurationSource)
        {
            Definition.SetDescription(description, configurationSource);
            return this;
        }
    }
}