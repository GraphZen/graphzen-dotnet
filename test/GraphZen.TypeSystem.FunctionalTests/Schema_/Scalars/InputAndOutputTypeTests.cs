// Copyright (c) GraphZen LLC. All rights reserved.
// Licensed under the GraphZen Community License. See the LICENSE file in the project root for license information.

using System.Diagnostics.CodeAnalysis;
using FluentAssertions;
using GraphZen.Infrastructure;
using JetBrains.Annotations;
using Xunit;
using static GraphZen.TypeSystem.FunctionalTests.Specs.TypeSystemSpecs.InputAndOutputTypeSpecs;

namespace GraphZen.TypeSystem.FunctionalTests.Schema_.Scalars
{
    [NoReorder]
    public class InputAndOutputTypeTests
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
        // ReSharper disable once UnusedType.Local
        private struct PlainStructInvalidNameAnnotation
        {
        }


        [Spec(nameof(type_can_be_created_if_name_matches_input_type_id))]
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


        [Spec(nameof(type_can_be_created_if_name_matches_output_type_id))]
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


        [Spec(nameof(type_can_be_renamed_if_name_matches_input_type_id))]
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


        [Spec(nameof(type_can_be_renamed_if_name_matches_output_type_id))]
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


        [Spec(nameof(DEPRECATED_clr_typed_item_can_be_renamed_if_name_matches_input_type_identity))]
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


        [Spec(nameof(DEPRECATED_clr_typed_item_can_be_renamed_if_name_matches_output_type_identity))]
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


        [Spec(nameof(DEPRECATED_clr_typed_item_with_name_attribute_can_be_added_if_name_attribute_matches_with_input_type_identity
        ))]
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


        [Spec(nameof(DEPRECATED_clr_typed_item_with_name_attribute_can_be_added_if_name_attribute_matches_with_output_type_identity
        ))]
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
    }
}