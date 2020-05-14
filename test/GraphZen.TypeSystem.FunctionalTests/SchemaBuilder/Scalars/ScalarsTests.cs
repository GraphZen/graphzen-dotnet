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

namespace GraphZen.TypeSystem.FunctionalTests.SchemaBuilder.Scalars
{
    [NoReorder]
    public class ScalarsTests
    {
        private struct PlainStruct
        {
        }

        [GraphQLName(AnnotatedName)]
        private struct PlainStructAnnotatedName
        {
            public const string AnnotatedName = nameof(AnnotatedName);
        }

        [GraphQLName("#$%^")]
        private struct PlainStructInvalidNameAnnotation
        {
        }


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
                _.Scalar<PlainStruct>().Name("Bar");
            });
            schema.GetScalar<PlainStruct>().Name.Should().Be("Bar");
        }


        [Spec(nameof(InputAndOutputTypeCollectionSpecs
            .clr_typed_item_can_be_renamed_if_name_matches_output_type_identity))]
        [Fact(Skip = "needs impl")]
        public void clr_typed_item_can_be_renamed_if_name_matches_output_type_identity_()
        {
            var schema = Schema.Create(_ =>
            {
                _.Object("Foo").Field("outputField", "Bar");
                _.Scalar<PlainStruct>().Name("Bar");
            });
            schema.GetScalar<PlainStruct>().Name.Should().Be("Bar");
        }


        [Spec(nameof(InputAndOutputTypeCollectionSpecs
            .clr_typed_item_with_name_attribute_can_be_added_if_name_attribute_matches_with_input_type_identity))]
        [Fact]
        public void
            clr_typed_item_with_name_attribute_can_be_added_if_name_attribute_matches_with_input_type_identity_()
        {
            var schema = Schema.Create(_ =>
            {
                _.InputObject("Foo").Field("inputField", PlainStructAnnotatedName.AnnotatedName);
                _.Scalar<PlainStructAnnotatedName>().Name("Bar");
            });
            schema.HasScalar<PlainStructAnnotatedName>().Should().BeTrue();
        }


        [Spec(nameof(InputAndOutputTypeCollectionSpecs
            .clr_typed_item_with_name_attribute_can_be_added_if_name_attribute_matches_with_output_type_identity))]
        [Fact]
        public void
            clr_typed_item_with_name_attribute_can_be_added_if_name_attribute_matches_with_output_type_identity_()
        {
            var schema = Schema.Create(_ =>
            {
                _.Object("Foo").Field("outputField", PlainStructAnnotatedName.AnnotatedName);
                _.Scalar<PlainStructAnnotatedName>().Name("Bar");
            });
            schema.HasScalar<PlainStructAnnotatedName>().Should().BeTrue();
        }


        [Spec(nameof(named_item_can_be_added_via_sdl))]
        [Fact]
        public void named_item_can_be_added_via_sdl_()
        {
            var schema = Schema.Create(_ => { _.FromSchema(@"scalar Foo"); });
            schema.HasScalar("Foo").Should().BeTrue();
        }


        [Spec(nameof(named_item_can_be_added_via_sdl_extension))]
        [Fact(Skip = "needs impl")]
        public void named_item_can_be_added_via_sdl_extension_()
        {
            var schema = Schema.Create(_ => { _.FromSchema(@"extend scalar Foo"); });
            schema.HasScalar("Foo").Should().BeTrue();
        }


        [Spec(nameof(named_item_can_be_added))]
        [Fact]
        public void named_item_can_be_added_()
        {
            var schema = Schema.Create(_ => { _.Scalar("Foo"); });
            schema.HasScalar("Foo").Should().BeTrue();
        }


        [Spec(nameof(named_item_cannot_be_added_with_null_value))]
        [Fact]
        public void named_item_cannot_be_added_with_null_value_()
        {
            Schema.Create(_ =>
            {
                Action add = () => _.Scalar((string)null!);
                add.Should().ThrowArgumentNullException("name");
            });
        }


        [Spec(nameof(named_item_cannot_be_added_with_invalid_name))]
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


        [Spec(nameof(named_item_can_be_renamed))]
        [Fact]
        public void named_item_can_be_renamed_()
        {
            var schema = Schema.Create(_ => { _.Scalar("Foo").Name("Bar"); });
            schema.HasScalar("Foo").Should().BeFalse();
            schema.HasScalar("Bar").Should().BeTrue();
        }


     

        [Spec(nameof(named_item_can_be_removed))]
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


        [Spec(nameof(named_item_cannot_be_removed_with_null_value))]
        [Fact]
        public void named_item_cannot_be_removed_with_null_value_()
        {
            Schema.Create(_ =>
            {
                Action remove = () => _.RemoveScalar((string)null!);
                remove.Should().ThrowArgumentNullException("name");
            });
        }


        [Spec(nameof(clr_typed_item_can_be_added))]
        [Fact]
        public void clr_typed_item_can_be_added_()
        {
            var schema = Schema.Create(_ => { _.Scalar(typeof(PlainStruct)); });
            schema.HasScalar<PlainStruct>().Should().BeTrue();
        }


        [Spec(nameof(clr_typed_item_with_conflicting_name_can_be_added_with_custom_name))]
        [Fact(Skip = "needs impl")]
        public void clr_typed_item_can_be_added_with_custom_name_()
        {
            var schema = Schema.Create(_ => { _.Scalar(typeof(PlainStruct), "Foo"); });
            schema.GetScalar<PlainStruct>().Name.Should().Be("Foo");
        }


        [Spec(nameof(clr_typed_item_with_conflicting_name_can_be_added_via_type_param_with_custom_name))]
        [Fact(Skip = "needs impl")]
        public void clr_typed_item_can_be_added_via_type_param_with_custom_name_()
        {
            var schema = Schema.Create(_ => { _.Scalar<PlainStruct>("Foo"); });
            schema.HasScalar<PlainStruct>();
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
                    "Cannot get or create GraphQL scalar type builder with CLR type 'PlainStructInvalidNameAnnotation'. The name \"#$%^\" specified in the GraphQLNameAttribute on the PlainStructInvalidNameAnnotation CLR type is not a valid GraphQL name. Names are limited to underscores and alpha-numeric ASCII characters.");
            });
        }


        [Spec(nameof(clr_typed_item_can_be_removed))]
        [Fact(Skip = "needs impl")]
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
        [Fact(Skip = "needs impl")]
        public void clr_typed_item_can_be_removed_via_type_param_()
        {
            var schema = Schema.Create(_ =>
            {
                _.Scalar<PlainStruct>();
                _.RemoveScalar(typeof(PlainStruct));
            });
            schema.HasScalar<PlainStruct>().Should().BeFalse();
        }


        [Spec(nameof(clr_typed_item_cannot_be_removed_with_null_value))]
        [Fact]
        public void clr_typed_item_cannot_be_removed_with_null_value_()
        {
            Schema.Create(_ =>
            {
                _.Scalar<PlainStruct>();
                Action remove = () => _.RemoveScalar((Type)null!);
                remove.Should().ThrowArgumentNullException("clrType");
            });
        }


        [Spec(nameof(clr_typed_item_with_conflicting_name_annotation_can_be_added_with_custom_name))]
        [Fact]
        public void clr_typed_item_with_conflicting_name_annotation_can_be_added_with_custom_name_()
        {
            var schema = Schema.Create(_ =>
            {
                _.Scalar(PlainStructAnnotatedName.AnnotatedName);
                _.Scalar(typeof(PlainStructAnnotatedName), "Foo");
            });
            schema.GetScalar<PlainStructAnnotatedName>().Name.Should().Be("Foo");
        }


        [Spec(nameof(clr_typed_item_with_conflicting_name_annotation_can_be_added_via_type_param_with_custom_name))]
        [Fact]
        public void clr_typed_item_with_conflicting_name_annotation_can_be_added_via_type_param_with_custom_name_()
        {
            var schema = Schema.Create(_ =>
            {
                _.Scalar(PlainStructAnnotatedName.AnnotatedName);
                _.Scalar<PlainStructAnnotatedName>("Foo");
            });
            schema.GetScalar<PlainStructAnnotatedName>().Name.Should().Be("Foo");
        }


        [Spec(nameof(clr_typed_item_with_invalid_name_annotation_can_be_added_with_custom_name))]
        [Fact]
        public void clr_typed_item_with_invalid_name_annotation_can_be_added_with_custom_name_()
        {
            var schema = Schema.Create(_ => { _.Scalar(typeof(PlainStructInvalidNameAnnotation), "Foo"); });
            schema.HasScalar<PlainStructInvalidNameAnnotation>();
        }


        [Spec(nameof(clr_typed_item_with_invalid_name_annotation_can_be_added_via_type_param_with_custom_name))]
        [Fact]
        public void clr_typed_item_with_invalid_name_annotation_can_be_added_via_type_param_with_custom_name_()
        {
            var schema = Schema.Create(_ => { _.Scalar<PlainStructInvalidNameAnnotation>("Foo"); });
            schema.HasScalar<PlainStructInvalidNameAnnotation>();
        }

        [Spec(nameof(clr_typed_item_can_be_renamed))]
        [Fact]
        public void clr_typed_item_can_be_renamed_()
        {
            var schema = Schema.Create(_ => { _.Scalar<PlainStruct>().Name("Foo"); }
            );
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
        [InlineData("")]
        [InlineData("()#&$")]
        public void clr_typed_item_cannot_be_renamed_with_an_invalid_name_(string name)
        {
            Schema.Create(_ =>
            {
                var pocs = _.Scalar<PlainStruct>();
                Action rename = () => pocs.Name(name);
                rename.Should().Throw<InvalidNameException>().WithMessage(
                    $"Cannot rename scalar PlainStruct. \"{name}\" is not a valid GraphQL name. Names are limited to underscores and alpha-numeric ASCII characters.");
            });
        }


        [Spec(nameof(clr_typed_item_cannot_be_renamed_if_name_already_exists))]
        [Fact]
        public void clr_typed_item_cannot_be_renamed_if_name_already_exists_()
        {
            Schema.Create(_ =>
            {
                _.Scalar("Foo");
                var pocs = _.Scalar<PlainStruct>();
                Action rename = () => pocs.Name("Foo");
                rename.Should().Throw<DuplicateNameException>().WithMessage(
                    "Cannot rename scalar PlainStruct to \"Foo\", scalar Foo already exists. All GraphQL type names must be unique.");
            });
        }
    }
}