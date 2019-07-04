// Copyright (c) GraphZen LLC. All rights reserved.
// Licensed under the GraphZen Community License. See the LICENSE file in the project root for license information.

using System;
using GraphZen.Infrastructure;
using JetBrains.Annotations;

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
            Definition.AddType(Schema.GetOrAddTypeReference(objectType, Definition));
            return this;
        }

        [NotNull]
        public InternalUnionTypeBuilder IncludesType([NotNull] Type clrType,
            ConfigurationSource configurationSource)
        {
            var objectType = Schema.Builder.Object(clrType, ConfigurationSource.Convention);
            if (objectType != null)
            {
                Definition.AddType(Schema.NamedTypeReference(clrType, TypeKind.Object));
            }

            return this;
        }
    }
}