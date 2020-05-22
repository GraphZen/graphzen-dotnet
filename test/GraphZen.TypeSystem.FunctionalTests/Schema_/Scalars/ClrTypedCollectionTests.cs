// Copyright (c) GraphZen LLC. All rights reserved.
// Licensed under the GraphZen Community License. See the LICENSE file in the project root for license information.

using System;
using System.Diagnostics.CodeAnalysis;
using FluentAssertions;
using GraphZen.Infrastructure;
using JetBrains.Annotations;
using Xunit;
using static GraphZen.TypeSystem.FunctionalTests.Specs.TypeSystemSpecs.ClrTypedCollectionSpecs;

namespace GraphZen.TypeSystem.FunctionalTests.Schema_.Scalars
{
    [NoReorder]
    public class ClrTypedCollectionTests
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


        [Spec(nameof(clr_typed_item_can_be_added))]
        [Fact]
        public void clr_typed_item_can_be_added_()
        {
            var schema = Schema.Create(_ => { _.Scalar(typeof(PlainStruct)); });
            schema.HasScalar<PlainStruct>().Should().BeTrue();
        }


        [Spec(nameof(clr_typed_item_can_be_added_via_type_param))]
        [Fact]
        public void clr_typed_item_can_be_added_via_type_param_()
        {
            var schema = Schema.Create(_ => { _.Scalar<PlainStruct>(); });
            schema.HasScalar<PlainStruct>().Should().BeTrue();
        }


        [Spec(nameof(clr_typed_item_cannot_be_added_with_null_value))]
        [Fact]
        public void clr_typed_item_cannot_be_added_with_null_value_()
        {
            Schema.Create(_ =>
            {
                Action add = () => _.Scalar((Type)null!);
                add.Should().ThrowArgumentNullException("clrType");
            });
        }


        [Spec(nameof(clr_typed_item_cannot_be_added_with_invalid_name_attribute))]
        [Fact]
        public void clr_typed_item_cannot_be_added_with_invalid_name_attribute_()
        {
            Schema.Create(_ =>
            {
                Action add = () => _.Scalar<PlainStructInvalidNameAnnotation>();
                add.Should().Throw<InvalidNameException>().WithMessage(
                    "Cannot get or create GraphQL scalar type builder with CLR type 'PlainStructInvalidNameAnnotation'. The name \"(*&#\" specified in the GraphQLNameAttribute on the PlainStructInvalidNameAnnotation CLR type is not a valid GraphQL name. Names are limited to underscores and alpha-numeric ASCII characters.");
            });
        }


        [Spec(nameof(clr_typed_item_with_conflicting_name_can_be_added_with_custom_name))]
        [Fact]
        public void clr_typed_item_with_conflicting_name_can_be_added_with_custom_name_()
        {
            var schema = Schema.Create(_ =>
            {
                _.Scalar(nameof(PlainStruct));
                _.Scalar(typeof(PlainStruct), "Foo");
            });
            schema.GetScalar<PlainStruct>().Name.Should().Be("Foo");
            schema.GetScalar(nameof(PlainStruct)).Name.Should().Be(nameof(PlainStruct));
            schema.GetScalar(nameof(PlainStruct)).ClrType.Should().BeNull();
        }


        [Spec(nameof(clr_typed_item_with_conflicting_name_can_be_added_via_type_param_with_custom_name))]
        [Fact]
        public void clr_typed_item_with_conflicting_name_can_be_added_via_type_param_with_custom_name_()
        {
            var schema = Schema.Create(_ =>
            {
                _.Scalar(nameof(PlainStruct));
                _.Scalar<PlainStruct>("Foo");
            });
            schema.GetScalar<PlainStruct>().Name.Should().Be("Foo");
            schema.GetScalar(nameof(PlainStruct)).Name.Should().Be(nameof(PlainStruct));
            schema.GetScalar(nameof(PlainStruct)).ClrType.Should().BeNull();
        }


