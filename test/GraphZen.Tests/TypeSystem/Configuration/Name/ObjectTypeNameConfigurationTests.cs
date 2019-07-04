// Copyright (c) GraphZen LLC. All rights reserved.
// Licensed under the GraphZen Community License. See the LICENSE file in the project root for license information.

using GraphZen.Infrastructure;
using JetBrains.Annotations;

namespace GraphZen.TypeSystem
{
    [NoReorder]
    [UsedImplicitly]
    public class ObjectTypeNameConfigurationTests : NameConfigurationByExplicitConfigurationTests
    {
        private class ObjectNamedByConvention
        {
        }

        [GraphQLName("CustomName")]
        private class ObjectNamedByDataAnnotation
        {
        }


        public override string ConventionalName { get; } = nameof(ObjectNamedByConvention);

        public override void CreateMemberNamedByConvention(SchemaBuilder schemaBuilder) =>
            schemaBuilder.Object<ObjectNamedByConvention>();

        public override void CreateMemberWithCustomNameAttribute(SchemaBuilder schemaBuilder)
            => schemaBuilder.Object<ObjectNamedByDataAnnotation>();

        public override void SetNameOnMemberNamedByConvention(SchemaBuilder schemaBuilder, string name) =>
            schemaBuilder.Object<ObjectNamedByConvention>().Name(name);

        public override void SetNameOnMemberNamedByDataAnnotation(SchemaBuilder schemaBuilder, string name) =>
            schemaBuilder.Object<ObjectNamedByDataAnnotation>().Name(name);

        public override IMutableNamed GetMemberDefinitionNamedByConvention(SchemaBuilder schemaBuilder) =>
            schemaBuilder.GetDefinition().GetObject<ObjectNamedByConvention>();

        public override IMutableNamed GetMemberDefinitionWithCustomNameDataAnnotation(SchemaBuilder schemaBuilder) =>
            schemaBuilder.GetDefinition().GetObject<ObjectNamedByDataAnnotation>();

        public override INamed GetMemberNamedByConvention(Schema schema) => schema.GetObject<ObjectNamedByConvention>();

        public override INamed GetMemberNamedByDataAnnotation(Schema schema) =>
            schema.GetObject<ObjectNamedByDataAnnotation>();
    }
}