// Copyright (c) GraphZen LLC. All rights reserved.
// Licensed under the GraphZen Community License. See the LICENSE file in the project root for license information.

using System;
using System.Diagnostics.CodeAnalysis;
using FluentAssertions;
using GraphZen.Infrastructure;
using GraphZen.TypeSystem.FunctionalTests.Specs;
using JetBrains.Annotations;
using Xunit;

namespace GraphZen.TypeSystem.FunctionalTests.SchemaBuilder.Unions
{
    [NoReorder]
    public class UnionsTests
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


        [Spec(nameof(TypeSystemSpecs.UniquelyInputOutputTypeCollectionSpecs
            .named_item_cannot_be_added_if_name_conflicts_with_type_identity_of_opposite_io))]
        [Fact(Skip = "needs impl")]
        public void named_item_cannot_be_added_if_name_conflicts_with_type_identity_of_opposite_io_()
        {
            Schema.Create(_ =>
            {
                _.InputObject("Foo").Field("inputField", "Bar");
                Action add = () => _.Union("Bar");
                add.Should().Throw<Exception>().WithMessage("x");
            });
        }


        [Spec(nameof(TypeSystemSpecs.UniquelyInputOutputTypeCollectionSpecs
            .named_item_cannot_be_renamed_to_name_conflicts_with_type_identity_of_opposite_io))]
        [Fact(Skip = "TODO")]
        public void named_item_cannot_be_renamed_to_name_conflicts_with_type_identity_of_opposite_io_()
        {
            Schema.Create(_ =>
            {
                _.InputObject("Foo").Field("inputField", "Bar");
                var baz = _.Union("Baz");
                Action rename = () => baz.Name("Bar");
                rename.Should().Throw<Exception>().WithMessage("x");
            });
        }


        [Spec(nameof(TypeSystemSpecs.UniquelyInputOutputTypeCollectionSpecs
            .clr_typed_item_cannot_be_renamed_if_name_conflicts_with_type_identity_of_opposite_io))]
        [Fact(Skip = "todo")]
        public void clr_typed_item_cannot_be_renamed_if_name_conflicts_with_type_identity_of_opposite_io_()
        {
            Schema.Create(_ =>
            {
                _.InputObject("Foo").Field("inputField", "Bar");
                var union = _.Union<PlainAbstractClass>();
                Action rename = () => union.Name("Bar");
                rename.Should().Throw<Exception>().WithMessage("TODO: error message specific to input/output error");
            });
        }


        [Spec(nameof(TypeSystemSpecs.UniquelyInputOutputTypeCollectionSpecs
            .clr_typed_item_with_name_attribute_cannot_be_added_if_name_attribute_conflicts_with_type_identity_of_opposite_io
        ))]
        [Fact(Skip = "TODO")]
        public void
            clr_typed_item_with_name_attribute_cannot_be_added_if_name_attribute_conflicts_with_type_identity_of_opposite_io_()
        {
            Schema.Create(_ =>
            {
                _.InputObject("Foo").Field("inputField", PlainAbstractClassAnnotatedName.AnnotatedName);
                Action add = () => _.Union<PlainAbstractClassAnnotatedName>();
                add.Should().Throw<Exception>("x");
            });
        }


        [Spec(nameof(TypeSystemSpecs.UniquelyInputOutputTypeCollectionSpecs
            .clr_typed_item_cannot_be_added_with_custom_name_if_name_conflicts_with_type_identity_of_opposite_io))]
        [Fact(Skip = "TODO")]
        public void
            clr_typed_item_cannot_be_added_with_custom_name_if_name_conflicts_with_type_identity_of_opposite_io_()
        {
            Schema.Create(_ =>
            {
                _.InputObject("Foo").Field("inputField", "Bar");
                Action add = () => _.Union<PlainAbstractClassAnnotatedName>("Bar");
                add.Should().Throw<Exception>("x");
            });
        }


