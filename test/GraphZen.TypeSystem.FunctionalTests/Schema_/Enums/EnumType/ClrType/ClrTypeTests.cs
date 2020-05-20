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

namespace GraphZen.TypeSystem.FunctionalTests.Schema_.Enums.EnumType.ClrType
{
    [NoReorder]
    public class ClrTypeTests
    {
        public const string AnnotatedNameValue = nameof(AnnotatedNameValue);
        public enum PlainEnum
        {
        }

        [GraphQLName(AnnotatedNameValue)]
        public enum PlainEnumAnnotatedName
        {
        }

        [GraphQLName("(*&#")]
        public enum PlainEnumInvalidNameAnnotation
        {
        }

        [Spec(nameof(clr_type_can_be_added))]
        [Fact]
        public void clr_type_can_be_added_()
        {
            var schema = Schema.Create(_ => { _.Enum("Foo").ClrType(typeof(PlainEnum)); });
            schema.GetEnum("Foo").ClrType.Should().Be<PlainEnum>();
        }

        [Spec(nameof(clr_type_can_be_added_via_type_param))]
        [Fact]
        public void clr_type_can_be_added_via_type_param_()
        {
            var schema = Schema.Create(_ => { _.Enum("Foo").ClrType<PlainEnum>(); });
            schema.GetEnum("Foo").ClrType.Should().Be<PlainEnum>();
        }


        [Spec(nameof(clr_type_can_be_changed))]
        [Fact]
        public void clr_type_can_be_changed_()
        {
            var schema = Schema.Create(_ =>
            {
                _.Enum<PlainEnum>()
                    .Description("original type: " + typeof(PlainEnum))
                    .ClrType(typeof(PlainEnumAnnotatedName));
            });
            schema.HasEnum<PlainEnum>().Should().BeFalse();
            schema.GetEnum<PlainEnumAnnotatedName>().Description.Should()
                .Be("original type: " + typeof(PlainEnum));
        }

        [Spec(nameof(clr_type_can_be_changed_via_type_param))]
        [Fact]
        public void clr_type_can_be_changed_via_type_param_()
        {
            var schema = Schema.Create(_ =>
            {
                _.Enum<PlainEnum>()
                    .Description("original type: " + typeof(PlainEnum))
                    .ClrType<PlainEnumAnnotatedName>();
            });
            schema.HasEnum<PlainEnum>().Should().BeFalse();
            schema.GetEnum<PlainEnumAnnotatedName>().Description.Should()
                .Be("original type: " + typeof(PlainEnum));
        }


        [Spec(nameof(clr_type_can_be_added_with_custom_name))]
        [Fact]
        public void clr_type_can_be_added_with_custom_name_()
        {
            var schema = Schema.Create(_ => { _.Enum("Foo").ClrType(typeof(PlainEnum), "Bar"); });
            schema.HasEnum("Foo").Should().BeFalse();
            schema.GetEnum("Bar").ClrType.Should().Be<PlainEnum>();
            schema.GetEnum<PlainEnum>().Name.Should().Be("Bar");
        }


        [Spec(nameof(clr_type_can_be_added_via_type_param_with_custom_name))]
        [Fact]
        public void clr_type_can_be_added_via_type_param_with_custom_name_()
        {
            var schema = Schema.Create(_ => { _.Enum("Foo").ClrType<PlainEnum>("Bar"); });
            schema.HasEnum("Foo").Should().BeFalse();
            schema.GetEnum("Bar").ClrType.Should().Be<PlainEnum>();
            schema.GetEnum<PlainEnum>().Name.Should().Be("Bar");
        }


        [Spec(nameof(clr_type_can_be_changed_with_custom_name))]
        [Fact]
        public void clr_type_can_be_changed_with_custom_name_()
        {
            var schema = Schema.Create(_ =>
            {
                _.Enum<PlainEnum>()
                    .Description("original type: " + typeof(PlainEnum))
                    .ClrType(typeof(PlainEnumAnnotatedName), "Foo");
            });
            schema.HasEnum<PlainEnum>().Should().BeFalse();
            var foo = schema.GetEnum("Foo");
            foo.Description.Should()
                .Be("original type: " + typeof(PlainEnum));
            foo.ClrType.Should().Be<PlainEnumAnnotatedName>();
        }


