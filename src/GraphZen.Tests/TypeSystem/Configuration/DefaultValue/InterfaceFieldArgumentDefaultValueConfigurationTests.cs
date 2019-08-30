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
    public class InterfaceFieldArgumentDefaultValueConfigurationTests : DefaultValueConfigurationTestsWithConventions
    {
        private interface IFooInterface
        {
            [UsedImplicitly]
            string FieldWithArgDefaultValues(
                string noDefaultValueByConvention,
                [DefaultValue("default value set by attribute")]
                string defaultValueSetByAttribute = "default value set by convention",
                string defaultValueSetByConvention = "default value set by convention");
        }

        public override void CreateMembersByConvention(SchemaBuilder schemaBuilder)
        {
            schemaBuilder.Interface<IFooInterface>();
        }

        public override InputValueDefinition GetMemberDefinitionWithNoDefaultValueByConvention(
            SchemaBuilder schemaBuilder) =>
            MutableArgumentsContainerDefinitionArgumentAccessorExtensions.GetArgument(
                MutableFieldsContainerDefinitionFieldAccessorExtensions.GetField(
                    schemaBuilder.GetDefinition().GetInterface<IFooInterface>(),
                    nameof(IFooInterface.FieldWithArgDefaultValues).FirstCharToLower()), "noDefaultValueByConvention");


        public override InputValueDefinition GetMemberDefinitionWithDefaultValueConfiguredByConvention(
            SchemaBuilder schemaBuilder) =>
            MutableArgumentsContainerDefinitionArgumentAccessorExtensions.GetArgument(
                MutableFieldsContainerDefinitionFieldAccessorExtensions.GetField(
                    schemaBuilder.GetDefinition().GetInterface<IFooInterface>(),
                    nameof(IFooInterface.FieldWithArgDefaultValues).FirstCharToLower()), "defaultValueSetByConvention");

        public override InputValueDefinition GetMemberDefinitionWithDefaultValueConfiguredByDataAnnotation(
            SchemaBuilder schemaBuilder) =>
            MutableArgumentsContainerDefinitionArgumentAccessorExtensions.GetArgument(
                MutableFieldsContainerDefinitionFieldAccessorExtensions.GetField(
                    schemaBuilder.GetDefinition().GetInterface<IFooInterface>(),
                    nameof(IFooInterface.FieldWithArgDefaultValues).FirstCharToLower()), "defaultValueSetByAttribute");

        public override void SetDefaultValueOnMemberWithNoDefaultValueByConvention(SchemaBuilder schemaBuilder,
            object defaultValue)
        {
            schemaBuilder.Interface<IFooInterface>()
                .Field<string>(nameof(IFooInterface.FieldWithArgDefaultValues).FirstCharToLower(),
                    f => f.Argument<string>("noDefaultValueByConvention", a => a.DefaultValue(defaultValue)));
        }

        public override void SetDefaultValueOnMemberWithDefaultValueSetByConvention(SchemaBuilder schemaBuilder,
            object defaultValue)
        {
            schemaBuilder.Interface<IFooInterface>()
                .Field<string>(nameof(IFooInterface.FieldWithArgDefaultValues).FirstCharToLower(),
                    f => f.Argument<string>("defaultValueSetByConvention", a => a.DefaultValue(defaultValue)));
        }

        public override void SetDefaultValueOnMemberWithDefaultValueSetByAttribute(SchemaBuilder schemaBuilder,
            object defaultValue)
        {
            schemaBuilder.Interface<IFooInterface>()
                .Field<string>(nameof(IFooInterface.FieldWithArgDefaultValues).FirstCharToLower(),
                    f => f.Argument<string>("defaultValueSetByAttribute", a => a.DefaultValue(defaultValue)));
        }

        public override InputValue GetMemberWithNoDefaultValueByConvention(Schema schema) =>
            schema.GetInterface<IFooInterface>()
                .GetField(nameof(IFooInterface.FieldWithArgDefaultValues).FirstCharToLower())
                .GetArgument("noDefaultValueByConvention");

        public override InputValue GetMemberWithDefaultValueSetByConvention(Schema schema) =>
            schema.GetInterface<IFooInterface>()
                .GetField(nameof(IFooInterface.FieldWithArgDefaultValues).FirstCharToLower())
                .GetArgument("defaultValueSetByConvention");

        public override InputValue GetMemberWithDefaultValueSetByDataAnnotation(Schema schema) =>
            schema.GetInterface<IFooInterface>()
                .GetField(nameof(IFooInterface.FieldWithArgDefaultValues).FirstCharToLower())
                .GetArgument("defaultValueSetByAttribute");
    }
}