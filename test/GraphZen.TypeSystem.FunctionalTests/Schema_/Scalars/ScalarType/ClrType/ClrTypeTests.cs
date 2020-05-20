// Copyright (c) GraphZen LLC. All rights reserved.
// Licensed under the GraphZen Community License. See the LICENSE file in the project root for license information.

using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using FluentAssertions;
using GraphZen.Infrastructure;
using JetBrains.Annotations;
using Xunit;
using static GraphZen.TypeSystem.FunctionalTests.Specs.TypeSystemSpecs.ClrTypeSpecs;


namespace GraphZen.TypeSystem.FunctionalTests.Schema_.Scalars.ScalarType.ClrType
{
    [NoReorder]
    public class ClrTypeTests
    {
        public struct PlainStruct
        {
        }

        [GraphQLName(AnnotatedNameValue)]
        public struct PlainStructAnnotatedName
        {
            public const string AnnotatedNameValue = nameof(AnnotatedNameValue);
        }

        [GraphQLName("(*&#")]
        public struct PlainStructInvalidNameAnnotation
        {
        }

        [Spec(nameof(clr_type_can_be_added))]
        [Fact]
        public void clr_type_can_be_added_()
        {
            var schema = Schema.Create(_ => { _.Scalar("Foo").ClrType(typeof(PlainStruct)); });
            schema.GetScalar("Foo").ClrType.Should().Be<PlainStruct>();
        }

        [Spec(nameof(clr_type_can_be_added_via_type_param))]
        [Fact]
        public void clr_type_can_be_added_via_type_param_()
        {
            var schema = Schema.Create(_ => { _.Scalar("Foo").ClrType<PlainStruct>(); });
            schema.GetScalar("Foo").ClrType.Should().Be<PlainStruct>();
        }


        [Spec(nameof(clr_type_can_be_changed))]
        [Fact]
        public void clr_type_can_be_changed_()
        {
            var schema = Schema.Create(_ =>
            {
                _.Scalar<PlainStruct>()
                    .Description("original type: " + typeof(PlainStruct))
                    .ClrType(typeof(PlainStructAnnotatedName));
            });
            schema.HasScalar<PlainStruct>().Should().BeFalse();
            schema.GetScalar<PlainStructAnnotatedName>().Description.Should()
                .Be("original type: " + typeof(PlainStruct));
        }

        [Spec(nameof(clr_type_can_be_changed_via_type_param))]
        [Fact]
        public void clr_type_can_be_changed_via_type_param_()
        {
            var schema = Schema.Create(_ =>
            {
                _.Scalar<PlainStruct>()
                    .Description("original type: " + typeof(PlainStruct))
                    .ClrType<PlainStructAnnotatedName>();
            });
            schema.HasScalar<PlainStruct>().Should().BeFalse();
            schema.GetScalar<PlainStructAnnotatedName>().Description.Should()
                .Be("original type: " + typeof(PlainStruct));
        }


        [Spec(nameof(clr_type_can_be_added_with_custom_name))]
        [Fact]
        public void clr_type_can_be_added_with_custom_name_()
        {
            var schema = Schema.Create(_ => { _.Scalar("Foo").ClrType(typeof(PlainStruct), "Bar"); });
            schema.HasScalar("Foo").Should().BeFalse();
            schema.GetScalar("Bar").ClrType.Should().Be<PlainStruct>();
            schema.GetScalar<PlainStruct>().Name.Should().Be("Bar");
        }


        [Spec(nameof(clr_type_can_be_added_via_type_param_with_custom_name))]
        [Fact]
        public void clr_type_can_be_added_via_type_param_with_custom_name_()
        {
            var schema = Schema.Create(_ => { _.Scalar("Foo").ClrType<PlainStruct>("Bar"); });
            schema.HasScalar("Foo").Should().BeFalse();
            schema.GetScalar("Bar").ClrType.Should().Be<PlainStruct>();
            schema.GetScalar<PlainStruct>().Name.Should().Be("Bar");
        }


        [Spec(nameof(clr_type_can_be_changed_with_custom_name))]
        [Fact]
        public void clr_type_can_be_changed_with_custom_name_()
        {
            var schema = Schema.Create(_ =>
            {
                _.Scalar<PlainStruct>()
                    .Description("original type: " + typeof(PlainStruct))
                    .ClrType(typeof(PlainStructAnnotatedName), "Foo");
            });
            schema.HasScalar<PlainStruct>().Should().BeFalse();
            var foo = schema.GetScalar("Foo");
            foo.Description.Should()
                .Be("original type: " + typeof(PlainStruct));
            foo.ClrType.Should().Be<PlainStructAnnotatedName>();
        }