        [Spec(nameof(TypeSystemSpecs.UniquelyInputOutputTypeCollectionSpecs
            .clr_type_cannot_be_changed_with_custom_name_if_name_conflicts_with_type_identity_of_opposite_io))]
        [Fact(Skip = "TODO")]
        public void clr_type_cannot_be_changed_with_custom_name_if_name_conflicts_with_type_identity_of_opposite_io_()
        {
            // var schema = Schema.Create(_ => { });
        }


        [Spec(nameof(TypeSystemSpecs.NamedCollectionSpecs.named_item_can_be_added_via_sdl))]
        [Fact]
        public void named_item_can_be_added_via_sdl_()
        {
            var schema = Schema.Create(_ => { _.FromSchema(@"union Foo"); });
            schema.HasUnion("Foo").Should().BeTrue();
        }


        [Spec(nameof(TypeSystemSpecs.NamedCollectionSpecs.named_item_can_be_added_via_sdl_extension))]
        [Fact(Skip = "TODO")]
        public void named_item_can_be_added_via_sdl_extension_()
        {
            var schema = Schema.Create(_ => { _.FromSchema(@"extend union Foo"); });
            schema.HasUnion("Foo").Should().BeTrue();
        }


        [Spec(nameof(TypeSystemSpecs.NamedCollectionSpecs.named_item_can_be_added))]
        [Fact]
        public void named_item_can_be_added_()
        {
            var schema = Schema.Create(_ => { _.Union("Foo"); });
            schema.HasUnion("Foo").Should().BeTrue();
        }


        [Spec(nameof(TypeSystemSpecs.NamedCollectionSpecs.named_item_cannot_be_added_with_null_value))]
        [Fact]
        public void named_item_cannot_be_added_with_null_value_()
        {
            Schema.Create(_ =>
            {
                Action add = () => _.Union((string)null!);
                add.Should().ThrowArgumentNullException("name");
            });
        }


        [Spec(nameof(TypeSystemSpecs.NamedCollectionSpecs.named_item_cannot_be_added_with_invalid_name))]
        [Theory]
        [InlineData("  ")]
        [InlineData(" #)(* ")]
        public void named_item_cannot_be_added_with_invalid_name_(string name)
        {
            Schema.Create(_ =>
            {
                Action add = () => _.Union(name);
                add.Should().Throw<InvalidNameException>()
                    .WithMessage(
                        $"Cannot get or create GraphQL type builder for union named \"{name}\". The type name \"{name}\" is not a valid GraphQL name. Names are limited to underscores and alpha-numeric ASCII characters.");
            });
        }


        [Spec(nameof(TypeSystemSpecs.NamedCollectionSpecs.named_item_can_be_renamed))]
        [Fact]
        public void named_item_can_be_renamed_()
        {
            var schema = Schema.Create(_ => { _.Union("Foo").Name("Bar"); });
            schema.HasUnion("Foo").Should().BeFalse();
            schema.HasUnion("Bar").Should().BeTrue();
        }


        [Spec(nameof(TypeSystemSpecs.NamedCollectionSpecs.named_item_cannot_be_renamed_with_null_value))]
        [Fact]
        public void named_item_cannot_be_renamed_with_null_value_()
        {
            Schema.Create(_ =>
            {
                var foo = _.Union("Foo");
                Action rename = () => foo.Name(null!);
                rename.Should().ThrowArgumentNullException("name");
            });
        }


        [Spec(nameof(TypeSystemSpecs.NamedCollectionSpecs.named_item_cannot_be_renamed_with_an_invalid_name))]
        [Theory]
        [InlineData(" 323")]
        [InlineData(" 32#()*3")]
        [InlineData("")]
        public void named_item_cannot_be_renamed_with_an_invalid_name_(string name)
        {
            Schema.Create(_ =>
            {
                var foo = _.Union("Foo");
                Action rename = () => foo.Name(name);
                rename.Should().Throw<InvalidNameException>().WithMessage(
                    $"Cannot rename union Foo. \"{name}\" is not a valid GraphQL name. Names are limited to underscores and alpha-numeric ASCII characters.");
            });
        }


