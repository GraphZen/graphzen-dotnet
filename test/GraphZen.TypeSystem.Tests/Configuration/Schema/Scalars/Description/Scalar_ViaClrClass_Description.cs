// Copyright (c) GraphZen LLC. All rights reserved.
// Licensed under the GraphZen Community License. See the LICENSE file in the project root for license information.

using System.ComponentModel;
using System.Diagnostics.CodeAnalysis;
using GraphZen.Infrastructure;
using GraphZen.TypeSystem.Tests.Configuration.Infrastructure;
using JetBrains.Annotations;

namespace GraphZen.TypeSystem.Tests.Configuration.Scalars.Description
{
    // ReSharper disable once InconsistentNaming
    public class Scalar_ViaClrClass_Description : Scalar_Description, ILeafConventionConfigurationFixture
    {
        public const string DataAnnotationDescriptionValue = nameof(DataAnnotationDescriptionValue);

        public LeafConventionContext GetContext() =>
            new LeafConventionContext
            {
                ParentName = nameof(ExampleScalar),
                DataAnnotationValue = DataAnnotationDescriptionValue
            };

        [Description(DataAnnotationDescriptionValue)]
        public class ExampleScalar
        {
        }

        public void ConfigureContextConventionally(SchemaBuilder sb)
        {
            sb.Scalar<ExampleScalar>();
        }

        public void ConfigureClrContext(SchemaBuilder sb, string parentName)
        {
            sb.Scalar<ExampleScalar>();
        }
    }
}