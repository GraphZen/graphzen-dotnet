// Copyright (c) GraphZen LLC. All rights reserved.
// Licensed under the GraphZen Community License. See the LICENSE file in the project root for license information.

using System.Diagnostics.CodeAnalysis;
using FluentAssertions;
using GraphZen.Infrastructure;
using JetBrains.Annotations;
using Xunit;
using static GraphZen.TypeSystem.FunctionalTests.Specs.TypeSystemSpecs.InputAndOutputTypeSpecs;

namespace GraphZen.TypeSystem.FunctionalTests.Schema_.Enums
{
    [NoReorder]
    public class InputAndOutputTypeTests
    {
        public const string AnnotatedName = nameof(AnnotatedName);

        private enum PlainEnum
        {
        }

        [GraphQLName(AnnotatedName)]
        private enum PlainEnumAnnotatedName
        {
        }

        [GraphQLName("abc ()(*322*&%^")]
        // ReSharper disable once UnusedType.Local
        private enum PlainEnumInvalidNameAnnotation
        {
        }


        [Spec(nameof(type_can_be_created_if_name_matches_input_type_id))]
        [Fact]
        public void named_item_can_be_added_if_name_matches_input_type_identity_()
        {
            var schema = Schema.Create(_ =>
            {
                _.InputObject("Foo").Field("inputField", "Bar");
                _.Enum("Bar");
            });
            schema.HasEnum("Bar").Should().BeTrue();
        }


        [Spec(nameof(type_can_be_created_if_name_matches_output_type_id))]
        [Fact]
        public void named_item_can_be_added_if_name_matches_output_type_identity_()
        {
            var schema = Schema.Create(_ =>
            {
                _.Object("Foo").Field("outputField", "Bar");
                _.Enum("Bar");
            });
            schema.HasEnum("Bar").Should().BeTrue();
        }


        [Spec(nameof(type_can_be_renamed_if_name_matches_input_type_id))]
        [Fact]
        public void named_item_can_be_renamed_to_name_with_input_type_identity_()
        {
            var schema = Schema.Create(_ =>
            {
                _.InputObject("Foo").Field("inputField", "Bar");
                _.Enum("Baz").Name("Bar");
            });
            schema.HasEnum("Bar").Should().BeTrue();
        }


        [Spec(nameof(type_can_be_renamed_if_name_matches_output_type_id))]
        [Fact]
        public void named_item_can_be_renamed_to_name_with_output_type_identity_()
        {
            var schema = Schema.Create(_ =>
            {
                _.Object("Foo").Field("outputField", "Bar");
                _.Enum("Baz").Name("Bar");
            });
            schema.HasEnum("Bar").Should().BeTrue();
        }


        [Spec(nameof(type_can_be_created_via_clr_type_if_name_matches_input_id))]
        [Fact]
        public void type_can_be_created_via_clr_type_if_name_matches_input_id_()
        {
            var schema = Schema.Create(_ =>
            {
                _.InputObject("Foo").Field("field", nameof(PlainEnum));
                _.Enum<PlainEnum>();
            });
            var type = schema.GetEnum<PlainEnum>();
            schema.GetInputObject("Foo").GetField("field").FieldType.GetNamedType().Should().Be(type);
        }


        [Spec(nameof(type_can_be_created_via_clr_type_if_name_matches_output_id))]
        [Fact(Skip = "TODO")]
        public void type_can_be_created_via_clr_type_if_name_matches_output_id_()
        {
            // var schema = Schema.Create(_ => { });
        }


        [Spec(nameof(type_can_be_created_via_clr_type_if_name_annotation_matches_input_id))]
        [Fact(Skip = "TODO")]
        public void type_can_be_created_via_clr_type_if_name_annotation_matches_input_id_()
        {
            // var schema = Schema.Create(_ => { });
        }


        [Spec(nameof(type_can_be_created_via_clr_type_if_name_annotation_matches_output_id))]
        [Fact(Skip = "TODO")]
        public void type_can_be_created_via_clr_type_if_name_annotation_matches_output_id_()
        {
            // var schema = Schema.Create(_ => { });
        }


        [Spec(nameof(type_can_be_created_via_clr_type_if_custom_name_matches_input_id))]
        [Fact(Skip = "TODO")]
        public void type_can_be_created_via_clr_type_if_custom_name_matches_input_id_()
        {
            // var schema = Schema.Create(_ => { });
        }


        [Spec(nameof(type_can_be_created_via_clr_type_if_custom_name_matches_output_id))]
        [Fact(Skip = "TODO")]
        public void type_can_be_created_via_clr_type_if_custom_name_matches_output_id_()
        {
            // var schema = Schema.Create(_ => { });
        }


        [Spec(nameof(clr_type_can_be_set_if_inferred_name_matches_input_id))]
        [Fact(Skip = "TODO")]
        public void clr_type_can_be_set_if_inferred_name_matches_input_id_()
        {
            // var schema = Schema.Create(_ => { });
        }


        [Spec(nameof(clr_type_can_be_set_if_inferred_name_matches_output_id))]
        [Fact(Skip = "TODO")]
        public void clr_type_can_be_set_if_inferred_name_matches_output_id_()
        {
            // var schema = Schema.Create(_ => { });
        }


        [Spec(nameof(clr_type_can_be_set_if_inferred_name_annotation_matches_input_id))]
        [Fact(Skip = "TODO")]
        public void clr_type_can_be_set_if_inferred_name_annotation_matches_input_id_()
        {
            // var schema = Schema.Create(_ => { });
        }


        [Spec(nameof(clr_type_can_be_set_if_inferred_name_annotation_matches_output_id))]
        [Fact(Skip = "TODO")]
        public void clr_type_can_be_set_if_inferred_name_annotation_matches_output_id_()
        {
            // var schema = Schema.Create(_ => { });
        }


        [Spec(nameof(clr_type_can_be_set_if_custom_name_matches_input_id))]
        [Fact(Skip = "TODO")]
        public void clr_type_can_be_set_if_custom_name_matches_input_id_()
        {
            // var schema = Schema.Create(_ => { });
        }


        [Spec(nameof(clr_type_can_be_set_if_custom_name_annotation_matches_output_id))]
        [Fact(Skip = "TODO")]
        public void clr_type_can_be_set_if_custom_name_annotation_matches_output_id_()
        {
            // var schema = Schema.Create(_ => { });
        }
    }
}