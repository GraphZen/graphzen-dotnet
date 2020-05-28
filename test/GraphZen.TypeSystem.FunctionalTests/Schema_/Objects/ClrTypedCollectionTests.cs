// Copyright (c) GraphZen LLC. All rights reserved.
// Licensed under the GraphZen Community License. See the LICENSE file in the project root for license information.

using System;
using System.Diagnostics.CodeAnalysis;
using FluentAssertions;
using GraphZen.Infrastructure;
using JetBrains.Annotations;
using Xunit;
using static GraphZen.TypeSystem.FunctionalTests.Specs.TypeSystemSpecs.ClrTypedCollectionSpecs;

namespace GraphZen.TypeSystem.FunctionalTests.Schema_.Objects
{
    [NoReorder]
    public class ClrTypedCollectionTests
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


        [Spec(nameof(clr_typed_item_can_be_added))]
        [Fact]
        public void clr_typed_item_can_be_added_()
        {
            var schema = Schema.Create(_ => { _.Object(typeof(PlainClass)); });
            schema.HasObject<PlainClass>().Should().BeTrue();
        }


        [Spec(nameof(clr_typed_item_can_be_added_via_type_param))]
        [Fact]
        public void clr_typed_item_can_be_added_via_type_param_()
        {
            var schema = Schema.Create(_ => { _.Object<PlainClass>(); });
            schema.HasObject<PlainClass>().Should().BeTrue();
        }


        [Spec(nameof(clr_typed_item_cannot_be_added_with_null_value))]
        [Fact]
        public void clr_typed_item_cannot_be_added_with_null_value_()
        {
            Schema.Create(_ =>
            {
                Action add = () => _.Object((Type)null!);
                add.Should().ThrowArgumentNullException("clrType");
            });
        }


        [Spec(nameof(clr_typed_item_cannot_be_added_with_invalid_name_attribute))]
        [Fact]
        public void clr_typed_item_cannot_be_added_with_invalid_name_attribute_()
        {
            Schema.Create(_ =>
            {
                Action add = () => _.Object<PlainClassInvalidNameAnnotation>();
                add.Should().Throw<InvalidNameException>().WithMessage(
                    "Cannot get or create GraphQL object type builder with CLR class 'PlainClassInvalidNameAnnotation'. The name \"(*&#\" specified in the GraphQLNameAttribute on the PlainClassInvalidNameAnnotation CLR class is not a valid GraphQL name. Names are limited to underscores and alpha-numeric ASCII characters.");
            });
        }


        [Spec(nameof(clr_typed_item_with_conflicting_name_can_be_added_with_custom_name))]
        [Fact]
        public void clr_typed_item_with_conflicting_name_can_be_added_with_custom_name_()
        {
            var schema = Schema.Create(_ =>
            {
                _.Object(nameof(PlainClass));
                _.Object(typeof(PlainClass), "Foo");
            });
            schema.GetObject<PlainClass>().Name.Should().Be("Foo");
            schema.GetObject(nameof(PlainClass)).Name.Should().Be(nameof(PlainClass));
            schema.GetObject(nameof(PlainClass)).ClrType.Should().BeNull();
        }


        [Spec(nameof(clr_typed_item_with_conflicting_name_can_be_added_via_type_param_with_custom_name))]
        [Fact]
        public void clr_typed_item_with_conflicting_name_can_be_added_via_type_param_with_custom_name_()
        {
            var schema = Schema.Create(_ =>
            {
                _.Object(nameof(PlainClass));
                _.Object<PlainClass>("Foo");
            });
            schema.GetObject<PlainClass>().Name.Should().Be("Foo");
            schema.GetObject(nameof(PlainClass)).Name.Should().Be(nameof(PlainClass));
            schema.GetObject(nameof(PlainClass)).ClrType.Should().BeNull();
        }


        [Spec(nameof(clr_typed_item_with_conflicting_name_annotation_can_be_added_with_custom_name))]
        [Fact]
        public void clr_typed_item_with_conflicting_name_annotation_can_be_added_with_custom_name_()
        {
            var schema = Schema.Create(_ =>
            {
                _.Object(PlainClassAnnotatedName.AnnotatedNameValue);
                _.Object(typeof(PlainClassAnnotatedName), "Foo");
            });
            schema.GetObject<PlainClassAnnotatedName>().Name.Should().Be("Foo");
            schema.GetObject(PlainClassAnnotatedName.AnnotatedNameValue).Name.Should()
                .Be(PlainClassAnnotatedName.AnnotatedNameValue);
            schema.GetObject(PlainClassAnnotatedName.AnnotatedNameValue).ClrType.Should().BeNull();
        }


