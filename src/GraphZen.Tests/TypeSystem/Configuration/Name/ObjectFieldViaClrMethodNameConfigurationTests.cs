// Copyright (c) GraphZen LLC. All rights reserved.
// Licensed under the GraphZen Community License. See the LICENSE file in the project root for license information.

using System.Diagnostics.CodeAnalysis;
using System.Linq;
using GraphZen.Infrastructure;
using GraphZen.TypeSystem.Internal;
using GraphZen.TypeSystem.Taxonomy;
using JetBrains.Annotations;
#nullable disable


namespace GraphZen.TypeSystem
{
    [NoReorder]
    [UsedImplicitly]
    public class ObjectFieldViaClrMethodNameConfigurationTests : NamedContainerItemConfigurationTests
    {
        private class FooObject
        {
            public string ConventionallyNamedField()
            {
                return "foo";
            }

            [GraphQLName("CustomName")]
            public string CustomNamedField()
            {
                return "bar";
            }
        }

        public override string ConventionalName { get; } =
            nameof(FooObject.ConventionallyNamedField).FirstCharToLower();

        public override void CreateMemberNamedByConvention(SchemaBuilder schemaBuilder)
        {
            schemaBuilder.Object<FooObject>();
        }

        public override void CreateMemberWithCustomNameAttribute(SchemaBuilder schemaBuilder)
        {
            schemaBuilder.Object<FooObject>();
        }

        public override void SetNameOnMemberNamedByConvention(SchemaBuilder schemaBuilder, string name)
        {
            schemaBuilder.Object<FooObject>().Field<string>(ConventionalName, _ => _.Name(name));
        }

        public override void SetNameOnMemberNamedByDataAnnotation(SchemaBuilder schemaBuilder, string name)
        {
            schemaBuilder.Object<FooObject>().Field<string>("CustomName", _ => _.Name(name));
        }

        public override IMutableNamed GetMemberDefinitionNamedByConvention(SchemaBuilder schemaBuilder)
        {
            return schemaBuilder.GetDefinition().GetObject<FooObject>().GetField(
                nameof(FooObject.ConventionallyNamedField).FirstCharToLower());
        }

        public override IMutableNamed GetMemberDefinitionWithCustomNameDataAnnotation(SchemaBuilder schemaBuilder)
        {
            return schemaBuilder.GetDefinition().GetObject<FooObject>().GetField("CustomName");
        }

        public override INamed GetMemberNamedByConvention(Schema schema)
        {
            return schema.GetObject<FooObject>()
                .GetFields().SingleOrDefault(_ =>
                    _.ClrInfo == typeof(FooObject).GetMethod(nameof(FooObject.ConventionallyNamedField)));
        }

        public override INamed GetMemberNamedByDataAnnotation(Schema schema)
        {
            return schema.GetObject<FooObject>()
                .GetFields().SingleOrDefault(_ =>
                    _.ClrInfo == typeof(FooObject).GetMethod(nameof(FooObject.CustomNamedField)));
        }
    }
}