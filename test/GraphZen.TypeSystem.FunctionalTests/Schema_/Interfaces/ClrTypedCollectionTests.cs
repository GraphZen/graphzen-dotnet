// Copyright (c) GraphZen LLC. All rights reserved.
// Licensed under the GraphZen Community License. See the LICENSE file in the project root for license information.

using System;
using System.Diagnostics.CodeAnalysis;
using FluentAssertions;
using GraphZen.Infrastructure;
using JetBrains.Annotations;
using Xunit;
using static GraphZen.TypeSystem.FunctionalTests.Specs.TypeSystemSpecs.ClrTypedCollectionSpecs;

namespace GraphZen.TypeSystem.FunctionalTests.Schema_.Interfaces
{
    [NoReorder]
    public class ClrTypedCollectionTests
    {
        // ReSharper disable once InconsistentNaming
        public interface PlainInterface
        {
        }

        [GraphQLName(AnnotatedNameValue)]
        // ReSharper disable once InconsistentNaming
        public interface PlainInterfaceAnnotatedName
        {
            public const string AnnotatedNameValue = nameof(AnnotatedNameValue);
        }

        [GraphQLName("(*&#")]
        // ReSharper disable once InconsistentNaming
        public interface PlainInterfaceInvalidNameAnnotation
        {
        }


        [Spec(nameof(clr_typed_item_can_be_added))]
        [Fact]
        public void clr_typed_item_can_be_added_()
        {
            var schema = Schema.Create(_ => { _.Interface(typeof(PlainInterface)); });
            schema.HasInterface<PlainInterface>().Should().BeTrue();
        }


        [Spec(nameof(clr_typed_item_can_be_added_via_type_param))]
        [Fact]
        public void clr_typed_item_can_be_added_via_type_param_()
        {
            var schema = Schema.Create(_ => { _.Interface<PlainInterface>(); });
            schema.HasInterface<PlainInterface>().Should().BeTrue();
        }


        [Spec(nameof(clr_typed_item_cannot_be_added_with_null_value))]
        [Fact]
        public void clr_typed_item_cannot_be_added_with_null_value_()
        {
            Schema.Create(_ =>
            {
                Action add = () => _.Interface((Type)null!);
                add.Should().ThrowArgumentNullException("clrType");
            });
        }


        [Spec(nameof(clr_typed_item_cannot_be_added_with_invalid_name_attribute))]
        [Fact]
        public void clr_typed_item_cannot_be_added_with_invalid_name_attribute_()
        {
            Schema.Create(_ =>
            {
                Action add = () => _.Interface<PlainInterfaceInvalidNameAnnotation>();
                add.Should().Throw<InvalidNameException>().WithMessage(
                    "Cannot get or create GraphQL interface type builder with CLR interface 'PlainInterfaceInvalidNameAnnotation'. The name \"(*&#\" specified in the GraphQLNameAttribute on the PlainInterfaceInvalidNameAnnotation CLR interface is not a valid GraphQL name. Names are limited to underscores and alpha-numeric ASCII characters.");
            });
        }


        [Spec(nameof(clr_typed_item_with_conflicting_name_can_be_added_with_custom_name))]
        [Fact]
        public void clr_typed_item_with_conflicting_name_can_be_added_with_custom_name_()
        {
            var schema = Schema.Create(_ =>
            {
                _.Interface(nameof(PlainInterface));
                _.Interface(typeof(PlainInterface), "Foo");
            });
            schema.GetInterface<PlainInterface>().Name.Should().Be("Foo");
            schema.GetInterface(nameof(PlainInterface)).Name.Should().Be(nameof(PlainInterface));
            schema.GetInterface(nameof(PlainInterface)).ClrType.Should().BeNull();
        }