        [Spec(nameof(clr_type_can_be_changed_via_type_param_with_custom_name))]
        [Fact]
        public void clr_type_can_be_changed_via_type_param_with_custom_name_()
        {
            var schema = Schema.Create(_ =>
            {
                _.Enum<PlainEnum>()
                    .Description("original type: " + typeof(PlainEnum))
                    .ClrType<PlainEnumAnnotatedName>("Foo");
            });
            schema.HasEnum<PlainEnum>().Should().BeFalse();
            var foo = schema.GetEnum("Foo");
            foo.Description.Should()
                .Be("original type: " + typeof(PlainEnum));
            foo.ClrType.Should().Be<PlainEnumAnnotatedName>();
        }


        [Spec(nameof(clr_type_cannot_be_null))]
        [Fact]
        public void clr_type_cannot_be_null_()
        {
            Schema.Create(_ =>
            {
                var foo = _.Enum("Foo");
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
                _.Enum<PlainEnum>();
                var foo = _.Enum("Foo");
                Action change = () => foo.ClrType<PlainEnum>();
                change.Should().Throw<DuplicateClrTypeException>().WithMessage(
                    "Cannot set CLR type on enum Foo to CLR enum 'PlainEnum': enum PlainEnum already exists with that CLR type.");
            });
        }

        [Spec(nameof(setting_clr_type_does_not_change_name))]
        [Fact]
        public void setting_clr_type_does_not_change_name_()
        {
            var schema = Schema.Create(_ => { _.Enum("Foo").ClrType<PlainEnum>(); });
            schema.GetEnum<PlainEnum>().Name.Should().Be("Foo");
        }


        [Spec(nameof(setting_clr_type_with_name_annotation_does_not_change_name))]
        [Fact]
        public void setting_clr_type_with_name_annotation_does_not_change_name_()
        {
            var schema = Schema.Create(_ => { _.Enum("Foo").ClrType<PlainEnumAnnotatedName>(); });
            schema.GetEnum<PlainEnumAnnotatedName>().Name.Should().Be("Foo");
        }


        [Spec(nameof(setting_clr_type_and_inferring_name_name_should_be_unique))]
        [Fact]
        public void setting_clr_type_and_inferring_name_name_should_be_unique()
        {
            Schema.Create(_ =>
            {
                _.Enum(nameof(PlainEnum));
                var foo = _.Enum("Foo");
                Action change = () => foo.ClrType<PlainEnum>(true);
                change.Should().Throw<DuplicateNameException>().WithMessage(
                    "Cannot set CLR type on enum Foo and infer name: the CLR enum name 'PlainEnum' conflicts with an existing enum named PlainEnum. All GraphQL type names must be unique.");
            });
        }


        [Spec(nameof(setting_clr_type_and_inferring_name_name_annotation_should_be_unique))]
        [Fact]
        public void setting_clr_type_and_inferring_name_name_annotation_should_be_unique()
        {
            Schema.Create(_ =>
            {
                _.Enum(nameof(PlainEnum));
                var foo = _.Enum("Foo");
                Action change = () => foo.ClrType<PlainEnum>(true);
                change.Should().Throw<DuplicateNameException>().WithMessage(
                    "Cannot set CLR type on enum Foo and infer name: the CLR enum name 'PlainEnum' conflicts with an existing enum named PlainEnum. All GraphQL type names must be unique.");
            });
        }




        [Spec(nameof(setting_clr_type_and_inferring_name_name_annotation_should_be_valid))]
        [Fact]
        public void clr_type_name_annotation_should_be_valid_()
        {
            Schema.Create(_ =>
            {
                var foo = _.Enum("Foo");
                new List<Action>
                {
                    () => foo.ClrType<PlainEnumInvalidNameAnnotation>(true),
                    () => foo.ClrType(typeof(PlainEnumInvalidNameAnnotation), true)
                }.ForEach(set =>
                {
                    set.Should().Throw<InvalidNameException>().WithMessage(
                        "Cannot set CLR type on enum Foo and infer name: the annotated name \"(*&#\" on CLR enum 'PlainEnumInvalidNameAnnotation' is not a valid GraphQL name.");
                });
            });
        }


