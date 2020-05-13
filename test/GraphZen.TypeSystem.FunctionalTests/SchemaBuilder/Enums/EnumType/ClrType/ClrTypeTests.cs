// Copyright (c) GraphZen LLC. All rights reserved.
// Licensed under the GraphZen Community License. See the LICENSE file in the project root for license information.

using System;
using System.Diagnostics.CodeAnalysis;
using FluentAssertions;
using GraphZen.Infrastructure;
using JetBrains.Annotations;
using Xunit;
using static GraphZen.TypeSystem.FunctionalTests.Specs.TypeSystemSpecs.ClrTypeSpecs;

namespace GraphZen.TypeSystem.FunctionalTests.SchemaBuilder.Enums.EnumType.ClrType
{
    [NoReorder]
    public class ClrTypeTests
    {
        public const string AnnotatedName = nameof(AnnotatedName);

        private enum PlainEnum
        {
        }

        [GraphQLName(AnnotatedName)]
        private enum PlainEnumAnnotatedName
        {
        }

        [GraphQLName("abc ()(*322*&%^")]
        // ReSharper disable once UnusedType.Local
        private enum PlainEnumInvalidNameAnnotation
        {
        }

        [Spec(nameof(clr_typed_item_can_have_clr_type_changed))]
        [Fact]
        public void clr_typed_item_can_have_clr_type_changed_()
        {
            var schema = Schema.Create(_ => { _.Enum<PlainEnum>().ClrType<PlainEnumAnnotatedName>(); });
            schema.HasEnum<PlainEnum>().Should().BeFalse();
            schema.HasEnum<PlainEnumAnnotatedName>().Should().BeTrue();
        }


        [Spec(nameof(clr_typed_item_cannot_have_clr_type_changed_with_null_value))]
        [Fact]
        public void clr_typed_item_cannot_have_clr_type_changed_with_null_value_()
        {
            Schema.Create(_ =>
            {
                var poce = _.Enum<PlainEnum>();
                Action change = () => poce.ClrType(null!);
                change.Should().ThrowArgumentNullException("clrType");
            });
        }

        [Spec(nameof(untyped_item_can_have_clr_type_added))]
        [Fact]
        public void untyped_item_can_have_clr_type_added_()
        {
            var schema = Schema.Create(_ => { _.Enum("Foo").ClrType<PlainEnum>(); });
            schema.GetEnum("Foo").ClrType.Should().Be<PlainEnum>();
        }


        [Spec(nameof(untyped_item_cannot_have_clr_type_added_that_is_already_in_use))]
        [Fact(Skip = "needs impl")]
        public void untyped_item_cannot_have_clr_type_added_that_is_already_in_use_()
        {
            Schema.Create(_ =>
            {
                _.Enum<PlainEnum>();
                var foo = _.Enum("Foo");
                Action add = () => foo.ClrType<PlainEnum>();
                add.Should().Throw<DuplicateClrTypeException>();
            });
        }

        [Spec(nameof(clr_typed_item_can_have_clr_type_removed))]
        [Fact(Skip = "needs impl")]
        public void clr_typed_item_can_have_clr_type_removed_()
        {
            var schema = Schema.Create(_ => { _.Enum<PlainEnum>().RemoveClrType(); });
            schema.GetEnum(nameof(PlainEnum)).ClrType.Should().BeNull();
        }


        [Spec(nameof(clr_typed_item_with_type_removed_should_retain_clr_type_name))]
        [Fact(Skip = "nees impl")]
        public void clr_typed_item_with_type_removed_should_retain_clr_type_name_()
        {
            var schema = Schema.Create(_ => { _.Enum<PlainEnum>().RemoveClrType(); });
            schema.HasEnum(nameof(PlainEnum)).Should().BeTrue();
        }


        [Spec(nameof(clr_typed_item_with_name_annotation_type_removed_should_retain_annotated_name))]
        [Fact(Skip = "needs impl")]
        public void clr_typed_item_with_name_annotation_type_removed_should_retain_annotated_name_()
        {
            var schema = Schema.Create(_ => { _.Enum<PlainEnumAnnotatedName>().RemoveClrType(); });
            schema.HasEnum(AnnotatedName).Should().BeTrue();
        }


        [Spec(nameof(clr_typed_item_can_be_renamed))]
        [Fact]
        public void clr_typed_item_can_be_renamed_()
        {
            var schema = Schema.Create(_ => { _.Enum<PlainEnum>().Name("Foo"); });
            schema.GetEnum<PlainEnum>().Name.Should().Be("Foo");
        }


        [Spec(nameof(clr_typed_item_with_name_attribute_can_be_renamed))]
        [Fact]
        public void clr_typed_item_with_name_attribute_can_be_renamed_()
        {
            var schema = Schema.Create(_ => { _.Enum<PlainEnumAnnotatedName>().Name("Foo"); });
            schema.GetEnum<PlainEnumAnnotatedName>().Name.Should().Be("Foo");
        }


        [Spec(nameof(clr_typed_item_cannot_be_renamed_with_an_invalid_name))]
        [Theory]
        [InlineData("  xy")]
        [InlineData("")]
        public void clr_typed_item_cannot_be_renamed_with_an_invalid_name_(string name)
        {
            Schema.Create(_ =>
            {
                var poce = _.Enum<PlainEnum>();
                Action rename = () => poce.Name(name);
                rename.Should().Throw<InvalidNameException>()
                    .WithMessage(@$"Cannot rename enum PlainEnum. ""{name}"" is not a valid GraphQL name. Names are limited to underscores and alpha-numeric ASCII characters.");
            });
        }


        [Spec(nameof(clr_typed_item_cannot_be_renamed_if_name_already_exists))]
        [Fact]
        public void clr_typed_item_cannot_be_renamed_if_name_already_exists_()
        {
            Schema.Create(_ =>
            {
                _.Enum("Foo");
                var poce = _.Enum<PlainEnum>();
                Action rename = () => poce.Name("Foo");
                rename.Should().Throw<DuplicateNameException>().WithMessage(
                    @"Cannot rename enum PlainEnum to ""Foo"", enum Foo already exists. All GraphQL type names must be unique.");
            });
        }

    }
}