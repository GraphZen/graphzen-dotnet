// Copyright (c) GraphZen LLC. All rights reserved.
// Licensed under the GraphZen Community License. See the LICENSE file in the project root for license information.

using GraphZen.Infrastructure;

namespace GraphZen.TypeSystem.Tests.Configuration.InputObjects.ClrType;

[NoReorder]
public class InputObjectClrTypeConfigurationTests
{
    public class ExampleInputObject
    {
    }

    [Fact]
    public void
        input_object_added_explicitly_subsequently_referenced_by_matching_clr_type_should_have_clr_type_set()
    {
        var schema = Schema.Create(sb =>
        {
            sb.InputObject(nameof(ExampleInputObject));
            var def = sb.GetDefinition().GetInputObject(nameof(ExampleInputObject));
            sb.InputObject<ExampleInputObject>();
            Assert.Equal(typeof(ExampleInputObject), def.ClrType);
        });
        Assert.Equal(typeof(ExampleInputObject), schema.GetInputObject<ExampleInputObject>().ClrType);
    }

    [Fact]
    public void
        input_object_added_explicitly_subsequently_referenced_by_matching_clr_type_via_field_should_have_clr_type_set()
    {
        var schema = Schema.Create(sb =>
        {
            sb.InputObject(nameof(ExampleInputObject));
            sb.InputObject("ParentObject").Field<ExampleInputObject>("field");
            var def = sb.GetDefinition().GetInputObject(nameof(ExampleInputObject));
            sb.InputObject<ExampleInputObject>();
            Assert.Equal(typeof(ExampleInputObject), def.ClrType);
        });
        Assert.Equal(typeof(ExampleInputObject), schema.GetInputObject<ExampleInputObject>().ClrType);
    }
}