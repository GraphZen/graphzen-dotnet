// Copyright (c) GraphZen LLC. All rights reserved.
// Licensed under the GraphZen Community License. See the LICENSE file in the project root for license information.

using System.Diagnostics.CodeAnalysis;
using GraphZen.Infrastructure;
using GraphZen.TypeSystem.Taxonomy;
using JetBrains.Annotations;
#nullable disable


namespace GraphZen.TypeSystem
{
    [NoReorder]
    [UsedImplicitly]
    public class UnionTypeNameConfigurationTests : NameConfigurationByExplicitConfigurationTests
    {
        private class UnionNamedByConvention
        {
        }

        [GraphQLName("CustomName")]
        private class UnionNamedByDataAnnotation
        {
        }


        public override string ConventionalName { get; } = nameof(UnionNamedByConvention);

        public override void CreateMemberNamedByConvention(SchemaBuilder schemaBuilder)
        {
            schemaBuilder.Union<UnionNamedByConvention>();
        }

        public override void CreateMemberWithCustomNameAttribute(SchemaBuilder schemaBuilder)
        {
            schemaBuilder.Union<UnionNamedByDataAnnotation>();
        }

        public override void SetNameOnMemberNamedByConvention(SchemaBuilder schemaBuilder, string name)
        {
            schemaBuilder.Union<UnionNamedByConvention>().Name(name);
        }

        public override void SetNameOnMemberNamedByDataAnnotation(SchemaBuilder schemaBuilder, string name)
        {
            schemaBuilder.Union<UnionNamedByDataAnnotation>().Name(name);
        }

        public override IMutableNamed GetMemberDefinitionNamedByConvention(SchemaBuilder schemaBuilder)
        {
            return schemaBuilder.GetDefinition().GetUnion<UnionNamedByConvention>();
        }

        public override IMutableNamed GetMemberDefinitionWithCustomNameDataAnnotation(SchemaBuilder schemaBuilder)
        {
            return schemaBuilder.GetDefinition().GetUnion<UnionNamedByDataAnnotation>();
        }

        public override INamed GetMemberNamedByConvention(Schema schema)
        {
            return schema.GetUnion<UnionNamedByConvention>();
        }

        public override INamed GetMemberNamedByDataAnnotation(Schema schema)
        {
            return schema.GetUnion<UnionNamedByDataAnnotation>();
        }
    }
}