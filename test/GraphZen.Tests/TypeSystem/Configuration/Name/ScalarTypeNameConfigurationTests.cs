// Copyright (c) GraphZen LLC. All rights reserved.
// Licensed under the GraphZen Community License. See the LICENSE file in the project root for license information.

using GraphZen.Infrastructure;
using GraphZen.TypeSystem.Taxonomy;


namespace GraphZen.TypeSystem
{
    [NoReorder]
    [UsedImplicitly]
    public class ScalarTypeNameConfigurationTests : NameConfigurationByExplicitConfigurationTests
    {
        private class ScalarNamedByConvention
        {
        }

        [GraphQLName("CustomName")]
        private class ScalarNamedByDataAnnotation
        {
        }


        public override string ConventionalName { get; } = nameof(ScalarNamedByConvention);

        public override void CreateMemberNamedByConvention(SchemaBuilder schemaBuilder) =>
            schemaBuilder.Scalar<ScalarNamedByConvention>();

        public override void CreateMemberWithCustomNameAttribute(SchemaBuilder schemaBuilder)
            => schemaBuilder.Scalar<ScalarNamedByDataAnnotation>();

        public override void SetNameOnMemberNamedByConvention(SchemaBuilder schemaBuilder, string name) =>
            schemaBuilder.Scalar<ScalarNamedByConvention>().Name(name);

        public override void SetNameOnMemberNamedByDataAnnotation(SchemaBuilder schemaBuilder, string name) =>
            schemaBuilder.Scalar<ScalarNamedByDataAnnotation>().Name(name);

        public override IMutableNamed GetMemberDefinitionNamedByConvention(SchemaBuilder schemaBuilder) =>
            schemaBuilder.GetDefinition().GetScalar<ScalarNamedByConvention>();

        public override IMutableNamed GetMemberDefinitionWithCustomNameDataAnnotation(SchemaBuilder schemaBuilder) =>
            schemaBuilder.GetDefinition().GetScalar<ScalarNamedByDataAnnotation>();

        public override INamed GetMemberNamedByConvention(Schema schema) => schema.GetScalar<ScalarNamedByConvention>();

        public override INamed GetMemberNamedByDataAnnotation(Schema schema) =>
            schema.GetScalar<ScalarNamedByDataAnnotation>();
    }
}