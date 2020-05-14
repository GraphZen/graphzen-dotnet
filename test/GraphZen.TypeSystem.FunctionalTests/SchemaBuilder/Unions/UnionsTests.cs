// Copyright (c) GraphZen LLC. All rights reserved.
// Licensed under the GraphZen Community License. See the LICENSE file in the project root for license information.

using System;
using System.Diagnostics.CodeAnalysis;
using FluentAssertions;
using GraphZen.Infrastructure;
using JetBrains.Annotations;
using Xunit;
using static GraphZen.TypeSystem.FunctionalTests.Specs.TypeSystemSpecs;
using static GraphZen.TypeSystem.FunctionalTests.Specs.TypeSystemSpecs.NamedCollectionSpecs;
using static GraphZen.TypeSystem.FunctionalTests.Specs.TypeSystemSpecs.ClrTypedCollectionSpecs;

//using static GraphZen.TypeSystem.FunctionalTests.Specs.TypeSystemSpecs.ClrTypeSpecs;

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


        [Spec(nameof(UniquelyInputOutputTypeCollectionSpecs
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


        [Spec(nameof(UniquelyInputOutputTypeCollectionSpecs
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


        [Spec(nameof(UniquelyInputOutputTypeCollectionSpecs
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


        [Spec(nameof(UniquelyInputOutputTypeCollectionSpecs
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


        [Spec(nameof(UniquelyInputOutputTypeCollectionSpecs
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


        [Spec(nameof(UniquelyInputOutputTypeCollectionSpecs
            .clr_type_cannot_be_changed_with_custom_name_if_name_conflicts_with_type_identity_of_opposite_io))]
        [Fact(Skip = "TODO")]
        public void clr_type_cannot_be_changed_with_custom_name_if_name_conflicts_with_type_identity_of_opposite_io_()
        {
            // var schema = Schema.Create(_ => { });
        }


        [Spec(nameof(named_item_can_be_added_via_sdl))]
        [Fact]
        public void named_item_can_be_added_via_sdl_()
        {
            var schema = Schema.Create(_ => { _.FromSchema(@"union Foo"); });
            schema.HasUnion("Foo").Should().BeTrue();
        }


        [Spec(nameof(named_item_can_be_added_via_sdl_extension))]
        [Fact(Skip = "TODO")]
        public void named_item_can_be_added_via_sdl_extension_()
        {
            var schema = Schema.Create(_ => { _.FromSchema(@"extend union Foo"); });
            schema.HasUnion("Foo").Should().BeTrue();
        }


        [Spec(nameof(named_item_can_be_added))]
        [Fact]
        public void named_item_can_be_added_()
        {
            var schema = Schema.Create(_ => { _.Union("Foo"); });
            schema.HasUnion("Foo").Should().BeTrue();
        }


        [Spec(nameof(named_item_cannot_be_added_with_null_value))]
        [Fact]
        public void named_item_cannot_be_added_with_null_value_()
        {
            Schema.Create(_ =>
            {
                Action add = () => _.Union((string)null!);
                add.Should().ThrowArgumentNullException("name");
            });
        }


        [Spec(nameof(named_item_cannot_be_added_with_invalid_name))]
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


        [Spec(nameof(named_item_can_be_renamed))]
        [Fact]
        public void named_item_can_be_renamed_()
        {
            var schema = Schema.Create(_ => { _.Union("Foo").Name("Bar"); });
            schema.HasUnion("Foo").Should().BeFalse();
            schema.HasUnion("Bar").Should().BeTrue();
        }


        [Spec(nameof(named_item_can_be_removed))]
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


        [Spec(nameof(named_item_cannot_be_removed_with_null_value))]
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


        [Spec(nameof(clr_typed_item_can_be_added))]
        [Fact]
        public void clr_typed_item_can_be_added_()
        {
            var schema = Schema.Create(_ => { _.Union(typeof(PlainAbstractClass)); });
            schema.HasUnion<PlainAbstractClass>().Should().BeTrue();
        }


        [Spec(nameof(clr_typed_item_can_be_added_via_type_param))]
        [Fact]
        public void clr_typed_item_can_be_added_via_type_param_()
        {
            var schema = Schema.Create(_ => { _.Union<PlainAbstractClass>(); });
            schema.HasUnion<PlainAbstractClass>().Should().BeTrue();
        }


        [Spec(nameof(clr_typed_item_cannot_be_added_with_null_value))]
        [Fact]
        public void clr_typed_item_cannot_be_added_with_null_value_()
        {
            Schema.Create(_ =>
            {
                Action add = () => _.Union((Type)null!);
                add.Should().ThrowArgumentNullException("clrType");
            });
        }


        [Spec(nameof(clr_typed_item_cannot_be_added_with_invalid_name_attribute))]
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


        [Spec(nameof(clr_typed_item_with_conflicting_name_can_be_added_with_custom_name))]
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


        [Spec(nameof(clr_typed_item_with_conflicting_name_can_be_added_via_type_param_with_custom_name))]
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


        [Spec(nameof(clr_typed_item_with_conflicting_name_annotation_can_be_added_with_custom_name))]
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


        [Spec(nameof(clr_typed_item_with_conflicting_name_annotation_can_be_added_via_type_param_with_custom_name))]
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


        [Spec(nameof(clr_typed_item_with_invalid_name_annotation_can_be_added_with_custom_name))]
        [Fact]
        public void clr_typed_item_with_invalid_name_annotation_can_be_added_with_custom_name_()
        {
            var schema = Schema.Create(_ => { _.Union(typeof(PlainAbstractClassInvalidNameAnnotation), "Foo"); });
            schema.GetUnion<PlainAbstractClassInvalidNameAnnotation>().Name.Should().Be("Foo");
        }


        [Spec(nameof(clr_typed_item_with_invalid_name_annotation_can_be_added_via_type_param_with_custom_name))]
        [Fact]
        public void clr_typed_item_with_invalid_name_annotation_can_be_added_via_type_param_with_custom_name_()
        {
            var schema = Schema.Create(_ => { _.Union<PlainAbstractClassInvalidNameAnnotation>("Foo"); });
            schema.GetUnion<PlainAbstractClassInvalidNameAnnotation>().Name.Should().Be("Foo");
        }


        [Spec(nameof(clr_typed_item_can_be_removed))]
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


        [Spec(nameof(clr_typed_item_can_be_removed_via_type_param))]
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


        [Spec(nameof(clr_typed_item_cannot_be_removed_with_null_value))]
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