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

namespace GraphZen.TypeSystem.FunctionalTests.Schema_.InputObjects.InputObjectType.ClrType
{
    [NoReorder]
    public class ClrTypeTests
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

        [Spec(nameof(clr_type_can_be_added))]
        [Fact]
        public void clr_type_can_be_added_()
        {
            var schema = Schema.Create(_ => { _.InputObject("Foo").ClrType(typeof(PlainClass)); });
            schema.GetInputObject("Foo").ClrType.Should().Be<PlainClass>();
        }

        [Spec(nameof(clr_type_can_be_added_via_type_param))]
        [Fact]
        public void clr_type_can_be_added_via_type_param_()
        {
            var schema = Schema.Create(_ => { _.InputObject("Foo").ClrType<PlainClass>(); });
            schema.GetInputObject("Foo").ClrType.Should().Be<PlainClass>();
        }


        [Spec(nameof(clr_type_can_be_changed))]
        [Fact]
        public void clr_type_can_be_changed_()
        {
            var schema = Schema.Create(_ =>
            {
                _.InputObject<PlainClass>()
                    .Description("original type: " + typeof(PlainClass))
                    .ClrType(typeof(PlainClassAnnotatedName));
            });
            schema.HasInputObject<PlainClass>().Should().BeFalse();
            schema.GetInputObject<PlainClassAnnotatedName>().Description.Should()
                .Be("original type: " + typeof(PlainClass));
        }

        [Spec(nameof(clr_type_can_be_changed_via_type_param))]
        [Fact]
        public void clr_type_can_be_changed_via_type_param_()
        {
            var schema = Schema.Create(_ =>
            {
                _.InputObject<PlainClass>()
                    .Description("original type: " + typeof(PlainClass))
                    .ClrType<PlainClassAnnotatedName>();
            });
            schema.HasInputObject<PlainClass>().Should().BeFalse();
            schema.GetInputObject<PlainClassAnnotatedName>().Description.Should()
                .Be("original type: " + typeof(PlainClass));
        }


        [Spec(nameof(clr_type_can_be_added_with_custom_name))]
        [Fact]
        public void clr_type_can_be_added_with_custom_name_()
        {
            var schema = Schema.Create(_ => { _.InputObject("Foo").ClrType(typeof(PlainClass), "Bar"); });
            schema.HasInputObject("Foo").Should().BeFalse();
            schema.GetInputObject("Bar").ClrType.Should().Be<PlainClass>();
            schema.GetInputObject<PlainClass>().Name.Should().Be("Bar");
        }


        [Spec(nameof(clr_type_can_be_added_via_type_param_with_custom_name))]
        [Fact]
        public void clr_type_can_be_added_via_type_param_with_custom_name_()
        {
            var schema = Schema.Create(_ => { _.InputObject("Foo").ClrType<PlainClass>("Bar"); });
            schema.HasInputObject("Foo").Should().BeFalse();
            schema.GetInputObject("Bar").ClrType.Should().Be<PlainClass>();
            schema.GetInputObject<PlainClass>().Name.Should().Be("Bar");
        }


        [Spec(nameof(clr_type_can_be_changed_with_custom_name))]
        [Fact]
        public void clr_type_can_be_changed_with_custom_name_()
        {
            var schema = Schema.Create(_ =>
            {
                _.InputObject<PlainClass>()
                    .Description("original type: " + typeof(PlainClass))
                    .ClrType(typeof(PlainClassAnnotatedName), "Foo");
            });
            schema.HasInputObject<PlainClass>().Should().BeFalse();
            var foo = schema.GetInputObject("Foo");
            foo.Description.Should()
                .Be("original type: " + typeof(PlainClass));
            foo.ClrType.Should().Be<PlainClassAnnotatedName>();
        }


        [Spec(nameof(clr_type_can_be_changed_via_type_param_with_custom_name))]
        [Fact]
        public void clr_type_can_be_changed_via_type_param_with_custom_name_()
        {
            var schema = Schema.Create(_ =>
            {
                _.InputObject<PlainClass>()
                    .Description("original type: " + typeof(PlainClass))
                    .ClrType<PlainClassAnnotatedName>("Foo");
            });
            schema.HasInputObject<PlainClass>().Should().BeFalse();
            var foo = schema.GetInputObject("Foo");
            foo.Description.Should()
                .Be("original type: " + typeof(PlainClass));
            foo.ClrType.Should().Be<PlainClassAnnotatedName>();
        }