        [Spec(nameof(clr_typed_item_with_conflicting_name_annotation_can_be_added_via_type_param_with_custom_name))]
        [Fact]
        public void clr_typed_item_with_conflicting_name_annotation_can_be_added_via_type_param_with_custom_name_()
        {
            var schema = Schema.Create(_ =>
            {
                _.Object(PlainClassAnnotatedName.AnnotatedNameValue);
                _.Object<PlainClassAnnotatedName>("Foo");
            });
            schema.GetObject<PlainClassAnnotatedName>().Name.Should().Be("Foo");
            schema.GetObject(PlainClassAnnotatedName.AnnotatedNameValue).Name.Should()
                .Be(PlainClassAnnotatedName.AnnotatedNameValue);
            schema.GetObject(PlainClassAnnotatedName.AnnotatedNameValue).ClrType.Should().BeNull();
        }


        [Spec(nameof(clr_typed_item_with_invalid_name_annotation_can_be_added_with_custom_name))]
        [Fact]
        public void clr_typed_item_with_invalid_name_annotation_can_be_added_with_custom_name_()
        {
            var schema = Schema.Create(_ => { _.Object(typeof(PlainClassInvalidNameAnnotation), "Foo"); });
            schema.GetObject<PlainClassInvalidNameAnnotation>().Name.Should().Be("Foo");
        }


        [Spec(nameof(clr_typed_item_with_invalid_name_annotation_can_be_added_via_type_param_with_custom_name))]
        [Fact]
        public void clr_typed_item_with_invalid_name_annotation_can_be_added_via_type_param_with_custom_name_()
        {
            var schema = Schema.Create(_ => { _.Object<PlainClassInvalidNameAnnotation>("Foo"); });
            schema.GetObject<PlainClassInvalidNameAnnotation>().Name.Should().Be("Foo");
        }


        [Spec(nameof(clr_typed_item_can_be_removed))]
        [Fact]
        public void clr_typed_item_can_be_removed_()
        {
            var schema = Schema.Create(_ =>
            {
                _.Object<PlainClass>();
                _.RemoveObject(typeof(PlainClass));
            });
            schema.HasObject<PlainClass>().Should().BeFalse();
        }


        [Spec(nameof(clr_typed_item_can_be_removed_via_type_param))]
        [Fact]
        public void clr_typed_item_can_be_removed_via_type_param_()
        {
            var schema = Schema.Create(_ =>
            {
                _.Object<PlainClass>();
                _.RemoveObject<PlainClass>();
            });
            schema.HasObject<PlainClass>().Should().BeFalse();
        }


        [Spec(nameof(clr_typed_item_cannot_be_removed_with_null_value))]
        [Fact]
        public void clr_typed_item_cannot_be_removed_with_null_value_()
        {
            Schema.Create(_ =>
            {
                Action remove = () => _.RemoveObject((Type)null!);
                remove.Should().ThrowArgumentNullException("clrType");
            });
        }

        [Spec(nameof(clr_typed_item_uses_clr_type_name))]
        [Fact]
        public void clr_typed_item_uses_clr_type_name_()
        {
            var schema = Schema.Create(_ => { _.Object<PlainClass>(); });
            schema.GetObject<PlainClass>().Name.Should().Be(nameof(PlainClass));
        }


        [Spec(nameof(clr_typed_item_with_name_annotation_uses_clr_type_name_annotation))]
        [Fact]
        public void clr_typed_item_with_name_annotation_uses_clr_type_name_annotation_()
        {
            var schema = Schema.Create(_ => { _.Object<PlainClassAnnotatedName>(); });
            schema.GetObject<PlainClassAnnotatedName>().Name.Should().Be(PlainClassAnnotatedName.AnnotatedNameValue);
        }


        [Spec(nameof(clr_typed_item_cannot_be_added_with_null_custom_name))]
        [Fact]
        public void clr_typed_item_cannot_be_added_with_null_custom_name_()
        {
            Schema.Create(_ =>
            {
                Action add = () => _.Object<PlainClass>(null!);
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
                Action add = () => _.Object<PlainClass>(name);
                add.Should().Throw<InvalidNameException>().WithMessage(
                    $"Cannot get or create GraphQL type builder for object named \"{name}\". The type name \"{name}\" is not a valid GraphQL name. Names are limited to underscores and alpha-numeric ASCII characters.");
            });
        }


        [Spec(nameof(clr_typed_item_can_be_renamed))]
        [Fact]
        public void clr_typed_item_can_be_renamed_()
        {
            var schema = Schema.Create(_ => { _.Object<PlainClass>().Name("Foo"); });
            schema.GetObject<PlainClass>().Name.Should().Be("Foo");
        }


