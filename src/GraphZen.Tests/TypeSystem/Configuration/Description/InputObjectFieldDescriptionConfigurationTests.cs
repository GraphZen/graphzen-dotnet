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
    public class InputObjectFieldDescriptionConfigurationTests : DescriptionConfigurationTests
    {
        public class InputObjectWithDescribedFields
        {
            [Description("set by data annotation")]
            public string With { get; set; }

            public string Without { get; set; }
        }


        public override void CreateMemberWithoutDataAnnotation(ISchemaBuilder<GraphQLContext> schemaBuilder)
        {
            schemaBuilder.InputObject<InputObjectWithDescribedFields>();
        }

        public override void CreateMemberWithDataAnnotation(ISchemaBuilder<GraphQLContext> schemaBuilder)
        {
            schemaBuilder.InputObject<InputObjectWithDescribedFields>();
        }

        public override MemberDefinition GetMemberDefinitionWithoutDataAnnotation(SchemaDefinition schemaDef)
        {
            return schemaDef.GetInputObject<InputObjectWithDescribedFields>().GetField("without");
        }

        public override MemberDefinition GetMemberDefinitionWithDataAnnotation(SchemaDefinition schemaDef)
        {
            return schemaDef.GetInputObject<InputObjectWithDescribedFields>().GetField("with");
        }

        public override Member GetMemberWithoutDataAnnotation(Schema schema)
        {
            return schema.GetInputObject<InputObjectWithDescribedFields>().Fields["without"];
        }

        public override Member GetMemberWithDataAnnotation(Schema schema)
        {
            return schema.GetInputObject<InputObjectWithDescribedFields>().Fields["with"];
        }

        public override void SetDescriptionOnMemberWithDataAnnotation(ISchemaBuilder<GraphQLContext> schemaBuilder,
            string description)
        {
            schemaBuilder.InputObject<InputObjectWithDescribedFields>()
                .Field<string>("with", f => f
                    .Description(description));
        }
    }
}