        [Spec(nameof(setting_clr_type_with_duplicate_custom_name_should_throw))]
        [Fact]
        public void custom_name_should_be_unique_()
        {
            Schema.Create(_ =>
            {
                _.Enum("Foo");
                var bar = _.Enum("Bar");
                new List<Action>
                {
                    () => bar.ClrType<PlainEnumAnnotatedName>("Foo"),
                    () => bar.ClrType(typeof(PlainEnumAnnotatedName), "Foo")
                }.ForEach(set =>
                {
                    set.Should().Throw<DuplicateNameException>().WithMessage(
                        "Cannot set CLR type on enum Bar with custom name: the custom name \"Foo\" conflicts with an existing enum named 'Foo'. All type names must be unique.");
                });
            });
        }


        [Spec(nameof(setting_clr_type_with_invalid_custom_name_should_throw))]
        [Fact]
        public void custom_name_should_be_valid_()
        {
            Schema.Create(_ =>
            {
                _.Enum("Foo");
                var bar = _.Enum("Bar");
                new List<Action>
                {
                    () => bar.ClrType<PlainEnumAnnotatedName>("invalid!"),
                    () => bar.ClrType(typeof(PlainEnumAnnotatedName), "invalid!")
                }.ForEach(set =>
                {
                    set.Should().Throw<InvalidNameException>().WithMessage(
                        "Cannot set CLR type on enum Bar with custom name: the custom name \"invalid!\" is not a valid GraphQL name.");
                });
            });
        }


        [Spec(nameof(setting_clr_type_with_null_custom_name_should_throw))]
        [Fact]
        public void custom_name_cannot_be_null_()
        {
            Schema.Create(_ =>
            {
                var bar = _.Enum("Bar");
                new List<Action>
                {
                    () => bar.ClrType<PlainEnumAnnotatedName>(null!),
                    () => bar.ClrType(typeof(PlainEnumAnnotatedName), null!)
                }.ForEach(set => { set.Should().ThrowArgumentNullException("name"); });
            });
        }


        [Spec(nameof(setting_clr_type_and_inferring_name_changes_name))]
        [Fact]
        public void changing_clr_type_changes_name_()
        {
            var schema = Schema.Create(_ => { _.Enum("Foo").ClrType<PlainEnum>(true); });
            schema.HasEnum("Foo").Should().BeFalse();
            schema.GetEnum<PlainEnum>().Name.Should().Be(nameof(PlainEnum));
        }


        [Spec(nameof(setting_clr_type_with_name_annotation_and_inferring_name_changes_name))]
        [Fact]
        public void changing_clr_type_with_name_annotation_changes_name_()
        {
            var schema = Schema.Create(_ => { _.Enum("Foo").ClrType<PlainEnumAnnotatedName>(true); });
            schema.HasEnum("Foo").Should().BeFalse();
            schema.GetEnum<PlainEnumAnnotatedName>().Name.Should().Be(AnnotatedNameValue);
        }


        [Spec(nameof(clr_type_can_be_removed))]
        [Fact]
        public void clr_type_can_be_removed_()
        {
            var schema = Schema.Create(_ => { _.Enum("Foo").ClrType<PlainEnum>().RemoveClrType(); });
            schema.GetEnum("Foo").ClrType.Should().BeNull();
        }


        [Spec(nameof(clr_typed_item_when_type_removed_should_retain_name))]
        [Fact]
        public void clr_typed_item_when_type_removed_should_retain_name_()
        {
            var schema = Schema.Create(_ => { _.Enum<PlainEnum>().RemoveClrType(); });
            schema.GetEnum(nameof(PlainEnum)).ClrType.Should().BeNull();
        }


        [Spec(nameof(clr_typed_item_with_name_annotation_when_clr_type_removed_should_retain_annotated_name))]
        [Fact]
        public void clr_typed_item_with_name_annotation_when_clr_type_removed_should_retain_annotated_name_()
        {
            var schema = Schema.Create(_ => { _.Enum<PlainEnumAnnotatedName>().RemoveClrType(); });
            schema.GetEnum(AnnotatedNameValue).ClrType.Should().BeNull();
        }


        [Spec(nameof(custom_named_clr_typed_item_when_type_removed_should_retain_custom_name))]
        [Fact]
        public void custom_named_clr_typed_item_when_type_removed_should_retain_custom_name_()
        {
            var schema = Schema.Create(_ => { _.Enum("Foo").ClrType<PlainEnum>("Bar").RemoveClrType(); });
            schema.GetEnum("Bar").ClrType.Should().BeNull();
        }
    }
}