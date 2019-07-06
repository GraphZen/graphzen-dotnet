// Copyright (c) GraphZen LLC. All rights reserved.
// Licensed under the GraphZen Community License. See the LICENSE file in the project root for license information.

using System.ComponentModel;
using GraphZen.Infrastructure;
using GraphZen.TypeSystem.Internal;

namespace GraphZen.TypeSystem
{
    [UsedImplicitly]
    [NoReorder]
    public class EnumValueDescriptionConfigurationTests : DescriptionConfigurationTests
    {
        public enum EnumWithDescribedMembers
        {
            WithoutDescription,

            [Description("set by data annotation")]
            WithDescription
        }

        public override void CreateMemberWithoutDataAnnotation(ISchemaBuilder<GraphQLContext> schemaBuilder)
        {
            schemaBuilder.Enum<EnumWithDescribedMembers>();
        }

        public override void CreateMemberWithDataAnnotation(ISchemaBuilder<GraphQLContext> schemaBuilder)
        {
            schemaBuilder.Enum<EnumWithDescribedMembers>();
        }

        public override MemberDefinition GetMemberDefinitionWithoutDataAnnotation(SchemaDefinition schemaDef) =>
            schemaDef.GetEnum<EnumWithDescribedMembers>()
                .GetValue(nameof(EnumWithDescribedMembers.WithoutDescription));

        public override MemberDefinition GetMemberDefinitionWithDataAnnotation(SchemaDefinition schemaDef) =>
            schemaDef.GetEnum<EnumWithDescribedMembers>()
                .GetValue(nameof(EnumWithDescribedMembers.WithDescription));

        public override Member GetMemberWithoutDataAnnotation(Schema schema) => schema
            .GetEnum<EnumWithDescribedMembers>().GetValue(nameof(EnumWithDescribedMembers.WithoutDescription));

        public override Member GetMemberWithDataAnnotation(Schema schema) =>
            schema.GetEnum<EnumWithDescribedMembers>().GetValue(nameof(EnumWithDescribedMembers.WithDescription));


        public override void SetDescriptionOnMemberWithDataAnnotation(ISchemaBuilder<GraphQLContext> schemaBuilder,
            string description)
        {
            schemaBuilder.Enum<EnumWithDescribedMembers>()
                .Value(EnumWithDescribedMembers.WithDescription, v => v.Description(description));
        }
    }
}