        [Spec(nameof(clr_typed_item_with_conflicting_name_can_be_added_via_type_param_with_custom_name))]
        [Fact]
        public void clr_typed_item_with_conflicting_name_can_be_added_via_type_param_with_custom_name_()
        {
            var schema = Schema.Create(_ =>
            {
                _.Interface(nameof(PlainInterface));
                _.Interface<PlainInterface>("Foo");
            });
            schema.GetInterface<PlainInterface>().Name.Should().Be("Foo");
            schema.GetInterface(nameof(PlainInterface)).Name.Should().Be(nameof(PlainInterface));
            schema.GetInterface(nameof(PlainInterface)).ClrType.Should().BeNull();
        }


        [Spec(nameof(clr_typed_item_with_conflicting_name_annotation_can_be_added_with_custom_name))]
        [Fact]
        public void clr_typed_item_with_conflicting_name_annotation_can_be_added_with_custom_name_()
        {
            var schema = Schema.Create(_ =>
            {
                _.Interface(PlainInterfaceAnnotatedName.AnnotatedNameValue);
                _.Interface(typeof(PlainInterfaceAnnotatedName), "Foo");
            });
            schema.GetInterface<PlainInterfaceAnnotatedName>().Name.Should().Be("Foo");
            schema.GetInterface(PlainInterfaceAnnotatedName.AnnotatedNameValue).Name.Should()
                .Be(PlainInterfaceAnnotatedName.AnnotatedNameValue);
            schema.GetInterface(PlainInterfaceAnnotatedName.AnnotatedNameValue).ClrType.Should().BeNull();
        }


        [Spec(nameof(clr_typed_item_with_conflicting_name_annotation_can_be_added_via_type_param_with_custom_name))]
        [Fact]
        public void clr_typed_item_with_conflicting_name_annotation_can_be_added_via_type_param_with_custom_name_()
        {
            var schema = Schema.Create(_ =>
            {
                _.Interface(PlainInterfaceAnnotatedName.AnnotatedNameValue);
                _.Interface<PlainInterfaceAnnotatedName>("Foo");
            });
            schema.GetInterface<PlainInterfaceAnnotatedName>().Name.Should().Be("Foo");
            schema.GetInterface(PlainInterfaceAnnotatedName.AnnotatedNameValue).Name.Should()
                .Be(PlainInterfaceAnnotatedName.AnnotatedNameValue);
            schema.GetInterface(PlainInterfaceAnnotatedName.AnnotatedNameValue).ClrType.Should().BeNull();
        }


        [Spec(nameof(clr_typed_item_with_invalid_name_annotation_can_be_added_with_custom_name))]
        [Fact]
        public void clr_typed_item_with_invalid_name_annotation_can_be_added_with_custom_name_()
        {
            var schema = Schema.Create(_ => { _.Interface(typeof(PlainInterfaceInvalidNameAnnotation), "Foo"); });
            schema.GetInterface<PlainInterfaceInvalidNameAnnotation>().Name.Should().Be("Foo");
        }


        [Spec(nameof(clr_typed_item_with_invalid_name_annotation_can_be_added_via_type_param_with_custom_name))]
        [Fact]
        public void clr_typed_item_with_invalid_name_annotation_can_be_added_via_type_param_with_custom_name_()
        {
            var schema = Schema.Create(_ => { _.Interface<PlainInterfaceInvalidNameAnnotation>("Foo"); });
            schema.GetInterface<PlainInterfaceInvalidNameAnnotation>().Name.Should().Be("Foo");
        }


        [Spec(nameof(clr_typed_item_can_be_removed))]
        [Fact]
        public void clr_typed_item_can_be_removed_()
        {
            var schema = Schema.Create(_ =>
            {
                _.Interface<PlainInterface>();
                _.RemoveInterface(typeof(PlainInterface));
            });
            schema.HasInterface<PlainInterface>().Should().BeFalse();
        }


        [Spec(nameof(clr_typed_item_can_be_removed_via_type_param))]
        [Fact]
        public void clr_typed_item_can_be_removed_via_type_param_()
        {
            var schema = Schema.Create(_ =>
            {
                _.Interface<PlainInterface>();
                _.RemoveInterface<PlainInterface>();
            });
            schema.HasInterface<PlainInterface>().Should().BeFalse();
        }


