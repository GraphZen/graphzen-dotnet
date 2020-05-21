// Copyright (c) GraphZen LLC. All rights reserved.
// Licensed under the GraphZen Community License. See the LICENSE file in the project root for license information.

using System;
using System.Diagnostics.CodeAnalysis;
using FluentAssertions;
using GraphZen.Infrastructure;
using JetBrains.Annotations;
using Xunit;
using static GraphZen.TypeSystem.FunctionalTests.Specs.TypeSystemSpecs.ClrTypedCollectionSpecs;

namespace GraphZen.TypeSystem.FunctionalTests.Schema_.Enums
{
    [NoReorder]
    public class ClrTypedCollectionTests
    {
        public const string AnnotatedNameValue = nameof(AnnotatedNameValue);

        private enum PlainEnum
        {
        }

        [GraphQLName(AnnotatedNameValue)]
        private enum PlainEnumAnnotatedName
        {
        }

        [GraphQLName("abc ()(*322*&%^")]
        private enum PlainEnumInvalidNameAnnotation
        {
        }




        [Spec(nameof(clr_typed_item_can_be_added))]
        [Fact]
        public void clr_typed_item_can_be_added_()
        {
            var schema = Schema.Create(_ => { _.Enum(typeof(PlainEnum)); });
            schema.HasEnum<PlainEnum>().Should().BeTrue();
        }


        [Spec(nameof(clr_typed_item_can_be_added_via_type_param))]
        [Fact]
        public void clr_typed_item_can_be_added_via_type_param_()
        {
            var schema = Schema.Create(_ => { _.Enum<PlainEnum>(); });
            schema.HasEnum<PlainEnum>().Should().BeTrue();
        }


        [Spec(nameof(clr_typed_item_cannot_be_added_with_null_value))]
        [Fact]
        public void clr_typed_item_cannot_be_added_with_null_value_()
        {
            Schema.Create(_ =>
            {
                Action add = () => _.Enum((Type)null!);
                add.Should().ThrowArgumentNullException("clrType");
            });
        }


        [Spec(nameof(clr_typed_item_cannot_be_added_with_invalid_name_attribute))]
        [Fact]
        public void clr_typed_item_cannot_be_added_with_invalid_name_attribute_()
        {
            Schema.Create(_ =>
            {
                Action add = () => _.Enum<PlainEnumInvalidNameAnnotation>();
                add.Should().Throw<InvalidNameException>().WithMessage(
                    "Cannot get or create GraphQL enum type builder with CLR enum 'PlainEnumInvalidNameAnnotation'. The name \"(*&#\" specified in the GraphQLNameAttribute on the PlainEnumInvalidNameAnnotation CLR enum is not a valid GraphQL name. Names are limited to underscores and alpha-numeric ASCII characters.");
            });
        }


        [Spec(nameof(clr_typed_item_with_conflicting_name_can_be_added_with_custom_name))]
        [Fact]
        public void clr_typed_item_with_conflicting_name_can_be_added_with_custom_name_()
        {
            var schema = Schema.Create(_ =>
            {
                _.Enum(nameof(PlainEnum));
                _.Enum(typeof(PlainEnum), "Foo");
            });
            schema.GetEnum<PlainEnum>().Name.Should().Be("Foo");
            schema.GetEnum(nameof(PlainEnum)).Name.Should().Be(nameof(PlainEnum));
            schema.GetEnum(nameof(PlainEnum)).ClrType.Should().BeNull();
        }


        [Spec(nameof(clr_typed_item_with_conflicting_name_can_be_added_via_type_param_with_custom_name))]
        [Fact]
        public void clr_typed_item_with_conflicting_name_can_be_added_via_type_param_with_custom_name_()
        {
            var schema = Schema.Create(_ =>
            {
                _.Enum(nameof(PlainEnum));
                _.Enum<PlainEnum>("Foo");
            });
            schema.GetEnum<PlainEnum>().Name.Should().Be("Foo");
            schema.GetEnum(nameof(PlainEnum)).Name.Should().Be(nameof(PlainEnum));
            schema.GetEnum(nameof(PlainEnum)).ClrType.Should().BeNull();
        }


        [Spec(nameof(clr_typed_item_with_conflicting_name_annotation_can_be_added_with_custom_name))]
        [Fact]
        public void clr_typed_item_with_conflicting_name_annotation_can_be_added_with_custom_name_()
        {
            var schema = Schema.Create(_ =>
            {
                _.Enum(AnnotatedNameValue);
                _.Enum(typeof(PlainEnumAnnotatedName), "Foo");
            });
            schema.GetEnum<PlainEnumAnnotatedName>().Name.Should().Be("Foo");
            schema.GetEnum(AnnotatedNameValue).Name.Should()
                .Be(AnnotatedNameValue);
            schema.GetEnum(AnnotatedNameValue).ClrType.Should().BeNull();
        }


