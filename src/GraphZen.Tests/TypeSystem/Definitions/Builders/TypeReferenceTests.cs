// Copyright (c) GraphZen LLC. All rights reserved.
// Licensed under the GraphZen Community License. See the LICENSE file in the project root for license information.

using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using FluentAssertions;
using GraphZen.Infrastructure;
using GraphZen.TypeSystem.Internal;
using JetBrains.Annotations;
using Xunit;
#nullable disable


namespace GraphZen.TypeSystem.Builders
{
    public class TypeReferenceTests
    {
        private class Foo
        {
        }

        [GraphQLName("Baz")]
        [UsedImplicitly]
        private class Bar
        {
        }


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

            var query = schema.GetType<ObjectType>("Query");

            query.FindField("fooNNClr")
                .FieldType.As<NonNullType>().OfType.As<ObjectType>().ClrType
                .Should().Be(typeof(Foo));
            query.FindField("fooNN")
                .FieldType.As<NonNullType>().OfType.As<ObjectType>().ClrType
                .Should().Be(typeof(Foo));
            query.FindField("foo")
                .FieldType.As<ObjectType>().ClrType
                .Should().Be(typeof(Foo));
            query.FindField("fooList")
                .FieldType.As<ListType>().OfType.As<ObjectType>().ClrType
                .Should().Be(typeof(Foo));

            query.FindField("fooClrList")
                .FieldType
                .As<NonNullType>().OfType
                .As<ListType>().OfType
                .As<NonNullType>().OfType
                .As<ObjectType>().ClrType
                .Should().Be(typeof(Foo));
        }
    }
}