        [Spec(nameof(clr_typed_item_cannot_be_removed_with_null_value))]
        [Fact]
        public void clr_typed_item_cannot_be_removed_with_null_value_()
        {
            Schema.Create(_ =>
            {
                Action remove = () => _.RemoveInterface((Type)null!);
                remove.Should().ThrowArgumentNullException("clrType");
            });
        }

        [Spec(nameof(clr_typed_item_uses_clr_type_name))]
        [Fact]
        public void clr_typed_item_uses_clr_type_name_()
        {
            var schema = Schema.Create(_ => { _.Interface<PlainInterface>(); });
            schema.GetInterface<PlainInterface>().Name.Should().Be(nameof(PlainInterface));
        }


        [Spec(nameof(clr_typed_item_with_name_annotation_uses_clr_type_name_annotation))]
        [Fact]
        public void clr_typed_item_with_name_annotation_uses_clr_type_name_annotation_()
        {
            var schema = Schema.Create(_ => { _.Interface<PlainInterfaceAnnotatedName>(); });
            schema.GetInterface<PlainInterfaceAnnotatedName>().Name.Should()
                .Be(PlainInterfaceAnnotatedName.AnnotatedNameValue);
        }


        [Spec(nameof(clr_typed_item_cannot_be_added_with_null_custom_name))]
        [Fact]
        public void clr_typed_item_cannot_be_added_with_null_custom_name_()
        {
            Schema.Create(_ =>
            {
                Action add = () => _.Interface<PlainInterface>(null!);
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
                Action add = () => _.Interface<PlainInterface>(name);
                add.Should().Throw<InvalidNameException>().WithMessage(
                    $"Cannot get or create GraphQL type builder for interface named \"{name}\". The type name \"{name}\" is not a valid GraphQL name. Names are limited to underscores and alpha-numeric ASCII characters.");
            });
        }


        [Spec(nameof(clr_typed_item_can_be_renamed))]
        [Fact]
        public void clr_typed_item_can_be_renamed_()
        {
            var schema = Schema.Create(_ => { _.Interface<PlainInterface>().Name("Foo"); });
            schema.GetInterface<PlainInterface>().Name.Should().Be("Foo");
        }


