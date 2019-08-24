#nullable disable
// Copyright (c) GraphZen LLC. All rights reserved.
// Licensed under the GraphZen Community License. See the LICENSE file in the project root for license information.

using System.ComponentModel;
using GraphZen.Infrastructure;
using GraphZen.TypeSystem.Internal;

namespace GraphZen.TypeSystem
{
    [UsedImplicitly]
    [NoReorder]
    public class ObjectFieldViaMethodDescriptionConfigurationTests : DescriptionConfigurationTests
    {
        public class ObjectWithMethodFields
        {
            public string MethodWithoutDescription() => "";

            [Description("set by data annotation")]
            public string MethodWithDescription() => "";
        }

        public override void CreateMemberWithoutDataAnnotation(ISchemaBuilder<GraphQLContext> schemaBuilder)
        {
            schemaBuilder.Object<ObjectWithMethodFields>();
        }

        public override void CreateMemberWithDataAnnotation(ISchemaBuilder<GraphQLContext> schemaBuilder)
        {
            schemaBuilder.Object<ObjectWithMethodFields>();
        }

        public override MemberDefinition GetMemberDefinitionWithoutDataAnnotation(SchemaDefinition schemaDef) =>
            MutableFieldsContainerDefinitionFieldAccessorExtensions.FindField(
                schemaDef.GetObject<ObjectWithMethodFields>(),
                nameof(ObjectWithMethodFields.MethodWithoutDescription).FirstCharToLower());

        public override MemberDefinition GetMemberDefinitionWithDataAnnotation(SchemaDefinition schemaDef) =>
            MutableFieldsContainerDefinitionFieldAccessorExtensions.FindField(
                schemaDef.GetObject<ObjectWithMethodFields>(),
                nameof(ObjectWithMethodFields.MethodWithDescription).FirstCharToLower());

        public override Member GetMemberWithoutDataAnnotation(Schema schema) =>
            schema.GetObject<ObjectWithMethodFields>()
                .FindField(nameof(ObjectWithMethodFields.MethodWithoutDescription).FirstCharToLower());

        public override Member GetMemberWithDataAnnotation(Schema schema) =>
            schema.GetObject<ObjectWithMethodFields>()
                .FindField(nameof(ObjectWithMethodFields.MethodWithDescription).FirstCharToLower());

        public override void SetDescriptionOnMemberWithDataAnnotation(ISchemaBuilder<GraphQLContext> schemaBuilder,
            string description) => schemaBuilder.Object<ObjectWithMethodFields>()
            .Field<string>(nameof(ObjectWithMethodFields.MethodWithDescription).FirstCharToLower(),
                _ => _.Description(description));
    }
}