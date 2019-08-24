// Copyright (c) GraphZen LLC. All rights reserved.
// Licensed under the GraphZen Community License. See the LICENSE file in the project root for license information.
using JetBrains.Annotations;
#nullable disable


using System.ComponentModel;
using GraphZen.Infrastructure;
using GraphZen.TypeSystem.Internal;

namespace GraphZen.TypeSystem
{
    [UsedImplicitly]
    [NoReorder]
    public class ObjectFieldViaPropertyDescriptionConfigurationTests : DescriptionConfigurationTests
    {
        public class ObjectWithPropertyFields
        {
            public string PropertyWithoutDescription { get; set; }

            [Description("set by data annotation")]
            public string PropertyWithDescription { get; set; }
        }

        public override void CreateMemberWithoutDataAnnotation(ISchemaBuilder<GraphQLContext> schemaBuilder)
        {
            schemaBuilder.Object<ObjectWithPropertyFields>();
        }

        public override void CreateMemberWithDataAnnotation(ISchemaBuilder<GraphQLContext> schemaBuilder)
        {
            schemaBuilder.Object<ObjectWithPropertyFields>();
        }

        public override MemberDefinition GetMemberDefinitionWithoutDataAnnotation(SchemaDefinition schemaDef) =>
            MutableFieldsContainerDefinitionFieldAccessorExtensions.FindField(
                schemaDef.GetObject<ObjectWithPropertyFields>(),
                nameof(ObjectWithPropertyFields.PropertyWithoutDescription).FirstCharToLower());

        public override MemberDefinition GetMemberDefinitionWithDataAnnotation(SchemaDefinition schemaDef) =>
            MutableFieldsContainerDefinitionFieldAccessorExtensions.FindField(
                schemaDef.GetObject<ObjectWithPropertyFields>(),
                nameof(ObjectWithPropertyFields.PropertyWithDescription).FirstCharToLower());

        public override Member GetMemberWithoutDataAnnotation(Schema schema) =>
            schema.GetObject<ObjectWithPropertyFields>()
                .FindField(nameof(ObjectWithPropertyFields.PropertyWithoutDescription).FirstCharToLower());

        public override Member GetMemberWithDataAnnotation(Schema schema) =>
            schema.GetObject<ObjectWithPropertyFields>()
                .FindField(nameof(ObjectWithPropertyFields.PropertyWithDescription).FirstCharToLower());

        public override void SetDescriptionOnMemberWithDataAnnotation(ISchemaBuilder<GraphQLContext> schemaBuilder,
            string description) => schemaBuilder.Object<ObjectWithPropertyFields>()
            .Field(_ => _.PropertyWithDescription, _ => _.Description(description));
    }
}