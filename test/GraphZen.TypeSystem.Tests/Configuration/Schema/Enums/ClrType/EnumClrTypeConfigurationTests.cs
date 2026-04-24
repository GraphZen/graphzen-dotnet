// Copyright (c) GraphZen LLC. All rights reserved.
// Licensed under the GraphZen Community License. See the LICENSE file in the project root for license information.

using GraphZen.Infrastructure;

namespace GraphZen.TypeSystem.Tests.Configuration.Enums.ClrType;

[NoReorder]
public class EnumClrTypeConfigurationTests
{
    public enum ExampleEnum
    {
    }

    [Fact]
    public void enum_added_explicitly_subsequently_referenced_by_matching_clr_type_should_have_clr_type_set()
    {
        var schema = Schema.Create(sb =>
        {
            sb.Enum(nameof(ExampleEnum));
            var def = sb.GetDefinition().GetEnum(nameof(ExampleEnum));
            sb.Enum<ExampleEnum>();
            Assert.Equal(typeof(ExampleEnum), def.ClrType);
        });
        Assert.Equal(typeof(ExampleEnum), schema.GetEnum<ExampleEnum>().ClrType);
    }

    [Fact]
    public void
        enum_added_explicitly_subsequently_referenced_by_matching_clr_type_via_field_should_have_clr_type_set()
    {
        var schema = Schema.Create(sb =>
        {
            sb.Enum(nameof(ExampleEnum));
            sb.Object("Parent").Field<ExampleEnum>("field");
            var def = sb.GetDefinition().GetEnum(nameof(ExampleEnum));
            sb.Enum<ExampleEnum>();
            Assert.Equal(typeof(ExampleEnum), def.ClrType);
        });
        Assert.Equal(typeof(ExampleEnum), schema.GetEnum<ExampleEnum>().ClrType);
    }
}