        [Spec(nameof(clr_typed_item_with_conflicting_name_annotation_can_be_added_with_custom_name))]
        [Fact]
        public void clr_typed_item_with_conflicting_name_annotation_can_be_added_with_custom_name_()
        {
            var schema = Schema.Create(_ =>
            {
                _.Scalar(PlainStructAnnotatedName.AnnotatedNameValue);
                _.Scalar(typeof(PlainStructAnnotatedName), "Foo");
            });
            schema.GetScalar<PlainStructAnnotatedName>().Name.Should().Be("Foo");
            schema.GetScalar(PlainStructAnnotatedName.AnnotatedNameValue).Name.Should()
                .Be(PlainStructAnnotatedName.AnnotatedNameValue);
            schema.GetScalar(PlainStructAnnotatedName.AnnotatedNameValue).ClrType.Should().BeNull();
        }


        [Spec(nameof(clr_typed_item_with_conflicting_name_annotation_can_be_added_via_type_param_with_custom_name))]
        [Fact]
        public void clr_typed_item_with_conflicting_name_annotation_can_be_added_via_type_param_with_custom_name_()
        {
            var schema = Schema.Create(_ =>
            {
                _.Scalar(PlainStructAnnotatedName.AnnotatedNameValue);
                _.Scalar<PlainStructAnnotatedName>("Foo");
            });
            schema.GetScalar<PlainStructAnnotatedName>().Name.Should().Be("Foo");
            schema.GetScalar(PlainStructAnnotatedName.AnnotatedNameValue).Name.Should()
                .Be(PlainStructAnnotatedName.AnnotatedNameValue);
            schema.GetScalar(PlainStructAnnotatedName.AnnotatedNameValue).ClrType.Should().BeNull();
        }


        [Spec(nameof(clr_typed_item_with_invalid_name_annotation_can_be_added_with_custom_name))]
        [Fact]
        public void clr_typed_item_with_invalid_name_annotation_can_be_added_with_custom_name_()
        {
            var schema = Schema.Create(_ => { _.Scalar(typeof(PlainStructInvalidNameAnnotation), "Foo"); });
            schema.GetScalar<PlainStructInvalidNameAnnotation>().Name.Should().Be("Foo");
        }


        [Spec(nameof(clr_typed_item_with_invalid_name_annotation_can_be_added_via_type_param_with_custom_name))]
        [Fact]
        public void clr_typed_item_with_invalid_name_annotation_can_be_added_via_type_param_with_custom_name_()
        {
            var schema = Schema.Create(_ => { _.Scalar<PlainStructInvalidNameAnnotation>("Foo"); });
            schema.GetScalar<PlainStructInvalidNameAnnotation>().Name.Should().Be("Foo");
        }


        [Spec(nameof(clr_typed_item_can_be_removed))]
        [Fact]
        public void clr_typed_item_can_be_removed_()
        {
            var schema = Schema.Create(_ =>
            {
                _.Scalar<PlainStruct>();
                _.RemoveScalar(typeof(PlainStruct));
            });
            schema.HasScalar<PlainStruct>().Should().BeFalse();
        }


        [Spec(nameof(clr_typed_item_can_be_removed_via_type_param))]
        [Fact]
        public void clr_typed_item_can_be_removed_via_type_param_()
        {
            var schema = Schema.Create(_ =>
            {
                _.Scalar<PlainStruct>();
                _.RemoveScalar<PlainStruct>();
            });
            schema.HasScalar<PlainStruct>().Should().BeFalse();
        }


        [Spec(nameof(clr_typed_item_cannot_be_removed_with_null_value))]
        [Fact]
        public void clr_typed_item_cannot_be_removed_with_null_value_()
        {
            Schema.Create(_ =>
            {
                Action remove = () => _.RemoveScalar((Type)null!);
                remove.Should().ThrowArgumentNullException("clrType");
            });
        }

