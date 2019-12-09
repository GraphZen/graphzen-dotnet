// Copyright (c) GraphZen LLC. All rights reserved.
// Licensed under the GraphZen Community License. See the LICENSE file in the project root for license information.

using System.Diagnostics.CodeAnalysis;
using FluentAssertions;
using GraphZen.Infrastructure;
using JetBrains.Annotations;
using Xunit;

namespace GraphZen.TypeSystem.FunctionalTests.Objects
{
    [NoReorder]
    public class ObjectTypeCreationTests
    {
        [Fact]
        public void it_can_create_object_type_by_name()
        {
            var schema = Schema.Create(_ => _.Object("Foo"));
            var foo = schema.GetObject("Foo");
            foo.Should().BeOfType<ObjectType>();
        }

        public class Bar
        {
        }

        [Fact]
        public void it_can_create_object_type_with_clr_type()
        {
            var schema = Schema.Create(_ => _.Object(typeof(Bar)));
            var bar = schema.GetObject(nameof(Bar));
            bar.Should().BeOfType<ObjectType>();
        }

        [Fact]
        public void it_can_create_object_type_with_clr_type_via_generic_parameter()
        {
            var schema = Schema.Create(_ => _.Object<Bar>());
            var bar = schema.GetObject(nameof(Bar));
            bar.Should().BeOfType<ObjectType>();
        }

        [GraphQLName("CustomBaz")]
        public class Baz
        {
        }

        [Fact]
        public void it_can_create_object_type_with_custom_name_with_clr_type()
        {
            var schema = Schema.Create(_ => _.Object(typeof(Baz)));
            var baz = schema.GetObject("CustomBaz");
            baz.Should().BeOfType<ObjectType>();
            baz.Name.Should().Be("CustomBaz");
        }

        [Fact]
        public void it_can_create_object_type_from_generic_parameter_with_custom_name()
        {
            var schema = Schema.Create(_ => _.Object<Baz>());
            var baz = schema.GetObject("CustomBaz");
            baz.Should().BeOfType<ObjectType>();
            baz.Name.Should().Be("CustomBaz");
        }

        [Fact]
        public void it_can_create_object_type_from_sdl()
        {
            var schema = Schema.Create(@"type Foo");
            var foo = schema.GetObject("Foo");
            foo.Should().BeOfType<ObjectType>();
            foo.Name.Should().Be("Foo");
        }
    }
}