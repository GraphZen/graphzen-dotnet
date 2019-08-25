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
    public class ObjectTypeNameConfigurationTests : NameConfigurationByExplicitConfigurationTests
    {
        private class ObjectNamedByConvention
        {
        }

        [GraphQLName("CustomName")]
        private class ObjectNamedByDataAnnotation
        {
        }


        public override string ConventionalName { get; } = nameof(ObjectNamedByConvention);

        public override void CreateMemberNamedByConvention(SchemaBuilder schemaBuilder)
        {
            schemaBuilder.Object<ObjectNamedByConvention>();
        }

        public override void CreateMemberWithCustomNameAttribute(SchemaBuilder schemaBuilder)
        {
            schemaBuilder.Object<ObjectNamedByDataAnnotation>();
        }

        public override void SetNameOnMemberNamedByConvention(SchemaBuilder schemaBuilder, string name)
        {
            schemaBuilder.Object<ObjectNamedByConvention>().Name(name);
        }

        public override void SetNameOnMemberNamedByDataAnnotation(SchemaBuilder schemaBuilder, string name)
        {
            schemaBuilder.Object<ObjectNamedByDataAnnotation>().Name(name);
        }

        public override IMutableNamed GetMemberDefinitionNamedByConvention(SchemaBuilder schemaBuilder)
        {
            return schemaBuilder.GetDefinition().GetObject<ObjectNamedByConvention>();
        }

        public override IMutableNamed GetMemberDefinitionWithCustomNameDataAnnotation(SchemaBuilder schemaBuilder)
        {
            return schemaBuilder.GetDefinition().GetObject<ObjectNamedByDataAnnotation>();
        }

        public override INamed GetMemberNamedByConvention(Schema schema)
        {
            return schema.GetObject<ObjectNamedByConvention>();
        }

        public override INamed GetMemberNamedByDataAnnotation(Schema schema)
        {
            return schema.GetObject<ObjectNamedByDataAnnotation>();
        }
    }
}