        [Spec(nameof(TypeSystemSpecs.NamedCollectionSpecs.named_item_cannot_be_renamed_if_name_already_exists))]
        [Fact]
        public void named_item_cannot_be_renamed_if_name_already_exists_()
        {
            Schema.Create(_ =>
            {
                _.Union("Foo");
                var bar = _.Union("Bar");
                Action rename = () => bar.Name("Foo");
                rename.Should().Throw<DuplicateNameException>().WithMessage(
                    "Cannot rename union Bar to \"Foo\", union Foo already exists. All GraphQL type names must be unique.");
            });
        }


        [Spec(nameof(TypeSystemSpecs.NamedCollectionSpecs.named_item_can_be_removed))]
        [Fact(Skip = "TODO")]
        public void named_item_can_be_removed_()
        {
            var schema = Schema.Create(_ =>
            {
                _.Union("Foo");
                _.RemoveUnion("Foo");
            });
            schema.HasUnion("Foo").Should().BeFalse();
        }


        [Spec(nameof(TypeSystemSpecs.NamedCollectionSpecs.named_item_cannot_be_removed_with_null_value))]
        [Fact]
        public void named_item_cannot_be_removed_with_null_value_()
        {
            Schema.Create(_ =>
            {
                _.Union("Foo");
                Action remove = () => _.RemoveUnion((string)null!);
                remove.Should().ThrowArgumentNullException("name");
            });
        }


        [Spec(nameof(TypeSystemSpecs.ClrTypedCollectionSpecs.clr_typed_item_can_be_added))]
        [Fact]
        public void clr_typed_item_can_be_added_()
        {
            var schema = Schema.Create(_ => { _.Union(typeof(PlainAbstractClass)); });
            schema.HasUnion<PlainAbstractClass>().Should().BeTrue();
        }


        [Spec(nameof(TypeSystemSpecs.ClrTypedCollectionSpecs.clr_typed_item_can_be_added_via_type_param))]
        [Fact]
        public void clr_typed_item_can_be_added_via_type_param_()
        {
            var schema = Schema.Create(_ => { _.Union<PlainAbstractClass>(); });
            schema.HasUnion<PlainAbstractClass>().Should().BeTrue();
        }


        [Spec(nameof(TypeSystemSpecs.ClrTypedCollectionSpecs.clr_typed_item_cannot_be_added_with_null_value))]
        [Fact]
        public void clr_typed_item_cannot_be_added_with_null_value_()
        {
            Schema.Create(_ =>
            {
                Action add = () => _.Union((Type)null!);
                add.Should().ThrowArgumentNullException("clrType");
            });
        }


        [Spec(nameof(TypeSystemSpecs.ClrTypedCollectionSpecs
            .clr_typed_item_cannot_be_added_with_invalid_name_attribute))]
        [Fact]
        public void clr_typed_item_cannot_be_added_with_invalid_name_attribute_()
        {
            Schema.Create(_ =>
            {
                Action add = () => _.Union(typeof(PlainAbstractClassInvalidNameAnnotation));
                add.Should().Throw<InvalidNameException>().WithMessage(
                    "Cannot get or create GraphQL union type builder with CLR class 'PlainAbstractClassInvalidNameAnnotation'. The name \"@)(*#\" specified in the GraphQLNameAttribute on the PlainAbstractClassInvalidNameAnnotation CLR class is not a valid GraphQL name. Names are limited to underscores and alpha-numeric ASCII characters.");
            });
        }


