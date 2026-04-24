// Copyright (c) GraphZen LLC. All rights reserved.
// Licensed under the GraphZen Community License. See the LICENSE file in the project root for license information.

using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using GraphZen.Infrastructure;
using JetBrains.Annotations;
using Xunit;

namespace GraphZen.TypeSystem.Tests;

public class TypeReferenceTests
{
    [Fact]
    public void ObjectFieldsEndUpWithCorrectTypes()
    {
        var schema = Schema.Create(_ =>
        {
            _.Object("Bar");
            _.Object<Foo>();
            _.Object("Query")
                .Field<Foo>("fooNNClr")
                .Field("fooNN", "Foo!")
                .Field("foo", "Foo")
                .Field("foo", "Foo")
                .Field("fooList", "[Foo]")
                .Field<List<Foo>>("fooClrList");
        });

        var query = schema.GetObject("Query");

        Assert.Equal(typeof(Foo),
            ((ObjectType)((NonNullType)query.FindField("fooNNClr")!.FieldType).OfType).ClrType);
        Assert.Equal(typeof(Foo),
            ((ObjectType)((NonNullType)query.FindField("fooNN")!.FieldType).OfType).ClrType);
        Assert.Equal(typeof(Foo),
            ((ObjectType)query.FindField("foo")!.FieldType).ClrType);
        Assert.Equal(typeof(Foo),
            ((ObjectType)((ListType)query.FindField("fooList")!.FieldType).OfType).ClrType);

        Assert.Equal(typeof(Foo),
            ((ObjectType)((NonNullType)((ListType)((NonNullType)query.FindField("fooClrList")!
                .FieldType).OfType).OfType).OfType).ClrType);
    }

    private class Foo
    {
    }

    [GraphQLName("Baz")]
    [UsedImplicitly]
    private class Bar
    {
    }
}