// Copyright (c) GraphZen LLC. All rights reserved.
// Licensed under the GraphZen Community License. See the LICENSE file in the project root for license information.

using System.ComponentModel;
using System.Diagnostics.CodeAnalysis;
using GraphZen.Configuration.Infrastructure;
using GraphZen.Infrastructure;
using GraphZen.TypeSystem;
using JetBrains.Annotations;

namespace GraphZen.Configuration.InputObjects.Description
{
    public class InputObject_ViaClrClass_Description : InputObject_Description, ILeafConventionConfigurationFixture
    {
        public const string DataAnnotationDescription = nameof(DataAnnotationDescription);

        [Description(DataAnnotationDescription)]
        private class ExampleInputObject
        {
        }


        public LeafConventionContext GetContext() =>
            new LeafConventionContext
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