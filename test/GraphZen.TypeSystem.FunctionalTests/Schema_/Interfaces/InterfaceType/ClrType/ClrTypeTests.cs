// Copyright (c) GraphZen LLC. All rights reserved.
// Licensed under the GraphZen Community License. See the LICENSE file in the project root for license information.

using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using FluentAssertions;
using GraphZen.Infrastructure;
using JetBrains.Annotations;
using Xunit;
using static GraphZen.TypeSystem.FunctionalTests.Specs.TypeSystemSpecs.ClrTypeSpecs;

namespace GraphZen.TypeSystem.FunctionalTests.Schema_.Interfaces.InterfaceType.ClrType
{
    [NoReorder]
    public class ClrTypeTests
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

        [Spec(nameof(clr_type_can_be_added))]
        [Fact]
        public void clr_type_can_be_added_()
        {
            var schema = Schema.Create(_ => { _.Interface("Foo").ClrType(typeof(PlainInterface)); });
            schema.GetInterface("Foo").ClrType.Should().Be<PlainInterface>();
        }

        [Spec(nameof(clr_type_can_be_added_via_type_param))]
        [Fact]
        public void clr_type_can_be_added_via_type_param_()
        {
            var schema = Schema.Create(_ => { _.Interface("Foo").ClrType<PlainInterface>(); });
            schema.GetInterface("Foo").ClrType.Should().Be<PlainInterface>();
        }


        [Spec(nameof(clr_type_can_be_changed))]
        [Fact]
        public void clr_type_can_be_changed_()
        {
            var schema = Schema.Create(_ =>
            {
                _.Interface<PlainInterface>()
                    .Description("original type: " + typeof(PlainInterface))
                    .ClrType(typeof(PlainInterfaceAnnotatedName));
            });
            schema.HasInterface<PlainInterface>().Should().BeFalse();
            schema.GetInterface<PlainInterfaceAnnotatedName>().Description.Should()
                .Be("original type: " + typeof(PlainInterface));
        }

        [Spec(nameof(clr_type_can_be_changed_via_type_param))]
        [Fact]
        public void clr_type_can_be_changed_via_type_param_()
        {
            var schema = Schema.Create(_ =>
            {
                _.Interface<PlainInterface>()
                    .Description("original type: " + typeof(PlainInterface))
                    .ClrType<PlainInterfaceAnnotatedName>();
            });
            schema.HasInterface<PlainInterface>().Should().BeFalse();
            schema.GetInterface<PlainInterfaceAnnotatedName>().Description.Should()
                .Be("original type: " + typeof(PlainInterface));
        }


        [Spec(nameof(clr_type_can_be_added_with_custom_name))]
        [Fact]
        public void clr_type_can_be_added_with_custom_name_()
        {
            var schema = Schema.Create(_ => { _.Interface("Foo").ClrType(typeof(PlainInterface), "Bar"); });
            schema.HasInterface("Foo").Should().BeFalse();
            schema.GetInterface("Bar").ClrType.Should().Be<PlainInterface>();
            schema.GetInterface<PlainInterface>().Name.Should().Be("Bar");
        }


        [Spec(nameof(clr_type_can_be_added_via_type_param_with_custom_name))]
        [Fact]
        public void clr_type_can_be_added_via_type_param_with_custom_name_()
        {
            var schema = Schema.Create(_ => { _.Interface("Foo").ClrType<PlainInterface>("Bar"); });
            schema.HasInterface("Foo").Should().BeFalse();
            schema.GetInterface("Bar").ClrType.Should().Be<PlainInterface>();
            schema.GetInterface<PlainInterface>().Name.Should().Be("Bar");
        }


        [Spec(nameof(clr_type_can_be_changed_with_custom_name))]
        [Fact]
        public void clr_type_can_be_changed_with_custom_name_()
        {
            var schema = Schema.Create(_ =>
            {
                _.Interface<PlainInterface>()
                    .Description("original type: " + typeof(PlainInterface))
                    .ClrType(typeof(PlainInterfaceAnnotatedName), "Foo");
            });
            schema.HasInterface<PlainInterface>().Should().BeFalse();
            var foo = schema.GetInterface("Foo");
            foo.Description.Should()
                .Be("original type: " + typeof(PlainInterface));
            foo.ClrType.Should().Be<PlainInterfaceAnnotatedName>();
        }


        [Spec(nameof(clr_type_can_be_changed_via_type_param_with_custom_name))]
        [Fact]
        public void clr_type_can_be_changed_via_type_param_with_custom_name_()
        {
            var schema = Schema.Create(_ =>
            {
                _.Interface<PlainInterface>()
                    .Description("original type: " + typeof(PlainInterface))
                    .ClrType<PlainInterfaceAnnotatedName>("Foo");
            });
            schema.HasInterface<PlainInterface>().Should().BeFalse();
            var foo = schema.GetInterface("Foo");
            foo.Description.Should()
                .Be("original type: " + typeof(PlainInterface));
            foo.ClrType.Should().Be<PlainInterfaceAnnotatedName>();
        }


