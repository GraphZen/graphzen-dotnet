// Copyright (c) GraphZen LLC. All rights reserved.
// Licensed under the GraphZen Community License. See the LICENSE file in the project root for license information.

using System.ComponentModel;
using GraphZen.TypeSystem.Tests.Configuration.Infrastructure;

namespace GraphZen.TypeSystem.Tests.Configuration.Objects.Fields.Arguments.Description;

// ReSharper disable once InconsistentNaming
public class Object_Field_Argument_ViaClrMethod_Description : Object_Field_Argument_Description,
    ILeafConventionConfigurationFixture
{
    public const string DataAnnotationDescriptionValue = nameof(DataAnnotationDescriptionValue);

    public LeafConventionContext GetContext() =>
        new() { ParentName = "argName", DataAnnotationValue = DataAnnotationDescriptionValue };

    public void ConfigureContextConventionally(SchemaBuilder sb)
    {
        sb.Object<ExampleObject>();
    }

    public void ConfigureClrContext(SchemaBuilder sb, string parentName)
    {
        sb.Object<ExampleObject>();
    }

    [GraphQLName(GreatGrandparent)]
    public class ExampleObject
    {
        [GraphQLName(Grandparent)]
        public string ExampleField([Description(DataAnnotationDescriptionValue)] string argName) =>
            throw new NotImplementedException();
    }
}
