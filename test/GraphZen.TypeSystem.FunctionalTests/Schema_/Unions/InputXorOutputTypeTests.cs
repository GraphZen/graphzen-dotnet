// Copyright (c) GraphZen LLC. All rights reserved.
// Licensed under the GraphZen Community License. See the LICENSE file in the project root for license information.

using System;
using System.Diagnostics.CodeAnalysis;
using FluentAssertions;
using GraphZen.Infrastructure;
using JetBrains.Annotations;
using Xunit;
using static GraphZen.TypeSystem.FunctionalTests.Specs.TypeSystemSpecs.InputXorOutputTypeSpecs;

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

        [Spec(nameof(cannot_create_type_if_name_conflicts_with_type_identity_of_opposite_io))]
        [Fact]
        public void cannot_create_type_if_name_conflicts_with_type_identity_of_opposite_io()
        {
            Schema.Create(_ =>
            {
                _.InputObject("Foo").Field("inputField", "Bar");
                Action add = () => _.Union("Bar");
                add.Should().Throw<InvalidTypeException>().WithMessage(
                    "Cannot create union Bar: Union types are output types and an input field or argument already references a type named 'Bar'. GraphQL input type references are reserved for scalar, enum, or input object types.");
            });
        }

        [Spec(nameof(cannot_rename_type_if_name_conflicts_with_type_identity_of_opposite_io))]
        [Fact]
        public void cannot_rename_type_if_name_conflicts_with_type_identity_of_opposite_io()
        {
            Schema.Create(_ =>
            {
                _.InputObject("Foo").Field("inputField", "Bar");
                var baz = _.Union("Baz");
                Action rename = () => baz.Name("Bar");
                rename
                    .Should().Throw<InvalidTypeException>().WithMessage(
                        "Cannot rename union Baz to \"Bar\": Union types are output types and an input field or argument already references a type named \"Bar\". GraphQL input type references are reserved for scalar, enum, or input object types.");
            });
        }

        [Spec(nameof(cannot_create_type_via_clr_type_if_name_conflicts_with_type_identity_of_opposite_io))]
        [Fact]
        public void cannot_create_type_via_clr_type_if_name_conflicts_with_type_identity_of_opposite_io_()
        {
            Schema.Create(_ =>
            {
                _.InputObject("Foo").Field("inputField", nameof(PlainAbstractClass));
                Action add = () => _.Union<PlainAbstractClass>();
                add.Should().Throw<InvalidTypeException>().WithMessage(
                    "Cannot create union PlainAbstractClass: Union types are output types and an input field or argument already references a type named 'PlainAbstractClass'. GraphQL input type references are reserved for scalar, enum, or input object types.");
            });
        }


        [Spec(nameof(cannot_create_type_via_clr_type_if_name_annotation_conflicts_with_type_identity_of_opposite_io))]
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


        [Spec(nameof(cannot_set_clr_type_if_custom_name_conflicts_with_type_identity_of_opposite_io))]
        [Fact]
        public void clr_type_cannot_be_added_with_custom_name_if_name_conflicts_with_type_identity_of_opposite_io_()
        {
            Schema.Create(_ =>
            {
                _.InputObject("Foo").Field("inputField", "Bar");
                var baz = _.Union("Baz");
                Action set = () => baz.ClrType<PlainAbstractClass>("Bar");
                set.Should().Throw<InvalidTypeException>().WithMessage(
                    "Cannot rename union Baz to \"Bar\": Union types are output types and an input field or argument already references a type named \"Bar\". GraphQL input type references are reserved for scalar, enum, or input object types.");
            });
        }


        [Spec(nameof(cannot_create_type_via_clr_type_if_custom_name_conflicts_with_type_identity_of_opposite_io))]
        [Fact]
        public void cannot_create_type_via_clr_type_if_custom_name_conflicts_with_type_identity_of_opposite_io_()
        {
            Schema.Create(_ =>
            {
                _.InputObject("Foo").Field("inputField", "Bar");
                Action add = () => _.Union<PlainAbstractClass>("Bar");
                add.Should().Throw<InvalidTypeException>().WithMessage(
                    "Cannot create union Bar: Union types are output types and an input field or argument already references a type named 'Bar'. GraphQL input type references are reserved for scalar, enum, or input object types.");
            });
        }


        [Spec(nameof(cannot_set_clr_type_if_inferred_name_conflicts_with_type_identity_of_opposite_io))]
        [Fact]
        public void cannot_set_clr_type_if_inferred_name_conflicts_with_type_identity_of_opposite_io_()
        {
            Schema.Create(_ =>
            {
                _.InputObject("Foo").Field("inputField", nameof(PlainAbstractClass));
                var baz = _.Union("Baz");
                Action set = () => baz.ClrType<PlainAbstractClass>(true);
                set.Should().Throw<InvalidTypeException>().WithMessage(
                    "Cannot set CLR type on union Baz and infer name \"PlainAbstractClass\": Union types are output types and an input field or argument already references a type named \"PlainAbstractClass\". GraphQL input type references are reserved for scalar, enum, or input object types.");
            });
        }


        [Spec(nameof(cannot_set_clr_type_if_inferred_name_annotation_if_name_conflicts_with_type_identity_of_opposite_io
        ))]
        [Fact]
        public void
            cannot_set_clr_type_if_inferred_name_annotation_if_name_conflicts_with_type_identity_of_opposite_io_()
        {
            Schema.Create(_ =>
            {
                _.InputObject("Foo").Field("inputField", PlainAbstractClassAnnotatedName.AnnotatedNameValue);
                var baz = _.Union("Baz");
                Action set = () => baz.ClrType<PlainAbstractClassAnnotatedName>(true);
                set.Should().Throw<InvalidTypeException>().WithMessage(
                    "Cannot set CLR type on union Baz and infer name \"PlainAbstractClass\": Union types are output types and an input field or argument already references a type named \"PlainAbstractClass\". GraphQL input type references are reserved for scalar, enum, or input object types.");
            });
        }
    }
}