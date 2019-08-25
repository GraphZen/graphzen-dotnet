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
    public class InputObjectFieldViaClrPropertyNameConfigurationTests : NamedContainerItemConfigurationTests
    {
        private class InputObject
        {
            public string ConventionallyNamedField { get; set; }

            [GraphQLName("CustomName")] public string CustomNamedField { get; set; }
        }

        public override string ConventionalName { get; } =
            nameof(InputObject.ConventionallyNamedField).FirstCharToLower();

        public override void CreateMemberNamedByConvention(SchemaBuilder schemaBuilder)
        {
            schemaBuilder.InputObject<InputObject>();
        }

        public override void CreateMemberWithCustomNameAttribute(SchemaBuilder schemaBuilder)
        {
            schemaBuilder.InputObject<InputObject>();
        }

        public override void SetNameOnMemberNamedByConvention(SchemaBuilder schemaBuilder, string name)
        {
            schemaBuilder.InputObject<InputObject>().Field(_ => _.ConventionallyNamedField, _ => _.Name(name));
        }

        public override void SetNameOnMemberNamedByDataAnnotation(SchemaBuilder schemaBuilder, string name)
        {
            schemaBuilder.InputObject<InputObject>().Field(_ => _.CustomNamedField, _ => _.Name(name));
        }

        public override IMutableNamed GetMemberDefinitionNamedByConvention(SchemaBuilder schemaBuilder)
        {
            return schemaBuilder.GetDefinition().GetInputObject<InputObject>()
                .GetField(nameof(InputObject.ConventionallyNamedField).FirstCharToLower());
        }

        public override IMutableNamed GetMemberDefinitionWithCustomNameDataAnnotation(SchemaBuilder schemaBuilder)
        {
            return schemaBuilder.GetDefinition().GetInputObject<InputObject>()
                .GetField("CustomName");
        }

        public override INamed GetMemberNamedByConvention(Schema schema)
        {
            return schema.GetInputObject<InputObject>()
                .GetFields().SingleOrDefault(_ =>
                    _.ClrInfo == typeof(InputObject).GetProperty(nameof(InputObject.ConventionallyNamedField)));
        }

        public override INamed GetMemberNamedByDataAnnotation(Schema schema)
        {
            return schema.GetInputObject<InputObject>()
                .GetFields().SingleOrDefault(_ =>
                    _.ClrInfo == typeof(InputObject).GetProperty(nameof(InputObject.CustomNamedField)));
        }
    }
}