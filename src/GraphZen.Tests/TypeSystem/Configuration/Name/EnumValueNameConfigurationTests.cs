// Copyright (c) GraphZen LLC. All rights reserved.
// Licensed under the GraphZen Community License. See the LICENSE file in the project root for license information.
using JetBrains.Annotations;
#nullable disable


using GraphZen.Infrastructure;
using GraphZen.TypeSystem.Internal;
using GraphZen.TypeSystem.Taxonomy;

namespace GraphZen.TypeSystem
{
    [NoReorder]
    [UsedImplicitly]
    public class EnumValueNameConfigurationTests : NameConfigurationByDataAnnotationTests
    {
        private enum FooEnum
        {
            ConventionallyNamedValue,
            [GraphQLName("CustomName")] CustomNamedValue
        }

        public override string ConventionalName { get; } = nameof(FooEnum.ConventionallyNamedValue);

        public override void CreateMemberNamedByConvention(SchemaBuilder schemaBuilder) =>
            schemaBuilder.Enum<FooEnum>();

        public override void CreateMemberWithCustomNameAttribute(SchemaBuilder schemaBuilder)
            => schemaBuilder.Enum<FooEnum>();

        public override IMutableNamed GetMemberDefinitionNamedByConvention(SchemaBuilder schemaBuilder) =>
            schemaBuilder.GetDefinition().GetEnum<FooEnum>().GetValue(nameof(FooEnum.ConventionallyNamedValue));

        public override IMutableNamed GetMemberDefinitionWithCustomNameDataAnnotation(SchemaBuilder schemaBuilder) =>
            schemaBuilder.GetDefinition().GetEnum<FooEnum>().GetValue("CustomName");

        public override INamed GetMemberNamedByConvention(Schema schema) =>
            schema.GetEnum<FooEnum>().GetValue(FooEnum.ConventionallyNamedValue);

        public override INamed GetMemberNamedByDataAnnotation(Schema schema) =>
            schema.GetEnum<FooEnum>().GetValue(FooEnum.CustomNamedValue);
    }
}