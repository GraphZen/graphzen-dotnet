// Copyright (c) GraphZen LLC. All rights reserved.
// Licensed under the GraphZen Community License. See the LICENSE file in the project root for license information.

using System.ComponentModel;
using GraphZen.Infrastructure;
using GraphZen.TypeSystem.Tests.Configuration.Infrastructure;

namespace GraphZen.TypeSystem.Tests.Configuration.InputObjects.Fields.Description;

// ReSharper disable once InconsistentNaming
public class InputObject_Field_ViaClrProperty_Description : InputObject_Field_Description,
    ILeafConventionConfigurationFixture
{
    public const string DataAnnotationDescriptionValue = nameof(DataAnnotationDescriptionValue);

    public LeafConventionContext GetContext() =>
        new()
        {
            ParentName = nameof(ExampleInputObject.ExampleField).FirstCharToLower(),
            DataAnnotationValue = DataAnnotationDescriptionValue
        };

    public void ConfigureContextConventionally(SchemaBuilder sb)
    {
        sb.InputObject<ExampleInputObject>();
    }

    public void ConfigureClrContext(SchemaBuilder sb, string parentName)
    {
        sb.InputObject<ExampleInputObject>();
    }

    [GraphQLName(Grandparent)]
    public class ExampleInputObject
    {
        [Description(DataAnnotationDescriptionValue)]
        public string? ExampleField { get; set; }
    }
}
