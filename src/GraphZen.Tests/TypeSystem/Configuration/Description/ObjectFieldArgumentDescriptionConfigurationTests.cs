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
    public class ObjectFieldArgumentDescriptionConfigurationTests : DescriptionConfigurationTests
    {
        [UsedImplicitly]
        public class ObjectWithFieldWithDescribedArguments
        {
            [UsedImplicitly]
            public string Foo(string without, [Description("set by data annotation")]
                string with)
            {
                return "bar";
            }
        }

        public override void CreateMemberWithoutDataAnnotation(ISchemaBuilder<GraphQLContext> schemaBuilder)
        {
            schemaBuilder.Object<ObjectWithFieldWithDescribedArguments>();
        }

        public override void CreateMemberWithDataAnnotation(ISchemaBuilder<GraphQLContext> schemaBuilder)
        {
            schemaBuilder.Object<ObjectWithFieldWithDescribedArguments>();
        }

        public override MemberDefinition GetMemberDefinitionWithoutDataAnnotation(SchemaDefinition schemaDef)
        {
            return MutableArgumentsContainerDefinitionArgumentAccessorExtensions.GetArgument(
                MutableFieldsContainerDefinitionFieldAccessorExtensions.GetField(
                    schemaDef.GetObject<ObjectWithFieldWithDescribedArguments>(), "foo"), "without");
        }

        public override MemberDefinition GetMemberDefinitionWithDataAnnotation(SchemaDefinition schemaDef)
        {
            return MutableArgumentsContainerDefinitionArgumentAccessorExtensions.GetArgument(
                MutableFieldsContainerDefinitionFieldAccessorExtensions.GetField(
                    schemaDef.GetObject<ObjectWithFieldWithDescribedArguments>(), "foo"), "with");
        }

        public override Member GetMemberWithoutDataAnnotation(Schema schema)
        {
            return schema.GetObject<ObjectWithFieldWithDescribedArguments>()
                .GetField("foo").GetArgument("without");
        }

        public override Member GetMemberWithDataAnnotation(Schema schema)
        {
            return schema.GetObject<ObjectWithFieldWithDescribedArguments>()
                .GetField("foo").GetArgument("with");
        }

        public override void SetDescriptionOnMemberWithDataAnnotation(ISchemaBuilder<GraphQLContext> schemaBuilder,
            string description)
        {
            schemaBuilder.Object<ObjectWithFieldWithDescribedArguments>()
                .Field<string>("foo", _ => _.Argument<string>("with", arg => arg.Description(description)));
        }
    }
}