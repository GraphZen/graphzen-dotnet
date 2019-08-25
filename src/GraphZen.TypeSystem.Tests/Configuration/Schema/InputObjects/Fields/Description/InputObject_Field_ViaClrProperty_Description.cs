// Copyright (c) GraphZen LLC. All rights reserved.
// Licensed under the GraphZen Community License. See the LICENSE file in the project root for license information.

using System.ComponentModel;
using System.Diagnostics.CodeAnalysis;
using GraphZen.Infrastructure;
using GraphZen.TypeSystem;
using JetBrains.Annotations;
#nullable disable

namespace GraphZen.InputObjects.Fields.Description
{
    public class InputObject_Field_ViaClrProperty_Description : InputObject_Field_Description,
        ILeafConventionConfigurationFixture
    {
        [GraphQLName(Grandparent)]
        public class ExampleInputObject
        {
            [Description(DataAnnotationDescriptionValue)]
            public string ExampleField { get; set; }
        }

        public const string DataAnnotationDescriptionValue = nameof(DataAnnotationDescriptionValue);

        public LeafConventionContext GetContext()
        {
            return new LeafConventionContext
            {
                ParentName = nameof(ExampleInputObject.ExampleField).FirstCharToLower(),
                DataAnnotationValue = DataAnnotationDescriptionValue
            };
        }

        public void ConfigureContextConventionally(SchemaBuilder sb)
        {
            sb.InputObject<ExampleInputObject>();
        }

        public void ConfigureClrContext(SchemaBuilder sb, string parentName)
        {
            sb.InputObject<ExampleInputObject>();
        }
    }
}