        [Spec(nameof(clr_type_can_be_changed_via_type_param_with_custom_name))]
        [Fact]
        public void clr_type_can_be_changed_via_type_param_with_custom_name_()
        {
            var schema = Schema.Create(_ =>
            {
                _.Scalar<PlainStruct>()
                    .Description("original type: " + typeof(PlainStruct))
                    .ClrType<PlainStructAnnotatedName>("Foo");
            });
            schema.HasScalar<PlainStruct>().Should().BeFalse();
            var foo = schema.GetScalar("Foo");
            foo.Description.Should()
                .Be("original type: " + typeof(PlainStruct));
            foo.ClrType.Should().Be<PlainStructAnnotatedName>();
        }


        [Spec(nameof(clr_type_cannot_be_null))]
        [Fact]
        public void clr_type_cannot_be_null_()
        {
            Schema.Create(_ =>
            {
                var foo = _.Scalar("Foo");
                new List<Action>
                {
                    () => foo.ClrType(null!),
                    () => foo.ClrType(null!, "bar")
                }.ForEach(a => a.Should().ThrowArgumentNullException("clrType"));
            });
        }


        [Spec(nameof(clr_type_should_be_unique))]
        [Fact]
        public void clr_type_should_be_unique_()
        {
            Schema.Create(_ =>
            {
                _.Scalar<PlainStruct>();
                var foo = _.Scalar("Foo");
                Action change = () => foo.ClrType<PlainStruct>();
                change.Should().Throw<DuplicateClrTypeException>().WithMessage(
                    "Cannot set CLR type on scalar Foo to CLR type 'PlainStruct': scalar PlainStruct already exists with that CLR type.");
            });
        }

        [Spec(nameof(setting_clr_type_does_not_change_name))]
        [Fact]
        public void setting_clr_type_does_not_change_name_()
        {
            var schema = Schema.Create(_ => { _.Scalar("Foo").ClrType<PlainStruct>(); });
            schema.GetScalar<PlainStruct>().Name.Should().Be("Foo");
        }


        [Spec(nameof(setting_clr_type_with_name_annotation_does_not_change_name))]
        [Fact]
        public void setting_clr_type_with_name_annotation_does_not_change_name_()
        {
            var schema = Schema.Create(_ => { _.Scalar("Foo").ClrType<PlainStructAnnotatedName>(); });
            schema.GetScalar<PlainStructAnnotatedName>().Name.Should().Be("Foo");
        }


        [Spec(nameof(setting_clr_type_and_inferring_name_name_should_be_unique))]
        [Fact]
        public void setting_clr_type_and_inferring_name_name_should_be_unique()
        {
            Schema.Create(_ =>
            {
                _.Scalar(nameof(PlainStruct));
                var foo = _.Scalar("Foo");
                Action change = () => foo.ClrType<PlainStruct>(true);
                change.Should().Throw<DuplicateNameException>().WithMessage(
                    "Cannot set CLR type on scalar Foo and infer name: the CLR type name 'PlainStruct' conflicts with an existing scalar named PlainStruct. All GraphQL type names must be unique.");
            });
        }


        [Spec(nameof(setting_clr_type_and_inferring_name_name_annotation_should_be_unique))]
        [Fact]
        public void setting_clr_type_and_inferring_name_name_annotation_should_be_unique()
        {
            Schema.Create(_ =>
            {
                _.Scalar(nameof(PlainStruct));
                var foo = _.Scalar("Foo");
                Action change = () => foo.ClrType<PlainStruct>(true);
                change.Should().Throw<DuplicateNameException>().WithMessage(
                    "Cannot set CLR type on scalar Foo and infer name: the CLR type name 'PlainStruct' conflicts with an existing scalar named PlainStruct. All GraphQL type names must be unique.");
            });
        }


        [Spec(nameof(setting_clr_type_and_inferring_name_name_should_be_valid))]
        [Fact]
        public void setting_clr_type_and_inferring_name_name_should_be_valid_()
        {
            Schema.Create(_ =>
            {
                var foo = _.Scalar("Foo");
                Action setClrType = () => foo.ClrType<InputValueBuilder<string>>(true);
                setClrType.Should().Throw<InvalidNameException>().WithMessage(
                    "Cannot set CLR type on scalar Foo and infer name: the CLR class name 'InputValueBuilder`1' is not a valid GraphQL name.");
            });
        }


        [Spec(nameof(setting_clr_type_and_inferring_name_name_annotation_should_be_valid))]
        [Fact]
        public void clr_type_name_annotation_should_be_valid_()
        {
            Schema.Create(_ =>
            {
                var foo = _.Scalar("Foo");
                new List<Action>
                {
                    () => foo.ClrType<PlainStructInvalidNameAnnotation>(true),
                    () => foo.ClrType(typeof(PlainStructInvalidNameAnnotation), true)
                }.ForEach(set =>
                {
                    set.Should().Throw<InvalidNameException>().WithMessage(
                        "Cannot set CLR type on scalar Foo and infer name: the annotated name \"(*&#\" on CLR type 'PlainStructInvalidNameAnnotation' is not a valid GraphQL name.");
                });
            });
        }


