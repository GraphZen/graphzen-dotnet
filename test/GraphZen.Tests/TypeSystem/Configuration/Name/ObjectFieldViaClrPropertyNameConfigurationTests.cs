// Copyright (c) GraphZen LLC. All rights reserved.
// Licensed under the GraphZen Community License. See the LICENSE file in the project root for license information.

using System.Linq;
using GraphZen.Infrastructure;
using GraphZen.Infrastructure.Extensions;
using GraphZen.Internal;
using GraphZen.TypeSystem.Internal;
using GraphZen.TypeSystem.Taxonomy;


namespace GraphZen.TypeSystem
{
    [NoReorder]
    [UsedImplicitly]
    public class ObjectFieldViaClrPropertyNameConfigurationTests : NamedContainerItemConfigurationTests
    {
        private class FooObject
        {
            public string ConventionallyNamedField { get; set; }

            [GraphQLName("CustomName")]
            public string CustomNamedField { get; set; }
        }

        public override string ConventionalName { get; } =
            nameof(FooObject.ConventionallyNamedField).FirstCharToLower();

        public override void CreateMemberNamedByConvention(SchemaBuilder schemaBuilder) =>
            schemaBuilder.Object<FooObject>();

        public override void CreateMemberWithCustomNameAttribute(SchemaBuilder schemaBuilder)
            => schemaBuilder.Object<FooObject>();

        public override void SetNameOnMemberNamedByConvention(SchemaBuilder schemaBuilder, string name) =>
            schemaBuilder.Object<FooObject>().Field(_ => _.ConventionallyNamedField, _ => _.Name(name));

        public override void SetNameOnMemberNamedByDataAnnotation(SchemaBuilder schemaBuilder, string name) =>
            schemaBuilder.Object<FooObject>().Field(_ => _.CustomNamedField, _ => _.Name(name));

        public override IMutableNamed GetMemberDefinitionNamedByConvention(SchemaBuilder schemaBuilder) =>
            MutableFieldsContainerDefinitionFieldAccessorExtensions.GetField(
                schemaBuilder.GetDefinition().GetObject<FooObject>(),
                nameof(FooObject.ConventionallyNamedField).FirstCharToLower());


        public override IMutableNamed GetMemberDefinitionWithCustomNameDataAnnotation(SchemaBuilder schemaBuilder) =>
            MutableFieldsContainerDefinitionFieldAccessorExtensions.GetField(
                schemaBuilder.GetDefinition().GetObject<FooObject>(), "CustomName");

        public override INamed GetMemberNamedByConvention(Schema schema) => schema.GetObject<FooObject>()
            .GetFields().SingleOrDefault(_ =>
                _.ClrInfo == typeof(FooObject).GetProperty(nameof(FooObject.ConventionallyNamedField)));

        public override INamed GetMemberNamedByDataAnnotation(Schema schema) => schema.GetObject<FooObject>()
            .GetFields().SingleOrDefault(_ =>
                _.ClrInfo == typeof(FooObject).GetProperty(nameof(FooObject.CustomNamedField)));
    }
}