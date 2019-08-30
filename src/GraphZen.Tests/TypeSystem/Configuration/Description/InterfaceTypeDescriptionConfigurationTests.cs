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
    public class InterfaceTypeDescriptionConfigurationTests : DescriptionConfigurationTests
    {
        public interface IWithoutDescription
        {
        }

        [Description("set by data annotation")]
        public interface IWithDescription
        {
        }

        public override void CreateMemberWithoutDataAnnotation(ISchemaBuilder<GraphQLContext> schemaBuilder)
        {
            schemaBuilder.Interface<IWithoutDescription>();
        }

        public override void CreateMemberWithDataAnnotation(ISchemaBuilder<GraphQLContext> schemaBuilder)
        {
            schemaBuilder.Interface<IWithDescription>();
        }

        public override MemberDefinition GetMemberDefinitionWithoutDataAnnotation(SchemaDefinition schemaDef) =>
            schemaDef.GetInterface<IWithoutDescription>();

        public override MemberDefinition GetMemberDefinitionWithDataAnnotation(SchemaDefinition schemaDef) =>
            schemaDef.GetInterface<IWithDescription>();

        public override Member GetMemberWithoutDataAnnotation(Schema schema) =>
            schema.GetInterface<IWithoutDescription>();

        public override Member GetMemberWithDataAnnotation(Schema schema) => schema.GetInterface<IWithDescription>();

        public override void SetDescriptionOnMemberWithDataAnnotation(ISchemaBuilder<GraphQLContext> schemaBuilder,
            string description)
        {
            schemaBuilder.Interface<IWithDescription>().Description(description);
        }
    }
}