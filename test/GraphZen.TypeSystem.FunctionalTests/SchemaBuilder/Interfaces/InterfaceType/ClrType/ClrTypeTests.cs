// Copyright (c) GraphZen LLC. All rights reserved.
// Licensed under the GraphZen Community License. See the LICENSE file in the project root for license information.

using System;
using System.Diagnostics.CodeAnalysis;
using FluentAssertions;
using GraphZen.Infrastructure;
using JetBrains.Annotations;
using Xunit;
using static GraphZen.TypeSystem.FunctionalTests.Specs.TypeSystemSpecs.ClrTypeSpecs;

namespace GraphZen.TypeSystem.FunctionalTests.SchemaBuilder.Interfaces.InterfaceType.ClrType
{
    [NoReorder]
    public class ClrTypeTests
    {
        // ReSharper disable once InconsistentNaming
        private interface PlainInterface
        {
        }

        [GraphQLName(AnnotatedName)]
        // ReSharper disable once InconsistentNaming
        private interface PlainInterfaceAnnotatedName
        {
            public const string AnnotatedName = nameof(AnnotatedName);
        }

        [GraphQLName("abc &*(")]
        // ReSharper disable once InconsistentNaming
        // ReSharper disable once UnusedType.Local
        private interface PlainInterfaceInvalidNameAnnotation
        {
        }


        [Spec(nameof(clr_typed_item_can_have_clr_type_changed))]
        [Fact]
        public void clr_typed_item_can_have_clr_type_changed_()
        {
            var schema = Schema.Create(_ =>
            {
                _.Interface<PlainInterface>().ClrType(typeof(PlainInterfaceAnnotatedName));
            });
            schema.HasInterface<PlainInterface>().Should().BeFalse();
            schema.HasInterface<PlainInterfaceAnnotatedName>().Should().BeTrue();
        }


        [Spec(nameof(clr_typed_item_cannot_have_clr_type_changed_with_null_value))]
        [Fact]
        public void clr_typed_item_cannot_have_clr_type_changed_with_null_value_()
        {
            Schema.Create(_ =>
            {
                var poci = _.Interface<PlainInterface>();
                Action remove = () => poci.ClrType(null!);
                remove.Should().ThrowArgumentNullException("clrType");
            });
        }


        [Spec(nameof(untyped_item_can_have_clr_type_added))]
        [Fact]
        public void untyped_item_can_have_clr_type_added_()
        {
            var schema = Schema.Create(_ => { _.Interface("Foo").ClrType<PlainInterface>(); });
            schema.GetInterface("Foo").ClrType.Should().Be<PlainInterface>();
        }


        [Spec(nameof(untyped_item_cannot_have_clr_type_added_that_is_already_in_use))]
        [Fact(Skip = "needs impl")]
        public void untyped_item_cannot_have_clr_type_added_that_is_already_in_use_()
        {
            Schema.Create(_ =>
            {
                _.Interface<PlainInterface>();
                var foo = _.Interface("Foo");
                Action add = () => foo.ClrType<PlainInterface>();
                add.Should().Throw<DuplicateClrTypeException>().WithMessage("x");
            });
        }


        [Spec(nameof(clr_typed_item_can_have_clr_type_removed))]
        [Fact(Skip = "needs design/impl")]
        public void clr_typed_item_can_have_clr_type_removed_()
        {
            var schema = Schema.Create(_ => { _.Interface<PlainInterface>().RemoveClrType(); });
            schema.GetInterface(nameof(PlainInterface)).ClrType.Should().BeNull();
        }


        [Spec(nameof(clr_typed_item_with_type_removed_should_retain_clr_type_name))]
        [Fact(Skip = "needs design/impl")]
        public void clr_typed_item_with_type_removed_should_retain_clr_type_name_()
        {
            var schema = Schema.Create(_ => { _.Interface<PlainInterface>().RemoveClrType(); });
            schema.HasInterface(nameof(PlainInterface)).Should().BeTrue();
        }


        [Spec(nameof(clr_typed_item_with_name_annotation_type_removed_should_retain_annotated_name))]
        [Fact(Skip = "needs design/impl")]
        public void clr_typed_item_with_name_annotation_type_removed_should_retain_annotated_name_()
        {
            var schema = Schema.Create(_ => { _.Interface<PlainInterfaceAnnotatedName>().RemoveClrType(); });
            schema.HasInterface(PlainInterfaceAnnotatedName.AnnotatedName).Should().BeTrue();
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
        [InlineData("  xy")]
        [InlineData("")]
        public void clr_typed_item_cannot_be_renamed_with_an_invalid_name_(string name)
        {
            Schema.Create(_ =>
            {
                var poci = _.Interface<PlainInterface>();
                Action rename = () => poci.Name(name);
                rename.Should().Throw<InvalidNameException>()
                    .WithMessage(
                        $"Cannot rename interface PlainInterface. \"{name}\" is not a valid GraphQL name. Names are limited to underscores and alpha-numeric ASCII characters.");
            });
        }


        [Spec(nameof(clr_typed_item_cannot_be_renamed_if_name_already_exists))]
        [Fact]
        public void clr_typed_item_cannot_be_renamed_if_name_already_exists_()
        {
            Schema.Create(_ =>
            {
                _.Interface("Foo");
                var poci = _.Interface<PlainInterface>();
                Action rename = () => poci.Name("Foo");
                rename.Should().Throw<DuplicateNameException>().WithMessage(
                    @"Cannot rename interface PlainInterface to ""Foo"", interface Foo already exists. All GraphQL type names must be unique.");
            });
        }
    }
}