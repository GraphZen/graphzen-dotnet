// Copyright (c) GraphZen LLC. All rights reserved.
// Licensed under the GraphZen Community License. See the LICENSE file in the project root for license information.

using System.ComponentModel;
using GraphZen.Infrastructure;
using GraphZen.TypeSystem.Internal;

namespace GraphZen.TypeSystem
{
    [UsedImplicitly]
    [NoReorder]
    public class InputObjectFieldViaPropertyDescriptionConfigurationTests : DescriptionConfigurationTests
    {
        public class InputObjectWithPropertyFields
        {
            public string PropertyWithoutDescription { get; set; }

            [Description("set by data annotation")]
            public string PropertyWithDescription { get; set; }
        }

        public override void CreateMemberWithoutDataAnnotation(ISchemaBuilder<GraphQLContext> schemaBuilder)
        {
            schemaBuilder.InputObject<InputObjectWithPropertyFields>();
        }

        public override void CreateMemberWithDataAnnotation(ISchemaBuilder<GraphQLContext> schemaBuilder)
        {
            schemaBuilder.InputObject<InputObjectWithPropertyFields>();
        }

        public override MemberDefinition GetMemberDefinitionWithoutDataAnnotation(SchemaDefinition schemaDef) =>
            schemaDef.GetInputObject<InputObjectWithPropertyFields>()
                .FindField(nameof(InputObjectWithPropertyFields.PropertyWithoutDescription).FirstCharToLower());

        public override MemberDefinition GetMemberDefinitionWithDataAnnotation(SchemaDefinition schemaDef) =>
            schemaDef.GetInputObject<InputObjectWithPropertyFields>()
                .FindField(nameof(InputObjectWithPropertyFields.PropertyWithDescription).FirstCharToLower());

        public override Member GetMemberWithoutDataAnnotation(Schema schema) =>
            schema.GetInputObject<InputObjectWithPropertyFields>()
                .FindField(nameof(InputObjectWithPropertyFields.PropertyWithoutDescription).FirstCharToLower());

        public override Member GetMemberWithDataAnnotation(Schema schema) =>
            schema.GetInputObject<InputObjectWithPropertyFields>()
                .FindField(nameof(InputObjectWithPropertyFields.PropertyWithDescription).FirstCharToLower());

        public override void SetDescriptionOnMemberWithDataAnnotation(ISchemaBuilder<GraphQLContext> schemaBuilder,
            string description) => schemaBuilder.InputObject<InputObjectWithPropertyFields>()
            .Field(_ => _.PropertyWithDescription, _ => _.Description(description));
    }
}