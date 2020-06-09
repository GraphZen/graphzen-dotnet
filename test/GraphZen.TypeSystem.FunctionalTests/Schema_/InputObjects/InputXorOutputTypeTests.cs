// Copyright (c) GraphZen LLC. All rights reserved.
// Licensed under the GraphZen Community License. See the LICENSE file in the project root for license information.

using System;
using System.Diagnostics.CodeAnalysis;
using FluentAssertions;
using GraphZen.Infrastructure;
using JetBrains.Annotations;
using Xunit;
using static GraphZen.TypeSystem.FunctionalTests.Specs.TypeSystemSpecs.InputXorOutputTypeSpecs;

namespace GraphZen.TypeSystem.FunctionalTests.Schema_.InputObjects
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
                _.Object("Foo").Field("inputField", "Bar");
                Action add = () => _.InputObject("Bar");
                add.Should().Throw<InvalidTypeException>().WithMessage(
                    "Cannot create input object Bar: Input Object types are input types and an object or interface field already references a type named 'Bar'. GraphQL output type references are reserved for scalar, enum, interface, object, or union types.");
            });
        }

        [Spec(nameof(cannot_rename_type_if_name_conflicts_with_type_identity_of_opposite_io))]
        [Fact]
        public void cannot_rename_type_if_name_conflicts_with_type_identity_of_opposite_io_()
        {
            Schema.Create(_ =>
            {
                _.Object("Foo").Field("inputField", "Bar");
                var baz = _.InputObject("Baz");
                Action rename = () => baz.Name("Bar");
                rename
                    .Should().Throw<InvalidTypeException>().WithMessage(
                        "Cannot rename input object Baz to \"Bar\": Input Object types are input types and an object or interface field already references a type named \"Bar\". GraphQL output type references are reserved for scalar, enum, interface, object, or union types.");
            });
        }

        [Spec(nameof(cannot_create_type_via_clr_type_if_name_conflicts_with_type_identity_of_opposite_io))]
        [Fact]
        public void cannot_create_type_via_clr_type_if_name_conflicts_with_type_identity_of_opposite_io_()
        {
            Schema.Create(_ =>
            {
                _.Object("Foo").Field("inputField", nameof(PlainClass));
                Action add = () => _.InputObject<PlainClass>();
                add.Should().Throw<InvalidTypeException>().WithMessage(
                    "Cannot create input object PlainClass: Input Object types are input types and an object or interface field already references a type named 'PlainClass'. GraphQL output type references are reserved for scalar, enum, interface, object, or union types.");
            });
        }


        [Spec(nameof(cannot_create_type_via_clr_type_if_name_annotation_conflicts_with_type_identity_of_opposite_io))]
        [Fact]
        public void cannot_create_type_via_clr_type_if_name_annotation_conflicts_with_type_identity_of_opposite_io_()
        {
            Schema.Create(_ =>
            {
                _.Object("Foo").Field("inputField", PlainClassAnnotatedName.AnnotatedNameValue);
                Action add = () => _.InputObject<PlainClassAnnotatedName>();
                add.Should().Throw<InvalidTypeException>().WithMessage(
                    "Cannot create input object AnnotatedNameValue: Input Object types are input types and an object or interface field already references a type named 'AnnotatedNameValue'. GraphQL output type references are reserved for scalar, enum, interface, object, or union types.");
            });
        }


        [Spec(nameof(cannot_create_type_via_clr_type_if_custom_name_conflicts_with_type_identity_of_opposite_io))]
        [Fact]
        public void cannot_create_type_via_clr_type_if_custom_name_conflicts_with_type_identity_of_opposite_io_()
        {
            Schema.Create(_ =>
            {
                _.Object("Foo").Field("inputField", "Bar");
                Action add = () => _.InputObject<PlainClass>("Bar");
                add.Should().Throw<InvalidTypeException>().WithMessage(
                    "Cannot create input object Bar: Input Object types are input types and an object or interface field already references a type named 'Bar'. GraphQL output type references are reserved for scalar, enum, interface, object, or union types.");
            });
        }


        [Spec(nameof(cannot_set_clr_type_if_inferred_name_conflicts_with_type_identity_of_opposite_io))]
        [Fact]
        public void cannot_set_clr_type_if_inferred_name_conflicts_with_type_identity_of_opposite_io_()
        {
            Schema.Create(_ =>
            {
                _.Object("Foo").Field("inputField", nameof(PlainClass));
                var baz = _.InputObject("Baz");
                Action set = () => baz.ClrType<PlainClass>(true);
                set.Should().Throw<InvalidTypeException>().WithMessage(
                    "Cannot set CLR type on input object Baz and infer name \"PlainClass\": Input Object types are input types and an object or interface field already references a type named \"PlainClass\". GraphQL output type references are reserved for scalar, enum, interface, object, or union types.");
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
                _.Object("Foo").Field("inputField", PlainClassAnnotatedName.AnnotatedNameValue);
                var baz = _.InputObject("Baz");
                Action set = () => baz.ClrType<PlainClassAnnotatedName>(true);
                set.Should().Throw<InvalidTypeException>().WithMessage(
                    "Cannot set CLR type on input type input object Baz and infer name: the annotated name \"AnnotatedNameValue\" on CLR class 'PlainClassAnnotatedName' refers to an output type referenced by an object or interface field. GraphQL output type references are reserved for scalar, enum, interface, object, or union types.");
            });
        }

        [Spec(nameof(cannot_set_clr_type_if_custom_name_conflicts_with_type_identity_of_opposite_io))]
        [Fact]
        public void cannot_set_clr_type_if_custom_name_conflicts_with_type_identity_of_opposite_io_()
        {
            Schema.Create(_ =>
            {
                _.Object("Foo").Field("inputField", "Bar");
                var baz = _.InputObject("Baz");
                Action set = () => baz.ClrType<PlainClass>("Bar");
                set.Should().Throw<InvalidTypeException>().WithMessage(
                    "Cannot rename input object Baz to \"Bar\": Input Object types are input types and an object or interface field already references a type named \"Bar\". GraphQL output type references are reserved for scalar, enum, interface, object, or union types.");
            });
        }
    }
}