        [Spec(nameof(clr_type_cannot_be_null))]
        [Fact]
        public void clr_type_cannot_be_null_()
        {
            Schema.Create(_ =>
            {
                var foo = _.Interface("Foo");
                new List<Action>
                {
                    () => foo.ClrType(null!),
                    () => foo.ClrType(null!, "bar")
                }.ForEach(a => a.Should().ThrowArgumentNullException("clrType"));
            });
        }


        [Spec(nameof(clr_type_should_be_unique))]
        [Fact]
        public void clr_type_should_be_unique_()
        {
            Schema.Create(_ =>
            {
                _.Interface<PlainInterface>();
                var foo = _.Interface("Foo");
                Action change = () => foo.ClrType<PlainInterface>();
                change.Should().Throw<DuplicateClrTypeException>().WithMessage(
                    "Cannot set CLR type on object Foo to CLR interface 'PlainInterface': object PlainInterface already exists with that CLR type.");
            });
        }

        [Spec(nameof(setting_clr_type_does_not_change_name))]
        [Fact]
        public void setting_clr_type_does_not_change_name_()
        {
            var schema = Schema.Create(_ => { _.Interface("Foo").ClrType<PlainInterface>(); });
            schema.GetInterface<PlainInterface>().Name.Should().Be("Foo");
        }


        [Spec(nameof(setting_clr_type_with_name_annotation_does_not_change_name))]
        [Fact]
        public void setting_clr_type_with_name_annotation_does_not_change_name_()
        {
            var schema = Schema.Create(_ => { _.Interface("Foo").ClrType<PlainInterfaceAnnotatedName>(); });
            schema.GetInterface<PlainInterfaceAnnotatedName>().Name.Should().Be("Foo");
        }


        [Spec(nameof(setting_clr_type_and_inferring_name_name_should_be_unique))]
        [Fact]
        public void setting_clr_type_and_inferring_name_name_should_be_unique()
        {
            Schema.Create(_ =>
            {
                _.Interface(nameof(PlainInterface));
                var foo = _.Interface("Foo");
                Action change = () => foo.ClrType<PlainInterface>(true);
                change.Should().Throw<DuplicateNameException>().WithMessage(
                    "Cannot set CLR type on object Foo and infer name: the CLR interface name 'PlainInterface' conflicts with an existing object named PlainInterface. All GraphQL type names must be unique.");
            });
        }


        [Spec(nameof(setting_clr_type_and_inferring_name_name_annotation_should_be_unique))]
        [Fact]
        public void setting_clr_type_and_inferring_name_name_annotation_should_be_unique()
        {
            Schema.Create(_ =>
            {
                _.Interface(nameof(PlainInterface));
                var foo = _.Interface("Foo");
                Action change = () => foo.ClrType<PlainInterface>(true);
                change.Should().Throw<DuplicateNameException>().WithMessage(
                    "Cannot set CLR type on object Foo and infer name: the CLR interface name 'PlainInterface' conflicts with an existing object named PlainInterface. All GraphQL type names must be unique.");
            });
        }


        [Spec(nameof(setting_clr_type_and_inferring_name_name_should_be_valid))]
        [Fact]
        public void setting_clr_type_and_inferring_name_name_should_be_valid_()
        {
            Schema.Create(_ =>
            {
                var foo = _.Interface("Foo");
                Action setClrType = () => foo.ClrType<InputValueBuilder<string>>(true);
                setClrType.Should().Throw<InvalidNameException>().WithMessage(
                    "Cannot set CLR type on object Foo and infer name: the CLR interface name 'InputValueBuilder`1' is not a valid GraphQL name.");
            });
        }


        [Spec(nameof(setting_clr_type_and_inferring_name_name_annotation_should_be_valid))]
        [Fact]
        public void clr_type_name_annotation_should_be_valid_()
        {
            Schema.Create(_ =>
            {
                var foo = _.Interface("Foo");
                new List<Action>
                {
                    () => foo.ClrType<PlainInterfaceInvalidNameAnnotation>(true),
                    () => foo.ClrType(typeof(PlainInterfaceInvalidNameAnnotation), true)
                }.ForEach(set =>
                {
                    set.Should().Throw<InvalidNameException>().WithMessage(
                        "Cannot set CLR type on object Foo and infer name: the annotated name \"(*&#\" on CLR interface 'PlainInterfaceInvalidNameAnnotation' is not a valid GraphQL name.");
                });
            });
        }