        [Spec(nameof(TypeSystemSpecs.ClrTypedCollectionSpecs
            .clr_typed_item_with_conflicting_name_can_be_added_with_custom_name))]
        [Fact]
        public void clr_typed_item_with_conflicting_name_can_be_added_with_custom_name_()
        {
            var schema = Schema.Create(_ =>
            {
                _.Union(nameof(PlainAbstractClass));
                _.Union(typeof(PlainAbstractClass), "Foo");
            });
            schema.GetUnion(nameof(PlainAbstractClass)).ClrType.Should().BeNull();
            schema.GetUnion("Foo").ClrType.Should().Be<PlainAbstractClass>();
        }


        [Spec(nameof(TypeSystemSpecs.ClrTypedCollectionSpecs
            .clr_typed_item_with_conflicting_name_can_be_added_via_type_param_with_custom_name))]
        [Fact]
        public void clr_typed_item_with_conflicting_name_can_be_added_via_type_param_with_custom_name_()
        {
            var schema = Schema.Create(_ =>
            {
                _.Union(nameof(PlainAbstractClass));
                _.Union<PlainAbstractClass>("Foo");
            });
            schema.GetUnion(nameof(PlainAbstractClass)).ClrType.Should().BeNull();
            schema.GetUnion("Foo").ClrType.Should().Be<PlainAbstractClass>();
        }


        [Spec(nameof(TypeSystemSpecs.ClrTypedCollectionSpecs
            .clr_typed_item_with_conflicting_name_annotation_can_be_added_with_custom_name))]
        [Fact]
        public void clr_typed_item_with_conflicting_name_annotation_can_be_added_with_custom_name_()
        {
            var schema = Schema.Create(_ =>
            {
                _.Union(PlainAbstractClassAnnotatedName.AnnotatedName);
                _.Union(typeof(PlainAbstractClassAnnotatedName), "Foo");
            });
            schema.GetUnion(PlainAbstractClassAnnotatedName.AnnotatedName).ClrType.Should().BeNull();
            schema.GetUnion("Foo").ClrType.Should().Be<PlainAbstractClassAnnotatedName>();
        }


        [Spec(nameof(TypeSystemSpecs.ClrTypedCollectionSpecs
            .clr_typed_item_with_conflicting_name_annotation_can_be_added_via_type_param_with_custom_name))]
        [Fact]
        public void clr_typed_item_with_conflicting_name_annotation_can_be_added_via_type_param_with_custom_name_()
        {
            var schema = Schema.Create(_ =>
            {
                _.Union(PlainAbstractClassAnnotatedName.AnnotatedName);
                _.Union<PlainAbstractClassAnnotatedName>("Foo");
            });
            schema.GetUnion(PlainAbstractClassAnnotatedName.AnnotatedName).ClrType.Should().BeNull();
            schema.GetUnion("Foo").ClrType.Should().Be<PlainAbstractClassAnnotatedName>();
        }


        [Spec(nameof(TypeSystemSpecs.ClrTypedCollectionSpecs
            .clr_typed_item_with_invalid_name_annotation_can_be_added_with_custom_name))]
        [Fact]
        public void clr_typed_item_with_invalid_name_annotation_can_be_added_with_custom_name_()
        {
            var schema = Schema.Create(_ => { _.Union(typeof(PlainAbstractClassInvalidNameAnnotation), "Foo"); });
            schema.GetUnion<PlainAbstractClassInvalidNameAnnotation>().Name.Should().Be("Foo");
        }


        [Spec(nameof(TypeSystemSpecs.ClrTypedCollectionSpecs
            .clr_typed_item_with_invalid_name_annotation_can_be_added_via_type_param_with_custom_name))]
        [Fact]
        public void clr_typed_item_with_invalid_name_annotation_can_be_added_via_type_param_with_custom_name_()
        {
            var schema = Schema.Create(_ => { _.Union<PlainAbstractClassInvalidNameAnnotation>("Foo"); });
            schema.GetUnion<PlainAbstractClassInvalidNameAnnotation>().Name.Should().Be("Foo");
        }


