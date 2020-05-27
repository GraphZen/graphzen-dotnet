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


namespace GraphZen.TypeSystem.FunctionalTests.Schema_.Unions.UnionType.ClrType
{
    [NoReorder]
    public class ClrTypeTests
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

        [Spec(nameof(clr_type_can_be_added))]
        [Fact]
        public void clr_type_can_be_added_()
        {
            var schema = Schema.Create(_ => { _.Union("Foo").ClrType(typeof(PlainAbstractClass)); });
            schema.GetUnion("Foo").ClrType.Should().Be<PlainAbstractClass>();
        }

        [Spec(nameof(clr_type_can_be_added_via_type_param))]
        [Fact]
        public void clr_type_can_be_added_via_type_param_()
        {
            var schema = Schema.Create(_ => { _.Union("Foo").ClrType<PlainAbstractClass>(); });
            schema.GetUnion("Foo").ClrType.Should().Be<PlainAbstractClass>();
        }


        [Spec(nameof(clr_type_can_be_changed))]
        [Fact]
        public void clr_type_can_be_changed_()
        {
            var schema = Schema.Create(_ =>
            {
                _.Union<PlainAbstractClass>()
                    .Description("original type: " + typeof(PlainAbstractClass))
                    .ClrType(typeof(PlainAbstractClassAnnotatedName));
            });
            schema.HasUnion<PlainAbstractClass>().Should().BeFalse();
            schema.GetUnion<PlainAbstractClassAnnotatedName>().Description.Should()
                .Be("original type: " + typeof(PlainAbstractClass));
        }

        [Spec(nameof(clr_type_can_be_changed_via_type_param))]
        [Fact]
        public void clr_type_can_be_changed_via_type_param_()
        {
            var schema = Schema.Create(_ =>
            {
                _.Union<PlainAbstractClass>()
                    .Description("original type: " + typeof(PlainAbstractClass))
                    .ClrType<PlainAbstractClassAnnotatedName>();
            });
            schema.HasUnion<PlainAbstractClass>().Should().BeFalse();
            schema.GetUnion<PlainAbstractClassAnnotatedName>().Description.Should()
                .Be("original type: " + typeof(PlainAbstractClass));
        }


        [Spec(nameof(clr_type_can_be_added_with_custom_name))]
        [Fact]
        public void clr_type_can_be_added_with_custom_name_()
        {
            var schema = Schema.Create(_ => { _.Union("Foo").ClrType(typeof(PlainAbstractClass), "Bar"); });
            schema.HasUnion("Foo").Should().BeFalse();
            schema.GetUnion("Bar").ClrType.Should().Be<PlainAbstractClass>();
            schema.GetUnion<PlainAbstractClass>().Name.Should().Be("Bar");
        }


        [Spec(nameof(clr_type_can_be_added_via_type_param_with_custom_name))]
        [Fact]
        public void clr_type_can_be_added_via_type_param_with_custom_name_()
        {
            var schema = Schema.Create(_ => { _.Union("Foo").ClrType<PlainAbstractClass>("Bar"); });
            schema.HasUnion("Foo").Should().BeFalse();
            schema.GetUnion("Bar").ClrType.Should().Be<PlainAbstractClass>();
            schema.GetUnion<PlainAbstractClass>().Name.Should().Be("Bar");
        }


        [Spec(nameof(clr_type_can_be_changed_with_custom_name))]
        [Fact]
        public void clr_type_can_be_changed_with_custom_name_()
        {
            var schema = Schema.Create(_ =>
            {
                _.Union<PlainAbstractClass>()
                    .Description("original type: " + typeof(PlainAbstractClass))
                    .ClrType(typeof(PlainAbstractClassAnnotatedName), "Foo");
            });
            schema.HasUnion<PlainAbstractClass>().Should().BeFalse();
            var foo = schema.GetUnion("Foo");
            foo.Description.Should()
                .Be("original type: " + typeof(PlainAbstractClass));
            foo.ClrType.Should().Be<PlainAbstractClassAnnotatedName>();
        }


        [Spec(nameof(clr_type_can_be_changed_via_type_param_with_custom_name))]
        [Fact]
        public void clr_type_can_be_changed_via_type_param_with_custom_name_()
        {
            var schema = Schema.Create(_ =>
            {
                _.Union<PlainAbstractClass>()
                    .Description("original type: " + typeof(PlainAbstractClass))
                    .ClrType<PlainAbstractClassAnnotatedName>("Foo");
            });
            schema.HasUnion<PlainAbstractClass>().Should().BeFalse();
            var foo = schema.GetUnion("Foo");
            foo.Description.Should()
                .Be("original type: " + typeof(PlainAbstractClass));
            foo.ClrType.Should().Be<PlainAbstractClassAnnotatedName>();
        }


