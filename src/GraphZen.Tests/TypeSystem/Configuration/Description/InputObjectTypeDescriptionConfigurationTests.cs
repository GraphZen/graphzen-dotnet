#nullable disable
// Copyright (c) GraphZen LLC. All rights reserved.
// Licensed under the GraphZen Community License. See the LICENSE file in the project root for license information.

using System.ComponentModel;
using GraphZen.Infrastructure;

namespace GraphZen.TypeSystem
{
    [UsedImplicitly]
    [NoReorder]
    public class InputObjectTypeDescriptionConfigurationTests : DescriptionConfigurationTests
    {
        public class InputObjectWithoutDescription
        {
        }

        [Description("set by data annotation")]
        public class InputObjectWithDescription
        {
        }


        public override void CreateMemberWithoutDataAnnotation(ISchemaBuilder<GraphQLContext> schemaBuilder)
        {
            schemaBuilder.InputObject<InputObjectWithoutDescription>();
        }

        public override void CreateMemberWithDataAnnotation(ISchemaBuilder<GraphQLContext> schemaBuilder)
        {
            schemaBuilder.InputObject<InputObjectWithDescription>();
        }

        public override MemberDefinition GetMemberDefinitionWithoutDataAnnotation(SchemaDefinition schemaDef) =>
            schemaDef.GetInputObject<InputObjectWithoutDescription>();

        public override MemberDefinition GetMemberDefinitionWithDataAnnotation(SchemaDefinition schemaDef) =>
            schemaDef.GetInputObject<InputObjectWithDescription>();

        public override Member GetMemberWithoutDataAnnotation(Schema schema) =>
            schema.GetInputObject<InputObjectWithoutDescription>();

        public override Member GetMemberWithDataAnnotation(Schema schema) =>
            schema.GetInputObject<InputObjectWithDescription>();

        public override void SetDescriptionOnMemberWithDataAnnotation(ISchemaBuilder<GraphQLContext> schemaBuilder,
            string description) => schemaBuilder.InputObject<InputObjectWithDescription>().Description(description);
    }
}