        [Spec(nameof(clr_typed_item_with_name_attribute_can_be_renamed))]
        [Fact]
        public void clr_typed_item_with_name_attribute_can_be_renamed_()
        {
            var schema = Schema.Create(_ => { _.Object<PlainClassAnnotatedName>().Name("Foo"); });
            schema.GetObject<PlainClassAnnotatedName>().Name.Should().Be("Foo");
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
                var b = _.Object<PlainClass>();
                Action rename = () => b.Name(name);
                rename.Should().Throw<InvalidNameException>().WithMessage(
                    $"Cannot rename object PlainClass: \"{name}\" is not a valid GraphQL name. Names are limited to underscores and alpha-numeric ASCII characters.");
            });
        }


        [Spec(nameof(clr_typed_item_cannot_be_renamed_if_name_already_exists))]
        [Fact]
        public void clr_typed_item_cannot_be_renamed_if_name_already_exists_()
        {
            Schema.Create(_ =>
            {
                _.Object("Foo");
                var b = _.Object<PlainClass>();
                Action rename = () => b.Name("Foo");
                rename.Should().Throw<DuplicateItemException>().WithMessage(
                    "Cannot rename object PlainClass to \"Foo\": a type with that name (object Foo) already exists. All GraphQL type names must be unique.");
            });
        }

        [Spec(nameof(clr_typed_item_subsequently_added_with_custom_name_sets_name))]
        [Fact]
        public void clr_typed_item_subsequently_added_with_custom_name_sets_name_()
        {
            var schema = Schema.Create(_ =>
            {
                _.Object<PlainClass>();
                _.Object<PlainClass>("Foo");
            });
            schema.GetObject<PlainClass>().Name.Should().Be("Foo");
        }


        [Spec(nameof(named_item_subsequently_added_with_type_and_custom_name_sets_clr_type))]
        [Fact]
        public void named_item_subsequently_added_with_type_and_custom_name_sets_clr_type_()
        {
            var schema = Schema.Create(_ =>
            {
                _.Object("Foo");
                _.Object<PlainClass>("Foo");
            });
            schema.GetObject("Foo").ClrType.Should().Be<PlainClass>();
        }

        [Spec(nameof(adding_clr_typed_item_updates_matching_named_items_clr_type))]
        [Fact]
        public void adding_clr_typed_item_updates_matching_named_items_clr_type_()
        {
            var schema = Schema.Create(_ =>
            {
                _.Object(nameof(PlainClass)).Description("foo");
                _.Object<PlainClass>();
            });
            schema.GetObject<PlainClass>().Description.Should().Be("foo");
        }


        [Spec(nameof(adding_clr_typed_item_with_name_annotation_updates_matching_named_items_clr_type))]
        [Fact]
        public void adding_clr_typed_item_with_name_annotation_updates_matching_named_items_clr_type_()
        {
            var schema = Schema.Create(_ =>
            {
                _.Object(PlainClassAnnotatedName.AnnotatedNameValue).Description("foo");
                _.Object<PlainClassAnnotatedName>();
            });
            schema.GetObject<PlainClassAnnotatedName>().Description.Should().Be("foo");
        }

        [Spec(nameof(clr_typed_item_cannot_be_added_with_custom_name_if_named_and_typed_items_already_exist))]
        [Fact]
        public void clr_typed_item_cannot_be_added_with_custom_name_if_named_and_typed_items_already_exist_()
        {
            Schema.Create(_ =>
            {
                _.Object("Foo");
                _.Object<PlainClass>();
                Action add = () => _.Object<PlainClass>("Foo");
                add.Should().Throw<DuplicateItemException>().WithMessage(
                    "Cannot create object Foo with CLR class 'PlainClass': both object Foo and object PlainClass already exist.");
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
                _.Object("Foo");
                _.Object<PlainClassAnnotatedName>();
                Action add = () => _.Object<PlainClassAnnotatedName>("Foo");
                add.Should().Throw<DuplicateItemException>().WithMessage(
                    "Cannot create object Foo with CLR class 'PlainClassAnnotatedName': both object Foo and object AnnotatedNameValue (CLR class: PlainClassAnnotatedName) already exist.");
            });
        }

        [Spec(nameof(adding_clr_typed_item_with_custom_name_does_not_update_item_matching_clr_type_name))]
        [Fact]
        public void adding_clr_typed_item_with_custom_name_does_not_update_item_matching_clr_type_name_()
        {
            var schema = Schema.Create(_ =>
            {
                _.Object(nameof(PlainClass));
                _.Object<PlainClass>("Foo");
            });
            schema.GetObject(nameof(PlainClass)).ClrType.Should().BeNull();
            schema.GetObject<PlainClass>().Name.Should().Be("Foo");
        }


        [Spec(nameof(adding_clr_typed_item_with_custom_name_does_not_update_item_matching_clr_type_name_annotation))]
        [Fact]
        public void adding_clr_typed_item_with_custom_name_does_not_update_item_matching_clr_type_name_annotation_()
        {
            var schema = Schema.Create(_ =>
            {
                _.Object(PlainClassAnnotatedName.AnnotatedNameValue);
                _.Object<PlainClassAnnotatedName>("Foo");
            });
            schema.GetObject(PlainClassAnnotatedName.AnnotatedNameValue).ClrType.Should().BeNull();
            schema.GetObject<PlainClassAnnotatedName>().Name.Should().Be("Foo");
        }
    }
}