        [Spec(nameof(setting_clr_type_with_duplicate_custom_name_should_throw))]
        [Fact]
        public void custom_name_should_be_unique_()
        {
            Schema.Create(_ =>
            {
                _.Interface("Foo");
                var bar = _.Interface("Bar");
                new List<Action>
                {
                    () => bar.ClrType<PlainInterfaceAnnotatedName>("Foo"),
                    () => bar.ClrType(typeof(PlainInterfaceAnnotatedName), "Foo")
                }.ForEach(set =>
                {
                    set.Should().Throw<DuplicateNameException>().WithMessage(
                        "Cannot set CLR type on object Bar with custom name: the custom name \"Foo\" conflicts with an existing object named 'Foo'. All type names must be unique.");
                });
            });
        }


        [Spec(nameof(setting_clr_type_with_invalid_custom_name_should_throw))]
        [Fact]
        public void custom_name_should_be_valid_()
        {
            Schema.Create(_ =>
            {
                _.Interface("Foo");
                var bar = _.Interface("Bar");
                new List<Action>
                {
                    () => bar.ClrType<PlainInterfaceAnnotatedName>("invalid!"),
                    () => bar.ClrType(typeof(PlainInterfaceAnnotatedName), "invalid!")
                }.ForEach(set =>
                {
                    set.Should().Throw<InvalidNameException>().WithMessage(
                        "Cannot set CLR type on object Bar with custom name: the custom name \"invalid!\" is not a valid GraphQL name.");
                });
            });
        }


        [Spec(nameof(setting_clr_type_with_null_custom_name_should_throw))]
        [Fact]
        public void custom_name_cannot_be_null_()
        {
            Schema.Create(_ =>
            {
                var bar = _.Interface("Bar");
                new List<Action>
                {
                    () => bar.ClrType<PlainInterfaceAnnotatedName>(null!),
                    () => bar.ClrType(typeof(PlainInterfaceAnnotatedName), null!)
                }.ForEach(set => { set.Should().ThrowArgumentNullException("name"); });
            });
        }


        [Spec(nameof(setting_clr_type_and_inferring_name_changes_name))]
        [Fact]
        public void changing_clr_type_changes_name_()
        {
            var schema = Schema.Create(_ => { _.Interface("Foo").ClrType<PlainInterface>(true); });
            schema.HasInterface("Foo").Should().BeFalse();
            schema.GetInterface<PlainInterface>().Name.Should().Be(nameof(PlainInterface));
        }


        [Spec(nameof(setting_clr_type_with_name_annotation_and_inferring_name_changes_name))]
        [Fact]
        public void changing_clr_type_with_name_annotation_changes_name_()
        {
            var schema = Schema.Create(_ => { _.Interface("Foo").ClrType<PlainInterfaceAnnotatedName>(true); });
            schema.HasInterface("Foo").Should().BeFalse();
            schema.GetInterface<PlainInterfaceAnnotatedName>().Name.Should()
                .Be(PlainInterfaceAnnotatedName.AnnotatedNameValue);
        }


        [Spec(nameof(clr_type_can_be_removed))]
        [Fact]
        public void clr_type_can_be_removed_()
        {
            var schema = Schema.Create(_ => { _.Interface("Foo").ClrType<PlainInterface>().RemoveClrType(); });
            schema.GetInterface("Foo").ClrType.Should().BeNull();
        }


        [Spec(nameof(clr_typed_item_when_type_removed_should_retain_name))]
        [Fact]
        public void clr_typed_item_when_type_removed_should_retain_name_()
        {
            var schema = Schema.Create(_ => { _.Interface<PlainInterface>().RemoveClrType(); });
            schema.GetInterface(nameof(PlainInterface)).ClrType.Should().BeNull();
        }


        [Spec(nameof(clr_typed_item_with_name_annotation_when_clr_type_removed_should_retain_annotated_name))]
        [Fact]
        public void clr_typed_item_with_name_annotation_when_clr_type_removed_should_retain_annotated_name_()
        {
            var schema = Schema.Create(_ => { _.Interface<PlainInterfaceAnnotatedName>().RemoveClrType(); });
            schema.GetInterface(PlainInterfaceAnnotatedName.AnnotatedNameValue).ClrType.Should().BeNull();
        }


        [Spec(nameof(custom_named_clr_typed_item_when_type_removed_should_retain_custom_name))]
        [Fact]
        public void custom_named_clr_typed_item_when_type_removed_should_retain_custom_name_()
        {
            var schema = Schema.Create(_ => { _.Interface("Foo").ClrType<PlainInterface>("Bar").RemoveClrType(); });
            schema.GetInterface("Bar").ClrType.Should().BeNull();
        }
    }
}