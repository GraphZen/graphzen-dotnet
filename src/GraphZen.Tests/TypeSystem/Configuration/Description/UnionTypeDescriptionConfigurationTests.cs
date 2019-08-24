#nullable disable
// Copyright (c) GraphZen LLC. All rights reserved.
// Licensed under the GraphZen Community License. See the LICENSE file in the project root for license information.

using System.ComponentModel;
using GraphZen.Infrastructure;

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

        public override MemberDefinition GetMemberDefinitionWithoutDataAnnotation(SchemaDefinition schemaDef) =>
            schemaDef.GetUnion<UnionWithoutDescription>();

        public override MemberDefinition GetMemberDefinitionWithDataAnnotation(SchemaDefinition schemaDef) =>
            schemaDef.GetUnion<UnionWithDescription>();

        public override Member GetMemberWithoutDataAnnotation(Schema schema) =>
            schema.GetUnion<UnionWithoutDescription>();

        public override Member GetMemberWithDataAnnotation(Schema schema) =>
            schema.GetUnion<UnionWithDescription>();

        public override void SetDescriptionOnMemberWithDataAnnotation(ISchemaBuilder<GraphQLContext> schemaBuilder,
            string description) => schemaBuilder.Union<UnionWithDescription>().Description(description);
    }
}