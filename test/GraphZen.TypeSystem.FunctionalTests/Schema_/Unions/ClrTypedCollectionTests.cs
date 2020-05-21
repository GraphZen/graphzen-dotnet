// Copyright (c) GraphZen LLC. All rights reserved.
// Licensed under the GraphZen Community License. See the LICENSE file in the project root for license information.

using System;
using System.Diagnostics.CodeAnalysis;
using FluentAssertions;
using GraphZen.Infrastructure;
using JetBrains.Annotations;
using Xunit;
using static GraphZen.TypeSystem.FunctionalTests.Specs.TypeSystemSpecs.ClrTypedCollectionSpecs;

namespace GraphZen.TypeSystem.FunctionalTests.Schema_.Unions
{
    [NoReorder]
    public class ClrTypedCollectionTests
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
                Action add = () => _.Union<PlainAbstractClassInvalidNameAnnotation>();
                add.Should().Throw<InvalidNameException>().WithMessage(
                    "Cannot get or create GraphQL union type builder with CLR class 'PlainAbstractClassInvalidNameAnnotation'. The name \"(*&#\" specified in the GraphQLNameAttribute on the PlainAbstractClassInvalidNameAnnotation CLR class is not a valid GraphQL name. Names are limited to underscores and alpha-numeric ASCII characters.");
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
            schema.GetUnion<PlainAbstractClass>().Name.Should().Be("Foo");
            schema.GetUnion(nameof(PlainAbstractClass)).Name.Should().Be(nameof(PlainAbstractClass));
            schema.GetUnion(nameof(PlainAbstractClass)).ClrType.Should().BeNull();
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
            schema.GetUnion<PlainAbstractClass>().Name.Should().Be("Foo");
            schema.GetUnion(nameof(PlainAbstractClass)).Name.Should().Be(nameof(PlainAbstractClass));
            schema.GetUnion(nameof(PlainAbstractClass)).ClrType.Should().BeNull();
        }


        [Spec(nameof(clr_typed_item_with_conflicting_name_annotation_can_be_added_with_custom_name))]
        [Fact]
        public void clr_typed_item_with_conflicting_name_annotation_can_be_added_with_custom_name_()
        {
            var schema = Schema.Create(_ =>
            {
                _.Union(PlainAbstractClassAnnotatedName.AnnotatedNameValue);
                _.Union(typeof(PlainAbstractClassAnnotatedName), "Foo");
            });
            schema.GetUnion<PlainAbstractClassAnnotatedName>().Name.Should().Be("Foo");
            schema.GetUnion(PlainAbstractClassAnnotatedName.AnnotatedNameValue).Name.Should()
                .Be(PlainAbstractClassAnnotatedName.AnnotatedNameValue);
            schema.GetUnion(PlainAbstractClassAnnotatedName.AnnotatedNameValue).ClrType.Should().BeNull();
        }


        [Spec(nameof(clr_typed_item_with_conflicting_name_annotation_can_be_added_via_type_param_with_custom_name))]
        [Fact]
        public void clr_typed_item_with_conflicting_name_annotation_can_be_added_via_type_param_with_custom_name_()
        {
            var schema = Schema.Create(_ =>
            {
                _.Union(PlainAbstractClassAnnotatedName.AnnotatedNameValue);
                _.Union<PlainAbstractClassAnnotatedName>("Foo");
            });
            schema.GetUnion<PlainAbstractClassAnnotatedName>().Name.Should().Be("Foo");
            schema.GetUnion(PlainAbstractClassAnnotatedName.AnnotatedNameValue).Name.Should()
                .Be(PlainAbstractClassAnnotatedName.AnnotatedNameValue);
            schema.GetUnion(PlainAbstractClassAnnotatedName.AnnotatedNameValue).ClrType.Should().BeNull();
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
        [Fact]
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
        [Fact]
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
                Action remove = () => _.RemoveUnion((Type)null!);
                remove.Should().ThrowArgumentNullException("clrType");
            });
        }

        [Spec(nameof(clr_typed_item_uses_clr_type_name))]
        [Fact]
        public void clr_typed_item_uses_clr_type_name_()
        {
            var schema = Schema.Create(_ => { _.Union<PlainAbstractClass>(); });
            schema.GetUnion<PlainAbstractClass>().Name.Should().Be(nameof(PlainAbstractClass));
        }


        [Spec(nameof(clr_typed_item_with_name_annotation_uses_clr_type_name_annotation))]
        [Fact]
        public void clr_typed_item_with_name_annotation_uses_clr_type_name_annotation_()
        {
            var schema = Schema.Create(_ => { _.Union<PlainAbstractClassAnnotatedName>(); });
            schema.GetUnion<PlainAbstractClassAnnotatedName>().Name.Should().Be(PlainAbstractClassAnnotatedName.AnnotatedNameValue);
        }


        [Spec(nameof(clr_typed_item_cannot_be_added_with_null_custom_name))]
        [Fact]
        public void clr_typed_item_cannot_be_added_with_null_custom_name_()
        {
            Schema.Create(_ =>
            {
                Action add = () => _.Union<PlainAbstractClass>(null!);
                add.Should().ThrowArgumentNullException("name");
            });
        }


        [Spec(nameof(clr_typed_item_cannot_be_added_with_invalid_custom_name))]
        [Theory]
        [InlineData("{name}")]
        [InlineData("LKSJ ((")]
        [InlineData("   ")]
        [InlineData(" )*(#&  ")]
        public void clr_typed_item_cannot_be_added_with_invalid_custom_name_(string name)
        {
            Schema.Create(_ =>
            {
                Action add = () => _.Union<PlainAbstractClass>(name);
                add.Should().Throw<InvalidNameException>().WithMessage(
                    $"Cannot get or create GraphQL type builder for union named \"{name}\". The type name \"{name}\" is not a valid GraphQL name. Names are limited to underscores and alpha-numeric ASCII characters.");
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
        [InlineData("{name}")]
        [InlineData("LKSJ ((")]
        [InlineData("   ")]
        [InlineData(" )*(#&  ")]
        public void clr_typed_item_cannot_be_renamed_with_an_invalid_name_(string name)
        {
            Schema.Create(_ =>
            {
                var b = _.Union<PlainAbstractClass>();
                Action rename = () => b.Name(name);
                rename.Should().Throw<InvalidNameException>().WithMessage(
                    $"Cannot rename union PlainAbstractClass: \"{name}\" is not a valid GraphQL name. Names are limited to underscores and alpha-numeric ASCII characters.");
            });
        }


        [Spec(nameof(clr_typed_item_cannot_be_renamed_if_name_already_exists))]
        [Fact]
        public void clr_typed_item_cannot_be_renamed_if_name_already_exists_()
        {
            Schema.Create(_ =>
            {
                _.Union("Foo");
                var b = _.Union<PlainAbstractClass>();
                Action rename = () => b.Name("Foo");
                rename.Should().Throw<DuplicateItemException>().WithMessage(
                    "Cannot rename union PlainAbstractClass to \"Foo\": a type with that name (union Foo) already exists. All GraphQL type names must be unique.");
            });
        }

        [Spec(nameof(clr_typed_item_subsequently_added_with_custom_name_sets_name))]
        [Fact]
        public void clr_typed_item_subsequently_added_with_custom_name_sets_name_()
        {
            var schema = Schema.Create(_ =>
            {
                _.Union<PlainAbstractClass>();
                _.Union<PlainAbstractClass>("Foo");
            });
            schema.GetUnion<PlainAbstractClass>().Name.Should().Be("Foo");
        }


        [Spec(nameof(named_item_subsequently_added_with_type_and_custom_name_sets_clr_type))]
        [Fact]
        public void named_item_subsequently_added_with_type_and_custom_name_sets_clr_type_()
        {
            var schema = Schema.Create(_ =>
            {
                _.Union("Foo");
                _.Union<PlainAbstractClass>("Foo");
            });
            schema.GetUnion("Foo").ClrType.Should().Be<PlainAbstractClass>();
        }

        [Spec(nameof(adding_clr_typed_item_updates_matching_named_items_clr_type))]
        [Fact]
        public void adding_clr_typed_item_updates_matching_named_items_clr_type_()
        {
            var schema = Schema.Create(_ =>
            {
                _.Union(nameof(PlainAbstractClass)).Description("foo");
                _.Union<PlainAbstractClass>();
            });
            schema.GetUnion<PlainAbstractClass>().Description.Should().Be("foo");
        }


        [Spec(nameof(adding_clr_typed_item_with_name_annotation_updates_matching_named_items_clr_type))]
        [Fact]
        public void adding_clr_typed_item_with_name_annotation_updates_matching_named_items_clr_type_()
        {
            var schema = Schema.Create(_ =>
            {
                _.Union(PlainAbstractClassAnnotatedName.AnnotatedNameValue).Description("foo");
                _.Union<PlainAbstractClassAnnotatedName>();
            });
            schema.GetUnion<PlainAbstractClassAnnotatedName>().Description.Should().Be("foo");
        }

        [Spec(nameof(clr_typed_item_cannot_be_added_with_custom_name_if_named_and_typed_items_already_exist))]
        [Fact]
        public void clr_typed_item_cannot_be_added_with_custom_name_if_named_and_typed_items_already_exist_()
        {
            Schema.Create(_ =>
            {
                _.Union("Foo");
                _.Union<PlainAbstractClass>();
                Action add = () => _.Union<PlainAbstractClass>("Foo");
                add.Should().Throw<DuplicateItemException>().WithMessage(
                    "Cannot create union Foo with CLR class 'PlainAbstractClass': both union Foo and union PlainAbstractClass (with CLR class PlainAbstractClass) already exist.");
            });
        }


        [Spec(nameof(
            clr_typed_item_with_name_annotation_cannot_be_added_with_custom_name_if_named_and_typed_items_already_exist
        ))]
        [Fact()]
        public void
            clr_typed_item_with_name_annotation_cannot_be_added_with_custom_name_if_named_and_typed_items_already_exist_()
        {
            Schema.Create(_ =>
            {
                _.Union("Foo");
                _.Union<PlainAbstractClassAnnotatedName>();
                Action add = () => _.Union<PlainAbstractClassAnnotatedName>("Foo");
                add.Should().Throw<DuplicateItemException>().WithMessage("Cannot create union Foo with CLR class 'PlainAbstractClassAnnotatedName': both union Foo and union AnnotatedNameValue (with CLR class PlainAbstractClassAnnotatedName) already exist.");
            });
        }

        [Spec(nameof(adding_clr_typed_item_with_custom_name_does_not_update_item_matching_clr_type_name))]
        [Fact]
        public void adding_clr_typed_item_with_custom_name_does_not_update_item_matching_clr_type_name_()
        {
            var schema = Schema.Create(_ =>
            {
                _.Union(nameof(PlainAbstractClass));
                _.Union<PlainAbstractClass>("Foo");
            });
            schema.GetUnion(nameof(PlainAbstractClass)).ClrType.Should().BeNull();
            schema.GetUnion<PlainAbstractClass>().Name.Should().Be("Foo");
        }


        [Spec(nameof(adding_clr_typed_item_with_custom_name_does_not_update_item_matching_clr_type_name_annotation))]
        [Fact]
        public void adding_clr_typed_item_with_custom_name_does_not_update_item_matching_clr_type_name_annotation_()
        {
            var schema = Schema.Create(_ =>
            {
                _.Union(PlainAbstractClassAnnotatedName.AnnotatedNameValue);
                _.Union<PlainAbstractClassAnnotatedName>("Foo");
            });
            schema.GetUnion(PlainAbstractClassAnnotatedName.AnnotatedNameValue).ClrType.Should().BeNull();
            schema.GetUnion<PlainAbstractClassAnnotatedName>().Name.Should().Be("Foo");
        }
    }
}