// Copyright (c) GraphZen LLC. All rights reserved.
// Licensed under the GraphZen Community License. See the LICENSE file in the project root for license information.

using JetBrains.Annotations;
#nullable disable
using System.ComponentModel;
using GraphZen.Infrastructure;
using GraphZen.TypeSystem;

namespace GraphZen.Interfaces.Description
{

    public class Interface_ViaClrClass_Description : Interface_Description, ILeafConventionConfigurationFixture
    {
        public const string DataAnnotationDescriptionValue = nameof(DataAnnotationDescriptionValue);

        public LeafConventionContext GetContext() => new LeafConventionContext
        {
            ParentName = nameof(IExampleInterface),
            DataAnnotationValue = DataAnnotationDescriptionValue
        };

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