// Copyright (c) GraphZen LLC. All rights reserved.
// Licensed under the GraphZen Community License. See the LICENSE file in the project root for license information.


using System;
using System.Diagnostics.CodeAnalysis;
using FluentAssertions;
using GraphZen.Infrastructure;
using JetBrains.Annotations;
using Xunit;
using static GraphZen.TypeSystem.FunctionalTests.Specs.TypeSystemSpecs.ClrTypeSpecs;


namespace GraphZen.TypeSystem.FunctionalTests.SchemaBuilder.Unions.UnionType.ClrType
{
    [NoReorder]
    public class ClrTypeTests
    {
        public abstract class PlainAbstractClass
        {
        }

        [GraphQLName(AnnotatedName)]
        public abstract class PlainAbstractClassAnnotatedName
        {
            public const string AnnotatedName = nameof(AnnotatedName);
        }

        [GraphQLName("@)(*#")]
        public abstract class PlainAbstractClassInvalidNameAnnotation
        {
        }

        [Spec(nameof(clr_typed_item_can_have_clr_type_changed))]
        [Fact]
        public void clr_typed_item_can_have_clr_type_changed_()
        {
            var schema = Schema.Create(_ =>
            {
                _.Union<PlainAbstractClass>().ClrType(typeof(PlainAbstractClassAnnotatedName));
            });
            schema.HasUnion<PlainAbstractClass>().Should().BeFalse();
            schema.HasUnion<PlainAbstractClassAnnotatedName>().Should().BeTrue();
        }


        [Spec(nameof(clr_typed_item_can_have_clr_type_changed_via_type_param))]
        [Fact(Skip = "TODO")]
        public void clr_typed_item_can_have_clr_type_changed_via_type_param_()
        {
            var schema = Schema.Create(_ =>
            {
                _.Union<PlainAbstractClass>().ClrType<PlainAbstractClassAnnotatedName>();
            });
            schema.HasUnion<PlainAbstractClass>().Should().BeFalse();
            schema.HasUnion<PlainAbstractClassAnnotatedName>().Should().BeTrue();
        }


        [Spec(nameof(clr_typed_item_cannot_have_clr_type_changed_with_null_value))]
        [Fact]
        public void clr_typed_item_cannot_have_clr_type_changed_with_null_value_()
        {
            Schema.Create(_ =>
            {
                var union = _.Union<PlainAbstractClass>();
                Action change = () => union.ClrType(null!);
                change.Should().ThrowArgumentNullException("clrType");
            });
        }

        [Spec(nameof(untyped_item_can_have_clr_type_added))]
        [Fact(Skip = "todo")]
        public void untyped_item_can_have_clr_type_added_()
        {
            var schema = Schema.Create(_ => { _.Union("Foo").ClrType<PlainAbstractClass>(); });
            schema.GetUnion("Foo").ClrType.Should().Be<PlainAbstractClass>();
        }


        [Spec(nameof(untyped_item_cannot_have_clr_type_added_that_is_already_in_use))]
        [Fact(Skip = "todo")]
        public void untyped_item_cannot_have_clr_type_added_that_is_already_in_use_()
        {
            Schema.Create(_ =>
            {
                _.Union<PlainAbstractClass>();
                var foo = _.Union("Foo");
                Action add = () => foo.ClrType<PlainAbstractClass>();
                add.Should().Throw<DuplicateClrTypeException>().WithMessage("x");
            });
        }


        [Spec(nameof(adding_clr_type_to_item_changes_name))]
        [Fact(Skip = "todo")]
        public void adding_clr_type_to_item_changes_name_()
        {
            var schema = Schema.Create(_ => { _.Union("Foo").ClrType(typeof(PlainAbstractClass)); });
            schema.HasUnion("Foo").Should().BeFalse();
            schema.GetUnion<PlainAbstractClass>().Name.Should().Be(nameof(PlainAbstractClass));
        }


