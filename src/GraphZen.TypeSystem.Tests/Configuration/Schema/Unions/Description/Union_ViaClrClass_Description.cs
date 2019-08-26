// Copyright (c) GraphZen LLC. All rights reserved.
// Licensed under the GraphZen Community License. See the LICENSE file in the project root for license information.

using System.ComponentModel;
using System.Diagnostics.CodeAnalysis;
using GraphZen.Infrastructure;
using GraphZen.TypeSystem;
using JetBrains.Annotations;

namespace GraphZen.Unions.Description
{
    public class Union_ViaClrClass_Description : Union_Description, ILeafConventionConfigurationFixture
    {
        public const string DataAnnotationDescriptionValue = nameof(DataAnnotationDescriptionValue);

        public LeafConventionContext GetContext()
        {
            return new LeafConventionContext
            {
                ParentName = nameof(ExampleUnion),
                DataAnnotationValue = DataAnnotationDescriptionValue
            };
        }

        public void ConfigureContextConventionally(SchemaBuilder sb)
        {
            sb.Union<ExampleUnion>();
        }

        public void ConfigureClrContext(SchemaBuilder sb, string parentName)
        {
            sb.Union<ExampleUnion>();
        }

        [Description(DataAnnotationDescriptionValue)]
        public class ExampleUnion
        {
        }
    }
}