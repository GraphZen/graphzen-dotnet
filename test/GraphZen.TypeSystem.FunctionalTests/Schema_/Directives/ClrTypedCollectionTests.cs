// Copyright (c) GraphZen LLC. All rights reserved.
// Licensed under the GraphZen Community License. See the LICENSE file in the project root for license information.

using System;
using System.Diagnostics.CodeAnalysis;
using FluentAssertions;
using GraphZen.Infrastructure;
using JetBrains.Annotations;
using Xunit;
using static GraphZen.TypeSystem.FunctionalTests.Specs.TypeSystemSpecs.ClrTypedCollectionSpecs;

namespace GraphZen.TypeSystem.FunctionalTests.Schema_.Directives
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
            var schema = Schema.Create(_ => { _.Directive(typeof(PlainClass)); });
            schema.HasDirective<PlainClass>().Should().BeTrue();
        }


        [Spec(nameof(clr_typed_item_can_be_added_via_type_param))]
        [Fact]
        public void clr_typed_item_can_be_added_via_type_param_()
        {
            var schema = Schema.Create(_ => { _.Directive<PlainClass>(); });
            schema.HasDirective<PlainClass>().Should().BeTrue();
        }


        [Spec(nameof(clr_typed_item_cannot_be_added_with_null_value))]
        [Fact]
        public void clr_typed_item_cannot_be_added_with_null_value_()
        {
            Schema.Create(_ =>
            {
                Action add = () => _.Directive((Type)null!);
                add.Should().ThrowArgumentNullException("clrType");
            });
        }


        [Spec(nameof(clr_typed_item_cannot_be_added_with_invalid_name_attribute))]
        [Fact]
        public void clr_typed_item_cannot_be_added_with_invalid_name_attribute_()
        {
            Schema.Create(_ =>
            {
                Action add = () => _.Directive<PlainClassInvalidNameAnnotation>();
                add.Should().Throw<InvalidNameException>().WithMessage(
                    "Cannot create directive with CLR class 'PlainClassInvalidNameAnnotation'. The name \"(*&#\" specified in the GraphQLNameAttribute on the PlainClassInvalidNameAnnotation CLR class is not a valid GraphQL name. Names are limited to underscores and alpha-numeric ASCII characters.");
            });
        }


        [Spec(nameof(clr_typed_item_with_conflicting_name_can_be_added_with_custom_name))]
        [Fact]
        public void clr_typed_item_with_conflicting_name_can_be_added_with_custom_name_()
        {
            var schema = Schema.Create(_ =>
            {
                _.Directive(nameof(PlainClass));
                _.Directive(typeof(PlainClass), "Foo");
            });
            schema.GetDirective<PlainClass>().Name.Should().Be("Foo");
            schema.GetDirective(nameof(PlainClass)).Name.Should().Be(nameof(PlainClass));
            schema.GetDirective(nameof(PlainClass)).ClrType.Should().BeNull();
        }


        [Spec(nameof(clr_typed_item_with_conflicting_name_can_be_added_via_type_param_with_custom_name))]
        [Fact]
        public void clr_typed_item_with_conflicting_name_can_be_added_via_type_param_with_custom_name_()
        {
            var schema = Schema.Create(_ =>
            {
                _.Directive(nameof(PlainClass));
                _.Directive<PlainClass>("Foo");
            });
            schema.GetDirective<PlainClass>().Name.Should().Be("Foo");
            schema.GetDirective(nameof(PlainClass)).Name.Should().Be(nameof(PlainClass));
            schema.GetDirective(nameof(PlainClass)).ClrType.Should().BeNull();
        }


        [Spec(nameof(clr_typed_item_with_conflicting_name_annotation_can_be_added_with_custom_name))]
        [Fact]
        public void clr_typed_item_with_conflicting_name_annotation_can_be_added_with_custom_name_()
        {
            var schema = Schema.Create(_ =>
            {
                _.Directive(PlainClassAnnotatedName.AnnotatedNameValue);
                _.Directive(typeof(PlainClassAnnotatedName), "Foo");
            });
            schema.GetDirective<PlainClassAnnotatedName>().Name.Should().Be("Foo");
            schema.GetDirective(PlainClassAnnotatedName.AnnotatedNameValue).Name.Should()
                .Be(PlainClassAnnotatedName.AnnotatedNameValue);
            schema.GetDirective(PlainClassAnnotatedName.AnnotatedNameValue).ClrType.Should().BeNull();
        }


        [Spec(nameof(clr_typed_item_with_conflicting_name_annotation_can_be_added_via_type_param_with_custom_name))]
        [Fact]
        public void clr_typed_item_with_conflicting_name_annotation_can_be_added_via_type_param_with_custom_name_()
        {
            var schema = Schema.Create(_ =>
            {
                _.Directive(PlainClassAnnotatedName.AnnotatedNameValue);
                _.Directive<PlainClassAnnotatedName>("Foo");
            });
            schema.GetDirective<PlainClassAnnotatedName>().Name.Should().Be("Foo");
            schema.GetDirective(PlainClassAnnotatedName.AnnotatedNameValue).Name.Should()
                .Be(PlainClassAnnotatedName.AnnotatedNameValue);
            schema.GetDirective(PlainClassAnnotatedName.AnnotatedNameValue).ClrType.Should().BeNull();
        }


        [Spec(nameof(clr_typed_item_with_invalid_name_annotation_can_be_added_with_custom_name))]
        [Fact]
        public void clr_typed_item_with_invalid_name_annotation_can_be_added_with_custom_name_()
        {
            var schema = Schema.Create(_ => { _.Directive(typeof(PlainClassInvalidNameAnnotation), "Foo"); });
            schema.GetDirective<PlainClassInvalidNameAnnotation>().Name.Should().Be("Foo");
        }


        [Spec(nameof(clr_typed_item_with_invalid_name_annotation_can_be_added_via_type_param_with_custom_name))]
        [Fact]
        public void clr_typed_item_with_invalid_name_annotation_can_be_added_via_type_param_with_custom_name_()
        {
            var schema = Schema.Create(_ => { _.Directive<PlainClassInvalidNameAnnotation>("Foo"); });
            schema.GetDirective<PlainClassInvalidNameAnnotation>().Name.Should().Be("Foo");
        }


        [Spec(nameof(clr_typed_item_can_be_removed))]
        [Fact]
        public void clr_typed_item_can_be_removed_()
        {
            var schema = Schema.Create(_ =>
            {
                _.Directive<PlainClass>();
                _.RemoveDirective(typeof(PlainClass));
            });
            schema.HasDirective<PlainClass>().Should().BeFalse();
        }


        [Spec(nameof(clr_typed_item_can_be_removed_via_type_param))]
        [Fact]
        public void clr_typed_item_can_be_removed_via_type_param_()
        {
            var schema = Schema.Create(_ =>
            {
                _.Directive<PlainClass>();
                _.RemoveDirective<PlainClass>();
            });
            schema.HasDirective<PlainClass>().Should().BeFalse();
        }


        [Spec(nameof(clr_typed_item_cannot_be_removed_with_null_value))]
        [Fact]
        public void clr_typed_item_cannot_be_removed_with_null_value_()
        {
            Schema.Create(_ =>
            {
                Action remove = () => _.RemoveDirective((Type)null!);
                remove.Should().ThrowArgumentNullException("clrType");
            });
        }

        [Spec(nameof(clr_typed_item_uses_clr_type_name))]
        [Fact]
        public void clr_typed_item_uses_clr_type_name_()
        {
            var schema = Schema.Create(_ => { _.Directive<PlainClass>(); });
            schema.GetDirective<PlainClass>().Name.Should().Be(nameof(PlainClass));
        }


        [Spec(nameof(clr_typed_item_with_name_annotation_uses_clr_type_name_annotation))]
        [Fact]
        public void clr_typed_item_with_name_annotation_uses_clr_type_name_annotation_()
        {
            var schema = Schema.Create(_ => { _.Directive<PlainClassAnnotatedName>(); });
            schema.GetDirective<PlainClassAnnotatedName>().Name.Should().Be(PlainClassAnnotatedName.AnnotatedNameValue);
        }


        [Spec(nameof(clr_typed_item_cannot_be_added_with_null_custom_name))]
        [Fact]
        public void clr_typed_item_cannot_be_added_with_null_custom_name_()
        {
            Schema.Create(_ =>
            {
                Action add = () => _.Directive<PlainClass>(null!);
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
                Action add = () => _.Directive<PlainClass>(name);
                add.Should().Throw<InvalidNameException>().WithMessage(
                    $"Cannot create directive named \"{name}\": \"{name}\" is not a valid GraphQL name. Names are limited to underscores and alpha-numeric ASCII characters.");
            });
        }


        [Spec(nameof(clr_typed_item_can_be_renamed))]
        [Fact]
        public void clr_typed_item_can_be_renamed_()
        {
            var schema = Schema.Create(_ => { _.Directive<PlainClass>().Name("Foo"); });
            schema.GetDirective<PlainClass>().Name.Should().Be("Foo");
        }


        [Spec(nameof(clr_typed_item_with_name_attribute_can_be_renamed))]
        [Fact]
        public void clr_typed_item_with_name_attribute_can_be_renamed_()
        {
            var schema = Schema.Create(_ => { _.Directive<PlainClassAnnotatedName>().Name("Foo"); });
            schema.GetDirective<PlainClassAnnotatedName>().Name.Should().Be("Foo");
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
                var b = _.Directive<PlainClass>();
                Action rename = () => b.Name(name);
                rename.Should().Throw<InvalidNameException>().WithMessage(
                    $"Cannot rename directive PlainClass: \"{name}\" is not a valid GraphQL name. Names are limited to underscores and alpha-numeric ASCII characters.");
            });
        }


        [Spec(nameof(clr_typed_item_cannot_be_renamed_if_name_already_exists))]
        [Fact]
        public void clr_typed_item_cannot_be_renamed_if_name_already_exists_()
        {
            Schema.Create(_ =>
            {
                _.Directive("Foo");
                var b = _.Directive<PlainClass>();
                Action rename = () => b.Name("Foo");
                rename.Should().Throw<DuplicateItemException>().WithMessage(
                    "Cannot rename directive PlainClass to \"Foo\": a directive named \"Foo\" already exists.");
            });
        }

        [Spec(nameof(clr_typed_item_subsequently_added_with_custom_name_sets_name))]
        [Fact]
        public void clr_typed_item_subsequently_added_with_custom_name_sets_name_()
        {
            var schema = Schema.Create(_ =>
            {
                _.Directive<PlainClass>();
                _.Directive<PlainClass>("Foo");
            });
            schema.GetDirective<PlainClass>().Name.Should().Be("Foo");
        }


        [Spec(nameof(named_item_subsequently_added_with_type_and_custom_name_sets_clr_type))]
        [Fact]
        public void named_item_subsequently_added_with_type_and_custom_name_sets_clr_type_()
        {
            var schema = Schema.Create(_ =>
            {
                _.Directive("Foo");
                _.Directive<PlainClass>("Foo");
            });
            schema.GetDirective("Foo").ClrType.Should().Be<PlainClass>();
        }

        [Spec(nameof(adding_clr_typed_item_updates_matching_named_items_clr_type))]
        [Fact]
        public void adding_clr_typed_item_updates_matching_named_items_clr_type_()
        {
            var schema = Schema.Create(_ =>
            {
                _.Directive(nameof(PlainClass)).Description("foo");
                _.Directive<PlainClass>();
            });
            schema.GetDirective<PlainClass>().Description.Should().Be("foo");
        }


        [Spec(nameof(adding_clr_typed_item_with_name_annotation_updates_matching_named_items_clr_type))]
        [Fact]
        public void adding_clr_typed_item_with_name_annotation_updates_matching_named_items_clr_type_()
        {
            var schema = Schema.Create(_ =>
            {
                _.Directive(PlainClassAnnotatedName.AnnotatedNameValue).Description("foo");
                _.Directive<PlainClassAnnotatedName>();
            });
            schema.GetDirective<PlainClassAnnotatedName>().Description.Should().Be("foo");
        }

        [Spec(nameof(clr_typed_item_cannot_be_added_with_custom_name_if_named_and_typed_items_already_exist))]
        [Fact]
        public void clr_typed_item_cannot_be_added_with_custom_name_if_named_and_typed_items_already_exist_()
        {
            Schema.Create(_ =>
            {
                _.Directive("Foo");
                _.Directive<PlainClass>();
                Action add = () => _.Directive<PlainClass>("Foo");
                add.Should().Throw<DuplicateItemException>().WithMessage(
                    "Cannot create directive Foo with CLR type 'PlainClass': both directive Foo and directive PlainClass already exist.");
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
                _.Directive("Foo");
                _.Directive<PlainClassAnnotatedName>();
                Action add = () => _.Directive<PlainClassAnnotatedName>("Foo");
                add.Should().Throw<DuplicateItemException>().WithMessage(
                    "Cannot create directive Foo with CLR type 'PlainClassAnnotatedName': both directive Foo and directive AnnotatedNameValue (CLR class: PlainClassAnnotatedName) already exist.");
            });
        }

        [Spec(nameof(adding_clr_typed_item_with_custom_name_does_not_update_item_matching_clr_type_name))]
        [Fact]
        public void adding_clr_typed_item_with_custom_name_does_not_update_item_matching_clr_type_name_()
        {
            var schema = Schema.Create(_ =>
            {
                _.Directive(nameof(PlainClass));
                _.Directive<PlainClass>("Foo");
            });
            schema.GetDirective(nameof(PlainClass)).ClrType.Should().BeNull();
            schema.GetDirective<PlainClass>().Name.Should().Be("Foo");
        }


        [Spec(nameof(adding_clr_typed_item_with_custom_name_does_not_update_item_matching_clr_type_name_annotation))]
        [Fact]
        public void adding_clr_typed_item_with_custom_name_does_not_update_item_matching_clr_type_name_annotation_()
        {
            var schema = Schema.Create(_ =>
            {
                _.Directive(PlainClassAnnotatedName.AnnotatedNameValue);
                _.Directive<PlainClassAnnotatedName>("Foo");
            });
            schema.GetDirective(PlainClassAnnotatedName.AnnotatedNameValue).ClrType.Should().BeNull();
            schema.GetDirective<PlainClassAnnotatedName>().Name.Should().Be("Foo");
        }
    }
}