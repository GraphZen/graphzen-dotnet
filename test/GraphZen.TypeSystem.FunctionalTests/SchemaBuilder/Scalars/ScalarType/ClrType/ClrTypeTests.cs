// Copyright (c) GraphZen LLC. All rights reserved.
// Licensed under the GraphZen Community License. See the LICENSE file in the project root for license information.


using System;
using System.Diagnostics.CodeAnalysis;
using FluentAssertions;
using GraphZen.Infrastructure;
using JetBrains.Annotations;
using Xunit;
using static GraphZen.TypeSystem.FunctionalTests.Specs.TypeSystemSpecs.ClrTypeSpecs;


namespace GraphZen.TypeSystem.FunctionalTests.SchemaBuilder.Scalars.ScalarType.ClrType
{
    [NoReorder]
    public class ClrTypeTests
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

        [Spec(nameof(clr_typed_item_can_have_clr_type_changed))]
        [Fact]
        public void clr_typed_item_can_have_clr_type_changed_()
        {
            var schema = Schema.Create(_ => { _.Scalar<PlainStruct>().ClrType(typeof(PlainStructAnnotatedName)); });
            schema.HasScalar<PlainStruct>().Should().BeFalse();
            schema.HasScalar<PlainStructAnnotatedName>().Should().BeTrue();
        }


        [Spec(nameof(clr_typed_item_cannot_have_clr_type_changed_with_null_value))]
        [Fact(Skip = "TODO")]
        public void clr_typed_item_cannot_have_clr_type_changed_with_null_value_()
        {
            Schema.Create(_ =>
            {
                var pocs = _.Scalar<PlainStruct>();
                Action change = () => pocs.ClrType(null!);
                change.Should().ThrowArgumentNullException("clrType");
            });
        }

        [Spec(nameof(untyped_item_can_have_clr_type_added))]
        [Fact(Skip = "needs impl")]
        public void untyped_item_can_have_clr_type_added_()
        {
            var schema = Schema.Create(_ => { _.Scalar("Foo").ClrType(typeof(PlainStruct)); });
            schema.HasScalar<PlainStruct>().Should().BeTrue();
        }


        [Spec(nameof(untyped_item_cannot_have_clr_type_added_that_is_already_in_use))]
        [Fact(Skip = "needs impl")]
        public void untyped_item_cannot_have_clr_type_added_that_is_already_in_use_()
        {
            Schema.Create(_ =>
            {
                _.Scalar<PlainStruct>();
                var foo = _.Scalar("Foo");
                Action add = () => foo.ClrType(typeof(PlainStruct));
                add.Should().Throw<DuplicateClrTypeException>().WithMessage("x");
            });
        }


        [Spec(nameof(adding_clr_type_to_item_changes_name))]
        [Fact(Skip = "needs impl")]
        public void adding_clr_type_to_item_changes_name_()
        {
            var schema = Schema.Create(_ => { _.Scalar("Foo").ClrType<PlainStruct>(); });
            schema.HasScalar("Foo").Should().BeFalse();
            schema.HasScalar<PlainStruct>().Should().BeTrue();
        }


        [Spec(nameof(clr_type_with_conflicting_name_can_be_added_using_custom_name))]
        [Fact(Skip = "needs impl")]
        public void adding_clr_type_to_item_with_name_changes_name_from_param_()
        {
            var schema = Schema.Create(_ => { _.Scalar("Foo").ClrType<PlainStruct>("Bar"); });
            schema.HasScalar("Foo").Should().BeFalse();
            schema.GetScalar<PlainStruct>().Name.Should().Be("Bar");
        }


        [Spec(nameof(cannot_add_clr_type_to_item_with_conflicting_custom_name))]
        [Fact(Skip = "needs impl")]
        public void cannot_add_clr_type_to_item_with_custom_name_if_name_conflicts_()
        {
            Schema.Create(_ =>
            {
                _.Scalar("Foo");
                var bar = _.Scalar("Bar");
                Action add = () => bar.ClrType<PlainStruct>("Foo");
                add.Should().Throw<DuplicateNameException>().WithMessage("x");
            });
        }

        [Spec(nameof(clr_typed_item_can_have_clr_type_changed_via_type_param))]
        [Fact]
        public void clr_typed_item_can_have_clr_type_changed_via_type_param_()
        {
            var schema = Schema.Create(_ => { _.Scalar<PlainStruct>().ClrType<PlainStructAnnotatedName>(); });
            schema.HasScalar<PlainStruct>().Should().BeFalse();
            schema.HasScalar<PlainStructAnnotatedName>().Should().BeTrue();
        }


        [Spec(nameof(adding_clr_type_to_item_via_type_param_changes_name))]
        [Fact(Skip = "todo")]
        public void adding_clr_type_to_item_via_type_param_changes_name_()
        {
            var schema = Schema.Create(_ => { _.Scalar("Foo").ClrType<PlainStruct>(); });
            schema.HasScalar("Foo").Should().BeFalse();
            schema.HasScalar<PlainStruct>().Should().BeTrue();
        }