        [Spec(nameof(clr_typed_item_with_name_attribute_can_be_renamed))]
        [Fact]
        public void clr_typed_item_with_name_attribute_can_be_renamed_()
        {
            var schema = Schema.Create(_ => { _.Interface<PlainInterfaceAnnotatedName>().Name("Foo"); });
            schema.GetInterface<PlainInterfaceAnnotatedName>().Name.Should().Be("Foo");
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
                var b = _.Interface<PlainInterface>();
                Action rename = () => b.Name(name);
                rename.Should().Throw<InvalidNameException>().WithMessage(
                    $"Cannot rename interface PlainInterface: \"{name}\" is not a valid GraphQL name. Names are limited to underscores and alpha-numeric ASCII characters.");
            });
        }


        [Spec(nameof(clr_typed_item_cannot_be_renamed_if_name_already_exists))]
        [Fact]
        public void clr_typed_item_cannot_be_renamed_if_name_already_exists_()
        {
            Schema.Create(_ =>
            {
                _.Interface("Foo");
                var b = _.Interface<PlainInterface>();
                Action rename = () => b.Name("Foo");
                rename.Should().Throw<DuplicateItemException>().WithMessage(
                    "Cannot rename interface PlainInterface to \"Foo\": a type with that name (interface Foo) already exists. All GraphQL type names must be unique.");
            });
        }

        [Spec(nameof(clr_typed_item_subsequently_added_with_custom_name_sets_name))]
        [Fact]
        public void clr_typed_item_subsequently_added_with_custom_name_sets_name_()
        {
            var schema = Schema.Create(_ =>
            {
                _.Interface<PlainInterface>();
                _.Interface<PlainInterface>("Foo");
            });
            schema.GetInterface<PlainInterface>().Name.Should().Be("Foo");
        }


        [Spec(nameof(named_item_subsequently_added_with_type_and_custom_name_sets_clr_type))]
        [Fact]
        public void named_item_subsequently_added_with_type_and_custom_name_sets_clr_type_()
        {
            var schema = Schema.Create(_ =>
            {
                _.Interface("Foo");
                _.Interface<PlainInterface>("Foo");
            });
            schema.GetInterface("Foo").ClrType.Should().Be<PlainInterface>();
        }

        [Spec(nameof(adding_clr_typed_item_updates_matching_named_items_clr_type))]
        [Fact]
        public void adding_clr_typed_item_updates_matching_named_items_clr_type_()
        {
            var schema = Schema.Create(_ =>
            {
                _.Interface(nameof(PlainInterface)).Description("foo");
                _.Interface<PlainInterface>();
            });
            schema.GetInterface<PlainInterface>().Description.Should().Be("foo");
        }


        [Spec(nameof(adding_clr_typed_item_with_name_annotation_updates_matching_named_items_clr_type))]
        [Fact]
        public void adding_clr_typed_item_with_name_annotation_updates_matching_named_items_clr_type_()
        {
            var schema = Schema.Create(_ =>
            {
                _.Interface(PlainInterfaceAnnotatedName.AnnotatedNameValue).Description("foo");
                _.Interface<PlainInterfaceAnnotatedName>();
            });
            schema.GetInterface<PlainInterfaceAnnotatedName>().Description.Should().Be("foo");
        }

        [Spec(nameof(clr_typed_item_cannot_be_added_with_custom_name_if_named_and_typed_items_already_exist))]
        [Fact]
        public void clr_typed_item_cannot_be_added_with_custom_name_if_named_and_typed_items_already_exist_()
        {
            Schema.Create(_ =>
            {
                _.Interface("Foo");
                _.Interface<PlainInterface>();
                Action add = () => _.Interface<PlainInterface>("Foo");
                add.Should().Throw<DuplicateItemException>().WithMessage(
                    "Cannot create interface Foo with CLR interface 'PlainInterface': both interface Foo and interface PlainInterface (with CLR interface PlainInterface) already exist.");
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
                _.Interface("Foo");
                _.Interface<PlainInterfaceAnnotatedName>();
                Action add = () => _.Interface<PlainInterfaceAnnotatedName>("Foo");
                add.Should().Throw<DuplicateItemException>().WithMessage(
                    "Cannot create interface Foo with CLR interface 'PlainInterfaceAnnotatedName': both interface Foo and interface AnnotatedNameValue (with CLR interface PlainInterfaceAnnotatedName) already exist.");
            });
        }

        [Spec(nameof(adding_clr_typed_item_with_custom_name_does_not_update_item_matching_clr_type_name))]
        [Fact]
        public void adding_clr_typed_item_with_custom_name_does_not_update_item_matching_clr_type_name_()
        {
            var schema = Schema.Create(_ =>
            {
                _.Interface(nameof(PlainInterface));
                _.Interface<PlainInterface>("Foo");
            });
            schema.GetInterface(nameof(PlainInterface)).ClrType.Should().BeNull();
            schema.GetInterface<PlainInterface>().Name.Should().Be("Foo");
        }


        [Spec(nameof(adding_clr_typed_item_with_custom_name_does_not_update_item_matching_clr_type_name_annotation))]
        [Fact]
        public void adding_clr_typed_item_with_custom_name_does_not_update_item_matching_clr_type_name_annotation_()
        {
            var schema = Schema.Create(_ =>
            {
                _.Interface(PlainInterfaceAnnotatedName.AnnotatedNameValue);
                _.Interface<PlainInterfaceAnnotatedName>("Foo");
            });
            schema.GetInterface(PlainInterfaceAnnotatedName.AnnotatedNameValue).ClrType.Should().BeNull();
            schema.GetInterface<PlainInterfaceAnnotatedName>().Name.Should().Be("Foo");
        }
    }
}