        [Spec(nameof(clr_typed_item_uses_clr_type_name))]
        [Fact]
        public void clr_typed_item_uses_clr_type_name_()
        {
            var schema = Schema.Create(_ => { _.Scalar<PlainStruct>(); });
            schema.GetScalar<PlainStruct>().Name.Should().Be(nameof(PlainStruct));
        }


        [Spec(nameof(clr_typed_item_with_name_annotation_uses_clr_type_name_annotation))]
        [Fact]
        public void clr_typed_item_with_name_annotation_uses_clr_type_name_annotation_()
        {
            var schema = Schema.Create(_ => { _.Scalar<PlainStructAnnotatedName>(); });
            schema.GetScalar<PlainStructAnnotatedName>().Name.Should().Be(PlainStructAnnotatedName.AnnotatedNameValue);
        }


        [Spec(nameof(clr_typed_item_cannot_be_added_with_null_custom_name))]
        [Fact]
        public void clr_typed_item_cannot_be_added_with_null_custom_name_()
        {
            Schema.Create(_ =>
            {
                Action add = () => _.Scalar<PlainStruct>(null!);
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
                Action add = () => _.Scalar<PlainStruct>(name);
                add.Should().Throw<InvalidNameException>().WithMessage(
                    $"Cannot get or create GraphQL type builder for scalar named \"{name}\". The type name \"{name}\" is not a valid GraphQL name. Names are limited to underscores and alpha-numeric ASCII characters.");
            });
        }


        [Spec(nameof(clr_typed_item_can_be_renamed))]
        [Fact]
        public void clr_typed_item_can_be_renamed_()
        {
            var schema = Schema.Create(_ => { _.Scalar<PlainStruct>().Name("Foo"); });
            schema.GetScalar<PlainStruct>().Name.Should().Be("Foo");
        }


