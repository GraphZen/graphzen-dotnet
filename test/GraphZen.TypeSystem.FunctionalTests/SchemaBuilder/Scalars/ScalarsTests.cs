// Copyright (c) GraphZen LLC. All rights reserved.
// Licensed under the GraphZen Community License. See the LICENSE file in the project root for license information.


using System;
using System.Diagnostics.CodeAnalysis;
using FluentAssertions;
using GraphZen.Infrastructure;
using JetBrains.Annotations;
using Xunit;
using static GraphZen.TypeSystem.FunctionalTests.Specs.TypeSystemSpecs;

namespace GraphZen.TypeSystem.FunctionalTests.SchemaBuilder.Scalars
{
    [NoReorder]
    public class ScalarsTests
    {
        private struct Pocs
        {
        }

        [GraphQLName(AnnotatedName)]
        private struct PocsNameAnnotated
        {
            public const string AnnotatedName = nameof(AnnotatedName);
        }

        [GraphQLName("#$%^")]
        private struct PocsInvalidNameAnnotation {}


        [Spec(nameof(InputAndOutputTypeCollectionSpecs.named_item_can_be_added_if_name_matches_input_type_identity))]
        [Fact]
        public void named_item_can_be_added_if_name_matches_input_type_identity_()
        {
            var schema = Schema.Create(_ =>
            {
                _.InputObject("Foo").Field("inputField", "Bar");
                _.Scalar("Bar");
            });
            schema.HasScalar("Bar");
        }


        [Spec(nameof(InputAndOutputTypeCollectionSpecs.named_item_can_be_added_if_name_matches_output_type_identity))]
        [Fact]
        public void named_item_can_be_added_if_name_matches_output_type_identity_()
        {
            var schema = Schema.Create(_ =>
            {
                _.Object("Foo").Field("outputField", "Bar");
                _.Scalar("Bar");
            });
            schema.HasScalar("Bar");
        }


        [Spec(nameof(InputAndOutputTypeCollectionSpecs.named_item_can_be_renamed_to_name_with_input_type_identity))]
        [Fact(Skip = "needs impl")]
        public void named_item_can_be_renamed_to_name_with_input_type_identity_()
        {
            var schema = Schema.Create(_ =>
            {
                _.InputObject("Foo").Field("inputField", "Bar");
                _.Scalar("Baz").Name("Bar");
            });
            schema.HasScalar("Bar");
        }


        [Spec(nameof(InputAndOutputTypeCollectionSpecs.named_item_can_be_renamed_to_name_with_output_type_identity))]
        [Fact(Skip = "needs impl")]
        public void named_item_can_be_renamed_to_name_with_output_type_identity_()
        {
            var schema = Schema.Create(_ =>
            {
                _.Object("Foo").Field("outputField", "Bar");
                _.Scalar("Baz").Name("Bar");
            });
            schema.HasScalar("Bar");
        }


        [Spec(nameof(InputAndOutputTypeCollectionSpecs
            .clr_typed_item_can_be_renamed_if_name_matches_input_type_identity))]
        [Fact(Skip = "needs impl")]
        public void clr_typed_item_can_be_renamed_if_name_matches_input_type_identity_()
        {
            var schema = Schema.Create(_ =>
            {
                _.InputObject("Foo").Field("inputField", "Bar");
                _.Scalar<Pocs>().Name("Bar");
            });
            schema.GetScalar<Pocs>().Name.Should().Be("Bar");
        }


        [Spec(nameof(InputAndOutputTypeCollectionSpecs
            .clr_typed_item_can_be_renamed_if_name_matches_output_type_identity))]
        [Fact(Skip = "needs impl")]
        public void clr_typed_item_can_be_renamed_if_name_matches_output_type_identity_()
        {
            var schema = Schema.Create(_ =>
            {
                _.Object("Foo").Field("outputField", "Bar");
                _.Scalar<Pocs>().Name("Bar");
            });
            schema.GetScalar<Pocs>().Name.Should().Be("Bar");
        }


        [Spec(nameof(InputAndOutputTypeCollectionSpecs
            .clr_typed_item_with_name_attribute_can_be_added_if_name_attribute_matches_with_input_type_identity))]
        [Fact]
        public void
            clr_typed_item_with_name_attribute_can_be_added_if_name_attribute_matches_with_input_type_identity_()
        {
            var schema = Schema.Create(_ =>
            {
                _.InputObject("Foo").Field("inputField", PocsNameAnnotated.AnnotatedName);
                _.Scalar<PocsNameAnnotated>().Name("Bar");
            });
            schema.HasScalar<PocsNameAnnotated>().Should().BeTrue();
        }