        [Spec(nameof(clr_typed_item_with_conflicting_name_annotation_can_be_added_via_type_param_with_custom_name))]
        [Fact]
        public void clr_typed_item_with_conflicting_name_annotation_can_be_added_via_type_param_with_custom_name_()
        {
            var schema = Schema.Create(_ =>
            {
                _.Enum(AnnotatedNameValue);
                _.Enum<PlainEnumAnnotatedName>("Foo");
            });
            schema.GetEnum<PlainEnumAnnotatedName>().Name.Should().Be("Foo");
            schema.GetEnum(AnnotatedNameValue).Name.Should()
                .Be(AnnotatedNameValue);
            schema.GetEnum(AnnotatedNameValue).ClrType.Should().BeNull();
        }


        [Spec(nameof(clr_typed_item_with_invalid_name_annotation_can_be_added_with_custom_name))]
        [Fact]
        public void clr_typed_item_with_invalid_name_annotation_can_be_added_with_custom_name_()
        {
            var schema = Schema.Create(_ => { _.Enum(typeof(PlainEnumInvalidNameAnnotation), "Foo"); });
            schema.GetEnum<PlainEnumInvalidNameAnnotation>().Name.Should().Be("Foo");
        }


        [Spec(nameof(clr_typed_item_with_invalid_name_annotation_can_be_added_via_type_param_with_custom_name))]
        [Fact]
        public void clr_typed_item_with_invalid_name_annotation_can_be_added_via_type_param_with_custom_name_()
        {
            var schema = Schema.Create(_ => { _.Enum<PlainEnumInvalidNameAnnotation>("Foo"); });
            schema.GetEnum<PlainEnumInvalidNameAnnotation>().Name.Should().Be("Foo");
        }


        [Spec(nameof(clr_typed_item_can_be_removed))]
        [Fact]
        public void clr_typed_item_can_be_removed_()
        {
            var schema = Schema.Create(_ =>
            {
                _.Enum<PlainEnum>();
                _.RemoveEnum(typeof(PlainEnum));
            });
            schema.HasEnum<PlainEnum>().Should().BeFalse();
        }


        [Spec(nameof(clr_typed_item_can_be_removed_via_type_param))]
        [Fact]
        public void clr_typed_item_can_be_removed_via_type_param_()
        {
            var schema = Schema.Create(_ =>
            {
                _.Enum<PlainEnum>();
                _.RemoveEnum<PlainEnum>();
            });
            schema.HasEnum<PlainEnum>().Should().BeFalse();
        }


        [Spec(nameof(clr_typed_item_cannot_be_removed_with_null_value))]
        [Fact]
        public void clr_typed_item_cannot_be_removed_with_null_value_()
        {
            Schema.Create(_ =>
            {
                Action remove = () => _.RemoveEnum((Type)null!);
                remove.Should().ThrowArgumentNullException("clrType");
            });
        }

        [Spec(nameof(clr_typed_item_uses_clr_type_name))]
        [Fact]
        public void clr_typed_item_uses_clr_type_name_()
        {
            var schema = Schema.Create(_ => { _.Enum<PlainEnum>(); });
            schema.GetEnum<PlainEnum>().Name.Should().Be(nameof(PlainEnum));
        }


        [Spec(nameof(clr_typed_item_with_name_annotation_uses_clr_type_name_annotation))]
        [Fact]
        public void clr_typed_item_with_name_annotation_uses_clr_type_name_annotation_()
        {
            var schema = Schema.Create(_ => { _.Enum<PlainEnumAnnotatedName>(); });
            schema.GetEnum<PlainEnumAnnotatedName>().Name.Should().Be(AnnotatedNameValue);
        }


