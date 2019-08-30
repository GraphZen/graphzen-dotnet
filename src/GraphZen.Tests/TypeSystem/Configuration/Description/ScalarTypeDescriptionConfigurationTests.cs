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
    public class ScalarTypeDescriptionConfigurationTests : DescriptionConfigurationTests
    {
        public class ScalarWithoutDescription
        {
        }

        [Description("set by data annotation")]
        public class ScalarWithDescription
        {
        }


        public override void CreateMemberWithoutDataAnnotation(ISchemaBuilder<GraphQLContext> schemaBuilder)
        {
            schemaBuilder.Scalar<ScalarWithoutDescription>();
        }

        public override void CreateMemberWithDataAnnotation(ISchemaBuilder<GraphQLContext> schemaBuilder)
        {
            schemaBuilder.Scalar<ScalarWithDescription>();
        }

        public override MemberDefinition GetMemberDefinitionWithoutDataAnnotation(SchemaDefinition schemaDef) =>
            schemaDef.GetScalar<ScalarWithoutDescription>();

        public override MemberDefinition GetMemberDefinitionWithDataAnnotation(SchemaDefinition schemaDef) =>
            schemaDef.GetScalar<ScalarWithDescription>();

        public override Member GetMemberWithoutDataAnnotation(Schema schema) =>
            schema.GetScalar<ScalarWithoutDescription>();

        public override Member GetMemberWithDataAnnotation(Schema schema) => schema.GetScalar<ScalarWithDescription>();

        public override void SetDescriptionOnMemberWithDataAnnotation(ISchemaBuilder<GraphQLContext> schemaBuilder,
            string description)
        {
            schemaBuilder.Scalar<ScalarWithDescription>().Description(description);
        }
    }
}