        [Spec(nameof(InputAndOutputTypeCollectionSpecs
            .clr_typed_item_with_name_attribute_can_be_added_if_name_attribute_matches_with_output_type_identity))]
        [Fact]
        public void
            clr_typed_item_with_name_attribute_can_be_added_if_name_attribute_matches_with_output_type_identity_()
        {
            var schema = Schema.Create(_ =>
            {
                _.Object("Foo").Field("outputField", PocsNameAnnotated.AnnotatedName);
                _.Scalar<PocsNameAnnotated>().Name("Bar");
            });
            schema.HasScalar<PocsNameAnnotated>().Should().BeTrue();
        }


        [Spec(nameof(NamedCollectionSpecs.named_item_can_be_added_via_sdl))]
        [Fact]
        public void named_item_can_be_added_via_sdl_()
        {
            var schema = Schema.Create(_ => { _.FromSchema(@"scalar Foo"); });
            schema.HasScalar("Foo").Should().BeTrue();
        }


        [Spec(nameof(NamedCollectionSpecs.named_item_can_be_added_via_sdl_extension))]
        [Fact(Skip = "needs impl")]
        public void named_item_can_be_added_via_sdl_extension_()
        {
            var schema = Schema.Create(_ => { _.FromSchema(@"extend scalar Foo"); });
            schema.HasScalar("Foo").Should().BeTrue();
        }


        [Spec(nameof(NamedCollectionSpecs.named_item_can_be_added))]
        [Fact]
        public void named_item_can_be_added_()
        {
            var schema = Schema.Create(_ => { _.Scalar("Foo"); });
            schema.HasScalar("Foo").Should().BeTrue();
        }


        [Spec(nameof(NamedCollectionSpecs.named_item_cannot_be_added_with_null_value))]
        [Fact]
        public void named_item_cannot_be_added_with_null_value_()
        {
            Schema.Create(_ =>
            {
                Action add = () => _.Scalar((string)null!);
                add.Should().ThrowArgumentNullException("name");
            });
        }


        [Spec(nameof(NamedCollectionSpecs.named_item_cannot_be_added_with_invalid_name))]
        [Theory]
        [InlineData("xx 09880")]
        public void named_item_cannot_be_added_with_invalid_name_(string name)
        {
            Schema.Create(_ =>
            {
                Action add = () => _.Scalar(name);
                add.Should().Throw<InvalidNameException>().WithMessage(
                    $"Cannot get or create GraphQL type builder for scalar named \"{name}\". The type name \"{name}\" is not a valid GraphQL name. Names are limited to underscores and alpha-numeric ASCII characters.");
            });
        }


        [Spec(nameof(NamedCollectionSpecs.named_item_can_be_renamed))]
        [Fact]
        public void named_item_can_be_renamed_()
        {
            var schema = Schema.Create(_ => { _.Scalar("Foo").Name("Bar"); });
            schema.HasScalar("Foo").Should().BeFalse();
            schema.HasScalar("Bar").Should().BeTrue();
        }


        [Spec(nameof(NamedCollectionSpecs.named_item_cannot_be_renamed_with_null_value))]
        [Fact]
        public void named_item_cannot_be_renamed_with_null_value_()
        {
            var schema = Schema.Create(_ =>
            {
                var foo = _.Scalar("Foo");
                Action rename = () => foo.Name(null!);
                rename.Should().ThrowArgumentNullException("name");
            });
        }


        [Spec(nameof(NamedCollectionSpecs.named_item_cannot_be_renamed_with_an_invalid_name))]
        [Theory]
        [InlineData("")]
        public void named_item_cannot_be_renamed_with_an_invalid_name_(string name)
        {
            Schema.Create(_ =>
            {
                var foo = _.Scalar("Foo");
                Action rename = () => foo.Name(name);
                rename.Should().Throw<InvalidNameException>().WithMessage(
                    $"Cannot rename scalar Foo. \"{name}\" is not a valid GraphQL name. Names are limited to underscores and alpha-numeric ASCII characters.");
            });
        }


        [Spec(nameof(NamedCollectionSpecs.named_item_cannot_be_renamed_if_name_already_exists))]
        [Fact]
        public void named_item_cannot_be_renamed_if_name_already_exists_()
        {
            Schema.Create(_ =>
            {
                _.Scalar("Foo");
                var bar = _.Scalar("Bar");
                Action rename = () => bar.Name("Foo");
                rename.Should().Throw<DuplicateNameException>().WithMessage(
                    "Cannot rename scalar Bar to \"Foo\", scalar Foo already exists. All GraphQL type names must be unique.");
            });
        }


