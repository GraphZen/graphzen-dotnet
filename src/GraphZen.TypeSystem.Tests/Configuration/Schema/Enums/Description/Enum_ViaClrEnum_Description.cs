// Copyright (c) GraphZen LLC. All rights reserved.
// Licensed under the GraphZen Community License. See the LICENSE file in the project root for license information.

using JetBrains.Annotations;
#nullable disable
using System.ComponentModel;
using GraphZen.Infrastructure;
using GraphZen.TypeSystem;

namespace GraphZen.Enums.Description
{
    public class Enum_ViaClrEnum_Description : Enum_Description, ILeafConventionConfigurationFixture
    {
        [Description(DataAnnotationDescriptionValue)]
        public enum ExampleEnum
        {
        }

        public const string DataAnnotationDescriptionValue = nameof(DataAnnotationDescriptionValue);

        public LeafConventionContext GetContext() => new LeafConventionContext
        {
            ParentName = nameof(ExampleEnum),
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