        [Spec(nameof(clr_type_cannot_be_null))]
        [Fact]
        public void clr_type_cannot_be_null_()
        {
            Schema.Create(_ =>
            {
                var foo = _.Union("Foo");
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
                _.Union<PlainAbstractClass>();
                var foo = _.Union("Foo");
                Action change = () => foo.ClrType<PlainAbstractClass>();
                change.Should().Throw<DuplicateItemException>().WithMessage(
                    "Cannot set CLR type on union Foo to CLR class 'PlainAbstractClass': union PlainAbstractClass already exists with that CLR type.");
            });
        }

        [Spec(nameof(setting_clr_type_does_not_change_name))]
        [Fact]
        public void setting_clr_type_does_not_change_name_()
        {
            var schema = Schema.Create(_ => { _.Union("Foo").ClrType<PlainAbstractClass>(); });
            schema.GetUnion<PlainAbstractClass>().Name.Should().Be("Foo");
        }


        [Spec(nameof(setting_clr_type_with_name_annotation_does_not_change_name))]
        [Fact]
        public void setting_clr_type_with_name_annotation_does_not_change_name_()
        {
            var schema = Schema.Create(_ => { _.Union("Foo").ClrType<PlainAbstractClassAnnotatedName>(); });
            schema.GetUnion<PlainAbstractClassAnnotatedName>().Name.Should().Be("Foo");
        }


        [Spec(nameof(setting_clr_type_and_inferring_name_name_should_be_unique))]
        [Fact]
        public void setting_clr_type_and_inferring_name_name_should_be_unique()
        {
            Schema.Create(_ =>
            {
                _.Union(nameof(PlainAbstractClass));
                var foo = _.Union("Foo");
                Action change = () => foo.ClrType<PlainAbstractClass>(true);
                change.Should().Throw<DuplicateItemException>().WithMessage(
                    "Cannot set CLR type on union Foo and infer name: the CLR class name 'PlainAbstractClass' conflicts with an existing union named PlainAbstractClass. All GraphQL type names must be unique.");
            });
        }


        [Spec(nameof(setting_clr_type_and_inferring_name_name_annotation_should_be_unique))]
        [Fact]
        public void setting_clr_type_and_inferring_name_name_annotation_should_be_unique()
        {
            Schema.Create(_ =>
            {
                _.Union(nameof(PlainAbstractClass));
                var foo = _.Union("Foo");
                Action change = () => foo.ClrType<PlainAbstractClass>(true);
                change.Should().Throw<DuplicateItemException>().WithMessage(
                    "Cannot set CLR type on union Foo and infer name: the CLR class name 'PlainAbstractClass' conflicts with an existing union named PlainAbstractClass. All GraphQL type names must be unique.");
            });
        }


        [Spec(nameof(setting_clr_type_and_inferring_name_name_should_be_valid))]
        [Fact]
        public void setting_clr_type_and_inferring_name_name_should_be_valid_()
        {
            Schema.Create(_ =>
            {
                var foo = _.Union("Foo");
                Action setClrType = () => foo.ClrType<ArgumentBuilder<string>>(true);
                setClrType.Should().Throw<InvalidNameException>().WithMessage(
                    "Cannot set CLR type on union Foo and infer name: the CLR class name 'ArgumentBuilder`1' is not a valid GraphQL name.");
            });
        }


        [Spec(nameof(setting_clr_type_and_inferring_name_name_annotation_should_be_valid))]
        [Fact]
        public void clr_type_name_annotation_should_be_valid_()
        {
            Schema.Create(_ =>
            {
                var foo = _.Union("Foo");
                new List<Action>
                {
                    () => foo.ClrType<PlainAbstractClassInvalidNameAnnotation>(true),
                    () => foo.ClrType(typeof(PlainAbstractClassInvalidNameAnnotation), true)
                }.ForEach(set =>
                {
                    set.Should().Throw<InvalidNameException>().WithMessage(
                        "Cannot set CLR type on union Foo and infer name: the annotated name \"(*&#\" on CLR class 'PlainAbstractClassInvalidNameAnnotation' is not a valid GraphQL name.");
                });
            });
        }


