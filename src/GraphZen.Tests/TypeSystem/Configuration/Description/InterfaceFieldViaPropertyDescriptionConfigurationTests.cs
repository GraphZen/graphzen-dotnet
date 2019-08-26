// Copyright (c) GraphZen LLC. All rights reserved.
// Licensed under the GraphZen Community License. See the LICENSE file in the project root for license information.

using System.ComponentModel;
using System.Diagnostics.CodeAnalysis;
using GraphZen.Infrastructure;
using GraphZen.TypeSystem.Internal;
using JetBrains.Annotations;

#nullable disable


namespace GraphZen.TypeSystem
{
    [UsedImplicitly]
    [NoReorder]
    public class InterfaceFieldViaPropertyDescriptionConfigurationTests : DescriptionConfigurationTests
    {
        public interface IInterfaceWithPropertyFields
        {
            string PropertyWithoutDescription { get; set; }

            [Description("set by data annotation")]
            string PropertyWithDescription { get; set; }
        }

        public override void CreateMemberWithoutDataAnnotation(ISchemaBuilder<GraphQLContext> schemaBuilder)
        {
            schemaBuilder.Interface<IInterfaceWithPropertyFields>();
        }

        public override void CreateMemberWithDataAnnotation(ISchemaBuilder<GraphQLContext> schemaBuilder)
        {
            schemaBuilder.Interface<IInterfaceWithPropertyFields>();
        }

        public override MemberDefinition GetMemberDefinitionWithoutDataAnnotation(SchemaDefinition schemaDef)
        {
            return MutableFieldsContainerDefinitionFieldAccessorExtensions.FindField(
                schemaDef.GetInterface<IInterfaceWithPropertyFields>(),
                nameof(IInterfaceWithPropertyFields.PropertyWithoutDescription).FirstCharToLower());
        }

        public override MemberDefinition GetMemberDefinitionWithDataAnnotation(SchemaDefinition schemaDef)
        {
            return MutableFieldsContainerDefinitionFieldAccessorExtensions.FindField(
                schemaDef.GetInterface<IInterfaceWithPropertyFields>(),
                nameof(IInterfaceWithPropertyFields.PropertyWithDescription).FirstCharToLower());
        }

        public override Member GetMemberWithoutDataAnnotation(Schema schema)
        {
            return schema.GetInterface<IInterfaceWithPropertyFields>()
                .FindField(nameof(IInterfaceWithPropertyFields.PropertyWithoutDescription).FirstCharToLower());
        }

        public override Member GetMemberWithDataAnnotation(Schema schema)
        {
            return schema.GetInterface<IInterfaceWithPropertyFields>()
                .FindField(nameof(IInterfaceWithPropertyFields.PropertyWithDescription).FirstCharToLower());
        }

        public override void SetDescriptionOnMemberWithDataAnnotation(ISchemaBuilder<GraphQLContext> schemaBuilder,
            string description)
        {
            schemaBuilder.Interface<IInterfaceWithPropertyFields>()
                .Field(_ => _.PropertyWithDescription, _ => _.Description(description));
        }
    }
}