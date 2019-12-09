// Copyright (c) GraphZen LLC. All rights reserved.
// Licensed under the GraphZen Community License. See the LICENSE file in the project root for license information.

using System.ComponentModel;
using System.Diagnostics.CodeAnalysis;
using GraphZen.Infrastructure;
using GraphZen.TypeSystem.Tests.Configuration.Infrastructure;
using JetBrains.Annotations;

namespace GraphZen.TypeSystem.Tests.Configuration.Enums.EnumValues.Description
{
    // ReSharper disable once InconsistentNaming
    public class EnumValue_ViaClrEnumValue_Description : EnumValue_Description, ILeafConventionConfigurationFixture
    {
        [GraphQLName(Grandparent)]
        public enum ExampleEnum
        {
            [Description(DataAnnotationDescriptionValue)]
            ExampleEnumValue
        }

        public const string DataAnnotationDescriptionValue = nameof(DataAnnotationDescriptionValue);

        public LeafConventionContext GetContext() =>
            new LeafConventionContext
            {
                ParentName = nameof(ExampleEnum.ExampleEnumValue),
                DataAnnotationValue = DataAnnotationDescriptionValue
            };

        public void ConfigureContextConventionally(SchemaBuilder sb)
        {
            sb.Enum<ExampleEnum>();
        }

        public void ConfigureClrContext(SchemaBuilder sb, string parentName)
        {
            sb.Enum<ExampleEnum>();
        }
    }
}