        [Spec(nameof(setting_clr_type_with_duplicate_custom_name_should_throw))]
        [Fact]
        public void custom_name_should_be_unique_()
        {
            Schema.Create(_ =>
            {
                _.Union("Foo");
                var bar = _.Union("Bar");
                new List<Action>
                {
                    () => bar.ClrType<PlainAbstractClassAnnotatedName>("Foo"),
                    () => bar.ClrType(typeof(PlainAbstractClassAnnotatedName), "Foo")
                }.ForEach(set =>
                {
                    set.Should().Throw<DuplicateItemException>().WithMessage(
                        "Cannot set CLR type on union Bar with custom name: the custom name \"Foo\" conflicts with an existing union named 'Foo'. All type names must be unique.");
                });
            });
        }


        [Spec(nameof(setting_clr_type_with_invalid_custom_name_should_throw))]
        [Fact]
        public void custom_name_should_be_valid_()
        {
            Schema.Create(_ =>
            {
                _.Union("Foo");
                var bar = _.Union("Bar");
                new List<Action>
                {
                    () => bar.ClrType<PlainAbstractClassAnnotatedName>("invalid!"),
                    () => bar.ClrType(typeof(PlainAbstractClassAnnotatedName), "invalid!")
                }.ForEach(set =>
                {
                    set.Should().Throw<InvalidNameException>().WithMessage(
                        "Cannot set CLR type on union Bar with custom name: the custom name \"invalid!\" is not a valid GraphQL name.");
                });
            });
        }


        [Spec(nameof(setting_clr_type_with_null_custom_name_should_throw))]
        [Fact]
        public void custom_name_cannot_be_null_()
        {
            Schema.Create(_ =>
            {
                var bar = _.Union("Bar");
                new List<Action>
                {
                    () => bar.ClrType<PlainAbstractClassAnnotatedName>(null!),
                    () => bar.ClrType(typeof(PlainAbstractClassAnnotatedName), null!)
                }.ForEach(set => { set.Should().ThrowArgumentNullException("name"); });
            });
        }


        [Spec(nameof(setting_clr_type_and_inferring_name_changes_name))]
        [Fact]
        public void changing_clr_type_changes_name_()
        {
            var schema = Schema.Create(_ => { _.Union("Foo").ClrType<PlainAbstractClass>(true); });
            schema.HasUnion("Foo").Should().BeFalse();
            schema.GetUnion<PlainAbstractClass>().Name.Should().Be(nameof(PlainAbstractClass));
        }


        [Spec(nameof(setting_clr_type_with_name_annotation_and_inferring_name_changes_name))]
        [Fact]
        public void changing_clr_type_with_name_annotation_changes_name_()
        {
            var schema = Schema.Create(_ => { _.Union("Foo").ClrType<PlainAbstractClassAnnotatedName>(true); });
            schema.HasUnion("Foo").Should().BeFalse();
            schema.GetUnion<PlainAbstractClassAnnotatedName>().Name.Should()
                .Be(PlainAbstractClassAnnotatedName.AnnotatedNameValue);
        }


        [Spec(nameof(clr_type_can_be_removed))]
        [Fact]
        public void clr_type_can_be_removed_()
        {
            var schema = Schema.Create(_ => { _.Union("Foo").ClrType<PlainAbstractClass>().RemoveClrType(); });
            schema.GetUnion("Foo").ClrType.Should().BeNull();
        }


        [Spec(nameof(clr_typed_item_when_type_removed_should_retain_name))]
        [Fact]
        public void clr_typed_item_when_type_removed_should_retain_name_()
        {
            var schema = Schema.Create(_ => { _.Union<PlainAbstractClass>().RemoveClrType(); });
            schema.GetUnion(nameof(PlainAbstractClass)).ClrType.Should().BeNull();
        }


        [Spec(nameof(clr_typed_item_with_name_annotation_when_clr_type_removed_should_retain_annotated_name))]
        [Fact]
        public void clr_typed_item_with_name_annotation_when_clr_type_removed_should_retain_annotated_name_()
        {
            var schema = Schema.Create(_ => { _.Union<PlainAbstractClassAnnotatedName>().RemoveClrType(); });
            schema.GetUnion(PlainAbstractClassAnnotatedName.AnnotatedNameValue).ClrType.Should().BeNull();
        }


        [Spec(nameof(custom_named_clr_typed_item_when_type_removed_should_retain_custom_name))]
        [Fact]
        public void custom_named_clr_typed_item_when_type_removed_should_retain_custom_name_()
        {
            var schema = Schema.Create(_ => { _.Union("Foo").ClrType<PlainAbstractClass>("Bar").RemoveClrType(); });
            schema.GetUnion("Bar").ClrType.Should().BeNull();
        }
    }
}