        [Spec(nameof(clr_typed_item_with_name_attribute_can_be_renamed))]
        [Fact]
        public void clr_typed_item_with_name_attribute_can_be_renamed_()
        {
            var schema = Schema.Create(_ => { _.Scalar<PlainStructAnnotatedName>().Name("Foo"); });
            schema.GetScalar<PlainStructAnnotatedName>().Name.Should().Be("Foo");
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
                var b = _.Scalar<PlainStruct>();
                Action rename = () => b.Name(name);
                rename.Should().Throw<InvalidNameException>().WithMessage(
                    $"Cannot rename scalar PlainStruct: \"{name}\" is not a valid GraphQL name. Names are limited to underscores and alpha-numeric ASCII characters.");
            });
        }


        [Spec(nameof(clr_typed_item_cannot_be_renamed_if_name_already_exists))]
        [Fact]
        public void clr_typed_item_cannot_be_renamed_if_name_already_exists_()
        {
            Schema.Create(_ =>
            {
                _.Scalar("Foo");
                var b = _.Scalar<PlainStruct>();
                Action rename = () => b.Name("Foo");
                rename.Should().Throw<DuplicateItemException>().WithMessage(
                    "Cannot rename scalar PlainStruct to \"Foo\": a type with that name (scalar Foo) already exists. All GraphQL type names must be unique.");
            });
        }

        [Spec(nameof(clr_typed_item_subsequently_added_with_custom_name_sets_name))]
        [Fact]
        public void clr_typed_item_subsequently_added_with_custom_name_sets_name_()
        {
            var schema = Schema.Create(_ =>
            {
                _.Scalar<PlainStruct>();
                _.Scalar<PlainStruct>("Foo");
            });
            schema.GetScalar<PlainStruct>().Name.Should().Be("Foo");
        }


        [Spec(nameof(named_item_subsequently_added_with_type_and_custom_name_sets_clr_type))]
        [Fact]
        public void named_item_subsequently_added_with_type_and_custom_name_sets_clr_type_()
        {
            var schema = Schema.Create(_ =>
            {
                _.Scalar("Foo");
                _.Scalar<PlainStruct>("Foo");
            });
            schema.GetScalar("Foo").ClrType.Should().Be<PlainStruct>();
        }

        [Spec(nameof(adding_clr_typed_item_updates_matching_named_items_clr_type))]
        [Fact]
        public void adding_clr_typed_item_updates_matching_named_items_clr_type_()
        {
            var schema = Schema.Create(_ =>
            {
                _.Scalar(nameof(PlainStruct)).Description("foo");
                _.Scalar<PlainStruct>();
            });
            schema.GetScalar<PlainStruct>().Description.Should().Be("foo");
        }


        [Spec(nameof(adding_clr_typed_item_with_name_annotation_updates_matching_named_items_clr_type))]
        [Fact]
        public void adding_clr_typed_item_with_name_annotation_updates_matching_named_items_clr_type_()
        {
            var schema = Schema.Create(_ =>
            {
                _.Scalar(PlainStructAnnotatedName.AnnotatedNameValue).Description("foo");
                _.Scalar<PlainStructAnnotatedName>();
            });
            schema.GetScalar<PlainStructAnnotatedName>().Description.Should().Be("foo");
        }

        [Spec(nameof(clr_typed_item_cannot_be_added_with_custom_name_if_named_and_typed_items_already_exist))]
        [Fact]
        public void clr_typed_item_cannot_be_added_with_custom_name_if_named_and_typed_items_already_exist_()
        {
            Schema.Create(_ =>
            {
                _.Scalar("Foo");
                _.Scalar<PlainStruct>();
                Action add = () => _.Scalar<PlainStruct>("Foo");
                add.Should().Throw<DuplicateItemException>().WithMessage(
                    "Cannot create scalar Foo with CLR type 'PlainStruct': both scalar Foo and scalar PlainStruct (with CLR type PlainStruct) already exist.");
            });
        }


        [Spec(nameof(
            clr_typed_item_with_name_annotation_cannot_be_added_with_custom_name_if_named_and_typed_items_already_exist
        ))]
        [Fact]
        public void
            clr_typed_item_with_name_annotation_cannot_be_added_with_custom_name_if_named_and_typed_items_already_exist_()
        {
            Schema.Create(_ =>
            {
                _.Scalar("Foo");
                _.Scalar<PlainStructAnnotatedName>();
                Action add = () => _.Scalar<PlainStructAnnotatedName>("Foo");
                add.Should().Throw<DuplicateItemException>().WithMessage(
                    "Cannot create scalar Foo with CLR type 'PlainStructAnnotatedName': both scalar Foo and scalar AnnotatedNameValue (with CLR type PlainStructAnnotatedName) already exist.");
            });
        }

        [Spec(nameof(adding_clr_typed_item_with_custom_name_does_not_update_item_matching_clr_type_name))]
        [Fact]
        public void adding_clr_typed_item_with_custom_name_does_not_update_item_matching_clr_type_name_()
        {
            var schema = Schema.Create(_ =>
            {
                _.Scalar(nameof(PlainStruct));
                _.Scalar<PlainStruct>("Foo");
            });
            schema.GetScalar(nameof(PlainStruct)).ClrType.Should().BeNull();
            schema.GetScalar<PlainStruct>().Name.Should().Be("Foo");
        }


        [Spec(nameof(adding_clr_typed_item_with_custom_name_does_not_update_item_matching_clr_type_name_annotation))]
        [Fact]
        public void adding_clr_typed_item_with_custom_name_does_not_update_item_matching_clr_type_name_annotation_()
        {
            var schema = Schema.Create(_ =>
            {
                _.Scalar(PlainStructAnnotatedName.AnnotatedNameValue);
                _.Scalar<PlainStructAnnotatedName>("Foo");
            });
            schema.GetScalar(PlainStructAnnotatedName.AnnotatedNameValue).ClrType.Should().BeNull();
            schema.GetScalar<PlainStructAnnotatedName>().Name.Should().Be("Foo");
        }
    }
}