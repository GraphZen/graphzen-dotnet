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
    public class InterfaceFieldArgumentDescriptionConfigurationTests : DescriptionConfigurationTests
    {
        [UsedImplicitly]
        public interface IInterfaceWithMethodFields
        {
            [UsedImplicitly]
            string Foo(string without, [Description("set by data annotation")]
                string with);
        }

        public override void CreateMemberWithoutDataAnnotation(ISchemaBuilder<GraphQLContext> schemaBuilder) =>
            schemaBuilder.Interface<IInterfaceWithMethodFields>();

        public override void CreateMemberWithDataAnnotation(ISchemaBuilder<GraphQLContext> schemaBuilder) =>
            schemaBuilder.Interface<IInterfaceWithMethodFields>();

        public override MemberDefinition GetMemberDefinitionWithoutDataAnnotation(SchemaDefinition schemaDef) =>
            MutableArgumentsContainerDefinitionArgumentAccessorExtensions.GetArgument(
                MutableFieldsContainerDefinitionFieldAccessorExtensions.GetField(
                    schemaDef.GetInterface<IInterfaceWithMethodFields>(), "foo"), "without");

        public override MemberDefinition GetMemberDefinitionWithDataAnnotation(SchemaDefinition schemaDef) =>
            MutableArgumentsContainerDefinitionArgumentAccessorExtensions.GetArgument(
                MutableFieldsContainerDefinitionFieldAccessorExtensions.GetField(
                    schemaDef.GetInterface<IInterfaceWithMethodFields>(), "foo"), "with");

        public override Member GetMemberWithoutDataAnnotation(Schema schema) =>
            schema.GetInterface<IInterfaceWithMethodFields>()
                .GetField("foo").GetArgument("without");

        public override Member GetMemberWithDataAnnotation(Schema schema) =>
            schema.GetInterface<IInterfaceWithMethodFields>()
                .GetField("foo").GetArgument("with");

        public override void SetDescriptionOnMemberWithDataAnnotation(ISchemaBuilder<GraphQLContext> schemaBuilder,
            string description) =>
            schemaBuilder.Interface<IInterfaceWithMethodFields>()
                .Field<string>("foo", _ => _.Argument<string>("with", arg => arg.Description(description)));
    }
}