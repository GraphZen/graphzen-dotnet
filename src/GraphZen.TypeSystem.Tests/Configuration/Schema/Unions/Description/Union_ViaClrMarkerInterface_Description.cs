// Copyright (c) GraphZen LLC. All rights reserved.
// Licensed under the GraphZen Community License. See the LICENSE file in the project root for license information.

using System.ComponentModel;
using System.Diagnostics.CodeAnalysis;
using GraphZen.Infrastructure;
using GraphZen.TypeSystem;
using JetBrains.Annotations;

namespace GraphZen.Unions.Description
{
    public class Union_ViaClrMarkerInterface_Description : Union_Description, ILeafConventionConfigurationFixture
    {
        public const string DataAnnotationDescriptionValue = nameof(DataAnnotationDescriptionValue);

        public LeafConventionContext GetContext()
        {
            return new LeafConventionContext
            {
                ParentName = nameof(IExampleUnion),
                DataAnnotationValue = DataAnnotationDescriptionValue
            };
        }

        public void ConfigureContextConventionally(SchemaBuilder sb)
        {
            sb.Object<Query>();
        }

        public void ConfigureClrContext(SchemaBuilder sb, string parentName)
        {
            sb.Object<Query>();
        }

        public class Query
        {
            public IExampleUnion? ExampleUnion { get; set; }
        }

        [Description(DataAnnotationDescriptionValue)]
        [GraphQLUnion]
        public interface IExampleUnion
        {
        }
    }
}