// Copyright (c) GraphZen LLC. All rights reserved.
// Licensed under the GraphZen Community License. See the LICENSE file in the project root for license information.

using System;
using System.Diagnostics.CodeAnalysis;
using FluentAssertions;
using GraphZen.Infrastructure;
using JetBrains.Annotations;
using Xunit;
using static GraphZen.TypeSystem.FunctionalTests.Specs.TypeSystemSpecs.InputXorOutputTypeSpecs;

namespace GraphZen.TypeSystem.FunctionalTests.Schema_.Objects
{
    [NoReorder]
    public class InputXorOutputTypeTests
    {
        public class PlainClass
        {
        }

        [GraphQLName(AnnotatedNameValue)]
        public class PlainClassAnnotatedName
        {
            public const string AnnotatedNameValue = nameof(AnnotatedNameValue);
        }

        [GraphQLName("(*&#")]
        public class PlainClassInvalidNameAnnotation
        {
        }

        [Spec(nameof(cannot_create_type_if_name_conflicts_with_type_identity_of_opposite_io))]
        [Fact]
        public void cannot_create_type_if_name_conflicts_with_type_identity_of_opposite_io_()
        {
            Schema.Create(_ =>
            {
                _.InputObject("Foo").Field("inputField", "Bar");
                Action add = () => _.Object("Bar");
                add.Should().Throw<InvalidTypeException>().WithMessage(
                    "Cannot create object Bar: Object types are output types and an input field or argument already references a type named 'Bar'. GraphQL input type references are reserved for scalar, enum, or input object types.");
            });
        }

        [Spec(nameof(cannot_rename_type_if_name_conflicts_with_type_identity_of_opposite_io))]
        [Fact]
        public void cannot_rename_type_if_name_conflicts_with_type_identity_of_opposite_io_()
        {
            Schema.Create(_ =>
            {
                _.InputObject("Foo").Field("inputField", "Bar");
                var baz = _.Object("Baz");
                Action rename = () => baz.Name("Bar");
                rename
                    .Should().Throw<InvalidTypeException>().WithMessage(
                        "Cannot rename object Baz to \"Bar\": Object types are output types and an input field or argument already references a type named \"Bar\". GraphQL input type references are reserved for scalar, enum, or input object types.");
            });
        }

        [Spec(nameof(cannot_create_type_via_clr_type_if_name_conflicts_with_type_identity_of_opposite_io))]
        [Fact]
        public void cannot_create_type_via_clr_type_if_name_conflicts_with_type_identity_of_opposite_io_()
        {
            Schema.Create(_ =>
            {
                _.InputObject("Foo").Field("inputField", nameof(PlainClass));
                Action add = () => _.Object<PlainClass>();
                add.Should().Throw<InvalidTypeException>().WithMessage(
                    "Cannot create object PlainClass: Object types are output types and an input field or argument already references a type named 'PlainClass'. GraphQL input type references are reserved for scalar, enum, or input object types.");
            });
        }


        [Spec(nameof(cannot_create_type_via_clr_type_if_name_annotation_conflicts_with_type_identity_of_opposite_io))]
        [Fact]
        public void cannot_create_type_via_clr_type_if_name_annotation_conflicts_with_type_identity_of_opposite_io_()
        {
            Schema.Create(_ =>
            {
                _.InputObject("Foo").Field("inputField", PlainClassAnnotatedName.AnnotatedNameValue);
                Action add = () => _.Object<PlainClassAnnotatedName>();
                add.Should().Throw<InvalidTypeException>().WithMessage(
                    "Cannot create object AnnotatedNameValue: Object types are output types and an input field or argument already references a type named 'AnnotatedNameValue'. GraphQL input type references are reserved for scalar, enum, or input object types.");
            });
        }


        [Spec(nameof(cannot_create_type_via_clr_type_if_custom_name_conflicts_with_type_identity_of_opposite_io))]
        [Fact]
        public void cannot_create_type_via_clr_type_if_custom_name_conflicts_with_type_identity_of_opposite_io_()
        {
            Schema.Create(_ =>
            {
                _.InputObject("Foo").Field("inputField", "Bar");
                Action add = () => _.Object<PlainClass>("Bar");
                add.Should().Throw<InvalidTypeException>().WithMessage(
                    "Cannot create object Bar: Object types are output types and an input field or argument already references a type named 'Bar'. GraphQL input type references are reserved for scalar, enum, or input object types.");
            });
        }


        [Spec(nameof(cannot_set_clr_type_if_inferred_name_conflicts_with_type_identity_of_opposite_io))]
        [Fact]
        public void cannot_set_clr_type_if_inferred_name_conflicts_with_type_identity_of_opposite_io_()
        {
            Schema.Create(_ =>
            {
                _.InputObject("Foo").Field("inputField", nameof(PlainClass));
                var baz = _.Object("Baz");
                Action set = () => baz.ClrType<PlainClass>(true);
                set.Should().Throw<InvalidTypeException>().WithMessage(
                    "Cannot set CLR type on object Baz and infer name \"PlainClass\": Object types are output types and an input field or argument already references a type named \"PlainClass\". GraphQL input type references are reserved for scalar, enum, or input object types.");
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
                _.InputObject("Foo").Field("inputField", PlainClassAnnotatedName.AnnotatedNameValue);
                var baz = _.Object("Baz");
                Action set = () => baz.ClrType<PlainClassAnnotatedName>(true);
                set.Should().Throw<InvalidTypeException>().WithMessage(
                    "Cannot set CLR type on output type object Baz and infer name: the annotated name \"AnnotatedNameValue\" on CLR class 'PlainClassAnnotatedName' refers to an input type referenced by a field argument, directive argument, or input field. GraphQL input type references are reserved for scalar, enum, or input object types.");
            });
        }

        [Spec(nameof(cannot_set_clr_type_if_custom_name_conflicts_with_type_identity_of_opposite_io))]
        [Fact]
        public void cannot_set_clr_type_if_custom_name_conflicts_with_type_identity_of_opposite_io_()
        {
            Schema.Create(_ =>
            {
                _.InputObject("Foo").Field("inputField", "Bar");
                var baz = _.Object("Baz");
                Action set = () => baz.ClrType<PlainClass>("Bar");
                set.Should().Throw<InvalidTypeException>().WithMessage(
                    "Cannot rename object Baz to \"Bar\": Object types are output types and an input field or argument already references a type named \"Bar\". GraphQL input type references are reserved for scalar, enum, or input object types.");
            });
        }
    }
}