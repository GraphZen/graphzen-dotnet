// Copyright (c) GraphZen LLC. All rights reserved.
// Licensed under the GraphZen Community License. See the LICENSE file in the project root for license information.

using System.Diagnostics.CodeAnalysis;
using GraphZen.Infrastructure;
using GraphZen.TypeSystem.Internal;
using GraphZen.TypeSystem.Taxonomy;
using JetBrains.Annotations;

#nullable disable


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

        public override void CreateMemberNamedByConvention(SchemaBuilder schemaBuilder)
        {
            schemaBuilder.Enum<FooEnum>();
        }

        public override void CreateMemberWithCustomNameAttribute(SchemaBuilder schemaBuilder)
        {
            schemaBuilder.Enum<FooEnum>();
        }

        public override IMutableNamed GetMemberDefinitionNamedByConvention(SchemaBuilder schemaBuilder)
        {
            return schemaBuilder.GetDefinition().GetEnum<FooEnum>().GetValue(nameof(FooEnum.ConventionallyNamedValue));
        }

        public override IMutableNamed GetMemberDefinitionWithCustomNameDataAnnotation(SchemaBuilder schemaBuilder)
        {
            return schemaBuilder.GetDefinition().GetEnum<FooEnum>().GetValue("CustomName");
        }

        public override INamed GetMemberNamedByConvention(Schema schema)
        {
            return schema.GetEnum<FooEnum>().GetValue(FooEnum.ConventionallyNamedValue);
        }

        public override INamed GetMemberNamedByDataAnnotation(Schema schema)
        {
            return schema.GetEnum<FooEnum>().GetValue(FooEnum.CustomNamedValue);
        }
    }
}