        [Spec(nameof(TypeSystemSpecs.ClrTypedCollectionSpecs.clr_typed_item_can_be_removed))]
        [Fact(Skip = "TODO")]
        public void clr_typed_item_can_be_removed_()
        {
            var schema = Schema.Create(_ =>
            {
                _.Union<PlainAbstractClass>();
                _.RemoveUnion(typeof(PlainAbstractClass));
            });
            schema.HasUnion<PlainAbstractClass>().Should().BeFalse();
        }


        [Spec(nameof(TypeSystemSpecs.ClrTypedCollectionSpecs.clr_typed_item_can_be_removed_via_type_param))]
        [Fact(Skip = "TODO")]
        public void clr_typed_item_can_be_removed_via_type_param_()
        {
            var schema = Schema.Create(_ =>
            {
                _.Union<PlainAbstractClass>();
                _.RemoveUnion<PlainAbstractClass>();
            });
            schema.HasUnion<PlainAbstractClass>().Should().BeFalse();
        }


        [Spec(nameof(TypeSystemSpecs.ClrTypedCollectionSpecs.clr_typed_item_cannot_be_removed_with_null_value))]
        [Fact]
        public void clr_typed_item_cannot_be_removed_with_null_value_()
        {
            Schema.Create(_ =>
            {
                _.Union<PlainAbstractClass>();
                Action remove = () => _.RemoveUnion((Type)null!);
                remove.Should().ThrowArgumentNullException("clrType");
            });
        }


        [Spec(nameof(TypeSystemSpecs.ClrTypedCollectionSpecs.clr_typed_item_can_have_clr_type_changed))]
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


        [Spec(nameof(TypeSystemSpecs.ClrTypedCollectionSpecs.clr_typed_item_can_have_clr_type_changed_via_type_param))]
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


        [Spec(nameof(TypeSystemSpecs.ClrTypedCollectionSpecs
            .clr_typed_item_cannot_have_clr_type_changed_with_null_value))]
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


        [Spec(nameof(TypeSystemSpecs.ClrTypedCollectionSpecs.clr_typed_item_can_have_clr_type_removed))]
        [Fact(Skip = "TODO")]
        public void clr_typed_item_can_have_clr_type_removed_()
        {
            var schema = Schema.Create(_ => { _.Union<PlainAbstractClass>().RemoveClrType(); });
            schema.HasUnion<PlainAbstractClass>().Should().BeFalse();
            schema.GetUnion(nameof(PlainAbstractClass)).ClrType.Should().BeNull();
        }


        [Spec(nameof(TypeSystemSpecs.ClrTypedCollectionSpecs
            .clr_typed_item_with_type_removed_should_retain_clr_type_name))]
        [Fact(Skip = "todo")]
        public void clr_typed_item_with_type_removed_should_retain_clr_type_name_()
        {
            var schema = Schema.Create(_ => { _.Union<PlainAbstractClass>().RemoveClrType(); });
            schema.HasUnion(nameof(PlainAbstractClass)).Should().BeTrue();
        }


        [Spec(nameof(TypeSystemSpecs.ClrTypedCollectionSpecs
            .clr_typed_item_with_name_annotation_type_removed_should_retain_annotated_name))]
        [Fact(Skip = "TODO")]
        public void clr_typed_item_with_name_annotation_type_removed_should_retain_annotated_name_()
        {
            var schema = Schema.Create(_ => { _.Union<PlainAbstractClassAnnotatedName>().RemoveClrType(); });
            schema.HasUnion(PlainAbstractClassAnnotatedName.AnnotatedName).Should().BeTrue();
        }


