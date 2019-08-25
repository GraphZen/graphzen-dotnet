// Copyright (c) GraphZen LLC. All rights reserved.
// Licensed under the GraphZen Community License. See the LICENSE file in the project root for license information.

using System.ComponentModel;
using System.Diagnostics.CodeAnalysis;
using GraphZen.Infrastructure;
using GraphZen.TypeSystem.Internal;
using JetBrains.Annotations;
#nullable disable


namespace GraphZen.TypeSystem
{
    [UsedImplicitly]
    [NoReorder]
    public class ObjectFieldArgumentDefaultValueConfigurationTests : DefaultValueConfigurationTestsWithConventions
    {
        private class FooObject
        {
            [UsedImplicitly]
            public string FieldWithArgDefaultValues(
                string noDefaultValueByConvention,
                [DefaultValue("default value set by attribute")]
                string defaultValueSetByAttribute = "default value set by convention",
                string defaultValueSetByConvention = "default value set by convention")
            {
                return "";
            }
        }

        public override void CreateMembersByConvention(SchemaBuilder schemaBuilder)
        {
            schemaBuilder.Object<FooObject>();
        }

        public override InputValueDefinition GetMemberDefinitionWithNoDefaultValueByConvention(
            SchemaBuilder schemaBuilder)
        {
            return MutableArgumentsContainerDefinitionArgumentAccessorExtensions.GetArgument(
                MutableFieldsContainerDefinitionFieldAccessorExtensions.GetField(
                    schemaBuilder.GetDefinition().GetObject<FooObject>(),
                    nameof(FooObject.FieldWithArgDefaultValues).FirstCharToLower()), "noDefaultValueByConvention");
        }


        public override InputValueDefinition GetMemberDefinitionWithDefaultValueConfiguredByConvention(
            SchemaBuilder schemaBuilder)
        {
            return MutableArgumentsContainerDefinitionArgumentAccessorExtensions.GetArgument(
                MutableFieldsContainerDefinitionFieldAccessorExtensions.GetField(
                    schemaBuilder.GetDefinition().GetObject<FooObject>(),
                    nameof(FooObject.FieldWithArgDefaultValues).FirstCharToLower()), "defaultValueSetByConvention");
        }

        public override InputValueDefinition GetMemberDefinitionWithDefaultValueConfiguredByDataAnnotation(
            SchemaBuilder schemaBuilder)
        {
            return MutableArgumentsContainerDefinitionArgumentAccessorExtensions.GetArgument(
                MutableFieldsContainerDefinitionFieldAccessorExtensions.GetField(
                    schemaBuilder.GetDefinition().GetObject<FooObject>(),
                    nameof(FooObject.FieldWithArgDefaultValues).FirstCharToLower()), "defaultValueSetByAttribute");
        }

        public override void SetDefaultValueOnMemberWithNoDefaultValueByConvention(SchemaBuilder schemaBuilder,
            object defaultValue)
        {
            schemaBuilder.Object<FooObject>()
                .Field<string>(nameof(FooObject.FieldWithArgDefaultValues).FirstCharToLower(),
                    f => f.Argument<string>("noDefaultValueByConvention", a => a.DefaultValue(defaultValue)));
        }

        public override void SetDefaultValueOnMemberWithDefaultValueSetByConvention(SchemaBuilder schemaBuilder,
            object defaultValue)
        {
            schemaBuilder.Object<FooObject>()
                .Field<string>(nameof(FooObject.FieldWithArgDefaultValues).FirstCharToLower(),
                    f => f.Argument<string>("defaultValueSetByConvention", a => a.DefaultValue(defaultValue)));
        }

        public override void SetDefaultValueOnMemberWithDefaultValueSetByAttribute(SchemaBuilder schemaBuilder,
            object defaultValue)
        {
            schemaBuilder.Object<FooObject>()
                .Field<string>(nameof(FooObject.FieldWithArgDefaultValues).FirstCharToLower(),
                    f => f.Argument<string>("defaultValueSetByAttribute", a => a.DefaultValue(defaultValue)));
        }

        public override InputValue GetMemberWithNoDefaultValueByConvention(Schema schema)
        {
            return schema.GetObject<FooObject>()
                .GetField(nameof(FooObject.FieldWithArgDefaultValues).FirstCharToLower())
                .GetArgument("noDefaultValueByConvention");
        }

        public override InputValue GetMemberWithDefaultValueSetByConvention(Schema schema)
        {
            return schema.GetObject<FooObject>()
                .GetField(nameof(FooObject.FieldWithArgDefaultValues).FirstCharToLower())
                .GetArgument("defaultValueSetByConvention");
        }

        public override InputValue GetMemberWithDefaultValueSetByDataAnnotation(Schema schema)
        {
            return schema
                .GetObject<FooObject>()
                .GetField(nameof(FooObject.FieldWithArgDefaultValues).FirstCharToLower())
                .GetArgument("defaultValueSetByAttribute");
        }
    }
}