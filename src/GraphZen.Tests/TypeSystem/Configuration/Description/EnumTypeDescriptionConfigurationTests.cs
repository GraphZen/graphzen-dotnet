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
    public class EnumTypeDescriptionConfigurationTests : DescriptionConfigurationTests
    {
        [Description("set by data annotation")]
        public enum EnumWithDescriptionDataAnnotation
        {
        }

        public enum EnumWithoutDescriptionDataAnnotation
        {
        }

        public override void CreateMemberWithoutDataAnnotation(ISchemaBuilder<GraphQLContext> schemaBuilder)
        {
            schemaBuilder.Enum<EnumWithoutDescriptionDataAnnotation>();
        }

        public override void CreateMemberWithDataAnnotation(ISchemaBuilder<GraphQLContext> schemaBuilder)
        {
            schemaBuilder.Enum<EnumWithDescriptionDataAnnotation>();
        }

        public override MemberDefinition GetMemberDefinitionWithoutDataAnnotation(SchemaDefinition schemaDef)
        {
            return schemaDef.GetEnum<EnumWithoutDescriptionDataAnnotation>();
        }

        public override MemberDefinition GetMemberDefinitionWithDataAnnotation(SchemaDefinition schemaDef)
        {
            return schemaDef.GetEnum<EnumWithDescriptionDataAnnotation>();
        }

        public override Member GetMemberWithoutDataAnnotation(Schema schema)
        {
            return schema.GetEnum<EnumWithoutDescriptionDataAnnotation>();
        }

        public override Member GetMemberWithDataAnnotation(Schema schema)
        {
            return schema.GetEnum<EnumWithDescriptionDataAnnotation>();
        }

        public override void SetDescriptionOnMemberWithDataAnnotation(ISchemaBuilder<GraphQLContext> schemaBuilder,
            string description)
        {
            schemaBuilder.Enum<EnumWithDescriptionDataAnnotation>().Description(description);
        }
    }
}