        [Spec(nameof(TypeSystemSpecs.ClrTypedCollectionSpecs
            .custom_named_clr_typed_item_with_type_removed_should_retain_custom_name))]
        [Fact(Skip = "TODO")]
        public void custom_named_clr_typed_item_with_type_removed_should_retain_custom_name_()
        {
            var schema = Schema.Create(_ => { _.Union<PlainAbstractClass>().Name("Foo").RemoveClrType(); });
            schema.HasUnion("Foo").Should().BeTrue();
        }


        [Spec(nameof(TypeSystemSpecs.ClrTypedCollectionSpecs.clr_typed_item_can_be_renamed))]
        [Fact]
        public void clr_typed_item_can_be_renamed_()
        {
            var schema = Schema.Create(_ => { _.Union<PlainAbstractClass>().Name("Foo"); });
            schema.GetUnion<PlainAbstractClass>().Name.Should().Be("Foo");
        }


        [Spec(nameof(TypeSystemSpecs.ClrTypedCollectionSpecs.clr_typed_item_with_name_attribute_can_be_renamed))]
        [Fact]
        public void clr_typed_item_with_name_attribute_can_be_renamed_()
        {
            var schema = Schema.Create(_ => { _.Union<PlainAbstractClassAnnotatedName>().Name("Foo"); });
            schema.GetUnion<PlainAbstractClassAnnotatedName>().Name.Should().Be("Foo");
        }


        [Spec(nameof(TypeSystemSpecs.ClrTypedCollectionSpecs.clr_typed_item_cannot_be_renamed_with_an_invalid_name))]
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


        [Spec(nameof(TypeSystemSpecs.ClrTypedCollectionSpecs.clr_typed_item_cannot_be_renamed_if_name_already_exists))]
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


        [Spec(nameof(TypeSystemSpecs.ClrTypedCollectionSpecs.untyped_item_can_have_clr_type_added))]
        [Fact(Skip = "todo")]
        public void untyped_item_can_have_clr_type_added_()
        {
            var schema = Schema.Create(_ => { _.Union("Foo").ClrType<PlainAbstractClass>(); });
            schema.GetUnion("Foo").ClrType.Should().Be<PlainAbstractClass>();
        }


        [Spec(nameof(TypeSystemSpecs.ClrTypedCollectionSpecs
            .untyped_item_cannot_have_clr_type_added_that_is_already_in_use))]
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



        [Spec(nameof(TypeSystemSpecs.ClrTypedCollectionSpecs.adding_clr_type_to_item_changes_name))]
        [Fact(Skip = "todo")]
        public void adding_clr_type_to_item_changes_name_()
        {
            var schema = Schema.Create(_ =>
            {
                _.Union("Foo").ClrType(typeof(PlainAbstractClass));
            });
            schema.HasUnion("Foo").Should().BeFalse();
            schema.GetUnion<PlainAbstractClass>().Name.Should().Be(nameof(PlainAbstractClass));
        }


        [Spec(nameof(TypeSystemSpecs.ClrTypedCollectionSpecs.adding_clr_type_to_item_via_type_param_changes_name))]
        [Fact(Skip = "todo")]
        public void adding_clr_type_to_item_via_type_param_changes_name_()
        {

            var schema = Schema.Create(_ =>
                        {
                            _.Union("Foo").ClrType<PlainAbstractClass>();
                        });
            schema.HasUnion("Foo").Should().BeFalse();
            schema.GetUnion<PlainAbstractClass>().Name.Should().Be(nameof(PlainAbstractClass));

        }


        [Spec(nameof(TypeSystemSpecs.ClrTypedCollectionSpecs
            .adding_clr_type_with_name_annotation_to_item_changes_name))]
        [Fact(Skip = "TODO")]
        public void adding_clr_type_with_name_annotation_to_item_changes_name_()
        {
            var schema = Schema.Create(_ =>
                        {
                            _.Union("Foo").ClrType(typeof(PlainAbstractClassAnnotatedName));
                        });
            schema.HasUnion("Foo").Should().BeFalse();
            schema.GetUnion<PlainAbstractClassAnnotatedName>().Name.Should().Be(PlainAbstractClassAnnotatedName.AnnotatedName);

        }


