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
    public class InterfaceFieldViaMethodDescriptionConfigurationTests : DescriptionConfigurationTests
    {
        public interface IInterfaceWithMethodFields
        {
            string MethodWithoutDescription();

            [Description("set by data annotation")]
            string MethodWithDescription();
        }

        public override void CreateMemberWithoutDataAnnotation(ISchemaBuilder<GraphQLContext> schemaBuilder)
        {
            schemaBuilder.Interface<IInterfaceWithMethodFields>();
        }

        public override void CreateMemberWithDataAnnotation(ISchemaBuilder<GraphQLContext> schemaBuilder)
        {
            schemaBuilder.Interface<IInterfaceWithMethodFields>();
        }

        public override MemberDefinition GetMemberDefinitionWithoutDataAnnotation(SchemaDefinition schemaDef)
        {
            return MutableFieldsContainerDefinitionFieldAccessorExtensions.FindField(
                schemaDef.GetInterface<IInterfaceWithMethodFields>(),
                nameof(IInterfaceWithMethodFields.MethodWithoutDescription).FirstCharToLower());
        }

        public override MemberDefinition GetMemberDefinitionWithDataAnnotation(SchemaDefinition schemaDef)
        {
            return MutableFieldsContainerDefinitionFieldAccessorExtensions.FindField(
                schemaDef.GetInterface<IInterfaceWithMethodFields>(),
                nameof(IInterfaceWithMethodFields.MethodWithDescription).FirstCharToLower());
        }

        public override Member GetMemberWithoutDataAnnotation(Schema schema)
        {
            return schema.GetInterface<IInterfaceWithMethodFields>()
                .FindField(nameof(IInterfaceWithMethodFields.MethodWithoutDescription).FirstCharToLower());
        }

        public override Member GetMemberWithDataAnnotation(Schema schema)
        {
            return schema.GetInterface<IInterfaceWithMethodFields>()
                .FindField(nameof(IInterfaceWithMethodFields.MethodWithDescription).FirstCharToLower());
        }

        public override void SetDescriptionOnMemberWithDataAnnotation(ISchemaBuilder<GraphQLContext> schemaBuilder,
            string description)
        {
            schemaBuilder.Interface<IInterfaceWithMethodFields>()
                .Field<string>(nameof(IInterfaceWithMethodFields.MethodWithDescription).FirstCharToLower(),
                    _ => _.Description(description));
        }
    }
}