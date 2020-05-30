// Copyright (c) GraphZen LLC. All rights reserved.
// Licensed under the GraphZen Community License. See the LICENSE file in the project root for license information.

using System;
using System.Diagnostics.CodeAnalysis;
using FluentAssertions;
using GraphZen.Infrastructure;
using JetBrains.Annotations;
using Xunit;
using static GraphZen.TypeSystem.FunctionalTests.Specs.TypeSystemSpecs.TypeReferenceSpecs;

namespace GraphZen.TypeSystem.FunctionalTests.Schema_.InputObjects.InputObjectType.Fields.InputField.FieldType
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
                _.InputObject("Foo");
                _.InputObject("Bar").Field("field", "Foo");
            });
            var foo = schema.GetInputObject("Foo");
            schema.GetInputObject("Bar").GetField("field").FieldType.GetNamedType().Should().Be(foo);
        }

        [Spec(nameof(it_cannot_be_created_if_type_has_io_conflict))]
        [Fact]
        public void it_cannot_be_created_if_type_has_io_conflict_()
        {
            Schema.Create(_ =>
            {
                _.Object("Foo");
                var bar = _.InputObject("Bar");
                Action add = () => bar.Field("baz", "[Foo]!");
                add.Should().Throw<InvalidTypeException>().WithMessage(
                    "Cannot create input object field Bar.baz with field type '[Foo]!'. object Foo is only an output type and input object fields can only use input types.");
            });
        }


        [Spec(nameof(it_can_be_created_with_clr_type_if_type_io_compatible))]
        [Fact]
        public void it_can_be_created_with_clr_type_if_type_io_compatible_()
        {
            var schema = Schema.Create(_ =>
            {
                _.InputObject<PlainClass>();
                _.InputObject("Bar").Field<PlainClass>("field");
            });
            var foo = schema.GetInputObject<PlainClass>();
            schema.GetInputObject("Bar").GetField("field").FieldType.GetNamedType().Should().Be(foo);
        }


        [Spec(nameof(it_cannot_be_created_with_clr_type_if_type_has_io_conflict))]
        [Fact]
        public void it_cannot_be_created_with_clr_type_if_type_has_io_conflict_()
        {
            Schema.Create(_ =>
            {
                _.Object<PlainClass>("Foo");
                var bar = _.InputObject("Bar");
                Action add = () => bar.Field<PlainClass>("baz");
                add.Should().Throw<InvalidTypeException>().WithMessage(
                    "Cannot create input object field Bar.baz with field type 'Foo!'. object Foo (CLR class: PlainClass) is only an output type and input object fields can only use input types.");
            });
        }


        [Spec(nameof(type_can_be_set_if_type_io_compatible))]
        [Fact]
        public void type_can_be_set_if_type_matches_own_io_identity_()
        {
            var schema = Schema.Create(_ =>
            {
                _.InputObject("Foo");
                _.InputObject("Bar").Field("field", "Baz", f => f.FieldType("Foo"));
            });
            var foo = schema.GetInputObject("Foo");
            schema.GetInputObject("Bar").GetField("field").FieldType.GetNamedType().Should().Be(foo);
        }


        [Spec(nameof(type_cannot_be_set_if_type_has_io_conflict))]
        [Fact]
        public void type_cannot_be_set_if_type_conflicts_with_own_io_identity_()
        {
            Schema.Create(_ =>
            {
                _.Object("Foo");
                _.InputObject("Bar").Field("field", "Baz", f =>
                {
                    Action set = () => f.FieldType("Foo");
                    set.Should().Throw<InvalidTypeException>().WithMessage(
                        "Cannot set field type to 'Foo' on input object field Bar.field. Object Foo is only an output type and input object fields can only use input types.");
                });
            });
        }

        [Spec(nameof(type_can_be_set_with_clr_type_if_type_io_compatible))]
        [Fact(Skip = "wip")]
        public void type_can_be_set_with_clr_type_if_type_matches_own_io_identity_()
        {
            var schema = Schema.Create(_ =>
            {
                _.InputObject("Foo");
                _.InputObject("Bar").Field("field", "Baz");
                _.InputObject("Bar").Field("field", "Foo");
            });
            var foo = schema.GetInputObject("Foo");
            schema.GetInputObject("Bar").GetField("field").FieldType.GetNamedType().Should().Be(foo);
        }


        [Spec(nameof(type_cannot_be_set_with_clr_type_if_type_has_io_conflict))]
        [Fact(Skip = "TODO")]
        public void type_cannot_be_set__with_clr_type_if_type_conflicts_with_own_io_identity_()
        {
            // var schema = Schema.Create(_ => { });
        }


        [Spec(nameof(type_can_be_set_via_parent_redefinition_if_type_io_compatible))]
        [Fact(Skip = "TODO")]
        public void type_can_be_set_via_parent_redefinition_if_type_io_compatible_()
        {
            // var schema = Schema.Create(_ => { });
        }


        [Spec(nameof(type_cannot_be_set_via_parent_redefinition_if_type_has_io_conflict))]
        [Fact(Skip = "TODO")]
        public void type_cannot_be_set_via_parent_redefinition_if_type_has_io_conflict_()
        {
            // var schema = Schema.Create(_ => { });
        }


        [Spec(nameof(type_can_be_set_via_parent_redefinition_with_clr_type_if_type_io_compatible))]
        [Fact(Skip = "TODO")]
        public void type_can_be_set_via_parent_redefinition_with_clr_type_if_type_io_compatible_()
        {
            // var schema = Schema.Create(_ => { });
        }


        [Spec(nameof(type_cannot_be_set_via_parent_redefinition_with_clr_type_if_type_has_io_conflict))]
        [Fact(Skip = "TODO")]
        public void type_cannot_be_set_via_parent_redefinition_with_clr_type_if_type_has_io_conflict_()
        {
            // var schema = Schema.Create(_ => { });
        }
    }
}