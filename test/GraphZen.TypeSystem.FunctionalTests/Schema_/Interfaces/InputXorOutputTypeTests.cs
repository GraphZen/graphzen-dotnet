// Copyright (c) GraphZen LLC. All rights reserved.
// Licensed under the GraphZen Community License. See the LICENSE file in the project root for license information.

using System;
using System.Diagnostics.CodeAnalysis;
using FluentAssertions;
using GraphZen.Infrastructure;
using JetBrains.Annotations;
using Xunit;
using static GraphZen.TypeSystem.FunctionalTests.Specs.TypeSystemSpecs.InputXorOutputTypeSpecs;

namespace GraphZen.TypeSystem.FunctionalTests.Schema_.Interfaces
{
    [NoReorder]
    public class InputXorOutputTypeTests
    {
        // ReSharper disable once InconsistentNaming
        private interface PlainInterface
        {
        }

        [GraphQLName(AnnotatedNameValue)]
        // ReSharper disable once InconsistentNaming
        // ReSharper disable once UnusedType.Local
        private interface PlainInterfaceAnnotatedName
        {
            public const string AnnotatedNameValue = nameof(AnnotatedNameValue);
        }

        [GraphQLName("abc &*(")]
        // ReSharper disable once InconsistentNaming
        // ReSharper disable once UnusedType.Local
        private interface PlainInterfaceInvalidNameAnnotation
        {
        }

        [Spec(nameof(cannot_create_type_if_name_conflicts_with_type_identity_of_opposite_io))]
        [Fact]
        public void cannot_create_type_if_name_conflicts_with_type_identity_of_opposite_io_()
        {
            Schema.Create(_ =>
            {
                _.InputObject("Foo").Field("inputField", "Bar");
                Action add = () => _.Interface("Bar");
                add.Should().Throw<InvalidTypeException>().WithMessage(
                    "Cannot create interface Bar: Interface types are output types and an input field or argument already references a type named 'Bar'. GraphQL input type references are reserved for scalar, enum, or input object types.");
            });
        }

        [Spec(nameof(cannot_rename_type_if_name_conflicts_with_type_identity_of_opposite_io))]
        [Fact]
        public void cannot_rename_type_if_name_conflicts_with_type_identity_of_opposite_io_()
        {
            Schema.Create(_ =>
            {
                _.InputObject("Foo").Field("inputField", "Bar");
                var baz = _.Interface("Baz");
                Action rename = () => baz.Name("Bar");
                rename
                    .Should().Throw<InvalidTypeException>().WithMessage(
                        "Cannot rename interface Baz to \"Bar\": Interface types are output types and an input field or argument already references a type named \"Bar\". GraphQL input type references are reserved for scalar, enum, or input object types.");
            });
        }

        [Spec(nameof(cannot_create_type_via_clr_type_if_name_conflicts_with_type_identity_of_opposite_io))]
        [Fact]
        public void cannot_create_type_via_clr_type_if_name_conflicts_with_type_identity_of_opposite_io_()
        {
            Schema.Create(_ =>
            {
                _.InputObject("Foo").Field("inputField", nameof(PlainInterface));
                Action add = () => _.Interface<PlainInterface>();
                add.Should().Throw<InvalidTypeException>().WithMessage(
                    "Cannot create interface PlainInterface: Interface types are output types and an input field or argument already references a type named 'PlainInterface'. GraphQL input type references are reserved for scalar, enum, or input object types.");
            });
        }


        [Spec(nameof(cannot_create_type_via_clr_type_if_name_annotation_conflicts_with_type_identity_of_opposite_io))]
        [Fact]
        public void cannot_create_type_via_clr_type_if_name_annotation_conflicts_with_type_identity_of_opposite_io_()
        {
            Schema.Create(_ =>
            {
                _.InputObject("Foo").Field("inputField", PlainInterfaceAnnotatedName.AnnotatedNameValue);
                Action add = () => _.Interface<PlainInterfaceAnnotatedName>();
                add.Should().Throw<InvalidTypeException>().WithMessage(
                    "Cannot create interface AnnotatedNameValue: Interface types are output types and an input field or argument already references a type named 'AnnotatedNameValue'. GraphQL input type references are reserved for scalar, enum, or input object types.");
            });
        }


        [Spec(nameof(cannot_create_type_via_clr_type_if_custom_name_conflicts_with_type_identity_of_opposite_io))]
        [Fact]
        public void cannot_create_type_via_clr_type_if_custom_name_conflicts_with_type_identity_of_opposite_io_()
        {
            Schema.Create(_ =>
            {
                _.InputObject("Foo").Field("inputField", "Bar");
                Action add = () => _.Interface<PlainInterface>("Bar");
                add.Should().Throw<InvalidTypeException>().WithMessage(
                    "Cannot create interface Bar: Interface types are output types and an input field or argument already references a type named 'Bar'. GraphQL input type references are reserved for scalar, enum, or input object types.");
            });
        }


        [Spec(nameof(cannot_set_clr_type_if_inferred_name_conflicts_with_type_identity_of_opposite_io))]
        [Fact]
        public void cannot_set_clr_type_if_inferred_name_conflicts_with_type_identity_of_opposite_io_()
        {
            Schema.Create(_ =>
            {
                _.InputObject("Foo").Field("inputField", nameof(PlainInterface));
                var baz = _.Interface("Baz");
                Action set = () => baz.ClrType<PlainInterface>(true);
                set.Should().Throw<InvalidTypeException>().WithMessage(
                    "Cannot set CLR type on interface Baz and infer name \"PlainInterface\": Interface types are output types and an input field or argument already references a type named \"PlainInterface\". GraphQL input type references are reserved for scalar, enum, or input object types.");
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
                _.InputObject("Foo").Field("inputField", PlainInterfaceAnnotatedName.AnnotatedNameValue);
                var baz = _.Interface("Baz");
                Action set = () => baz.ClrType<PlainInterfaceAnnotatedName>(true);
                set.Should().Throw<InvalidTypeException>().WithMessage(
                    "Cannot set CLR type on output type interface Baz and infer name: the annotated name \"AnnotatedNameValue\" on CLR interface 'PlainInterfaceAnnotatedName' refers to an input type referenced by a field argument, directive argument, or input field. GraphQL input type references are reserved for scalar, enum, or input object types.");
            });
        }

        [Spec(nameof(cannot_set_clr_type_if_custom_name_conflicts_with_type_identity_of_opposite_io))]
        [Fact]
        public void cannot_set_clr_type_if_custom_name_conflicts_with_type_identity_of_opposite_io_()
        {
            Schema.Create(_ =>
            {
                _.InputObject("Foo").Field("inputField", "Bar");
                var baz = _.Interface("Baz");
                Action set = () => baz.ClrType<PlainInterface>("Bar");
                set.Should().Throw<InvalidTypeException>().WithMessage(
                    "Cannot rename interface Baz to \"Bar\": Interface types are output types and an input field or argument already references a type named \"Bar\". GraphQL input type references are reserved for scalar, enum, or input object types.");
            });
        }
    }
}