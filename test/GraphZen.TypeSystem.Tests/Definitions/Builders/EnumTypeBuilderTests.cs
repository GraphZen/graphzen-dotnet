// Copyright (c) GraphZen LLC. All rights reserved.
// Licensed under the GraphZen Community License. See the LICENSE file in the project root for license information.

using System.ComponentModel;
using GraphZen.Infrastructure;

namespace GraphZen.TypeSystem.Tests;

[NoReorder]
[SuppressMessage("ReSharper", "UnusedMember.Local")]
public class EnumTypeBuilderTests
{
    private enum FooEnum
    {
        [Description("bar desc")] Bar,
        [GraphQLName("customBaz")] Baz
    }


    [Fact]
    public void EnumCreatedWithClrTypeInfersValues()
    {
        var schema = Schema.Create(sb => sb.Enum<FooEnum>());
        var values = schema.FindType<EnumType>(typeof(FooEnum))!.GetValues().ToReadOnlyList();
        Assert.Equal(2, values.Count);
        Assert.Equal(nameof(FooEnum.Bar), values[0].Name);
        Assert.Equal("bar desc", values[0].Description);
        Assert.Equal("customBaz", values[1].Name);
    }
}