        [Spec(nameof(adding_clr_type_to_item_via_type_param_changes_name))]
        [Fact(Skip = "todo")]
        public void adding_clr_type_to_item_via_type_param_changes_name_()
        {
            var schema = Schema.Create(_ => { _.Union("Foo").ClrType<PlainAbstractClass>(); });
            schema.HasUnion("Foo").Should().BeFalse();
            schema.GetUnion<PlainAbstractClass>().Name.Should().Be(nameof(PlainAbstractClass));
        }


        [Spec(nameof(adding_clr_type_with_name_annotation_to_item_changes_name))]
        [Fact(Skip = "TODO")]
        public void adding_clr_type_with_name_annotation_to_item_changes_name_()
        {
            var schema = Schema.Create(_ => { _.Union("Foo").ClrType(typeof(PlainAbstractClassAnnotatedName)); });
            schema.HasUnion("Foo").Should().BeFalse();
            schema.GetUnion<PlainAbstractClassAnnotatedName>().Name.Should()
                .Be(PlainAbstractClassAnnotatedName.AnnotatedName);
        }


        [Spec(nameof(adding_clr_type_with_name_annotation_to_item_via_type_pram_changes_name))]
        [Fact(Skip = "TODO")]
        public void adding_clr_type_with_name_annotation_to_item_via_type_pram_changes_name_()
        {
            var schema = Schema.Create(_ => { _.Union("Foo").ClrType<PlainAbstractClassAnnotatedName>(); });
            schema.HasUnion("Foo").Should().BeFalse();
            schema.GetUnion<PlainAbstractClassAnnotatedName>().Name.Should()
                .Be(PlainAbstractClassAnnotatedName.AnnotatedName);
        }


        [Spec(nameof(clr_type_with_conflicting_name_can_be_added_using_custom_name))]
        [Fact(Skip = "TODO")]
        public void clr_type_with_conflicting_name_can_be_added_using_custom_name_()
        {
            var schema = Schema.Create(_ =>
            {
                _.Union(nameof(PlainAbstractClass));
                _.Union("Foo").ClrType(typeof(PlainAbstractClass), "Bar");
            });
            schema.GetUnion(nameof(PlainAbstractClass)).ClrType.Should().BeNull();
            schema.GetUnion("Bar").ClrType.Should().Be<PlainAbstractClass>();
        }


        [Spec(nameof(clr_type_with_conflicting_name_annotation_can_be_added_using_custom_name))]
        [Fact(Skip = "TODO")]
        public void clr_type_with_conflicting_name_annotation_can_be_added_using_custom_name_()
        {
            var schema = Schema.Create(_ =>
            {
                _.Union(PlainAbstractClassAnnotatedName.AnnotatedName);
                _.Union("Foo").ClrType(typeof(PlainAbstractClassAnnotatedName), "Bar");
            });
            schema.GetUnion(PlainAbstractClassAnnotatedName.AnnotatedName).ClrType.Should().BeNull();
            schema.GetUnion("Bar").ClrType.Should().Be<PlainAbstractClassAnnotatedName>();
        }


        [Spec(nameof(clr_type_with_conflicting_name_can_be_added_via_type_param_using_custom_name))]
        [Fact(Skip = "TODO")]
        public void clr_type_with_conflicting_name_can_be_added_via_type_param_using_custom_name_()
        {
            var schema = Schema.Create(_ =>
            {
                _.Union(nameof(PlainAbstractClass));
                _.Union("Foo").ClrType<PlainAbstractClass>("Bar");
            });
            schema.GetUnion(nameof(PlainAbstractClass)).ClrType.Should().BeNull();
            schema.GetUnion("Bar").ClrType.Should().Be<PlainAbstractClass>();
        }


        [Spec(nameof(clr_type_with_conflicting_name_annotation_can_be_added_via_type_param_using_custom_name))]
        [Fact(Skip = "TODO")]
        public void clr_type_with_conflicting_name_annotation_can_be_added_via_type_param_using_custom_name_()
        {
            var schema = Schema.Create(_ =>
            {
                _.Union(PlainAbstractClassAnnotatedName.AnnotatedName);
                _.Union("Foo").ClrType<PlainAbstractClassAnnotatedName>("Bar");
            });
            schema.GetUnion(PlainAbstractClassAnnotatedName.AnnotatedName).ClrType.Should().BeNull();
            schema.GetUnion("Bar").ClrType.Should().Be<PlainAbstractClassAnnotatedName>();
        }

