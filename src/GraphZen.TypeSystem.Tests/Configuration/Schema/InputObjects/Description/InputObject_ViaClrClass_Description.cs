// Copyright (c) GraphZen LLC. All rights reserved.
// Licensed under the GraphZen Community License. See the LICENSE file in the project root for license information.

using JetBrains.Annotations;
#nullable disable
using System.ComponentModel;
using GraphZen.Infrastructure;
using GraphZen.TypeSystem;

namespace GraphZen.InputObjects.Description
{
    public class InputObject_ViaClrClass_Description : InputObject_Description, ILeafConventionConfigurationFixture
    {
        public const string DataAnnotationDescription = nameof(DataAnnotationDescription);

        [Description(DataAnnotationDescription)]
        class ExampleInputObject { }



        public LeafConventionContext GetContext() => new LeafConventionContext()
        {
            ParentName = nameof(ExampleInputObject),
            DataAnnotationValue = DataAnnotationDescription
        };

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