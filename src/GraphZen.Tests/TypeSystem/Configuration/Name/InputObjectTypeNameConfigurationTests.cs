// Copyright (c) GraphZen LLC. All rights reserved.
// Licensed under the GraphZen Community License. See the LICENSE file in the project root for license information.

using System.Diagnostics.CodeAnalysis;
using GraphZen.Infrastructure;
using GraphZen.TypeSystem.Taxonomy;
using JetBrains.Annotations;

#nullable disable


namespace GraphZen.TypeSystem
{
    [NoReorder]
    [UsedImplicitly]
    public class InputObjectTypeNameConfigurationTests : NameConfigurationByExplicitConfigurationTests
    {
        private class InputObjectNamedByConvention
        {
        }

        [GraphQLName("CustomName")]
        private class InputObjectNamedByDataAnnotation
        {
        }


        public override string ConventionalName { get; } = nameof(InputObjectNamedByConvention);

        public override void CreateMemberNamedByConvention(SchemaBuilder schemaBuilder)
        {
            schemaBuilder.InputObject<InputObjectNamedByConvention>();
        }

        public override void CreateMemberWithCustomNameAttribute(SchemaBuilder schemaBuilder)
        {
            schemaBuilder.InputObject<InputObjectNamedByDataAnnotation>();
        }

        public override void SetNameOnMemberNamedByConvention(SchemaBuilder schemaBuilder, string name)
        {
            schemaBuilder.InputObject<InputObjectNamedByConvention>().Name(name);
        }

        public override void SetNameOnMemberNamedByDataAnnotation(SchemaBuilder schemaBuilder, string name)
        {
            schemaBuilder.InputObject<InputObjectNamedByDataAnnotation>().Name(name);
        }

        public override IMutableNamed GetMemberDefinitionNamedByConvention(SchemaBuilder schemaBuilder)
        {
            return schemaBuilder.GetDefinition().GetInputObject<InputObjectNamedByConvention>();
        }

        public override IMutableNamed GetMemberDefinitionWithCustomNameDataAnnotation(SchemaBuilder schemaBuilder)
        {
            return schemaBuilder.GetDefinition().GetInputObject<InputObjectNamedByDataAnnotation>();
        }

        public override INamed GetMemberNamedByConvention(Schema schema)
        {
            return schema.GetInputObject<InputObjectNamedByConvention>();
        }

        public override INamed GetMemberNamedByDataAnnotation(Schema schema)
        {
            return schema.GetInputObject<InputObjectNamedByDataAnnotation>();
        }
    }
}