        [Spec(nameof(clr_type_cannot_be_null))]
        [Fact]
        public void clr_type_cannot_be_null_()
        {
            Schema.Create(_ =>
            {
                var foo = _.InputObject("Foo");
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
                _.InputObject<PlainClass>();
                var foo = _.InputObject("Foo");
                Action change = () => foo.ClrType<PlainClass>();
                change.Should().Throw<DuplicateItemException>().WithMessage(
                    "Cannot set CLR type on input object Foo to CLR class 'PlainClass': input object PlainClass already exists with that CLR type.");
            });
        }

        [Spec(nameof(setting_clr_type_does_not_change_name))]
        [Fact]
        public void setting_clr_type_does_not_change_name_()
        {
            var schema = Schema.Create(_ => { _.InputObject("Foo").ClrType<PlainClass>(); });
            schema.GetInputObject<PlainClass>().Name.Should().Be("Foo");
        }


        [Spec(nameof(setting_clr_type_with_name_annotation_does_not_change_name))]
        [Fact]
        public void setting_clr_type_with_name_annotation_does_not_change_name_()
        {
            var schema = Schema.Create(_ => { _.InputObject("Foo").ClrType<PlainClassAnnotatedName>(); });
            schema.GetInputObject<PlainClassAnnotatedName>().Name.Should().Be("Foo");
        }


        [Spec(nameof(setting_clr_type_and_inferring_name_name_should_be_unique))]
        [Fact]
        public void setting_clr_type_and_inferring_name_name_should_be_unique()
        {
            Schema.Create(_ =>
            {
                _.InputObject(nameof(PlainClass));
                var foo = _.InputObject("Foo");
                Action change = () => foo.ClrType<PlainClass>(true);
                change.Should().Throw<DuplicateItemException>().WithMessage(
                    "Cannot set CLR type on input object Foo and infer name: the CLR class name 'PlainClass' conflicts with an existing input object named PlainClass. All GraphQL type names must be unique.");
            });
        }


        [Spec(nameof(setting_clr_type_and_inferring_name_name_annotation_should_be_unique))]
        [Fact]
        public void setting_clr_type_and_inferring_name_name_annotation_should_be_unique()
        {
            Schema.Create(_ =>
            {
                _.InputObject(nameof(PlainClass));
                var foo = _.InputObject("Foo");
                Action change = () => foo.ClrType<PlainClass>(true);
                change.Should().Throw<DuplicateItemException>().WithMessage(
                    "Cannot set CLR type on input object Foo and infer name: the CLR class name 'PlainClass' conflicts with an existing input object named PlainClass. All GraphQL type names must be unique.");
            });
        }


        [Spec(nameof(setting_clr_type_and_inferring_name_name_should_be_valid))]
        [Fact]
        public void setting_clr_type_and_inferring_name_name_should_be_valid_()
        {
            Schema.Create(_ =>
            {
                var foo = _.InputObject("Foo");
                Action setClrType = () => foo.ClrType<ArgumentBuilder<string>>(true);
                setClrType.Should().Throw<InvalidNameException>().WithMessage(
                    "Cannot set CLR type on input object Foo and infer name: the CLR class name 'ArgumentBuilder`1' is not a valid GraphQL name.");
            });
        }


        [Spec(nameof(setting_clr_type_and_inferring_name_name_annotation_should_be_valid))]
        [Fact]
        public void clr_type_name_annotation_should_be_valid_()
        {
            Schema.Create(_ =>
            {
                var foo = _.InputObject("Foo");
                new List<Action>
                {
                    () => foo.ClrType<PlainClassInvalidNameAnnotation>(true),
                    () => foo.ClrType(typeof(PlainClassInvalidNameAnnotation), true)
                }.ForEach(set =>
                {
                    set.Should().Throw<InvalidNameException>().WithMessage(
                        "Cannot set CLR type on input object Foo and infer name: the annotated name \"(*&#\" on CLR class 'PlainClassInvalidNameAnnotation' is not a valid GraphQL name.");
                });
            });
        }


