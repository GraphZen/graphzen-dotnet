// Copyright (c) GraphZen LLC. All rights reserved.
// Licensed under the GraphZen Community License. See the LICENSE file in the project root for license information.

using GraphZen.Infrastructure;
using JetBrains.Annotations;

namespace GraphZen.TypeSystem
{
    [NoReorder]
    [UsedImplicitly]
    public class EnumTypeNameConfigurationTests : NameConfigurationByExplicitConfigurationTests
    {
        private enum EnumNamedByConvention
        {
        }

        [GraphQLName("CustomName")]
        private enum FooEnum
        {
        }


        public override string ConventionalName { get; } = nameof(EnumNamedByConvention);

        public override void CreateMemberNamedByConvention(SchemaBuilder schemaBuilder) =>
            schemaBuilder.Enum<EnumNamedByConvention>();

        public override void CreateMemberWithCustomNameAttribute(SchemaBuilder schemaBuilder)
            => schemaBuilder.Enum<FooEnum>();

        public override void SetNameOnMemberNamedByConvention(SchemaBuilder schemaBuilder, string name) =>
            schemaBuilder.Enum<EnumNamedByConvention>().Name(name);

        public override void SetNameOnMemberNamedByDataAnnotation(SchemaBuilder schemaBuilder, string name) =>
            schemaBuilder.Enum<FooEnum>().Name(name);

        public override IMutableNamed GetMemberDefinitionNamedByConvention(SchemaBuilder schemaBuilder) =>
            schemaBuilder.GetDefinition().GetEnum<EnumNamedByConvention>();

        public override IMutableNamed GetMemberDefinitionWithCustomNameDataAnnotation(SchemaBuilder schemaBuilder) =>
            schemaBuilder.GetDefinition().GetEnum<FooEnum>();

        public override INamed GetMemberNamedByConvention(Schema schema) => schema.GetEnum<EnumNamedByConvention>();

        public override INamed GetMemberNamedByDataAnnotation(Schema schema) =>
            schema.GetEnum<FooEnum>();
    }
}