        [Spec(nameof(adding_clr_type_with_name_annotation_to_item_changes_name))]
        [Fact(Skip = "TODO")]
        public void adding_clr_type_with_name_annotation_to_item_changes_name_()
        {
            var schema = Schema.Create(_ => { _.Scalar("Foo").ClrType(typeof(PlainStructAnnotatedName)); });
            schema.HasScalar("Foo").Should().BeFalse();
            schema.GetScalar<PlainStructAnnotatedName>().Name.Should().Be(PlainStructAnnotatedName.AnnotatedName);
        }


        [Spec(nameof(adding_clr_type_with_name_annotation_to_item_via_type_pram_changes_name))]
        [Fact(Skip = "TODO")]
        public void adding_clr_type_with_name_annotation_to_item_via_type_pram_changes_name_()
        {
            var schema = Schema.Create(_ => { _.Scalar("Foo").ClrType<PlainStructAnnotatedName>(); });
            schema.HasScalar("Foo").Should().BeFalse();
            schema.GetScalar<PlainStructAnnotatedName>().Name.Should().Be(PlainStructAnnotatedName.AnnotatedName);
        }


        [Spec(nameof(clr_type_with_conflicting_name_can_be_added_via_type_param_using_custom_name))]
        [Fact]
        public void clr_type_with_conflicting_name_can_be_added_via_type_param_using_custom_name_()
        {
            var schema = Schema.Create(_ =>
            {
                _.Scalar(nameof(PlainStruct));
                _.Scalar("Foo").ClrType<PlainStruct>("Bar");
            });
            schema.HasScalar(nameof(PlainStruct)).Should().BeTrue();
            schema.GetScalar<PlainStruct>().Name.Should().Be("Bar");
        }


        [Spec(nameof(clr_type_with_conflicting_name_annotation_can_be_added_using_custom_name))]
        [Fact]
        public void clr_type_with_conflicting_name_annotation_can_be_added_using_custom_name_()
        {
            var schema = Schema.Create(_ =>
            {
                _.Scalar(PlainStructAnnotatedName.AnnotatedName);
                _.Scalar("Foo").ClrType(typeof(PlainStructAnnotatedName), "Bar");
            });
            schema.GetScalar(PlainStructAnnotatedName.AnnotatedName).ClrType.Should().BeNull();
            schema.GetScalar<PlainStructAnnotatedName>().Name.Should().Be("Bar");
        }


        [Spec(nameof(clr_type_with_conflicting_name_annotation_can_be_added_via_type_param_using_custom_name))]
        [Fact]
        public void clr_type_with_conflicting_name_annotation_can_be_added_via_type_param_using_custom_name_()
        {
            var schema = Schema.Create(_ =>
            {
                _.Scalar(PlainStructAnnotatedName.AnnotatedName);
                _.Scalar("Foo").ClrType<PlainStructAnnotatedName>("Bar");
            });
            schema.GetScalar(PlainStructAnnotatedName.AnnotatedName).ClrType.Should().BeNull();
            schema.GetScalar<PlainStructAnnotatedName>().Name.Should().Be("Bar");
        }

        [Spec(nameof(clr_typed_item_can_have_clr_type_removed))]
        [Fact(Skip = "needs impl")]
        public void clr_typed_item_can_have_clr_type_removed_()
        {
            var schema = Schema.Create(_ => { _.Scalar<PlainStruct>().RemoveClrType(); });
            schema.GetEnum(nameof(PlainStruct)).ClrType.Should().BeNull();
        }


        [Spec(nameof(clr_typed_item_with_type_removed_should_retain_clr_type_name))]
        [Fact(Skip = "needs impl")]
        public void clr_typed_item_with_type_removed_should_retain_clr_type_name_()
        {
            var schema = Schema.Create(_ => { _.Scalar<PlainStruct>().RemoveClrType(); });
            schema.HasScalar(nameof(PlainStruct)).Should().BeTrue();
        }


        [Spec(nameof(clr_typed_item_with_name_annotation_type_removed_should_retain_annotated_name))]
        [Fact(Skip = "needs impl")]
        public void clr_typed_item_with_name_annotation_type_removed_should_retain_annotated_name_()
        {
            var schema = Schema.Create(_ => { _.Scalar<PlainStructAnnotatedName>().RemoveClrType(); });
            schema.HasScalar(PlainStructAnnotatedName.AnnotatedName).Should().BeTrue();
        }


        [Spec(nameof(custom_named_clr_typed_item_with_type_removed_should_retain_custom_name))]
        [Fact(Skip = "needs impl")]
        public void custom_named_clr_typed_item_with_type_removed_should_retain_custom_name_()
        {
            var schema = Schema.Create(_ => { _.Scalar<PlainStruct>().Name("Foo").RemoveClrType(); });
            schema.HasScalar("Foo").Should().BeTrue();
        }



    }
}