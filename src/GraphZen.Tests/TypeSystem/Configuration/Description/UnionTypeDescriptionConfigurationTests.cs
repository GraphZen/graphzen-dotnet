// Copyright (c) GraphZen LLC. All rights reserved.
// Licensed under the GraphZen Community License. See the LICENSE file in the project root for license information.

using System.ComponentModel;
using System.Diagnostics.CodeAnalysis;
using GraphZen.Infrastructure;
using JetBrains.Annotations;
#nullable disable


namespace GraphZen.TypeSystem
{
    [UsedImplicitly]
    [NoReorder]
    public class UnionTypeDescriptionConfigurationTests : DescriptionConfigurationTests
    {
        public class UnionWithoutDescription
        {
        }

        [Description("set by data annotation")]
        public class UnionWithDescription
        {
        }


        public override void CreateMemberWithoutDataAnnotation(ISchemaBuilder<GraphQLContext> schemaBuilder)
        {
            schemaBuilder.Union<UnionWithoutDescription>();
        }

        public override void CreateMemberWithDataAnnotation(ISchemaBuilder<GraphQLContext> schemaBuilder)
        {
            schemaBuilder.Union<UnionWithDescription>();
        }

        public override MemberDefinition GetMemberDefinitionWithoutDataAnnotation(SchemaDefinition schemaDef)
        {
            return schemaDef.GetUnion<UnionWithoutDescription>();
        }

        public override MemberDefinition GetMemberDefinitionWithDataAnnotation(SchemaDefinition schemaDef)
        {
            return schemaDef.GetUnion<UnionWithDescription>();
        }

        public override Member GetMemberWithoutDataAnnotation(Schema schema)
        {
            return schema.GetUnion<UnionWithoutDescription>();
        }

        public override Member GetMemberWithDataAnnotation(Schema schema)
        {
            return schema.GetUnion<UnionWithDescription>();
        }

        public override void SetDescriptionOnMemberWithDataAnnotation(ISchemaBuilder<GraphQLContext> schemaBuilder,
            string description)
        {
            schemaBuilder.Union<UnionWithDescription>().Description(description);
        }
    }
}