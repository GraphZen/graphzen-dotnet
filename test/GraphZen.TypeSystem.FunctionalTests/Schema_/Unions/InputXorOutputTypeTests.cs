// Copyright (c) GraphZen LLC. All rights reserved.
// Licensed under the GraphZen Community License. See the LICENSE file in the project root for license information.

using System;
using System.Diagnostics.CodeAnalysis;
using FluentAssertions;
using GraphZen.Infrastructure;
using GraphZen.TypeSystem.FunctionalTests.Specs;
using JetBrains.Annotations;
using Xunit;

namespace GraphZen.TypeSystem.FunctionalTests.Schema_.Unions
{
    [NoReorder]
    public class InputXorOutputTypeTests
    {
        public abstract class PlainAbstractClass
        {
        }

        [GraphQLName(AnnotatedNameValue)]
        public abstract class PlainAbstractClassAnnotatedName
        {
            public const string AnnotatedNameValue = nameof(AnnotatedNameValue);
        }

        [GraphQLName("(*&#")]
        public abstract class PlainAbstractClassInvalidNameAnnotation
        {
        }

        [Spec(nameof(TypeSystemSpecs.InputXorOutputTypeSpecs
            .cannot_create_type_if_name_conflicts_with_type_identity_of_opposite_io))]
        [Fact]
        public void named_item_cannot_be_added_if_name_conflicts_with_type_identity_of_opposite_io_()
        {
            Schema.Create(_ =>
            {
                _.InputObject("Foo").Field("inputField", "Bar");
                Action add = () => _.Union("Bar");
                add.Should().Throw<InvalidTypeException>().WithMessage(
                    "Cannot create union Bar: Union types are output types and an input field or argument already references a type named 'Bar'. GraphQL input type references are reserved for scalar, enum, or input object types.");
            });
        }


        [Spec(nameof(TypeSystemSpecs.InputXorOutputTypeSpecs
            .cannot_create_type_via_clr_type_if_name_annotation_conflicts_with_type_identity_of_opposite_io))]
        [Fact]
        public void named_item_cannot_be_renamed_to_name_conflicts_with_type_identity_of_opposite_io_()
        {
            Schema.Create(_ =>
            {
                _.InputObject("Foo").Field("inputField", "Bar");
                var baz = _.Union("Baz");
                Action rename = () => baz.Name("Bar");
                rename.Should().Throw<InvalidTypeException>().WithMessage(
                    "Cannot rename union Baz to \"Bar\": Union types are output types and an input field or argument already references a type named \"Bar\". GraphQL input type references are reserved for scalar, enum, or input object types.");
            });
        }


        [Spec(nameof(TypeSystemSpecs.InputXorOutputTypeSpecs
            .cannot_set_clr_type_if_custom_name_conflicts_with_type_identity_of_opposite_io))]
        [Fact]
        public void clr_type_cannot_be_added_with_custom_name_if_name_conflicts_with_type_identity_of_opposite_io_()
        {
            Schema.Create(_ =>
            {
                _.InputObject("Foo").Field("inputField", "Bar");
                Action add = () => _.Union<PlainAbstractClass>("Bar");
                add.Should().Throw<InvalidTypeException>().WithMessage(
                    "Cannot create union Bar: Union types are output types and an input field or argument already references a type named 'Bar'. GraphQL input type references are reserved for scalar, enum, or input object types.");

            });

        }



        [Spec(nameof(TypeSystemSpecs.InputXorOutputTypeSpecs
            .DEPRECATED_clr_typed_item_cannot_be_renamed_if_name_conflicts_with_type_identity_of_opposite_io))]
        [Fact()]
        public void clr_typed_item_cannot_be_renamed_if_name_conflicts_with_type_identity_of_opposite_io_()
        {
            Schema.Create(_ =>
            {
                _.InputObject("Foo").Field("inputField", "Bar");
                var union = _.Union<PlainAbstractClass>();
                Action rename = () => union.Name("Bar");
                rename.Should().Throw<InvalidTypeException>().WithMessage(
                    "Cannot rename union PlainAbstractClass to \"Bar\": Union types are output types and an input field or argument already references a type named \"Bar\". GraphQL input type references are reserved for scalar, enum, or input object types.");
            });
        }


        [Spec(nameof(
            TypeSystemSpecs.InputXorOutputTypeSpecs
                .DEPRECATED_cannot_add_clr_typed_with_name_attribute_if_conflicts_with_type_identity_of_opposite_io
        ))]
        [Fact(Skip = "TODO")]
        public void
            clr_typed_item_with_name_attribute_cannot_be_added_if_name_attribute_conflicts_with_type_identity_of_opposite_io_()
        {
            // var schema = Schema.Create(_ => { });
        }


        [Spec(nameof(TypeSystemSpecs.InputXorOutputTypeSpecs
            .cannot_create_type_via_clr_type_if_custom_name_conflicts_with_type_identity_of_opposite_io
        ))]
        [Fact(Skip = "TODO")]
        public void
            clr_typed_item_cannot_be_added_with_custom_name_if_name_conflicts_with_type_identity_of_opposite_io_()
        {
            // var schema = Schema.Create(_ => { });
        }


        [Spec(nameof(TypeSystemSpecs.InputXorOutputTypeSpecs
            .DEPRECATED_clr_type_cannot_be_changed_with_custom_name_if_name_conflicts_with_type_identity_of_opposite_io))]
        [Fact(Skip = "TODO")]
        public void clr_type_cannot_be_changed_with_custom_name_if_name_conflicts_with_type_identity_of_opposite_io_()
        {
            // var schema = Schema.Create(_ => { });
        }
    }
}