        [Spec(nameof(TypeSystemSpecs.ClrTypedCollectionSpecs
            .adding_clr_type_with_name_annotation_to_item_via_type_pram_changes_name))]
        [Fact(Skip = "TODO")]
        public void adding_clr_type_with_name_annotation_to_item_via_type_pram_changes_name_()
        {
            var schema = Schema.Create(_ =>
                                    {
                                        _.Union("Foo").ClrType<PlainAbstractClassAnnotatedName>();
                                    });
            schema.HasUnion("Foo").Should().BeFalse();
            schema.GetUnion<PlainAbstractClassAnnotatedName>().Name.Should().Be(PlainAbstractClassAnnotatedName.AnnotatedName);

        }


        [Spec(nameof(TypeSystemSpecs.ClrTypedCollectionSpecs
            .clr_type_with_conflicting_name_can_be_added_using_custom_name))]
        [Fact(Skip = "TODO")]
        public void clr_type_with_conflicting_name_can_be_added_using_custom_name_()
        {
            var schema = Schema.Create(_ =>
            {
                _.Union(nameof(PlainAbstractClass));
                _.Union("Foo").ClrType<PlainAbstractClass>("Bar");
            });
            schema.GetUnion(nameof(PlainAbstractClass)).ClrType.Should().BeNull();
            schema.GetUnion("Bar").ClrType.Should().Be<PlainAbstractClass>();
        }


        [Spec(nameof(TypeSystemSpecs.ClrTypedCollectionSpecs
            .clr_type_with_conflicting_name_annotation_can_be_added_using_custom_name))]
        [Fact(Skip = "TODO")]
        public void clr_type_with_conflicting_name_annotation_can_be_added_using_custom_name_()
        {
            var schema = Schema.Create(_ =>
                        {
                            _.Union(PlainAbstractClassAnnotatedName.AnnotatedName);
                            _.Union("Foo").ClrType<PlainAbstractClassAnnotatedName>("Bar");
                        });
            schema.GetUnion(PlainAbstractClassAnnotatedName.AnnotatedName).ClrType.Should().BeNull();
            schema.GetUnion("Bar").ClrType.Should().Be<PlainAbstractClassAnnotatedName>();

        }


        [Spec(nameof(TypeSystemSpecs.ClrTypedCollectionSpecs
            .clr_type_with_conflicting_name_cannot_be_added_with_invalid_custom_name))]
        [Fact(Skip = "TODO")]
        public void clr_type_with_conflicting_name_cannot_be_added_using_custom_name_if_name_null_()
        {
            // var schema = Schema.Create(_ => { });
        }


        [Spec(nameof(TypeSystemSpecs.ClrTypedCollectionSpecs
            .clr_type_with_conflicting_name_annotation_cannot_be_added_with_invalid_custom_name))]
        [Fact(Skip = "TODO")]
        public void clr_type_with_conflicting_name_annotation_cannot_be_added_using_custom_name_if_name_null_()
        {
            // var schema = Schema.Create(_ => { });
        }


        [Spec(nameof(TypeSystemSpecs.ClrTypedCollectionSpecs
            .clr_type_with_conflicting_name_cannot_be_added_with_null_custom_name))]
        [Fact(Skip = "TODO")]
        public void clr_type_with_conflicting_name_cannot_be_added_using_custom_name_if_name_invalid_()
        {
            // var schema = Schema.Create(_ => { });
        }


        [Spec(nameof(TypeSystemSpecs.ClrTypedCollectionSpecs
            .clr_type_with_conflicting_name_annotation_cannot_be_added_with_null_custom_name))]
        [Fact(Skip = "TODO")]
        public void clr_type_with_conflicting_name_annotation_cannot_be_added_using_custom_name_if_name_invalid_()
        {
            // var schema = Schema.Create(_ => { });
        }


