// Copyright (c) GraphZen LLC. All rights reserved.
// Licensed under the GraphZen Community License. See the LICENSE file in the project root for license information.

using System;
using GraphZen.Infrastructure;

namespace GraphZen.TypeSystem.Internal
{
    public class InternalUnionTypeBuilder : AnnotatableMemberDefinitionBuilder<UnionTypeDefinition>
    {
        public InternalUnionTypeBuilder([NotNull] UnionTypeDefinition definition,
            [NotNull] InternalSchemaBuilder schemaBuilder) : base(definition, schemaBuilder)
        {
        }

        [NotNull]
        public InternalUnionTypeBuilder ResolveType([NotNull] TypeResolver<object, GraphQLContext> resolveType)
        {
            Definition.ResolveType = resolveType;
            return this;
        }

        [NotNull]
        public InternalUnionTypeBuilder IncludesType([NotNull] string objectType,
            ConfigurationSource configurationSource)
        {
            var obj = Schema.Builder.Object(objectType, configurationSource)?.Definition;
            if (obj != null)
            {
                Definition.AddType(obj);
            }
            return this;
        }

        [NotNull]
        public InternalUnionTypeBuilder IncludesType([NotNull] Type clrType,
            ConfigurationSource configurationSource)
        {
            var objectType = Schema.Builder.Object(clrType, configurationSource)?.Definition;
            if (objectType != null)
            {
                Definition.AddType(objectType);
            }

            return this;
        }
    }
}