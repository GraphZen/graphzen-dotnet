// Copyright (c) GraphZen LLC. All rights reserved.
// Licensed under the GraphZen Community License. See the LICENSE file in the project root for license information.

using System.ComponentModel;
using GraphZen.Infrastructure;
using GraphZen.TypeSystem.Tests.Configuration.Infrastructure;

namespace GraphZen.TypeSystem.Tests.Configuration.Objects.Fields.Description;

// ReSharper disable once InconsistentNaming
public class Object_Field_ViaClrProperty_Description : Object_Field_Description, ILeafConventionConfigurationFixture
{
    public const string DataAnnotationDescriptionValue = nameof(DataAnnotationDescriptionValue);

    public LeafConventionContext GetContext() =>
        new()
        {
            ParentName = nameof(ExampleObject.ExampleField).FirstCharToLower(),
            DataAnnotationValue = DataAnnotationDescriptionValue
        };

    public void ConfigureContextConventionally(SchemaBuilder sb)
    {
        sb.Object<ExampleObject>();
    }

    public void ConfigureClrContext(SchemaBuilder sb, string parentName)
    {
        sb.Object<ExampleObject>();
    }

    [GraphQLName(Grandparent)]
    public class ExampleObject
    {
        [Description(DataAnnotationDescriptionValue)]
        public string? ExampleField { get; set; }
    }
}