        [Spec(nameof(NamedCollectionSpecs.named_item_can_be_removed))]
        [Fact(Skip = "needs impl")]
        public void named_item_can_be_removed_()
        {
            var schema = Schema.Create(_ =>
            {
                _.Scalar("Foo");
                _.RemoveScalar("Foo");
            });
            schema.HasScalar("Foo").Should().BeFalse();
        }


        [Spec(nameof(NamedCollectionSpecs.named_item_cannot_be_removed_with_null_value))]
        [Fact]
        public void named_item_cannot_be_removed_with_null_value_()
        {
            Schema.Create(_ =>
            {
                Action remove = () => _.RemoveScalar((string)null!);
                remove.Should().ThrowArgumentNullException("name");
            });
        }


        [Spec(nameof(NamedCollectionSpecs.named_item_cannot_be_removed_with_invalid_name))]
        [Theory(Skip = "needs impl")]
        [InlineData("")]
        [InlineData("xas )(*")]
        public void named_item_cannot_be_removed_with_invalid_name_(string name)
        {
            Schema.Create(_ =>
            {
                Action remove = () => _.RemoveScalar(name);
                remove.Should().Throw<InvalidNameException>().WithMessage("x");
            });
        }


        [Spec(nameof(ClrTypedCollectionSpecs.clr_typed_item_can_be_added))]
        [Fact]
        public void clr_typed_item_can_be_added_()
        {
            var schema = Schema.Create(_ => { _.Scalar(typeof(Pocs)); });
            schema.HasScalar<Pocs>().Should().BeTrue();
        }


        [Spec(nameof(ClrTypedCollectionSpecs.clr_typed_item_with_conflicting_name_can_be_added_with_custom_name))]
        [Fact(Skip = "needs impl")]
        public void clr_typed_item_can_be_added_with_custom_name_()
        {
            var schema = Schema.Create(_ => { _.Scalar(typeof(Pocs), "Foo"); });
            schema.GetScalar<Pocs>().Name.Should().Be("Foo");
        }


        [Spec(nameof(ClrTypedCollectionSpecs.DEPRECATED_clr_typed_item_with_conflicting_name_can_be_added_via_type_param))]
        [Fact]
        public void clr_typed_item_can_be_added_via_type_param_()
        {
            var schema = Schema.Create(_ =>
            {
                _.Scalar<Pocs>();
            });
            schema.HasScalar<Pocs>();
        }


        [Spec(nameof(ClrTypedCollectionSpecs.clr_typed_item_with_conflicting_name_can_be_added_via_type_param_with_custom_name))]
        [Fact(Skip = "needs impl")]
        public void clr_typed_item_can_be_added_via_type_param_with_custom_name_()
        {
            var schema = Schema.Create(_ =>
                        {
                            _.Scalar<Pocs>("Foo");
                        });
            schema.HasScalar<Pocs>();
        }


        [Spec(nameof(ClrTypedCollectionSpecs.clr_typed_item_cannot_be_added_with_null_value))]
        [Fact]
        public void clr_typed_item_cannot_be_added_with_null_value_()
        {
            Schema.Create(_ =>
            {
                Action add = () => _.Scalar((Type)null!);
                add.Should().ThrowArgumentNullException("clrType");
            });
        }


        [Spec(nameof(ClrTypedCollectionSpecs.clr_typed_item_cannot_be_added_with_invalid_name_attribute))]
        [Fact()]
        public void clr_typed_item_cannot_be_added_with_invalid_name_attribute_()
        {
            var schema = Schema.Create(_ =>
            {
                _.Scalar<PocsInvalidNameAnnotation>();
            });
        }


        [Spec(nameof(ClrTypedCollectionSpecs.clr_typed_item_can_be_removed))]
        [Fact(Skip = "TODO")]
        public void clr_typed_item_can_be_removed_()
        {
            var schema = Schema.Create(_ => { });
        }


        [Spec(nameof(ClrTypedCollectionSpecs.clr_typed_item_can_be_removed_via_type_param))]
        [Fact(Skip = "TODO")]
        public void clr_typed_item_can_be_removed_via_type_param_()
        {
            var schema = Schema.Create(_ => { });
        }


        [Spec(nameof(ClrTypedCollectionSpecs.clr_typed_item_cannot_be_removed_with_null_value))]
        [Fact(Skip = "TODO")]
        public void clr_typed_item_cannot_be_removed_with_null_value_()
        {
            var schema = Schema.Create(_ => { });
        }


