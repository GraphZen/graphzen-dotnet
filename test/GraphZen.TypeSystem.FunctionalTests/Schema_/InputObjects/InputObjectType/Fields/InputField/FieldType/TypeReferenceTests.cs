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


        [Spec(nameof(it_can_be_created_if_type_matches_own_io_identity))]
        [Fact]
        public void it_can_be_created_if_type_matches_own_io_identity_()
        {
            var schema = Schema.Create(_ =>
            {
                _.InputObject("Foo");
                _.InputObject("Bar").Field("field", "Foo");
            });
            var foo = schema.GetInputObject("Foo");
            schema.GetInputObject("Bar").GetField("field").FieldType.GetNamedType().Should().Be(foo);
        }


        [Spec(nameof(it_can_be_created_with_clr_type_if_type_matches_own_io_identity))]
        [Fact]
        public void it_can_be_created_with_clr_type_if_type_matches_own_io_identity_()
        {
            var schema = Schema.Create(_ =>
            {
                _.InputObject<PlainClass>();
                _.InputObject("Bar").Field<PlainClass>("field");
            });
            var foo = schema.GetInputObject<PlainClass>();
            schema.GetInputObject("Bar").GetField("field").FieldType.GetNamedType().Should().Be(foo);
        }


        [Spec(nameof(it_cannot_be_created_if_type_conflicts_with_own_io_identity))]
        [Fact]
        public void it_cannot_be_created_if_type_conflicts_with_own_io_identity_()
        {
            Schema.Create(_ =>
            {
                _.Object("Foo");
                var bar = _.InputObject("Bar");
                Action add = () => bar.Field("baz", "[Foo]!");
                add.Should().Throw<InvalidTypeException>().WithMessage("Cannot create input field baz on input object Bar with type '[Foo]!': object Foo is only an output type and input fields can only use input types.");
            });
        }


        [Spec(nameof(it_cannot_be_created_with_clr_type_if_type_conflicts_with_own_io_identity))]
        [Fact]
        public void it_cannot_be_created_with_clr_type_if_type_conflicts_with_own_io_identity_()
        {
            Schema.Create(_ =>
            {
                _.Object<PlainClass>("Foo"); var bar = _.InputObject("Bar"); Action add = () => bar.Field<PlainClass>("baz");
                add.Should().Throw<InvalidTypeException>().WithMessage("Cannot create input field baz on input object Bar with type 'Foo!': object Foo (CLR class: PlainClass) is only an output type and input fields can only use input types.");
            });
        }


        [Spec(nameof(type_can_be_set_if_type_matches_own_io_identity))]
        [Fact(Skip = "TODO")]
        public void type_can_be_set_if_type_matches_own_io_identity_()
        {
            // var schema = Schema.Create(_ => { });
        }


        [Spec(nameof(type_can_be_set_with_clr_type_if_type_matches_own_io_identity))]
        [Fact(Skip = "TODO")]
        public void type_can_be_set_with_clr_type_if_type_matches_own_io_identity_()
        {
            // var schema = Schema.Create(_ => { });
        }


        [Spec(nameof(type_cannot_be_set_if_type_conflicts_with_own_io_identity))]
        [Fact(Skip = "TODO")]
        public void type_cannot_be_set_if_type_conflicts_with_own_io_identity_()
        {
            // var schema = Schema.Create(_ => { });
        }


        [Spec(nameof(type_cannot_be_set__with_clr_type_if_type_conflicts_with_own_io_identity))]
        [Fact(Skip = "TODO")]
        public void type_cannot_be_set__with_clr_type_if_type_conflicts_with_own_io_identity_()
        {
            // var schema = Schema.Create(_ => { });
        }
    }
}