        [Spec(nameof(clr_typed_item_cannot_be_added_with_null_custom_name))]
        [Fact]
        public void clr_typed_item_cannot_be_added_with_null_custom_name_()
        {
            Schema.Create(_ =>
            {
                Action add = () => _.Enum<PlainEnum>(null!);
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
                Action add = () => _.Enum<PlainEnum>(name);
                add.Should().Throw<InvalidNameException>().WithMessage(
                    $"Cannot get or create GraphQL type builder for enum named \"{name}\". The type name \"{name}\" is not a valid GraphQL name. Names are limited to underscores and alpha-numeric ASCII characters.");
            });
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
        [InlineData("{name}")]
        [InlineData("LKSJ ((")]
        [InlineData("   ")]
        [InlineData(" )*(#&  ")]
        public void clr_typed_item_cannot_be_renamed_with_an_invalid_name_(string name)
        {
            Schema.Create(_ =>
            {
                var b = _.Enum<PlainEnum>();
                Action rename = () => b.Name(name);
                rename.Should().Throw<InvalidNameException>().WithMessage(
                    $"Cannot rename enum PlainEnum: \"{name}\" is not a valid GraphQL name. Names are limited to underscores and alpha-numeric ASCII characters.");
            });
        }


        [Spec(nameof(clr_typed_item_cannot_be_renamed_if_name_already_exists))]
        [Fact]
        public void clr_typed_item_cannot_be_renamed_if_name_already_exists_()
        {
            Schema.Create(_ =>
            {
                _.Enum("Foo");
                var b = _.Enum<PlainEnum>();
                Action rename = () => b.Name("Foo");
                rename.Should().Throw<DuplicateItemException>().WithMessage(
                    "Cannot rename enum PlainEnum to \"Foo\": a type with that name (enum Foo) already exists. All GraphQL type names must be unique.");
            });
        }

        [Spec(nameof(clr_typed_item_subsequently_added_with_custom_name_sets_name))]
        [Fact]
        public void clr_typed_item_subsequently_added_with_custom_name_sets_name_()
        {
            var schema = Schema.Create(_ =>
            {
                _.Enum<PlainEnum>();
                _.Enum<PlainEnum>("Foo");
            });
            schema.GetEnum<PlainEnum>().Name.Should().Be("Foo");
        }


        [Spec(nameof(named_item_subsequently_added_with_type_and_custom_name_sets_clr_type))]
        [Fact]
        public void named_item_subsequently_added_with_type_and_custom_name_sets_clr_type_()
        {
            var schema = Schema.Create(_ =>
            {
                _.Enum("Foo");
                _.Enum<PlainEnum>("Foo");
            });
            schema.GetEnum("Foo").ClrType.Should().Be<PlainEnum>();
        }

        [Spec(nameof(adding_clr_typed_item_updates_matching_named_items_clr_type))]
        [Fact]
        public void adding_clr_typed_item_updates_matching_named_items_clr_type_()
        {
            var schema = Schema.Create(_ =>
            {
                _.Enum(nameof(PlainEnum)).Description("foo");
                _.Enum<PlainEnum>();
            });
            schema.GetEnum<PlainEnum>().Description.Should().Be("foo");
        }


        [Spec(nameof(adding_clr_typed_item_with_name_annotation_updates_matching_named_items_clr_type))]
        [Fact]
        public void adding_clr_typed_item_with_name_annotation_updates_matching_named_items_clr_type_()
        {
            var schema = Schema.Create(_ =>
            {
                _.Enum(AnnotatedNameValue).Description("foo");
                _.Enum<PlainEnumAnnotatedName>();
            });
            schema.GetEnum<PlainEnumAnnotatedName>().Description.Should().Be("foo");
        }

        [Spec(nameof(clr_typed_item_cannot_be_added_with_custom_name_if_named_and_typed_items_already_exist))]
        [Fact]
        public void clr_typed_item_cannot_be_added_with_custom_name_if_named_and_typed_items_already_exist_()
        {
            Schema.Create(_ =>
            {
                _.Enum("Foo");
                _.Enum<PlainEnum>();
                Action add = () => _.Enum<PlainEnum>("Foo");
                add.Should().Throw<DuplicateItemException>().WithMessage(
                    "Cannot create enum Foo with CLR enum 'PlainEnum': both enum Foo and enum PlainEnum (with CLR enum PlainEnum) already exist.");
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
                _.Enum("Foo");
                _.Enum<PlainEnumAnnotatedName>();
                Action add = () => _.Enum<PlainEnumAnnotatedName>("Foo");
                add.Should().Throw<DuplicateItemException>().WithMessage("Cannot create enum Foo with CLR enum 'PlainEnumAnnotatedName': both enum Foo and enum AnnotatedNameValue (with CLR enum PlainEnumAnnotatedName) already exist.");
            });
        }

        [Spec(nameof(adding_clr_typed_item_with_custom_name_does_not_update_item_matching_clr_type_name))]
        [Fact]
        public void adding_clr_typed_item_with_custom_name_does_not_update_item_matching_clr_type_name_()
        {
            var schema = Schema.Create(_ =>
            {
                _.Enum(nameof(PlainEnum));
                _.Enum<PlainEnum>("Foo");
            });
            schema.GetEnum(nameof(PlainEnum)).ClrType.Should().BeNull();
            schema.GetEnum<PlainEnum>().Name.Should().Be("Foo");
        }


        [Spec(nameof(adding_clr_typed_item_with_custom_name_does_not_update_item_matching_clr_type_name_annotation))]
        [Fact]
        public void adding_clr_typed_item_with_custom_name_does_not_update_item_matching_clr_type_name_annotation_()
        {
            var schema = Schema.Create(_ =>
            {
                _.Enum(AnnotatedNameValue);
                _.Enum<PlainEnumAnnotatedName>("Foo");
            });
            schema.GetEnum(AnnotatedNameValue).ClrType.Should().BeNull();
            schema.GetEnum<PlainEnumAnnotatedName>().Name.Should().Be("Foo");
        }
    }
}