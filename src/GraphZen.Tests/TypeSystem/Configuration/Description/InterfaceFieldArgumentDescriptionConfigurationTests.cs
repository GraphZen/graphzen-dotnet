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
    public class InterfaceFieldArgumentDescriptionConfigurationTests : DescriptionConfigurationTests
    {
        [UsedImplicitly]
        public interface IInterfaceWithMethodFields
        {
            [UsedImplicitly]
            string Foo(string without, [Description("set by data annotation")]
                string with);
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
            return MutableArgumentsContainerDefinitionArgumentAccessorExtensions.GetArgument(
                MutableFieldsContainerDefinitionFieldAccessorExtensions.GetField(
                    schemaDef.GetInterface<IInterfaceWithMethodFields>(), "foo"), "without");
        }

        public override MemberDefinition GetMemberDefinitionWithDataAnnotation(SchemaDefinition schemaDef)
        {
            return MutableArgumentsContainerDefinitionArgumentAccessorExtensions.GetArgument(
                MutableFieldsContainerDefinitionFieldAccessorExtensions.GetField(
                    schemaDef.GetInterface<IInterfaceWithMethodFields>(), "foo"), "with");
        }

        public override Member GetMemberWithoutDataAnnotation(Schema schema)
        {
            return schema.GetInterface<IInterfaceWithMethodFields>()
                .GetField("foo").GetArgument("without");
        }

        public override Member GetMemberWithDataAnnotation(Schema schema)
        {
            return schema.GetInterface<IInterfaceWithMethodFields>()
                .GetField("foo").GetArgument("with");
        }

        public override void SetDescriptionOnMemberWithDataAnnotation(ISchemaBuilder<GraphQLContext> schemaBuilder,
            string description)
        {
            schemaBuilder.Interface<IInterfaceWithMethodFields>()
                .Field<string>("foo", _ => _.Argument<string>("with", arg => arg.Description(description)));
        }
    }
}