        [Spec(nameof(setting_clr_type_with_duplicate_custom_name_should_throw))]
        [Fact]
        public void custom_name_should_be_unique_()
        {
            Schema.Create(_ =>
            {
                _.Scalar("Foo");
                var bar = _.Scalar("Bar");
                new List<Action>
                {
                    () => bar.ClrType<PlainStructAnnotatedName>("Foo"),
                    () => bar.ClrType(typeof(PlainStructAnnotatedName), "Foo")
                }.ForEach(set =>
                {
                    set.Should().Throw<DuplicateNameException>().WithMessage(
                        "Cannot set CLR type on scalar Bar with custom name: the custom name \"Foo\" conflicts with an existing scalar named 'Foo'. All type names must be unique.");
                });
            });
        }


        [Spec(nameof(setting_clr_type_with_invalid_custom_name_should_throw))]
        [Fact]
        public void custom_name_should_be_valid_()
        {
            Schema.Create(_ =>
            {
                _.Scalar("Foo");
                var bar = _.Scalar("Bar");
                new List<Action>
                {
                    () => bar.ClrType<PlainStructAnnotatedName>("invalid!"),
                    () => bar.ClrType(typeof(PlainStructAnnotatedName), "invalid!")
                }.ForEach(set =>
                {
                    set.Should().Throw<InvalidNameException>().WithMessage(
                        "Cannot set CLR type on scalar Bar with custom name: the custom name \"invalid!\" is not a valid GraphQL name.");
                });
            });
        }


        [Spec(nameof(setting_clr_type_with_null_custom_name_should_throw))]
        [Fact]
        public void custom_name_cannot_be_null_()
        {
            Schema.Create(_ =>
            {
                var bar = _.Scalar("Bar");
                new List<Action>
                {
                    () => bar.ClrType<PlainStructAnnotatedName>(null!),
                    () => bar.ClrType(typeof(PlainStructAnnotatedName), null!)
                }.ForEach(set => { set.Should().ThrowArgumentNullException("name"); });
            });
        }


        [Spec(nameof(setting_clr_type_and_inferring_name_changes_name))]
        [Fact]
        public void changing_clr_type_changes_name_()
        {
            var schema = Schema.Create(_ => { _.Scalar("Foo").ClrType<PlainStruct>(true); });
            schema.HasScalar("Foo").Should().BeFalse();
            schema.GetScalar<PlainStruct>().Name.Should().Be(nameof(PlainStruct));
        }


        [Spec(nameof(setting_clr_type_with_name_annotation_and_inferring_name_changes_name))]
        [Fact]
        public void changing_clr_type_with_name_annotation_changes_name_()
        {
            var schema = Schema.Create(_ => { _.Scalar("Foo").ClrType<PlainStructAnnotatedName>(true); });
            schema.HasScalar("Foo").Should().BeFalse();
            schema.GetScalar<PlainStructAnnotatedName>().Name.Should().Be(PlainStructAnnotatedName.AnnotatedNameValue);
        }


        [Spec(nameof(clr_type_can_be_removed))]
        [Fact]
        public void clr_type_can_be_removed_()
        {
            var schema = Schema.Create(_ => { _.Scalar("Foo").ClrType<PlainStruct>().RemoveClrType(); });
            schema.GetScalar("Foo").ClrType.Should().BeNull();
        }


        [Spec(nameof(clr_typed_item_when_type_removed_should_retain_name))]
        [Fact]
        public void clr_typed_item_when_type_removed_should_retain_name_()
        {
            var schema = Schema.Create(_ => { _.Scalar<PlainStruct>().RemoveClrType(); });
            schema.GetScalar(nameof(PlainStruct)).ClrType.Should().BeNull();
        }


        [Spec(nameof(clr_typed_item_with_name_annotation_when_clr_type_removed_should_retain_annotated_name))]
        [Fact]
        public void clr_typed_item_with_name_annotation_when_clr_type_removed_should_retain_annotated_name_()
        {
            var schema = Schema.Create(_ => { _.Scalar<PlainStructAnnotatedName>().RemoveClrType(); });
            schema.GetScalar(PlainStructAnnotatedName.AnnotatedNameValue).ClrType.Should().BeNull();
        }


        [Spec(nameof(custom_named_clr_typed_item_when_type_removed_should_retain_custom_name))]
        [Fact]
        public void custom_named_clr_typed_item_when_type_removed_should_retain_custom_name_()
        {
            var schema = Schema.Create(_ => { _.Scalar("Foo").ClrType<PlainStruct>("Bar").RemoveClrType(); });
            schema.GetScalar("Bar").ClrType.Should().BeNull();
        }
    }
}