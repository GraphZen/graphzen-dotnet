// Copyright (c) GraphZen LLC. All rights reserved.
// Licensed under the GraphZen Community License. See the LICENSE file in the project root for license information.

using System.Diagnostics.CodeAnalysis;
using GraphZen.Infrastructure;
using JetBrains.Annotations;
using Xunit;

namespace GraphZen.TypeSystem.Tests.Configuration.Unions.ClrType;

[NoReorder]
public class UnionClrTypeConfigurationTests
{
    public class ExampleUnion
    {
    }

    [Fact]
    public void union_added_explicitly_subsequently_referenced_by_matching_clr_type_should_have_clr_type_set()
    {
        var schema = Schema.Create(sb =>
        {
            sb.Union(nameof(ExampleUnion));
            var def = sb.GetDefinition().GetUnion(nameof(ExampleUnion));
            sb.Union<ExampleUnion>();
            Assert.Equal(typeof(ExampleUnion), def.ClrType);
        });
        Assert.Equal(typeof(ExampleUnion), schema.GetUnion<ExampleUnion>().ClrType);
    }

    [Fact]
    public void
        union_added_explicitly_subsequently_referenced_by_matching_clr_type_via_field_should_have_clr_type_set()
    {
        var schema = Schema.Create(sb =>
        {
            sb.Union(nameof(ExampleUnion));
            sb.Object("Parent").Field<ExampleUnion>("field");
            var def = sb.GetDefinition().GetUnion(nameof(ExampleUnion));
            sb.Union<ExampleUnion>();
            Assert.Equal(typeof(ExampleUnion), def.ClrType);
        });
        Assert.Equal(typeof(ExampleUnion), schema.GetUnion<ExampleUnion>().ClrType);
    }
}