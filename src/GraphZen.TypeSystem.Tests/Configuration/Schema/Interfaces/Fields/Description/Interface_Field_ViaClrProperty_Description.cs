// Copyright (c) GraphZen LLC. All rights reserved.
// Licensed under the GraphZen Community License. See the LICENSE file in the project root for license information.

using System.ComponentModel;
using System.Diagnostics.CodeAnalysis;
using GraphZen.Configuration.Infrastructure;
using GraphZen.Infrastructure;
using GraphZen.TypeSystem;
using JetBrains.Annotations;

namespace GraphZen.Configuration.Interfaces.Fields.Description
{
    public class Interface_Field_ViaClrProperty_Description : Interface_Field_Description,
        ILeafConventionConfigurationFixture
    {
        [GraphQLName(Grandparent)]
        public interface IExampleInterface
        {
            [Description(DataAnnotationDescriptionValue)]
            string ExampleField { get; set; }
        }

        public const string DataAnnotationDescriptionValue = nameof(DataAnnotationDescriptionValue);

        public LeafConventionContext GetContext() =>
            new LeafConventionContext
            {
                ParentName = nameof(IExampleInterface.ExampleField).FirstCharToLower(),
                DataAnnotationValue = DataAnnotationDescriptionValue
            };

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