        [Spec(nameof(setting_clr_type_with_duplicate_custom_name_should_throw))]
        [Fact]
        public void custom_name_should_be_unique_()
        {
            Schema.Create(_ =>
            {
                _.InputObject("Foo");
                var bar = _.InputObject("Bar");
                new List<Action>
                {
                    () => bar.ClrType<PlainClassAnnotatedName>("Foo"),
                    () => bar.ClrType(typeof(PlainClassAnnotatedName), "Foo")
                }.ForEach(set =>
                {
                    set.Should().Throw<DuplicateItemException>().WithMessage(
                        "Cannot set CLR type on input object Bar with custom name: the custom name \"Foo\" conflicts with an existing input object named 'Foo'. All type names must be unique.");
                });
            });
        }


        [Spec(nameof(setting_clr_type_with_invalid_custom_name_should_throw))]
        [Fact]
        public void custom_name_should_be_valid_()
        {
            Schema.Create(_ =>
            {
                _.InputObject("Foo");
                var bar = _.InputObject("Bar");
                new List<Action>
                {
                    () => bar.ClrType<PlainClassAnnotatedName>("invalid!"),
                    () => bar.ClrType(typeof(PlainClassAnnotatedName), "invalid!")
                }.ForEach(set =>
                {
                    set.Should().Throw<InvalidNameException>().WithMessage(
                        "Cannot set CLR type on input object Bar with custom name: the custom name \"invalid!\" is not a valid GraphQL name.");
                });
            });
        }


        [Spec(nameof(setting_clr_type_with_null_custom_name_should_throw))]
        [Fact]
        public void custom_name_cannot_be_null_()
        {
            Schema.Create(_ =>
            {
                var bar = _.InputObject("Bar");
                new List<Action>
                {
                    () => bar.ClrType<PlainClassAnnotatedName>(null!),
                    () => bar.ClrType(typeof(PlainClassAnnotatedName), null!)
                }.ForEach(set => { set.Should().ThrowArgumentNullException("name"); });
            });
        }


        [Spec(nameof(setting_clr_type_and_inferring_name_changes_name))]
        [Fact]
        public void changing_clr_type_changes_name_()
        {
            var schema = Schema.Create(_ => { _.InputObject("Foo").ClrType<PlainClass>(true); });
            schema.HasInputObject("Foo").Should().BeFalse();
            schema.GetInputObject<PlainClass>().Name.Should().Be(nameof(PlainClass));
        }


        [Spec(nameof(setting_clr_type_with_name_annotation_and_inferring_name_changes_name))]
        [Fact]
        public void changing_clr_type_with_name_annotation_changes_name_()
        {
            var schema = Schema.Create(_ => { _.InputObject("Foo").ClrType<PlainClassAnnotatedName>(true); });
            schema.HasInputObject("Foo").Should().BeFalse();
            schema.GetInputObject<PlainClassAnnotatedName>().Name.Should()
                .Be(PlainClassAnnotatedName.AnnotatedNameValue);
        }


        [Spec(nameof(clr_type_can_be_removed))]
        [Fact]
        public void clr_type_can_be_removed_()
        {
            var schema = Schema.Create(_ => { _.InputObject("Foo").ClrType<PlainClass>().RemoveClrType(); });
            schema.GetInputObject("Foo").ClrType.Should().BeNull();
        }


        [Spec(nameof(clr_typed_item_when_type_removed_should_retain_name))]
        [Fact]
        public void clr_typed_item_when_type_removed_should_retain_name_()
        {
            var schema = Schema.Create(_ => { _.InputObject<PlainClass>().RemoveClrType(); });
            schema.GetInputObject(nameof(PlainClass)).ClrType.Should().BeNull();
        }


        [Spec(nameof(clr_typed_item_with_name_annotation_when_clr_type_removed_should_retain_annotated_name))]
        [Fact]
        public void clr_typed_item_with_name_annotation_when_clr_type_removed_should_retain_annotated_name_()
        {
            var schema = Schema.Create(_ => { _.InputObject<PlainClassAnnotatedName>().RemoveClrType(); });
            schema.GetInputObject(PlainClassAnnotatedName.AnnotatedNameValue).ClrType.Should().BeNull();
        }


        [Spec(nameof(custom_named_clr_typed_item_when_type_removed_should_retain_custom_name))]
        [Fact]
        public void custom_named_clr_typed_item_when_type_removed_should_retain_custom_name_()
        {
            var schema = Schema.Create(_ => { _.InputObject("Foo").ClrType<PlainClass>("Bar").RemoveClrType(); });
            schema.GetInputObject("Bar").ClrType.Should().BeNull();
        }
    }
}