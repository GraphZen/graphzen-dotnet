// Copyright (c) GraphZen LLC. All rights reserved.
// Licensed under the GraphZen Community License. See the LICENSE file in the project root for license information.

using System;
using System.Diagnostics.CodeAnalysis;
using FluentAssertions;
using GraphZen.Infrastructure;
using JetBrains.Annotations;
using Xunit;
using static GraphZen.TypeSystem.FunctionalTests.Specs.TypeSystemSpecs.TypeReferenceSpecs;

namespace GraphZen.TypeSystem.FunctionalTests.Schema_.Objects.ObjectType.Fields.Field.FieldType
{
    [NoReorder]
    public class TypeReferenceTests
    {
        public class PlainClass
        {
        }

        [Spec(nameof(it_can_be_created_if_type_io_compatible))]
        [Fact]
        public void it_can_be_created_if_type_io_compatible_()
        {
            var schema = Schema.Create(_ =>
            {
                _.Interface("Foo");
                _.Object("Bar").Field("arg", "Foo");
            });
            var foo = schema.GetInterface("Foo");
            schema.GetObject("Bar").GetField("arg").FieldType.GetNamedType().Should().Be(foo);
        }

        [Spec(nameof(it_cannot_be_created_if_type_has_io_conflict))]
        [Fact]
        public void it_cannot_be_created_if_type_has_io_conflict_()
        {
            Schema.Create(_ =>
            {
                _.InputObject("Foo");
                var bar = _.Object("Bar");
                Action add = () => bar.Field("baz", "[Foo]!");
                add.Should().Throw<InvalidTypeException>().WithMessage(
                    "Cannot create object field Bar.baz with field type '[Foo]!'. Object fields can only use output types. Input object Foo is only an input type.");
            });
        }


        [Spec(nameof(it_can_be_created_with_clr_type_if_type_io_compatible))]
        [Fact]
        public void it_can_be_created_with_clr_type_if_type_io_compatible_()
        {
            var schema = Schema.Create(_ =>
            {
                _.Object<PlainClass>();
                _.Object("Bar").Field<PlainClass>("field");
            });
            var foo = schema.GetObject<PlainClass>();
            schema.GetObject("Bar").GetField("field").FieldType.GetNamedType().Should().Be(foo);
        }


        [Spec(nameof(it_cannot_be_created_with_clr_type_if_type_has_io_conflict))]
        [Fact]
        public void it_cannot_be_created_with_clr_type_if_type_has_io_conflict_()
        {
            Schema.Create(_ =>
            {
                _.InputObject<PlainClass>("Foo");
                var bar = _.Object("Bar");
                Action add = () => bar.Field<PlainClass>("baz");
                add.Should().Throw<InvalidTypeException>().WithMessage(
                    "Cannot create object field Bar.baz with field type 'Foo!'. Object fields can only use output types. Input object Foo (CLR class: PlainClass) is only an input type.");
            });
        }


        [Spec(nameof(type_can_be_set_if_type_io_compatible))]
        [Fact]
        public void type_can_be_set_if_type_matches_own_io_identity_()
        {
            var schema = Schema.Create(_ =>
            {
                _.Interface("Foo");
                _.Object("Bar").Field("field", "Baz", f => f.FieldType("Foo"));
            });
            var foo = schema.GetInterface("Foo");
            schema.GetObject("Bar").GetField("field").FieldType.GetNamedType().Should().Be(foo);
        }


        [Spec(nameof(type_cannot_be_set_if_type_has_io_conflict))]
        [Fact]
        public void type_cannot_be_set_if_type_has_io_conflict_()
        {
            Schema.Create(_ =>
            {
                _.InputObject("Foo");
                _.Object("Bar").Field("field", "Baz", f =>
                {
                    Action set = () => f.FieldType("Foo");
                    set.Should().Throw<InvalidTypeException>().WithMessage(
                        "Cannot set field type to 'Foo' on object field Bar.field. Object fields can only use output types. Input object Foo is only an input type.");
                });
            });
        }

