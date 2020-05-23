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

        [GraphQLName(AnnotatedNameValue)]
        private struct PlainStructAnnotatedName
        {
            public const string AnnotatedNameValue = nameof(AnnotatedNameValue);
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
            schema.HasScalar("Bar").Should().BeTrue();
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
            schema.HasScalar("Bar").Should().BeTrue();
        }


        [Spec(nameof(type_can_be_renamed_if_name_matches_input_type_id))]
        [Fact]
        public void named_item_can_be_renamed_to_name_with_input_type_identity_()
        {
            var schema = Schema.Create(_ =>
            {
                _.InputObject("Foo").Field("inputField", "Bar");
                _.Scalar("Baz").Name("Bar");
            });
            schema.HasScalar("Bar").Should().BeTrue();
        }


        [Spec(nameof(type_can_be_renamed_if_name_matches_output_type_id))]
        [Fact]
        public void named_item_can_be_renamed_to_name_with_output_type_identity_()
        {
            var schema = Schema.Create(_ =>
            {
                _.Object("Foo").Field("outputField", "Bar");
                _.Scalar("Baz").Name("Bar");
            });
            schema.HasScalar("Bar").Should().BeTrue();
        }


        [Spec(nameof(type_can_be_created_via_clr_type_if_name_matches_input_id))]
        [Fact]
        public void type_can_be_created_via_clr_type_if_name_matches_input_id_()
        {
            var schema = Schema.Create(_ =>
            {
                _.InputObject("Foo").Field("field", nameof(PlainStruct));
                _.Scalar<PlainStruct>();
            });
            var type = schema.GetScalar<PlainStruct>();
            schema.GetInputObject("Foo").GetField("field").FieldType.GetNamedType().Should().Be(type);
        }


        [Spec(nameof(type_can_be_created_via_clr_type_if_name_matches_output_id))]
        [Fact]
        public void type_can_be_created_via_clr_type_if_name_matches_output_id_()
        {
            var schema = Schema.Create(_ =>
            {
                _.Object("Foo").Field("field", nameof(PlainStruct));
                _.Scalar<PlainStruct>();
            });
            var type = schema.GetScalar<PlainStruct>();
            schema.GetObject("Foo").GetField("field").FieldType.GetNamedType().Should().Be(type);
        }


        [Spec(nameof(type_can_be_created_via_clr_type_if_name_annotation_matches_input_id))]
        [Fact]
        public void type_can_be_created_via_clr_type_if_name_annotation_matches_input_id_()
        {
            var schema = Schema.Create(_ =>
            {
                _.InputObject("Foo").Field("field", PlainStructAnnotatedName.AnnotatedNameValue);
                _.Scalar<PlainStructAnnotatedName>();
            });
            var type = schema.GetScalar<PlainStructAnnotatedName>();
            schema.GetInputObject("Foo").GetField("field").FieldType.GetNamedType().Should().Be(type);
        }


        [Spec(nameof(type_can_be_created_via_clr_type_if_name_annotation_matches_output_id))]
        [Fact]
        public void type_can_be_created_via_clr_type_if_name_annotation_matches_output_id_()
        {
            var schema = Schema.Create(_ =>
            {
                _.Object("Foo").Field("field", PlainStructAnnotatedName.AnnotatedNameValue);
                _.Scalar<PlainStructAnnotatedName>();
            });
            var type = schema.GetScalar<PlainStructAnnotatedName>();
            schema.GetObject("Foo").GetField("field").FieldType.GetNamedType().Should().Be(type);
        }


        [Spec(nameof(type_can_be_created_via_clr_type_if_custom_name_matches_input_id))]
        [Fact]
        public void type_can_be_created_via_clr_type_if_custom_name_matches_input_id_()
        {
            var schema = Schema.Create(_ =>
            {
                _.InputObject("Foo").Field("field", "Bar");
                _.Scalar<PlainStruct>("Bar");
            });
            var type = schema.GetScalar<PlainStruct>();
            schema.GetInputObject("Foo").GetField("field").FieldType.GetNamedType().Should().Be(type);
        }


        [Spec(nameof(type_can_be_created_via_clr_type_if_custom_name_matches_output_id))]
        [Fact]
        public void type_can_be_created_via_clr_type_if_custom_name_matches_output_id_()
        {
            var schema = Schema.Create(_ =>
            {
                _.Object("Foo").Field("field", "Bar");
                _.Scalar<PlainStruct>("Bar");
            });
            var type = schema.GetScalar<PlainStruct>();
            schema.GetObject("Foo").GetField("field").FieldType.GetNamedType().Should().Be(type);
        }


        [Spec(nameof(clr_type_can_be_set_if_inferred_name_matches_input_id))]
        [Fact]
        public void clr_type_can_be_set_if_inferred_name_matches_input_id_()
        {
            var schema = Schema.Create(_ =>
            {
                _.InputObject("Foo").Field("field", nameof(PlainStruct));
                _.Scalar("Bar").ClrType<PlainStruct>(true);
            });
            var type = schema.GetScalar<PlainStruct>();
            schema.GetInputObject("Foo").GetField("field").FieldType.GetNamedType().Should().Be(type);
        }


        [Spec(nameof(clr_type_can_be_set_if_inferred_name_matches_output_id))]
        [Fact]
        public void clr_type_can_be_set_if_inferred_name_matches_output_id_()
        {
            var schema = Schema.Create(_ =>
            {
                _.Object("Foo").Field("field", nameof(PlainStruct));
                _.Scalar("Bar").ClrType<PlainStruct>(true);
            });
            var type = schema.GetScalar<PlainStruct>();
            schema.GetObject("Foo").GetField("field").FieldType.GetNamedType().Should().Be(type);
        }


        [Spec(nameof(clr_type_can_be_set_if_inferred_name_annotation_matches_input_id))]
        [Fact]
        public void clr_type_can_be_set_if_inferred_name_annotation_matches_input_id_()
        {
            var schema = Schema.Create(_ =>
            {
                _.InputObject("Foo").Field("field", PlainStructAnnotatedName.AnnotatedNameValue);
                _.Scalar("Bar").ClrType<PlainStructAnnotatedName>(true);
            });
            var type = schema.GetScalar<PlainStructAnnotatedName>();
            schema.GetInputObject("Foo").GetField("field").FieldType.GetNamedType().Should().Be(type);
        }


        [Spec(nameof(clr_type_can_be_set_if_inferred_name_annotation_matches_output_id))]
        [Fact]
        public void clr_type_can_be_set_if_inferred_name_annotation_matches_output_id_()
        {
            var schema = Schema.Create(_ =>
            {
                _.Object("Foo").Field("field", PlainStructAnnotatedName.AnnotatedNameValue);
                _.Scalar("Bar").ClrType<PlainStructAnnotatedName>(true);
            });
            var type = schema.GetScalar<PlainStructAnnotatedName>();
            schema.GetObject("Foo").GetField("field").FieldType.GetNamedType().Should().Be(type);
        }


        [Spec(nameof(clr_type_can_be_set_if_custom_name_matches_input_id))]
        [Fact]
        public void clr_type_can_be_set_if_custom_name_matches_input_id_()
        {
            var schema = Schema.Create(_ =>
            {
                _.InputObject("Foo").Field("field", "Bar");
                _.Scalar("Baz").ClrType<PlainStruct>("Bar");
            });
            var type = schema.GetScalar<PlainStruct>();
            schema.GetInputObject("Foo").GetField("field").FieldType.GetNamedType().Should().Be(type);
        }


        [Spec(nameof(clr_type_can_be_set_if_custom_name_annotation_matches_output_id))]
        [Fact]
        public void clr_type_can_be_set_if_custom_name_annotation_matches_output_id_()
        {
            var schema = Schema.Create(_ =>
            {
                _.Object("Foo").Field("field", "Bar");
                _.Scalar("Baz").ClrType<PlainStruct>("Bar");
            });
            var type = schema.GetScalar<PlainStruct>();
            schema.GetObject("Foo").GetField("field").FieldType.GetNamedType().Should().Be(type);
        }
    }
}