        [Spec(nameof(clr_typed_item_can_have_clr_type_removed))]
        [Fact(Skip = "TODO")]
        public void clr_typed_item_can_have_clr_type_removed_()
        {
            var schema = Schema.Create(_ => { _.Union<PlainAbstractClass>().RemoveClrType(); });
            schema.HasUnion<PlainAbstractClass>().Should().BeFalse();
            schema.GetUnion(nameof(PlainAbstractClass)).ClrType.Should().BeNull();
        }


        [Spec(nameof(clr_typed_item_with_type_removed_should_retain_clr_type_name))]
        [Fact(Skip = "todo")]
        public void clr_typed_item_with_type_removed_should_retain_clr_type_name_()
        {
            var schema = Schema.Create(_ => { _.Union<PlainAbstractClass>().RemoveClrType(); });
            schema.HasUnion(nameof(PlainAbstractClass)).Should().BeTrue();
        }


        [Spec(nameof(clr_typed_item_with_name_annotation_type_removed_should_retain_annotated_name))]
        [Fact(Skip = "TODO")]
        public void clr_typed_item_with_name_annotation_type_removed_should_retain_annotated_name_()
        {
            var schema = Schema.Create(_ => { _.Union<PlainAbstractClassAnnotatedName>().RemoveClrType(); });
            schema.HasUnion(PlainAbstractClassAnnotatedName.AnnotatedName).Should().BeTrue();
        }


        [Spec(nameof(custom_named_clr_typed_item_with_type_removed_should_retain_custom_name))]
        [Fact(Skip = "TODO")]
        public void custom_named_clr_typed_item_with_type_removed_should_retain_custom_name_()
        {
            var schema = Schema.Create(_ => { _.Union<PlainAbstractClass>().Name("Foo").RemoveClrType(); });
            schema.HasUnion("Foo").Should().BeTrue();
        }


        [Spec(nameof(clr_typed_item_can_be_renamed))]
        [Fact]
        public void clr_typed_item_can_be_renamed_()
        {
            var schema = Schema.Create(_ => { _.Union<PlainAbstractClass>().Name("Foo"); });
            schema.GetUnion<PlainAbstractClass>().Name.Should().Be("Foo");
        }


        [Spec(nameof(clr_typed_item_with_name_attribute_can_be_renamed))]
        [Fact]
        public void clr_typed_item_with_name_attribute_can_be_renamed_()
        {
            var schema = Schema.Create(_ => { _.Union<PlainAbstractClassAnnotatedName>().Name("Foo"); });
            schema.GetUnion<PlainAbstractClassAnnotatedName>().Name.Should().Be("Foo");
        }


        [Spec(nameof(clr_typed_item_cannot_be_renamed_with_an_invalid_name))]
        [Theory]
        [InlineData(" ")]
        [InlineData("")]
        [InlineData(")(*# ")]
        public void clr_typed_item_cannot_be_renamed_with_an_invalid_name_(string name)
        {
            Schema.Create(_ =>
            {
                var union = _.Union<PlainAbstractClassAnnotatedName>();
                Action rename = () => union.Name(name);
                rename.Should().Throw<InvalidNameException>().WithMessage(
                    $"Cannot rename union AnnotatedName. \"{name}\" is not a valid GraphQL name. Names are limited to underscores and alpha-numeric ASCII characters.");
            });
        }


        [Spec(nameof(clr_typed_item_cannot_be_renamed_if_name_already_exists))]
        [Fact]
        public void clr_typed_item_cannot_be_renamed_if_name_already_exists_()
        {
            Schema.Create(_ =>
            {
                _.Union("Foo");
                var union = _.Union<PlainAbstractClassAnnotatedName>();
                Action rename = () => union.Name("Foo");
                rename.Should().Throw<DuplicateNameException>().WithMessage(
                    "Cannot rename union AnnotatedName to \"Foo\", union Foo already exists. All GraphQL type names must be unique.");
            });
        }
    }
}