        [Spec(nameof(TypeSystemSpecs.ClrTypedCollectionSpecs
            .clr_type_with_conflicting_name_cannot_be_added_with_conflicting_custom_name))]
        [Fact(Skip = "TODO")]
        public void clr_type_with_conflicting_name_cannot_be_added_using_custom_name_if_name_conflicting_()
        {
            // var schema = Schema.Create(_ => { });
        }


        [Spec(nameof(TypeSystemSpecs.ClrTypedCollectionSpecs
            .clr_type_with_conflicting_name_annotation_cannot_be_added_with_conflicting_custom_name))]
        [Fact(Skip = "TODO")]
        public void clr_type_with_conflicting_name_annotation_cannot_be_added_using_custom_name_if_name_conflicting_()
        {
            // var schema = Schema.Create(_ => { });
        }


        [Spec(nameof(TypeSystemSpecs.ClrTypedCollectionSpecs
            .clr_type_with_conflicting_name_can_be_added_via_type_param_using_custom_name))]
        [Fact(Skip = "TODO")]
        public void clr_type_with_conflicting_name_can_be_added_via_type_param_using_custom_name_()
        {
            // var schema = Schema.Create(_ => { });
        }


        [Spec(nameof(TypeSystemSpecs.ClrTypedCollectionSpecs
            .clr_type_with_conflicting_name_annotation_can_be_added_via_type_param_using_custom_name))]
        [Fact(Skip = "TODO")]
        public void clr_type_with_conflicting_name_annotation_can_be_added_via_type_param_using_custom_name_()
        {
            // var schema = Schema.Create(_ => { });
        }


        [Spec(nameof(TypeSystemSpecs.ClrTypedCollectionSpecs
            .cannot_add_clr_type_to_item_with_conflicting_custom_name))]
        [Fact(Skip = "TODO")]
        public void cannot_add_clr_type_to_item_with_custom_name_if_name_conflicts_()
        {
            // var schema = Schema.Create(_ => { });
        }


        [Spec(nameof(TypeSystemSpecs.ClrTypedCollectionSpecs
            .cannot_add_clr_type_to_item_via_type_param_with_conflicting_custom_name))]
        [Fact(Skip = "TODO")]
        public void cannot_add_clr_type_to_item_via_type_param_with_custom_name_if_name_conflicts_()
        {
            // var schema = Schema.Create(_ => { });
        }


        [Spec(nameof(TypeSystemSpecs.ClrTypedCollectionSpecs
            .cannot_add_clr_type_to_item_with_invalid_custom_name))]
        [Fact(Skip = "TODO")]
        public void cannot_add_clr_type_to_item_with_custom_name_if_name_invalid_()
        {
            // var schema = Schema.Create(_ => { });
        }


        [Spec(nameof(TypeSystemSpecs.ClrTypedCollectionSpecs
            .cannot_add_clr_type_to_item_via_type_param_with_invalid_custom_name))]
        [Fact(Skip = "TODO")]
        public void cannot_add_clr_type_to_item_via_type_param_with_custom_name_if_name_invalid_()
        {
            // var schema = Schema.Create(_ => { });
        }


        [Spec(nameof(TypeSystemSpecs.ClrTypedCollectionSpecs
            .cannot_add_clr_type_to_item_with_null_custom_name))]
        [Fact(Skip = "TODO")]
        public void cannot_add_clr_type_to_item_with_custom_name_if_name_null_()
        {
            // var schema = Schema.Create(_ => { });
        }


        [Spec(nameof(TypeSystemSpecs.ClrTypedCollectionSpecs
            .cannot_add_clr_type_to_item_via_type_param_with_null_custom_name))]
        [Fact(Skip = "TODO")]
        public void cannot_add_clr_type_to_item_via_type_param_with_custom_name_if_name_null_()
        {
            // var schema = Schema.Create(_ => { });
        }
    }
}