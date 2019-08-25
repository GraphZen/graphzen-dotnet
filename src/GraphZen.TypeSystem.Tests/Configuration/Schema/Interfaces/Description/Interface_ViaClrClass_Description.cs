// Copyright (c) GraphZen LLC. All rights reserved.
// Licensed under the GraphZen Community License. See the LICENSE file in the project root for license information.

using System.ComponentModel;
using System.Diagnostics.CodeAnalysis;
using GraphZen.Infrastructure;
using GraphZen.TypeSystem;
using JetBrains.Annotations;
#nullable disable

namespace GraphZen.Interfaces.Description
{
    public class Interface_ViaClrClass_Description : Interface_Description, ILeafConventionConfigurationFixture
    {
        public const string DataAnnotationDescriptionValue = nameof(DataAnnotationDescriptionValue);

        public LeafConventionContext GetContext()
        {
            return new LeafConventionContext
            {
                ParentName = nameof(IExampleInterface),
                DataAnnotationValue = DataAnnotationDescriptionValue
            };
        }

        [Description(DataAnnotationDescriptionValue)]
        public interface IExampleInterface
        {
        }

        public void ConfigureContextConventionally(SchemaBuilder sb)
        {
            sb.Interface<IExampleInterface>();
        }

        public void ConfigureClrContext(SchemaBuilder sb, string parentName)
        {
            sb.Interface<IExampleInterface>();
        }
    }
}