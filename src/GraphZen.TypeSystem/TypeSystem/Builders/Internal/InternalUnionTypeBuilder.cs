// Copyright (c) GraphZen LLC. All rights reserved.
// Licensed under the GraphZen Community License. See the LICENSE file in the project root for license information.

using System;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using GraphZen.Infrastructure;
using JetBrains.Annotations;

namespace GraphZen.TypeSystem.Internal
{
    public class InternalUnionTypeBuilder : AnnotatableMemberDefinitionBuilder<MutableUnionType>
    {
        public InternalUnionTypeBuilder(MutableUnionType definition
        ) : base(definition)
        {
        }


        public InternalUnionTypeBuilder ResolveType(TypeResolver<object, GraphQLContext> resolveType)
        {
            Definition.ResolveType = resolveType;
            return this;
        }


        public InternalUnionTypeBuilder IncludesType(string objectType,
            ConfigurationSource configurationSource)
        {
            var obj = Schema.InternalBuilder.Object(objectType, configurationSource)?.Definition;
            if (obj != null)
            {
                Definition.AddType(obj);
            }

            return this;
        }


        public InternalUnionTypeBuilder IncludesType(Type clrType,
            ConfigurationSource configurationSource)
        {
            var objectType = Schema.InternalBuilder.Object(clrType, configurationSource)?.Definition;
            if (objectType != null)
            {
                Definition.AddType(objectType);
            }

            return this;
        }

        public InternalUnionTypeBuilder ClrType(Type clrType, string name, ConfigurationSource configurationSource)
        {
            if (Definition.SetClrType(clrType, name, configurationSource))
            {
                ConfigureFromClrType();
            }

            return this;
        }

        public InternalUnionTypeBuilder ClrType(Type clrType, bool inferName, ConfigurationSource configurationSource)
        {
            if (Definition.SetClrType(clrType, inferName, configurationSource))
            {
                ConfigureFromClrType();
            }

            return this;
        }

        public bool ConfigureFromClrType()
        {
            var clrType = Definition.ClrType;
            if (clrType == null)
            {
                return false;
            }

            if (clrType.TryGetDescriptionFromDataAnnotation(out var description))
            {
                Description(description, ConfigurationSource.DataAnnotation);
            }

            var implementingTypes = clrType.GetImplementingTypes().Where(_ => !_.IsAbstract);
            foreach (var implementingType in implementingTypes)
            {
                var memberType = Schema.InternalBuilder.Object(implementingType, ConfigurationSource.Convention)
                    ?.Definition;
                if (memberType != null)
                {
                    Definition.AddType(memberType);
                }
            }

            return true;
        }

        public InternalUnionTypeBuilder Description(string description, ConfigurationSource configurationSource)
        {
            Definition.SetDescription(description, configurationSource);
            return this;
        }

        public InternalUnionTypeBuilder SetName(string name, ConfigurationSource configurationSource)
        {
            Definition.SetName(name, configurationSource);
            return this;
        }

        public InternalUnionTypeBuilder RemoveClrType(ConfigurationSource configurationSource)
        {
            Definition.RemoveClrType(configurationSource);
            return this;
        }
    }
}