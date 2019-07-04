// Copyright (c) GraphZen LLC. All rights reserved.
// Licensed under the GraphZen Community License. See the LICENSE file in the project root for license information.

using System.ComponentModel;
using GraphZen.Infrastructure;
using GraphZen.Infrastructure.Extensions;
using GraphZen.TypeSystem.Internal;


// ReSharper disable UnusedAutoPropertyAccessor.Local
// ReSharper disable MemberCanBePrivate.Local

namespace GraphZen.TypeSystem
{
    [UsedImplicitly]
    [NoReorder]
    public class InputObjectFieldArgumentDefaultValueConfigurationTests : DefaultValueConfigurationTests
    {
        private class FooInputObject
        {
            public string NoDefaultValueSetByConvention { get; set; }

            [DefaultValue("default value set by attribute")]
            public string DefaultValueSetByAttribute { get; set; }
        }

        public override void CreateMembersByConvention(SchemaBuilder schemaBuilder)
        {
            schemaBuilder.InputObject<FooInputObject>();
        }

        public override InputValueDefinition GetMemberDefinitionWithNoDefaultValueByConvention(
            SchemaBuilder schemaBuilder) =>
            schemaBuilder.GetDefinition().GetInputObject<FooInputObject>()
                .GetField(nameof(FooInputObject.NoDefaultValueSetByConvention).FirstCharToLower());


        public override InputValueDefinition GetMemberDefinitionWithDefaultValueConfiguredByDataAnnotation(
            SchemaBuilder schemaBuilder) =>
            schemaBuilder.GetDefinition().GetInputObject<FooInputObject>()
                .GetField(nameof(FooInputObject.DefaultValueSetByAttribute).FirstCharToLower());

        public override void SetDefaultValueOnMemberWithNoDefaultValueByConvention(SchemaBuilder schemaBuilder,
            object defaultValue)
        {
            schemaBuilder.InputObject<FooInputObject>().Field<string>(
                nameof(FooInputObject.NoDefaultValueSetByConvention).FirstCharToLower(),
                f => f.DefaultValue(defaultValue));
        }


        public override void SetDefaultValueOnMemberWithDefaultValueSetByAttribute(SchemaBuilder schemaBuilder,
            object defaultValue)
        {
            schemaBuilder.InputObject<FooInputObject>().Field<string>(
                nameof(FooInputObject.DefaultValueSetByAttribute).FirstCharToLower(),
                f => f.DefaultValue(defaultValue));
        }

        public override InputValue GetMemberWithNoDefaultValueByConvention(Schema schema) =>
            schema.GetInputObject<FooInputObject>()
                .GetField(nameof(FooInputObject.NoDefaultValueSetByConvention).FirstCharToLower());


        public override InputValue GetMemberWithDefaultValueSetByDataAnnotation(Schema schema) =>
            schema.GetInputObject<FooInputObject>()
                .GetField(nameof(FooInputObject.DefaultValueSetByAttribute).FirstCharToLower());
    }
}