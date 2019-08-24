#nullable disable
// Copyright (c) GraphZen LLC. All rights reserved.
// Licensed under the GraphZen Community License. See the LICENSE file in the project root for license information.

using System.ComponentModel;
using GraphZen.Infrastructure;

namespace GraphZen.TypeSystem
{
    [UsedImplicitly]
    [NoReorder]
    public class ObjectTypeDescriptionConfigurationTests : DescriptionConfigurationTests
    {
        public class ObjectWithoutDescription
        {
        }

        [Description("set by data annotation")]
        public class ObjectWithDescription
        {
        }

        public override void CreateMemberWithoutDataAnnotation(ISchemaBuilder<GraphQLContext> schemaBuilder)
        {
            schemaBuilder.Object<ObjectWithoutDescription>();
        }

        public override void CreateMemberWithDataAnnotation(ISchemaBuilder<GraphQLContext> schemaBuilder)
        {
            schemaBuilder.Object<ObjectWithDescription>();
        }

        public override MemberDefinition GetMemberDefinitionWithoutDataAnnotation(SchemaDefinition schemaDef) =>
            schemaDef.GetObject<ObjectWithoutDescription>();

        public override MemberDefinition GetMemberDefinitionWithDataAnnotation(SchemaDefinition schemaDef) =>
            schemaDef.GetObject<ObjectWithDescription>();

        public override Member GetMemberWithoutDataAnnotation(Schema schema) =>
            schema.GetObject<ObjectWithoutDescription>();

        public override Member GetMemberWithDataAnnotation(Schema schema) =>
            schema.GetObject<ObjectWithDescription>();

        public override void SetDescriptionOnMemberWithDataAnnotation(ISchemaBuilder<GraphQLContext> schemaBuilder,
            string description) => schemaBuilder.Object<ObjectWithDescription>().Description(description);
    }
}