        [Spec(nameof(type_can_be_set_with_clr_type_if_type_io_compatible))]
        [Fact]
        public void type_can_be_set_with_clr_type_if_type_io_compatible_()
        {
            var schema = Schema.Create(_ =>
            {
                _.Object<PlainClass>();
                _.Object("Bar").Field("field", "Baz", f => f.FieldType<PlainClass>());
            });
            var foo = schema.GetObject<PlainClass>();
            schema.GetObject("Bar").GetField("field").FieldType.GetNamedType().Should().Be(foo);
        }


        [Spec(nameof(type_cannot_be_set_with_clr_type_if_type_has_io_conflict))]
        [Fact]
        public void type_cannot_be_set__with_clr_type_if_type_conflicts_with_own_io_identity_()
        {
            Schema.Create(_ =>
            {
                _.InputObject<PlainClass>();
                _.Object("Bar").Field("field", "Baz", f =>
                {
                    Action set = () => f.FieldType<PlainClass>();
                    set.Should().Throw<InvalidTypeException>().WithMessage(
                        "Cannot set field type to 'PlainClass!' on object field Bar.field. Object fields can only use output types. Input object PlainClass is only an input type.");
                });
            });
        }


        [Spec(nameof(type_can_be_set_via_parent_redefinition_if_type_io_compatible))]
        [Fact]
        public void type_can_be_set_via_parent_redefinition_if_type_io_compatible_()
        {
            var schema = Schema.Create(_ =>
            {
                _.Interface("Foo");
                _.Object("Bar").Field("field", "Bar");
                _.Object("Bar").Field("field", "Foo");
            });
            var foo = schema.GetInterface("Foo");
            schema.GetObject("Bar").GetField("field").FieldType.GetNamedType().Should().Be(foo);
        }


        [Spec(nameof(type_cannot_be_set_via_parent_redefinition_if_type_has_io_conflict))]
        [Fact]
        public void type_cannot_be_set_via_parent_redefinition_if_type_has_io_conflict_()
        {
            Schema.Create(_ =>
            {
                _.InputObject("Foo");
                var bar = _.Object("Bar").Field("field", "Bar");
                Action redefine = () => bar.Field("field", "Foo");
                redefine.Should().Throw<InvalidTypeException>().WithMessage(
                    "Cannot set field type to 'Foo' on object field Bar.field. Object fields can only use output types. Input object Foo is only an input type.");
            });
        }


        [Spec(nameof(type_can_be_set_via_parent_redefinition_with_clr_type_if_type_io_compatible))]
        [Fact]
        public void type_can_be_set_via_parent_redefinition_with_clr_type_if_type_io_compatible_()
        {
            var schema = Schema.Create(_ =>
            {
                _.Object<PlainClass>();
                _.Object("Bar").Field("field", "Bar");
                _.Object("Bar").Field<PlainClass>("field");
            });
            var typed = schema.GetObject<PlainClass>();
            schema.GetObject("Bar").GetField("field").FieldType.GetNamedType().Should().Be(typed);
        }


        [Spec(nameof(type_cannot_be_set_via_parent_redefinition_with_clr_type_if_type_has_io_conflict))]
        [Fact]
        public void type_cannot_be_set_via_parent_redefinition_with_clr_type_if_type_has_io_conflict_()
        {
            Schema.Create(_ =>
            {
                _.InputObject<PlainClass>();
                var bar = _.Object("Bar").Field("field", "Bar");
                Action redefine = () => bar.Field<PlainClass>("field");
                redefine.Should().Throw<InvalidTypeException>().WithMessage(
                    "Cannot set field type to 'PlainClass!' on object field Bar.field. Object fields can only use output types. Input object PlainClass is only an input type.");
            });
        }
    }
}