        [Spec(nameof(ClrTypedCollectionSpecs.clr_typed_item_can_have_clr_type_changed))]
        [Fact(Skip = "TODO")]
        public void clr_typed_item_can_have_clr_type_changed_()
        {
            var schema = Schema.Create(_ => { });
        }


        [Spec(nameof(ClrTypedCollectionSpecs.clr_typed_item_cannot_have_clr_type_changed_with_null_value))]
        [Fact(Skip = "TODO")]
        public void clr_typed_item_cannot_have_clr_type_changed_with_null_value_()
        {
            var schema = Schema.Create(_ => { });
        }


        [Spec(nameof(ClrTypedCollectionSpecs.clr_typed_item_can_have_clr_type_removed))]
        [Fact(Skip = "TODO")]
        public void clr_typed_item_can_have_clr_type_removed_()
        {
            var schema = Schema.Create(_ => { });
        }


        [Spec(nameof(ClrTypedCollectionSpecs.clr_typed_item_with_type_removed_should_retain_clr_type_name))]
        [Fact(Skip = "TODO")]
        public void clr_typed_item_with_type_removed_should_retain_clr_type_name_()
        {
            var schema = Schema.Create(_ => { });
        }


        [Spec(nameof(ClrTypedCollectionSpecs
            .clr_typed_item_with_name_annotation_type_removed_should_retain_annotated_name))]
        [Fact(Skip = "TODO")]
        public void clr_typed_item_with_name_annotation_type_removed_should_retain_annotated_name_()
        {
            var schema = Schema.Create(_ => { });
        }


        [Spec(nameof(ClrTypedCollectionSpecs.custom_named_clr_typed_item_with_type_removed_should_retain_custom_name))]
        [Fact(Skip = "TODO")]
        public void custom_named_clr_typed_item_with_type_removed_should_retain_custom_name_()
        {
            var schema = Schema.Create(_ => { });
        }


        [Spec(nameof(ClrTypedCollectionSpecs.clr_typed_item_can_be_renamed))]
        [Fact(Skip = "TODO")]
        public void clr_typed_item_can_be_renamed_()
        {
            var schema = Schema.Create(_ => { });
        }


        [Spec(nameof(ClrTypedCollectionSpecs.clr_typed_item_with_name_attribute_can_be_renamed))]
        [Fact(Skip = "TODO")]
        public void clr_typed_item_with_name_attribute_can_be_renamed_()
        {
            var schema = Schema.Create(_ => { });
        }


        [Spec(nameof(ClrTypedCollectionSpecs.clr_typed_item_cannot_be_renamed_with_an_invalid_name))]
        [Fact(Skip = "TODO")]
        public void clr_typed_item_cannot_be_renamed_with_an_invalid_name_()
        {
            var schema = Schema.Create(_ => { });
        }


        [Spec(nameof(ClrTypedCollectionSpecs.clr_typed_item_cannot_be_renamed_if_name_already_exists))]
        [Fact(Skip = "TODO")]
        public void clr_typed_item_cannot_be_renamed_if_name_already_exists_()
        {
            var schema = Schema.Create(_ => { });
        }


        [Spec(nameof(ClrTypedCollectionSpecs.untyped_item_can_have_clr_type_added))]
        [Fact(Skip = "TODO")]
        public void untyped_item_can_have_clr_type_added_()
        {
            var schema = Schema.Create(_ => { });
        }


        [Spec(nameof(ClrTypedCollectionSpecs.untyped_item_cannot_have_clr_type_added_that_is_already_in_use))]
        [Fact(Skip = "TODO")]
        public void untyped_item_cannot_have_clr_type_added_that_is_already_in_use_()
        {
            var schema = Schema.Create(_ => { });
        }


        [Spec(nameof(ClrTypedCollectionSpecs.adding_clr_type_to_item_changes_name))]
        [Fact(Skip = "TODO")]
        public void adding_clr_type_to_item_changes_name_()
        {
            var schema = Schema.Create(_ => { });
        }


        [Spec(nameof(ClrTypedCollectionSpecs.clr_type_with_conflicting_name_can_be_added_with_custom_name))]
        [Fact(Skip = "TODO")]
        public void adding_clr_type_to_item_with_name_changes_name_from_param_()
        {
            var schema = Schema.Create(_ => { });
        }




        [Spec(nameof(ClrTypedCollectionSpecs.cannot_add_clr_type_to_item_with_custom_name_if_name_conflicts))]
        [Fact(Skip = "TODO")]
        public void cannot_add_clr_type_to_item_with_custom_name_if_name_conflicts_()
        {
            var schema = Schema.Create(_ => { });
        }
    }
}